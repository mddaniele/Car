namespace CarLibrary
{
    public abstract class Car
    {
        public string Brand { get; }
        public int Year { get; }
        public int MaxSpeed { get; protected set; }
        private List<string> _wheels;

        public IReadOnlyList<string> Wheels => _wheels.AsReadOnly();

        protected Car(string brand, int year)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(year, 1885, nameof(year)); // oldest car is from 1885, older car shouldn't be accepted
            ArgumentOutOfRangeException.ThrowIfGreaterThan(year, DateTime.Today.Year, nameof(year)); // newer car than current date shouldn't be accepted

            Brand = brand;
            Year = year;
            _wheels = ["Vorne rechts", "Vorne links", "Hinten rechts", "Hinten links"];
        }

        public void AddWheel(string wheelPosition)
        {
            if (_wheels.Count >= 4)
                throw new InvalidOperationException("Ein Auto kann nicht mehr als 4 Räder haben.");

            _wheels.Add(wheelPosition);
        }

        public void RemoveWheel(string wheelPosition)
        {
            if (_wheels.Count <= 0)
                throw new InvalidOperationException("Ein Auto muss mindestens 0 Räder haben.");

            _wheels.Remove(wheelPosition);
        }

        public override string ToString()
        {
            return $"{Brand}\t{Year}\t{MaxSpeed} km/h";
        }
    }
}
