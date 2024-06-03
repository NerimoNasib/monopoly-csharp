namespace Monopoly
{
    public class Board
    {
        public List<Space> spaces { get; private set; }
        private List<Card> chanceCards;
        private List<Card> communityChestCards;

        public Board()
        {
            spaces = new List<Space>();
            chanceCards = new List<Card>();
            communityChestCards = new List<Card>();
            InitializeSpaces();
            InitializeCards();
        }

        private void InitializeSpaces()
        {
            spaces.Add(new GoSpace(1, "Go"));
            spaces.Add(new PropertySpace(2, "Mediterranean Avenue (Brown)", 60, 2, 50, 200, "Brown"));
            spaces.Add(new CommunityChestSpace(3, "Community Chest"));
            spaces.Add(new PropertySpace(4, "Baltic Avenue (Brown)", 60, 4, 50, 200, "Brown"));
            spaces.Add(new TaxSpace(5, "Income Tax"));
            spaces.Add(new RailroadSpace(6, "Reading Railroad", 200, 25));
            spaces.Add(new PropertySpace(7, "Oriental Avenue (Blue)", 100, 6, 50, 200, "Blue"));
            spaces.Add(new ChanceSpace(8, "Chance"));
            spaces.Add(new PropertySpace(9, "Vermont Avenue (Blue)", 100, 6, 50, 200, "Blue"));
            spaces.Add(new PropertySpace(10, "Connecticut Avenue (Blue)", 120, 8, 50, 200, "Blue"));
            spaces.Add(new JailSpace(11, "Jail/Just Visiting"));
            spaces.Add(new PropertySpace(12, "St. Charles Place (Pink)", 140, 10, 50, 200, "Pink"));
            spaces.Add(new UtilitySpace(13, "Electric Company", 150, 0));
            spaces.Add(new PropertySpace(14, "States Avenue (Pink)", 140, 10, 50, 200, "Pink"));
            spaces.Add(new PropertySpace(15, "Virginia Avenue (Pink)", 160, 12, 50, 200, "Pink"));
            spaces.Add(new RailroadSpace(16, "Pennsylvania Railroad", 200, 25));
            spaces.Add(new PropertySpace(17, "St. James Place (Orange)", 180, 14, 50, 200, "Orange"));
            spaces.Add(new CommunityChestSpace(18, "Community Chest"));
            spaces.Add(new PropertySpace(19, "Tennessee Avenue  (Orange)", 180, 14, 50, 200, "Orange"));
            spaces.Add(new PropertySpace(20, "New York Avenue  (Orange)", 200, 16, 50, 200, "Orange"));
            spaces.Add(new FreeParkingSpace(21, "Free Parking"));
            spaces.Add(new PropertySpace(22, "Kentucky Avenue (Red)", 220, 18, 50, 200, "Red"));
            spaces.Add(new ChanceSpace(23, "Chance"));
            spaces.Add(new PropertySpace(24, "Indiana Avenue (Red)", 220, 18, 50, 200, "Red"));
            spaces.Add(new PropertySpace(25, "Illinois Avenue (Red)", 240, 20, 50, 200, "Red"));
            spaces.Add(new RailroadSpace(26, "B&O Railroad", 200, 25));
            spaces.Add(new PropertySpace(27, "Atlantic Avenue (Yellow)", 260, 22, 50, 200, "Yellow"));
            spaces.Add(new PropertySpace(28, "Ventnor Avenue (Yellow)", 260, 22, 50, 200, "Yellow"));
            spaces.Add(new UtilitySpace(29, "Water Works", 150, 0));
            spaces.Add(new PropertySpace(30, "Marvin Gardens (Yellow)", 280, 24, 50, 200, "Yellow"));
            spaces.Add(new GoToJailSpace(31, "Go To Jail"));
            spaces.Add(new PropertySpace(32, "Pacific Avenue (Green)", 300, 26, 50, 200, "Green"));
            spaces.Add(new PropertySpace(33, "North Carolina Avenue (Green)", 300, 26, 50, 200, "Green"));
            spaces.Add(new CommunityChestSpace(34, "Community Chest"));
            spaces.Add(new PropertySpace(35, "Pennsylvania Avenue (Green)", 320, 28, 50, 200, "Green"));
            spaces.Add(new RailroadSpace(36, "Short Line", 200, 25));
            spaces.Add(new ChanceSpace(37, "Chance"));
            spaces.Add(new PropertySpace(38, "Park Place (Purple)", 350, 35, 50, 200, "Purple"));
            spaces.Add(new TaxSpace(39, "Luxury Tax"));
            spaces.Add(new PropertySpace(40, "Boardwalk (Purple)", 400, 50, 50, 200, "Purple"));
        }


        private void InitializeCards()
        {
            chanceCards.Add(new Card("Advance to Go", (player, game) => { player.Position = 0; player.Money += 200; }));
            chanceCards.Add(new Card("Advance to Illinois Avenue", (player, game) => player.Position = 24));
            chanceCards.Add(new Card("Advance to St. Charles Place", (player, game) => player.Position = 11));
            chanceCards.Add(new Card("Advance to nearest Utility", (player, game) =>
            {
                if (player.Position < 12 || player.Position >= 28)
                    player.Position = 12;
                else
                    player.Position = 28;
            }));
            chanceCards.Add(new Card("Advance to nearest Railroad", (player, game) =>
            {
                if (player.Position < 5 || player.Position >= 35)
                    player.Position = 5;
                else if (player.Position < 15)
                    player.Position = 15;
                else if (player.Position < 25)
                    player.Position = 25;
                else
                    player.Position = 35;
            }));
            chanceCards.Add(new Card("Bank pays you dividend of $50", (player, game) => player.Money += 50));
            chanceCards.Add(new Card("Get out of Jail Free", (player, game) => { }));
            chanceCards.Add(new Card("Go back 3 spaces", (player, game) => player.Position -= 3));
            chanceCards.Add(new Card("Go to Jail", (player, game) => player.Position = 10));
            chanceCards.Add(new Card("Make general repairs on all your property", (player, game) => player.Money -= 100));
            chanceCards.Add(new Card("Take a trip to Reading Railroad", (player, game) => player.Position = 5));
            chanceCards.Add(new Card("Pay poor tax of $15", (player, game) => player.Money -= 15));
            chanceCards.Add(new Card("Take a walk on the Boardwalk", (player, game) => player.Position = 39));
            chanceCards.Add(new Card("Elected Chairman of the Board", (player, game) => player.Money -= 50));
            chanceCards.Add(new Card("Building loan matures", (player, game) => player.Money += 150));
            communityChestCards.Add(new Card("Advance to Go", (player, game) => { player.Position = 0; player.Money += 200; }));
            communityChestCards.Add(new Card("Bank error in your favor", (player, game) => player.Money += 200));
            communityChestCards.Add(new Card("Doctor's fees", (player, game) => player.Money -= 50));
            communityChestCards.Add(new Card("From sale of stock you get $50", (player, game) => player.Money += 50));
            communityChestCards.Add(new Card("Get out of Jail Free", (player, game) => { }));
            communityChestCards.Add(new Card("Go to Jail", (player, game) => player.Position = 10));
            communityChestCards.Add(new Card("Grand Opera Night", (player, game) => player.Money += 50));
            communityChestCards.Add(new Card("Holiday fund matures", (player, game) => player.Money += 100));
            communityChestCards.Add(new Card("Income tax refund", (player, game) => player.Money += 20));
            communityChestCards.Add(new Card("It is your birthday", (player, game) => player.Money += 10));
            communityChestCards.Add(new Card("Life insurance matures", (player, game) => player.Money += 100));
            communityChestCards.Add(new Card("Hospital fees", (player, game) => player.Money -= 100));
            communityChestCards.Add(new Card("School fees", (player, game) => player.Money -= 50));
            communityChestCards.Add(new Card("Receive $25 consultancy fee", (player, game) => player.Money += 25));
            communityChestCards.Add(new Card("You are assessed for street repairs", (player, game) => player.Money -= 40));
            communityChestCards.Add(new Card("You have won second prize in a beauty contest", (player, game) => player.Money += 10));
            communityChestCards.Add(new Card("You inherit $100", (player, game) => player.Money += 100));
        }

        public Space GetSpace(int position)
        {
            return spaces[position];
        }

        public int GetSpaceCount()
        {
            return spaces.Count;
        }

        public Card DrawChanceCard()
        {
            Random random = new Random();
            int index = random.Next(chanceCards.Count);
            return chanceCards[index];
        }

        public Card DrawCommunityChestCard()
        {
            Random random = new Random();
            int index = random.Next(communityChestCards.Count);
            return communityChestCards[index];
        }
    }
}