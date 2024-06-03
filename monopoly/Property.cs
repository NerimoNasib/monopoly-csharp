namespace Monopoly
{
    public abstract class Property
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Rent { get; set; }
        public Player Owner { get; set; }
        public string Color { get; set; }
    }

    public class PropertyData : Property
    {
        public PropertyData(string name, decimal price, decimal rent, string color)
        {
            Name = name;
            Price = price;
            Rent = rent;
            Color = color;
            Owner = null;
        }
    }
}