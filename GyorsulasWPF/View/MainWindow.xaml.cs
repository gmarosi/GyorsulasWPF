using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GyorsulásWPF.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private Image _player = null!;
        private Image _fuelCan = null!;
        private ProgressBar _progressFuel = null!;
        private Button _buttonSave = null!;
        private Button _buttonToMenu = null!;
        private Label _labelGameOver = null!;
        private Label _labelOverTime = null!;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        public void InitElements()
        {
            // Setup of dynamic elements
            #region _player

            _player = new Image();
            _player.Width = 60;
            _player.Height = 140;
            _player.Source = new BitmapImage(new Uri("pack://application:,,,/../../images/bike.png"));
            _player.Stretch = Stretch.Fill;
            Panel.SetZIndex(_player, 2);

            Binding bindingPlayer = new Binding("BikePos");
            bindingPlayer.Mode = BindingMode.OneWay;
            bindingPlayer.Converter = new PositionConverter();
            _player.SetBinding(Canvas.LeftProperty, bindingPlayer);
            _player.SetValue(Canvas.TopProperty, canvasMain.Height - _player.Height - 15);

            #endregion

            #region _progressFuel

            _progressFuel = new ProgressBar();
            _progressFuel.Height = 40;
            _progressFuel.Width = 100;
            _progressFuel.Minimum = 0;
            _progressFuel.Maximum = 1;
            Panel.SetZIndex(_progressFuel, 3);

            Binding bindingFuel = new Binding("Fuel");
            bindingFuel.Mode = BindingMode.OneWay;
            _progressFuel.SetBinding(ProgressBar.ValueProperty, bindingFuel);
            _progressFuel.SetValue(Canvas.LeftProperty, 0.0);
            _progressFuel.SetValue(Canvas.TopProperty, 0.0);

            #endregion

            #region _fuelCan

            _fuelCan = new Image();
            _fuelCan.Width = 60;
            _fuelCan.Height = 60;
            _fuelCan.Source = new BitmapImage(new Uri("pack://application:,,,/../../images/fuelCan.png"));
            _fuelCan.Stretch = Stretch.Fill;
            Panel.SetZIndex(_fuelCan, 1);

            Binding bindingCanSpawned = new Binding("CanSpawned");
            bindingCanSpawned.Mode = BindingMode.OneWay;
            bindingCanSpawned.Converter = new VisibilityConverter();
            _fuelCan.SetBinding(Rectangle.VisibilityProperty, bindingCanSpawned);

            Binding bindingCanPos = new Binding("CanPos");
            bindingCanPos.Mode = BindingMode.OneWay;
            bindingCanPos.Converter = new PositionConverter();
            _fuelCan.SetBinding(Canvas.LeftProperty, bindingCanPos);

            Binding bindingCanProg = new Binding("CanProg");
            bindingCanProg.Mode = BindingMode.OneWay;
            bindingCanProg.Converter = new ProgressConverter();
            _fuelCan.SetBinding(Canvas.TopProperty, bindingCanProg);

            #endregion

            #region _buttonSave

            _buttonSave = new Button();
            _buttonSave.Height = buttonNewGame.Height;
            _buttonSave.Width = buttonNewGame.Width;
            _buttonSave.SetValue(Canvas.LeftProperty, buttonNewGame.GetValue(Canvas.LeftProperty));
            _buttonSave.SetValue(Canvas.TopProperty, buttonNewGame.GetValue(Canvas.TopProperty));
            Panel.SetZIndex(_buttonSave, 3);

            _buttonSave.Content = "Save Game";
            _buttonSave.FontStyle = buttonNewGame.FontStyle;
            _buttonSave.FontSize = buttonNewGame.FontSize;
            _buttonSave.FontFamily = buttonNewGame.FontFamily;
            _buttonSave.FontWeight = buttonNewGame.FontWeight;

            _buttonSave.Foreground = buttonNewGame.Foreground;
            _buttonSave.Background = buttonNewGame.Background;
            _buttonSave.BorderBrush = buttonNewGame.BorderBrush;

            _buttonSave.SetBinding(Button.CommandProperty, new Binding("SaveGameCommand"));

            #endregion

            #region _buttonToMenu

            _buttonToMenu = new Button();
            _buttonToMenu.Height = buttonNewGame.Height;
            _buttonToMenu.Width = buttonNewGame.Width;
            _buttonToMenu.SetValue(Canvas.LeftProperty, buttonLoadGame.GetValue(Canvas.LeftProperty));
            _buttonToMenu.SetValue(Canvas.TopProperty, buttonLoadGame.GetValue(Canvas.TopProperty));
            Panel.SetZIndex(_buttonToMenu, 3);

            _buttonToMenu.Content = "Back to Menu";
            _buttonToMenu.FontStyle = buttonNewGame.FontStyle;
            _buttonToMenu.FontSize = buttonNewGame.FontSize;
            _buttonToMenu.FontFamily = buttonNewGame.FontFamily;
            _buttonToMenu.FontWeight = buttonNewGame.FontWeight;

            _buttonToMenu.Foreground = buttonNewGame.Foreground;
            _buttonToMenu.Background = buttonNewGame.Background;
            _buttonToMenu.BorderBrush = buttonNewGame.BorderBrush;

            _buttonToMenu.SetBinding(Button.CommandProperty, new Binding("ToMenuCommand"));

            #endregion

            #region _labelGameOver

            _labelGameOver = new Label();
            _labelGameOver.Content = "Game Over!";
            _labelGameOver.FontFamily = labelTitle.FontFamily;
            _labelGameOver.FontSize = labelTitle.FontSize;
            _labelGameOver.FontStyle = labelTitle.FontStyle;
            _labelGameOver.FontWeight = labelTitle.FontWeight;
            _labelGameOver.Foreground = labelTitle.Foreground;

            _labelGameOver.SetValue(Canvas.LeftProperty, canvasMain.Width / 2 - _labelGameOver.Width / 2);
            _labelGameOver.SetValue(Canvas.TopProperty, labelTitle.GetValue(Canvas.TopProperty));

            #endregion

            #region _labelOverTime

            _labelOverTime = new Label();
            _labelOverTime.FontFamily = labelControl1.FontFamily;
            _labelOverTime.FontSize = labelControl1.FontSize;
            _labelOverTime.FontStyle = labelControl1.FontStyle;
            _labelOverTime.FontWeight = labelControl1.FontWeight;
            _labelOverTime.Foreground = labelControl1.Foreground;

            Binding bindingTime = new Binding("GameTime");
            bindingTime.Mode = BindingMode.OneWay;
            bindingTime.Converter = new TimeLabelConverter();
            _labelOverTime.SetBinding(ContentProperty, bindingTime);

            _labelOverTime.SetValue(Canvas.TopProperty, canvasMain.Height);

            #endregion

            // Adding dynamic controls to view
            canvasMain.Children.Add(_player);
            _player.Visibility = Visibility.Hidden;

            canvasMain.Children.Add(_progressFuel);
            _progressFuel.Visibility = Visibility.Hidden;

            canvasMain.Children.Add(_fuelCan); // Visibility already binded

            canvasMain.Children.Add(_buttonSave);
            _buttonSave.Visibility = Visibility.Hidden;

            canvasMain.Children.Add(_buttonToMenu);
            _buttonToMenu.Visibility = Visibility.Hidden;

            canvasMain.Children.Add(_labelGameOver);
            _labelGameOver.Visibility = Visibility.Hidden;

            canvasMain.Children.Add(_labelOverTime);
            _labelGameOver.Visibility = Visibility.Hidden;
        }

        public void NewGame()
        {
            // Revealing game elements
            _player.Visibility = Visibility.Visible;
            _progressFuel.Visibility = Visibility.Visible;

            // Hiding menu elements
            buttonNewGame.Visibility = Visibility.Hidden;
            buttonLoadGame.Visibility = Visibility.Hidden;
            _buttonSave.Visibility = Visibility.Hidden;

            labelTitle.Visibility = Visibility.Hidden;
            labelControl1.Visibility = Visibility.Hidden;
            labelControl2.Visibility = Visibility.Hidden;
            labelControl3.Visibility = Visibility.Hidden;
            labelControl4.Visibility = Visibility.Hidden;
        }

        public void Pause()
        {
            _buttonSave.Visibility = Visibility.Visible;
            buttonLoadGame.Visibility = Visibility.Visible;
        }

        public void UnPause()
        {
            _buttonSave.Visibility = Visibility.Hidden;
            buttonLoadGame.Visibility = Visibility.Hidden;
        }

        public void ToMenuVisibility()
        {
            _buttonToMenu.Visibility = Visibility.Hidden;
            _labelGameOver.Visibility = Visibility.Hidden;
            _labelOverTime.Visibility = Visibility.Hidden;

            buttonNewGame.Visibility = Visibility.Visible;
            buttonLoadGame.Visibility = Visibility.Visible;

            labelTitle.Visibility = Visibility.Visible;
            labelControl1.Visibility = Visibility.Visible;
            labelControl2.Visibility = Visibility.Visible;
            labelControl3.Visibility = Visibility.Visible;
            labelControl4.Visibility = Visibility.Visible;
        }

        public void ResetCanBinding()
        {
            Binding bindingCanSpawned = new Binding("CanSpawned");
            bindingCanSpawned.Mode = BindingMode.OneWay;
            bindingCanSpawned.Converter = new VisibilityConverter();
            _fuelCan.SetBinding(Rectangle.VisibilityProperty, bindingCanSpawned);
        }

        public void GameOver()
        {
            _player.Visibility = Visibility.Hidden;
            _progressFuel.Visibility = Visibility.Hidden;
            _fuelCan.Visibility = Visibility.Hidden;
            _buttonSave.Visibility = Visibility.Hidden;
            _buttonToMenu.Visibility = Visibility.Visible;

            _labelGameOver.Visibility = Visibility.Visible;

            _labelOverTime.SetValue(Canvas.LeftProperty, canvasMain.Width / 2 - _labelOverTime.ActualWidth / 2);
            _labelOverTime.SetValue(Canvas.TopProperty, labelControl1.GetValue(Canvas.TopProperty));
            _labelOverTime.Visibility = Visibility.Visible;
        }
    }
}
