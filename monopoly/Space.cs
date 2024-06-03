namespace Monopoly
{

    public abstract class Space
    {
        public string Name { get; }

        public Space(int id, string name)
        {
            Name = $"{id}. {name}";
        }
    }

    public class GoSpace : Space
    {
        public GoSpace(int id, string name) : base(id, name)
        {
        }
    }

    public class PropertySpace : Space
    {
        public int Id { get; }
        public decimal Price { get; }
        public decimal BaseRent { get; }
        public decimal HouseCost { get; }
        public decimal HotelCost { get; }
        public Player Owner { get; set; }
        public string Color { get; }
        public int NumberOfHouses { get; set; }
        public bool HasHotel { get; set; }

        public PropertySpace(int id, string name, decimal price, decimal baseRent, decimal houseCost, decimal hotelCost, string color) : base(id, name)
        {
            Id = id;
            Price = price;
            BaseRent = baseRent;
            HouseCost = houseCost;
            HotelCost = hotelCost;
            Owner = null;
            Color = color;
            NumberOfHouses = 0;
            HasHotel = false;
        }

        public decimal GetRent()
        {
            if (HasHotel)
            {
                return BaseRent * 5;
            }
            return BaseRent * (1 + NumberOfHouses);
        }
    }


    public class CommunityChestSpace : Space
    {
        public CommunityChestSpace(int id, string name) : base(id, name)
        {
        }
    }

    public class ChanceSpace : Space
    {
        public ChanceSpace(int id, string name) : base(id, name)
        {
        }
    }

    public class TaxSpace : Space
    {
        public decimal TaxAmount { get; set; }
        public TaxSpace(int id, string name) : base(id, name)
        {

        }
    }

    public class FreeParkingSpace : Space
    {
        public FreeParkingSpace(int id, string name) : base(id, name)
        {
        }
    }

    public class RailroadSpace : Space
    {
        public decimal Price { get; }
        public decimal Rent { get; }
        public Player Owner { get; set; }

        public RailroadSpace(int id, string name, decimal price, decimal rent) : base(id, name)
        {
            Price = price;
            Rent = rent;
            Owner = null;
        }
    }

    public class JailSpace : Space
    {
        public JailSpace(int id, string name) : base(id, name)
        {
        }
    }

    public class GoToJailSpace : Space
    {
        public GoToJailSpace(int id, string name) : base(id, name)
        {
        }
    }

    public class UtilitySpace : Space
    {
        public decimal Price { get; }
        public decimal RentMultiplier { get; }
        public Player Owner { get; set; }

        public UtilitySpace(int id, string name, decimal price, decimal rentMultiplier) : base(id, name)
        {
            Price = price;
            RentMultiplier = rentMultiplier;
            Owner = null;
        }
    }
}