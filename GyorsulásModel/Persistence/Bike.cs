namespace Gyorsulás.Persistence
{
    public class Bike
    {
        private double _position;
        private double _fuel;

        public Bike()
        {
            Position = 0;
            Fuel = 1;
        }

        public Bike(double position, double fuel)
        {
            Position = position;
            Fuel = fuel;
        }

        /// <summary>
        /// The position of the bike, ranging from -1 to 1
        /// </summary>
        public double Position
        {
            get
            {
                return _position;
            }
            set
            {
                if (value >= -1 && value <= 1)
                {
                    _position = value;
                }
                else if (value > 1)
                {
                    _position = 1;
                }
                else
                {
                    _position = -1;
                }
            }
        }

        /// <summary>
        /// The fuel level of the bike, ranging from 0 to 1
        /// </summary>
        public double Fuel
        {
            get
            {
                return _fuel;
            }
            set
            {
                if (value > 0 && value <= 1)
                {
                    _fuel = value;
                }
                else if (value <= 0)
                {
                    _fuel = 0;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Fuel must be between 0 and 1");
                }
            }
        }


        public void addFuel(double fuel)
        {
            _fuel += fuel;

            if (_fuel > 1)
            {
                _fuel = 1;
            }
        }

        public override string ToString()
        {
            return _position + " " + _fuel;
        }
    }
}
