namespace Gyorsulás.Model
{
    public class GyorsulasEventArgs
    {
        private bool _isOver;
        private double _bikePos;
        private double _fuel;
        private double _fuelPos;
        private double _fuelProg;

        public bool IsOver { get { return _isOver; } }

        public double BikePos { get { return _bikePos; } }

        public double Fuel { get { return _fuel; } }

        public double FuelPos { get { return _fuelPos; } }

        public double FuelProg { get { return _fuelProg; } }


        public GyorsulasEventArgs(bool isOver, double bikePos, double fuel, double fuelPos, double fuelProg)
        {
            _isOver = isOver;
            _bikePos = bikePos;
            _fuel = fuel;
            _fuelPos = fuelPos;
            _fuelProg = fuelProg;
        }

        public GyorsulasEventArgs(bool isOver, double bikePos, double fuel)
        {
            _isOver = isOver;
            _bikePos = bikePos;
            _fuel = fuel;
        }

        public GyorsulasEventArgs(bool isOver)
        {
            _isOver = isOver;
        }
    }
}
