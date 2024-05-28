namespace Gyorsulás.Persistence
{
    public class SaveManager : ISaveManager
    {
        public async Task SaveAsync(string path, GameState gameState)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(path)) // fájl megnyitása
                {
                    await writer.WriteLineAsync(gameState.Bike.ToString());
                    if (gameState.FuelCannister != null)
                        await writer.WriteLineAsync(gameState.FuelCannister.ToString());
                    else
                        await writer.WriteLineAsync();
                    await writer.WriteLineAsync(gameState.GameSpeed + " " + gameState.GameTime);
                }
            }
            catch
            {
                throw new GyorsulasDataException("Error during game saving operations!");
            }
        }

        public async Task<GameState> LoadAsync(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line = await reader.ReadLineAsync() ?? string.Empty;
                    string[] bikeData = line.Split(' ');
                    double bikepos = double.Parse(bikeData[0]);
                    double bikefuel = double.Parse(bikeData[1]);
                    Bike bike = new Bike(bikepos, bikefuel);

                    FuelCannister? fuelCan;
                    line = await reader.ReadLineAsync() ?? string.Empty;
                    if (line == "")
                        fuelCan = null;
                    else
                    {
                        string[] fuelCanData = line.Split(' ');
                        double fuelCanProg = double.Parse(fuelCanData[0]);
                        double fuelCanPos = double.Parse(fuelCanData[1]);
                        fuelCan = new FuelCannister(fuelCanProg, fuelCanPos);
                    }

                    GameState gameState = new GameState(bike, fuelCan);

                    line = await reader.ReadLineAsync() ?? string.Empty;
                    string[] state = line.Split(' ');
                    double speed = double.Parse(state[0]);
                    uint gameTime = uint.Parse(state[1]);

                    gameState.setGameSpeed(speed);
                    gameState.GameTime = gameTime;

                    return gameState;
                }
            }
            catch
            {
                throw new GyorsulasDataException("Error during game loading operations!");
            }
        }
    }
}
