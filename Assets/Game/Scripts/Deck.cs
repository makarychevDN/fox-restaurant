using System.Collections.Generic;

namespace foxRestaurant
{
    public class Deck<T>
    {
        private readonly List<T> source;
        private readonly int copiesCount;
        private List<T> deck;

        public Deck(List<T> source, int copiesCount = 2)
        {
            this.source = source;
            this.copiesCount = copiesCount;
            Generate();
        }

        private void Generate()
        {
            deck = new List<T>();
            for (int i = 0; i < copiesCount; i++)
                deck.AddRange(source);

            deck.Shuffle();
        }

        public T Draw()
        {
            if (deck.Count == 0)
                Generate();

            var card = deck[0];
            deck.RemoveAt(0);
            return card;
        }
    }
}