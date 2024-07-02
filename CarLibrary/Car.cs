namespace CarLibrary
{
    public abstract class Car
    {
        public string Brand { get; }
        public int Year { get; }
        public int MaxSpeed { get; protected set; }
        private List<string> _wheels; // As wheels have no futher functionality than having a label, a string representation is sufficient

        public IReadOnlyList<string> Wheels => _wheels.AsReadOnly();

        protected Car(string brand, int year)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(year, 1885, nameof(year)); // Assumption: oldest car is from 1885, older car shouldn't be accepted
            ArgumentOutOfRangeException.ThrowIfGreaterThan(year, DateTime.Today.Year, nameof(year)); // Assumption: newer car than current date shouldn't be accepted

            Brand = brand;
            Year = year;
            _wheels = ["Vorne rechts", "Vorne links", "Hinten rechts", "Hinten links"]; 
        }

        public void AddWheel(string wheelPosition)
        {
            // Assumption: There can never be more than 4 Wheels, adding more leads to an exception
            if (_wheels.Count >= 4)
                throw new InvalidOperationException("Ein Auto kann nicht mehr als 4 Räder haben.");

            _wheels.Add(wheelPosition);
        }

        public void RemoveWheel(string wheelPosition)
        {
            // Assumption: Wheels should be removable
            _wheels.Remove(wheelPosition);
        }

        public override string ToString()
        {
            return $"{Brand}\t{Year}\t{MaxSpeed} km/h";
        }
    }
}
