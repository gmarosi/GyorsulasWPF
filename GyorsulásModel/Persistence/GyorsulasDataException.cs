namespace Gyorsulás.Persistence
{
    public class GyorsulasDataException : IOException
    {
        public GyorsulasDataException() : base() { }
        public GyorsulasDataException(string message) : base(message) { }
    }
}
