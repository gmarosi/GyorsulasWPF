using Gyorsulás.Model;
using Gyorsulás.Persistence;
using Moq;

namespace GyorsulásTest
{
    [TestClass]
    public class GyorsulasTest
    {
        private GameModel _gameModel = null!;
        private GameState _mockedState = null!;
        private Mock<ISaveManager> _mock = null!;

        [TestInitialize]
        public void Initialize()
        {
            _mockedState = new GameState(new Bike(), new FuelCannister());
            _mockedState.GameTime = 200;
            _mockedState.setGameSpeed(1.5);
            _mockedState.Paused = false;

            _mockedState.Bike.Fuel = 0.6;
            _mockedState.Bike.Position = 0.3;

            _mockedState.FuelCannister.Position = 0.4;
            _mockedState.FuelCannister.Progress = 0.76;

            _mock = new Mock<ISaveManager>();
            _mock.Setup(mock => mock.LoadAsync(It.IsAny<string>()))
                .Returns(() => Task.FromResult(_mockedState));

            _gameModel = new GameModel(_mock.Object);

            _gameModel.Update += new EventHandler<GyorsulasEventArgs>(Game_Advanced);
            _gameModel.GameOver += new EventHandler<GyorsulasEventArgs>(Game_Over);
        }

        [TestMethod]
        public async Task GameLoadTest()
        {
            await _gameModel.LoadAsync("");

            Assert.AreEqual(_gameModel.GameTime, _mockedState.GameTime);
            Assert.AreEqual(_gameModel.State.Bike.Position, _mockedState.Bike.Position);
            Assert.AreEqual(_gameModel.State.Bike.Fuel, _mockedState.Bike.Fuel);

            Assert.IsTrue(_gameModel.FuelSpawned);

            Assert.AreEqual(_gameModel.State.FuelCannister.Progress, _mockedState.FuelCannister.Progress);
            Assert.AreEqual(_gameModel.State.FuelCannister.Position, _mockedState.FuelCannister.Position);
        }

        [TestMethod]
        public void GameProgressTest()
        {
            _gameModel.TimeAdvanced();
        }

        [TestMethod]
        public void GamePauseTest()
        {
            Assert.IsTrue(_gameModel.IsPaused);
            _gameModel.PauseGame();
            Assert.IsFalse(_gameModel.IsPaused);
            _gameModel.PauseGame();
            Assert.IsTrue(_gameModel.IsPaused);
        }

        [TestMethod]
        public void GameMoveTest()
        {
            Assert.IsTrue(_gameModel.State.Bike.Position == 0);
            _gameModel.MoveLeft();
            Assert.IsTrue(_gameModel.State.Bike.Position <= 0);
            _gameModel.MoveRight(); _gameModel.MoveRight();
            Assert.IsTrue(_gameModel.State.Bike.Position >= 0);
        }

        [TestMethod]
        public async Task GameFuelCaught()
        {
            await _gameModel.LoadAsync("");
            double fuel = _gameModel.State.Bike.Fuel;
            _gameModel.TimeAdvanced();

            Assert.IsTrue(fuel < _gameModel.State.Bike.Fuel);
            Assert.IsFalse(_gameModel.FuelSpawned);
        }

        private void Game_Advanced(object? sender, GyorsulasEventArgs e)
        {
            Assert.IsTrue(_gameModel.GameTime >= 0);
            Assert.AreEqual(_gameModel.FuelSpawned, _gameModel.State.FuelCannister != null);
            Assert.AreEqual(_gameModel.IsPaused, _gameModel.State.Paused);

            Assert.IsFalse(e.IsOver);
            Assert.AreEqual(e.Fuel, _gameModel.State.Bike.Fuel);
            Assert.AreEqual(e.BikePos, _gameModel.State.Bike.Position);

            if (_gameModel.State.FuelCannister != null)
            {
                Assert.AreEqual(e.FuelPos, _gameModel.State.FuelCannister.Position);
                Assert.AreEqual(e.FuelProg, _gameModel.State.FuelCannister.Progress);
            }
        }

        private void Game_Over(object? sender, GyorsulasEventArgs e)
        {
            Assert.IsTrue(e.IsOver);
            Assert.AreEqual(_gameModel.State.Bike.Fuel <= 0, e.IsOver);
        }
    }
}