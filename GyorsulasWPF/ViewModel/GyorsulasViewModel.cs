using Gyorsulás.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GyorsulásWPF.ViewModel
{
    public class GyorsulasViewModel : ViewModelBase
    {
        #region Fields

        private GameModel _model;

        #endregion

        #region Properties

        public DelegateCommand NewGameCommand { get; private set; }

        public DelegateCommand LoadGameCommand { get; private set; }

        public DelegateCommand SaveGameCommand { get; private set; }

        public DelegateCommand MoveLeftCommand { get; private set; }

        public DelegateCommand MoveRightCommand { get; private set; }

        public DelegateCommand PauseCommand { get; private set; }

        public DelegateCommand ToMenuCommand { get; private set; }

        public bool IsPaused { get { return _model.IsPaused; } }

        public bool InMenu { get; set; }

        public uint GameTime { get { return _model.GameTime; } }

        public double Fuel { get { return _model.State.Bike.Fuel; } }

        public double BikePos { get { return _model.State.Bike.Position; } }

        public double CanPos { get { return _model.FuelSpawned ? _model.State.FuelCannister.Position : 0; } }

        public double CanProg { get { return _model.FuelSpawned ? _model.State.FuelCannister.Progress : 0; } }

        public bool CanSpawned { get { return _model.FuelSpawned; } }

        #endregion

        #region Events

        public event EventHandler? NewGame;

        public event EventHandler? LoadGame;

        public event EventHandler? SaveGame;

        public event EventHandler? MoveLeft;

        public event EventHandler? MoveRight;

        public event EventHandler? Pause;

        public event EventHandler? ToMenu;

        #endregion

        #region Constructors

        public GyorsulasViewModel(GameModel model)
        {
            _model = model;
            _model.Update += Model_Update;

            NewGameCommand = new DelegateCommand(param => OnNewGame());
            LoadGameCommand = new DelegateCommand(param => IsPaused,param => OnLoadGame());
            SaveGameCommand = new DelegateCommand(param =>IsPaused, param => OnSaveGame());
            MoveLeftCommand = new DelegateCommand(param => !IsPaused, param => OnMoveLeft());
            MoveRightCommand = new DelegateCommand(param => !IsPaused, param => OnMoveRight());
            PauseCommand = new DelegateCommand(param => !InMenu, param => OnPause());
            ToMenuCommand = new DelegateCommand(param => OnToMenu());
        }

        #endregion

        #region Event Handlers

        private void Model_Update(object? sender, GyorsulasEventArgs e)
        {
            OnPropertyChanged(nameof(Fuel));
            OnPropertyChanged(nameof(BikePos));
            OnPropertyChanged(nameof(CanSpawned));
            OnPropertyChanged(nameof(GameTime));

            if(CanSpawned)
                OnPropertyChanged(nameof(CanPos));
                OnPropertyChanged(nameof(CanProg));
        }

        #endregion

        #region Event Methods

        private void OnNewGame()
        {
            NewGame?.Invoke(this, EventArgs.Empty);
        }

        private void OnLoadGame()
        {
            LoadGame?.Invoke(this, EventArgs.Empty);
        }

        private void OnSaveGame()
        {
            SaveGame?.Invoke(this, EventArgs.Empty);
        }

        private void OnMoveLeft()
        {
            MoveLeft?.Invoke(this, EventArgs.Empty);
        }

        private void OnMoveRight()
        {
            MoveRight?.Invoke(this, EventArgs.Empty);
        }

        private void OnPause()
        {
            Pause?.Invoke(this, EventArgs.Empty);
        }

        private void OnToMenu()
        {
            ToMenu?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
