namespace Monopoly{
        public class Card
    {
        public string Description { get; set; }
        public Action<Player, GameController> Effect { get; set; }

        public Card(string description, Action<Player, GameController> effect)
        {
            Description = description;
            Effect = effect;
        }
    }
}