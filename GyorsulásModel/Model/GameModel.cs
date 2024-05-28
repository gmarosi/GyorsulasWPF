using Gyorsulás.Persistence;

namespace Gyorsulás.Model
{
    public class GameModel
    {
        private GameState _state = null!;
        private ISaveManager _saveManager = null!;

        public bool FuelSpawned { get { return _state.FuelCannister != null; } }
        public uint GameTime { get { return _state.GameTime; } }
        public GameState State { get { return _state; } }
        public bool IsPaused => _state.Paused;

        public event EventHandler<GyorsulasEventArgs>? Update;
        public event EventHandler<GyorsulasEventArgs>? GameOver;

        public GameModel(ISaveManager saveManager)
        {
            _state = new GameState();
            _saveManager = saveManager;
        }

        public void StartGame()
        {
            _state.Paused = false;
        }

        public void PauseGame() { _state.Paused = !_state.Paused; }

        public async Task LoadAsync(string path)
        {
            if (_saveManager == null)
                throw new InvalidOperationException("No data access is provided.");

            _state = await _saveManager.LoadAsync(path);
        }

        public async Task SaveAsync(string path)
        {
            if (_saveManager == null)
                throw new InvalidOperationException("No data access is provided.");

            await _saveManager.SaveAsync(path, _state);
        }

        public void TimeAdvanced()
        {
            _state.Update();

            if (_state.Bike.Fuel <= 0)
            {
                OnGameOver();
                return;
            }

            if (_state.FuelCannister != null && _state.FuelCannister.Progress >= 1)
            {
                _state.NotCaught();
            }

            OnUpdate();
        }

        public void MoveLeft()
        {
            _state.Bike.Position -= 0.2 * _state.StrafeSpeed;
        }

        public void MoveRight()
        {
            _state.Bike.Position += 0.2 * _state.StrafeSpeed;
        }

        private void OnUpdate()
        {
            if (FuelSpawned)
                Update?.Invoke(this, new GyorsulasEventArgs(false, _state.Bike.Position, _state.Bike.Fuel, _state.FuelCannister.Position, _state.FuelCannister.Progress));
            else
                Update?.Invoke(this, new GyorsulasEventArgs(false, _state.Bike.Position, _state.Bike.Fuel));
        }

        private void OnGameOver()
        {
            GameOver?.Invoke(this, new GyorsulasEventArgs(true));
        }
    }
}
