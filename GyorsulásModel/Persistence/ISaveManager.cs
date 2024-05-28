namespace Gyorsulás.Persistence
{
    public interface ISaveManager
    {
        public Task SaveAsync(string path, GameState gameState);
        public Task<GameState> LoadAsync(string path);
    }
}
