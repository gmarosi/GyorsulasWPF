using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Gyorsulás.Model;
using Gyorsulás.Persistence;
using GyorsulásWPF.ViewModel;
using GyorsulásWPF.View;
using System.Windows.Shapes;
using System.Windows.Data;
using Microsoft.Win32;

namespace GyorsulásWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        private GameModel _model = null!;
        private GyorsulasViewModel _vModel = null!;
        private MainWindow _view = null!;
        private DispatcherTimer _timer = null!;

        #endregion

        #region Constructors

        public App()
        {
            Startup += App_Startup;
        }

        #endregion

        #region Application Event Handlers

        private void App_Startup(object? sender, EventArgs e)
        {
            _model = new GameModel(new SaveManager());
            _model.GameOver += new EventHandler<GyorsulasEventArgs>(Model_GameOver);

            _vModel = new GyorsulasViewModel(_model);
            _vModel.InMenu = true;
            _vModel.NewGame += new EventHandler(ViewModel_NewGame);
            _vModel.LoadGame += new EventHandler(ViewModel_LoadGame);
            _vModel.SaveGame += new EventHandler(ViewModel_SaveGame);
            _vModel.MoveLeft += new EventHandler(ViewModel_MoveLeft);
            _vModel.MoveRight += new EventHandler(ViewModel_MoveRight);
            _vModel.Pause += new EventHandler(ViewModel_Pause);
            _vModel.ToMenu += new EventHandler(ViewModel_ToMenu);

            _view = new MainWindow();
            _view.DataContext = _vModel;
            _view.Show();

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(32);
            _timer.Tick += new EventHandler(Timer_AdvanceTime);

            _view.InitElements();
        }

        private void Timer_AdvanceTime(object? sender, EventArgs e)
        {
            _model.TimeAdvanced();
        }

        #endregion

        #region Model Event Handlers

        private void Model_GameOver(object? sender, GyorsulasEventArgs e)
        {
            _timer.Stop();

            _view.GameOver();

            _model.PauseGame();
        }

        #endregion

        #region ViewModel Event Handlers

        private void ViewModel_NewGame(object? sender, EventArgs e)
        {
            _model.StartGame();
            _vModel.InMenu = false;
            _timer.Start();
            _view.NewGame();
        }

        private async void ViewModel_LoadGame(object? sender, EventArgs e)
        {
            _timer.Stop();

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog(); // dialógusablak
                openFileDialog.Title = "Gyorsulás játék betöltése";
                openFileDialog.Filter = "Gyorsulás mentés|*.gysav";
                if (openFileDialog.ShowDialog() == true)
                {
                    // játék betöltése
                    await _model.LoadAsync(openFileDialog.FileName);

                    ViewModel_NewGame(this, e);
                }
            }
            catch (GyorsulasDataException)
            {
                MessageBox.Show("A fájl betöltése sikertelen!", "Gyorsulás", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ViewModel_SaveGame(object? sender, EventArgs e)
        {
            _timer.Stop();

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog(); // dialógablak
                saveFileDialog.Title = "Gyorsulás játék mentése";
                saveFileDialog.Filter = "Gyorsulás mentés|*.gysav";
                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        // játéktábla mentése
                        await _model.SaveAsync(saveFileDialog.FileName);
                    }
                    catch (GyorsulasDataException)
                    {
                        MessageBox.Show("Játék mentése sikertelen!" + Environment.NewLine + "Hibás az elérési út, vagy a könyvtár nem írható.", "Hiba!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("A fájl mentése sikertelen!", "Gyorsulás", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewModel_MoveLeft(object? sender, EventArgs e)
        {
            _model.MoveLeft();
        }

        private void ViewModel_MoveRight(object? sender, EventArgs e)
        {
            _model.MoveRight();
        }

        private void ViewModel_Pause(object? sender, EventArgs e)
        {
            if(_model.IsPaused)
            {
                _model.PauseGame();
                _timer.Start();
                _view.UnPause();
            }
            else
            {
                _model.PauseGame();
                _timer.Stop();
                _view.Pause();
            }
        }

        private void ViewModel_ToMenu(object? sender, EventArgs e)
        {
            _vModel.InMenu = true;

            _view.ToMenuVisibility();

            _model.State.Reset();

            _view.ResetCanBinding();
        }

        #endregion
    }
}
