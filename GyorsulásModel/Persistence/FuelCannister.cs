namespace Gyorsulás.Persistence
{
    public class FuelCannister
    {
        private double _progress;
        private double _position;

        public FuelCannister()
        {
            _progress = 0;
            Random r = new Random();
            _position = r.NextDouble() * 2 - 1;
        }

        public FuelCannister(double progress, double position)
        {
            _progress = progress;
            _position = position;
        }

        public double Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                if (value >= 0)
                {
                    _progress = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("FuelCannister progress must be larger than 0!");
                }
            }
        }

        public double Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (value >= -1 && value <= 1)
                    _position = value;
                else
                    throw new ArgumentOutOfRangeException("Fuel cannister position must be between -1 and 1!");
            }
        }

        public override string ToString()
        {
            return _progress + " " + _position;
        }
    }
}
