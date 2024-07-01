namespace CarLibrary
{
    public class Ford : Car
    {
        public Ford(int year) : base("Ford", year)
        {
            MaxSpeed = 250;
        }
    }
}