namespace Monopoly{
        public class Player
    {
        public string Name { get; set; }
        public decimal Money { get; set; }
        public int Position { get; set; }
        public List<Property> OwnedProperties { get; set; }

        public Player()
        {
            OwnedProperties = new List<Property>();
        }

        public List<string> GetOwnedPropertyColors()
        {
            var colors = new List<string>();
            foreach (var property in OwnedProperties)
            {
                if (!colors.Contains(property.Color))
                {
                    colors.Add(property.Color);
                }
            }
            return colors;
        }

        public bool OwnsAllPropertiesOfColor(string color, List<PropertySpace> allProperties)
        {
            foreach (var property in allProperties)
            {
                if (property.Color == color && property.Owner != this)
                {
                    return false;
                }
            }
            return true;
        }

        public void BuildHouse(PropertySpace property)
        {
            if (property.NumberOfHouses < 4 && !property.HasHotel)
            {
                if (Money >= property.HouseCost)
                {
                    Money -= property.HouseCost;
                    property.NumberOfHouses++;
                    Console.WriteLine($"Built a house on {property.Name}. Total houses: {property.NumberOfHouses}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds to build a house.");
                }
            }
            else if (property.NumberOfHouses == 4 && !property.HasHotel)
            {
                if (Money >= property.HotelCost)
                {
                    Money -= property.HotelCost;
                    property.HasHotel = true;
                    property.NumberOfHouses = 0;
                    Console.WriteLine($"Built a hotel on {property.Name}");
                }
                else
                {
                    Console.WriteLine("Insufficient funds to build a hotel.");
                }
            }
            else
            {
                Console.WriteLine("Cannot build more houses or hotels on this property.");
            }
        }

        public void SellProperty(PropertySpace property)
        {
            var propertyToRemove = OwnedProperties.FirstOrDefault(p => p.Name == property.Name);
            if (propertyToRemove != null)
            {
                Money += propertyToRemove.Price;
                OwnedProperties.Remove(propertyToRemove);
                property.Owner = null;
                Console.WriteLine($"Sold {propertyToRemove.Name} for ${propertyToRemove.Price}");
            }
            else
            {
                Console.WriteLine("You don't own this property to sell.");
            }
        }
    }
}