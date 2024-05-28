namespace Gyorsulás.Persistence
{
    public class GameState
    {
        private bool _spawn;
        private double _spawnTick;
        private uint _gameTime;
        private Bike _bike = null!;
        private FuelCannister _fuelCannister = null!;
        private double _gameSpeed;
        private const double _fuelUsage = 1;
        private const double _spawnRate = 1;
        private const double _strafeSpeed = 1;

        public Bike Bike { get { return _bike; } }

        public FuelCannister FuelCannister { get { return _fuelCannister; } }

        public bool Paused
        { get; set; }

        /// <summary>
        /// Stores the number of times time passed in game (not necceserily seconds)
        /// </summary>
        public uint GameTime { get { return _gameTime; } set { _gameTime = value; } }

        public double GameSpeed
        { get { return _gameSpeed; } }

        public double SpawnRate
        { get { return _spawnRate; } }

        public double StrafeSpeed
        { get { return _strafeSpeed; } }

        #region Constructors

        public GameState()
        {
            Paused = true;
            _gameTime = 0;
            _gameSpeed = 1;
            _bike = new Bike();
            _fuelCannister = null!;
            _spawnTick = 0;
        }

        public GameState(Bike bike)
        {
            if (bike == null)
                throw new ArgumentNullException("Cannot pass null Bike reference to GameState!");
            Paused = true;
            _gameTime = 0;
            _gameSpeed = 1;
            _bike = bike;
            _spawnTick = 0;
        }

        public GameState(Bike bike, FuelCannister? fuelCannister)
        {
            if (bike == null)
                throw new ArgumentNullException("Cannot pass null Bike reference to GameState!");
            Paused = true;
            _gameTime = 0;
            _gameSpeed = 1;
            _bike = bike;
            _fuelCannister = fuelCannister!;
            _spawnTick = 0;
        }

        #endregion

        public void Reset()
        {
            Paused = true;
            _gameTime = 0;
            _gameSpeed = 1;
            _bike = new Bike();
            _fuelCannister = null!;
            _spawnTick = 0;
        }

        public void setGameSpeed(double gameSpeed)
        {
            if (gameSpeed > 0)
                _gameSpeed = gameSpeed;
            else
                throw new ArgumentOutOfRangeException("Gamespeed must be larger than 0!");
        }

        internal void Update()
        {
            _bike.addFuel(_gameSpeed * _fuelUsage * -0.005);
            if (_fuelCannister != null)
            {
                _fuelCannister.Progress += GameSpeed * 0.05;
                IsCaught();
                _spawn = false;
            }
            else
            {
                SpawnFuel();
            }
            _gameSpeed += 0.002;
            _gameTime++;
        }

        private void IsCaught()
        {
            if (_fuelCannister.Progress >= 0.8 && Math.Abs(_fuelCannister.Position - _bike.Position) < 0.3)
            {
                _bike.addFuel(0.5);
                _fuelCannister = null!;
            }
        }

        private void SpawnFuel()
        {
            if (_spawn)
            {
                _spawnTick = 0;
                _spawn = false;
                _fuelCannister = new FuelCannister();
            }
            else
            {
                Random r = new Random();
                _spawnTick += 0.07 * this.GameSpeed * this.SpawnRate - r.NextDouble() / 20.0;
                _spawn = _spawnTick >= 1;
            }
        }

        public void NotCaught()
        {
            _fuelCannister = null!;
        }
    }
}
