using System.ComponentModel.DataAnnotations;

namespace Monopoly
{
    public class GameController
    {
        private Board board;
        public List<Player> players;
        private Dice dice;
        private const int boardX = 4;
        private const int boardY = 10;
        public int maxPlayer = 4;
        public bool startGame = false;

        public GameController()
        {
            board = new Board();
            players = new List<Player>();
            dice = new Dice();
        }

        public void StartGame()
        {

            while (!startGame)
            {
                Console.WriteLine("\nEnter player name or type 'start' to begin the game:");

                string input = Console.ReadLine();

                if (input.ToLower() == "start")
                {
                    if (GetPlayers().Count < 2)
                    {
                        Console.WriteLine("\nPlease add at least 2 players to start the game.");
                        continue;
                    }

                    startGame = true;
                    Console.WriteLine("\nGame started!");
                }
                else
                {
                    if (GetPlayers().Count >= maxPlayer)
                    {
                        Console.WriteLine("\nCannot add more players. The maximum number of players has been reached.");
                    }
                    else
                    {
                        AddPlayer(input, 2000);
                        Console.WriteLine($"\n{input} has joined the game with $2000.");
                    }
                }
            }
            PlayGame();
        }

        public List<Player> GetPlayers()
        {
            return players;
        }

        public void AddPlayer(string name, decimal initialMoney)
        {
            if (players.Count < maxPlayer)
            {
                Player player = new Player { Name = name, Money = initialMoney, Position = 0 };
                players.Add(player);
            }
            else
            {
                Console.WriteLine("Cannot add more players. The maximum number of players has been reached.");
            }
        }

        public Space GetCurrentSpace(IPlayer player)
        {
            int position = player.Position;
            return board.GetSpace(position);
        }

        public void MovePlayer(IPlayer player, int steps)
        {
            player.Position = (player.Position + steps) % board.GetSpaceCount();
        }

        public void DisplayPlayerStatus(Player player)
        {
            Space currentSpace = board.GetSpace(player.Position);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"!!Player Info!!\n>Player: |{player.Name}|\n>Position: |{currentSpace.Name}|\n>Balance: |${player.Money}|");

            if (currentSpace is PropertySpace propertySpace)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"\n!!Property Details!! \n>Name: |{propertySpace.Name}|\n>Price: |${propertySpace.Price}|\n>Rent: |${propertySpace.GetRent()}|");

                if (propertySpace.Owner != null)
                {
                    Console.WriteLine($"(!)Owned by: {propertySpace.Owner.Name}");
                }
                else
                {
                    Console.WriteLine("(!)Unowned");
                }
            }
            else if (currentSpace is CommunityChestSpace)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\n!!Community Chest!!");
                var card = board.DrawCommunityChestCard();
                Console.WriteLine($"Card: {card.Description}");
                ApplyCardEffect(player, card);
            }
            else if (currentSpace is ChanceSpace)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("\n!!Chance!!");
                var card = board.DrawChanceCard();
                Console.WriteLine($"Card: {card.Description}");
                ApplyCardEffect(player, card);
            }

            Console.ResetColor();
        }



        public Card DrawCard(Space space)
        {
            if (space is ChanceSpace)
            {
                return board.DrawChanceCard();
            }
            else if (space is CommunityChestSpace)
            {
                return board.DrawCommunityChestCard();
            }
            else
            {
                throw new InvalidOperationException("Invalid space for drawing a card.");
            }
        }

        public void ApplyCardEffect(Player player, Card card)
        {
            card.Effect(player, this);
        }

        public void OpenPlayerOwnedProperties(IPlayer player)
        {
            Console.WriteLine($"\n{player.Name}'s owned properties:");

            foreach (var space in board.spaces)
            {
                if (space is PropertySpace propertySpace && propertySpace.Owner == player)
                {
                    Console.WriteLine($"|Name: {propertySpace.Name}|Price: ${propertySpace.Price}|Rent: ${propertySpace.GetRent()}|");
                }
            }

            if (player.OwnedProperties.Count == 0)
            {
                Console.WriteLine("Player does not own any properties.");
            }
        }

        public bool IsBankrupt(IPlayer player)
        {
            return player.Money <= 0;
        }

        public bool IsOnlyOnePlayerLeft()
        {
            int playersWithMoney = 0;

            foreach (var player in players)
            {
                if (player.Money > 0)
                {
                    playersWithMoney++;
                }
            }

            return playersWithMoney == 1;
        }

        public void DisplayMap()
        {
            Console.WriteLine("\n!!Current Location!!");
            string[,] board = new string[boardX, boardY];
            int cellNumber = 1;

            for (int i = 0; i < boardX; i++)
            {
                for (int j = 0; j < boardY; j++)
                {
                    board[i, j] = "[" + cellNumber.ToString("00") + "]";
                    cellNumber++;
                }
            }

            foreach (var player in players)
            {
                int x = player.Position % boardY;
                int y = player.Position / boardY;
                string playerName = player.Name.Length > 1 ? player.Name.Substring(0, 2) : player.Name.Substring(0, 1);
                board[y, x] = $"[{playerName}]";
            }

            for (int i = 0; i < boardX; i++)
            {
                for (int j = 0; j < boardY; j++)
                {
                    Console.Write($"{board[i, j]}");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n");
        }

        public void PlayGame()
        {
            while (!IsOnlyOnePlayerLeft())
            {
                foreach (var player in players)
                {
                    if (!IsBankrupt(player))
                    {
                        TakeTurn(player);
                    }

                    if (IsOnlyOnePlayerLeft())
                    {
                        break;
                    }
                }
            }
            FinishGame();
        }

        public void FinishGame()
        {
            Console.WriteLine("Game over!");
            Player winner = players.Find(p => p.Money > 0);
            Console.WriteLine($"The winner is {winner.Name} with ${winner.Money}!");
        }

        private void TakeTurn(Player player)
        {
            Console.WriteLine($"\n{player.Name}'s turn.");

            int roll1 = dice.Roll();
            int roll2 = dice.Roll();
            int totalRoll = roll1 + roll2;

            Console.WriteLine($"{player.Name} rolled {roll1} and {roll2}, moving {totalRoll} spaces.");

            MovePlayer(player, totalRoll);
            DisplayPlayerStatus(player);
            DisplayMap();

            Space currentSpace = GetCurrentSpace(player);

            if (currentSpace is PropertySpace propertySpace)
            {
                HandlePropertySpace(player, propertySpace);

                if (player.OwnsAllPropertiesOfColor(propertySpace.Color, board.spaces.OfType<PropertySpace>().ToList()))
                {
                    Console.WriteLine($"You own all properties of color {propertySpace.Color}. Would you like to build a house on {propertySpace.Name}? (y/n)");
                    string input = Console.ReadLine();

                    if (input.ToLower() == "y")
                    {
                        player.BuildHouse(propertySpace);
                    }
                }
            }
            else if (currentSpace is ChanceSpace || currentSpace is CommunityChestSpace)
            {
                Card drawnCard = DrawCard(currentSpace);
                ApplyCardEffect(player, drawnCard);
            }
            else if (currentSpace is TaxSpace taxSpace)
            {
                HandleTaxSpace(player, taxSpace);
            }
            else if (currentSpace is GoToJailSpace)
            {
                Console.WriteLine($"{player.Name} is going to Jail!");
                player.Position = 10;
            }

            if (IsBankrupt(player))
            {
                Console.WriteLine($"{player.Name} is bankrupt!");
            }
        }

        private void HandlePropertySpace(Player player, PropertySpace propertySpace)
        {
            if (propertySpace.Owner == null)
            {
                Console.WriteLine($"{propertySpace.Name} is unowned. Would you like to buy it for ${propertySpace.Price}? (y/n)");
                string input = Console.ReadLine();

                if (input.ToLower() == "y" && player.Money >= propertySpace.Price)
                {
                    player.Money -= propertySpace.Price;
                    propertySpace.Owner = player;
                    player.OwnedProperties.Add(new PropertyData(propertySpace.Name, propertySpace.Price, propertySpace.GetRent(), propertySpace.Color));
                    Console.WriteLine($"{player.Name} bought {propertySpace.Name}!");
                }
                else if (player.Money < propertySpace.Price)
                {
                    Console.WriteLine("Insufficient funds!");
                }
                else
                {
                    Console.WriteLine("You skipped this round!");
                }
            }
            else if (propertySpace.Owner != player)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{propertySpace.Name} is owned by {propertySpace.Owner.Name}. Paying rent of ${propertySpace.GetRent()}.");
                Console.ResetColor();
                player.Money -= propertySpace.GetRent();
                propertySpace.Owner.Money += propertySpace.GetRent();
            }
            else
            {
                Console.WriteLine($"You own {propertySpace.Name}. What would you like to do?\n1. Pay rent\n2. Sell property");
                string input = Console.ReadLine();
                if (input == "1")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{propertySpace.Name} is owned by {propertySpace.Owner.Name}. Paying rent of ${propertySpace.GetRent()}.");
                    Console.ResetColor();
                }
                else if (input == "2")
                {
                    player.SellProperty(propertySpace);
                }
                else
                {
                    Console.WriteLine("Invalid option. Skipping this round.");
                }
            }
        }

        private void HandleTaxSpace(Player player, TaxSpace taxSpace)
        {
            decimal taxAmount = taxSpace.TaxAmount;
            Console.WriteLine($"{player.Name} needs to pay tax of ${taxAmount}.");
            player.Money -= taxAmount;
        }
    }
}