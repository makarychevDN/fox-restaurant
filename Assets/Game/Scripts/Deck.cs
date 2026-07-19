using System.Collections.Generic;
using System.Linq;

namespace foxRestaurant
{
    public class Deck<T>
    {
        private readonly List<T> source;
        private readonly int copiesCount;
        private List<T> deck;

        public Deck(List<T> source, int copiesCount)
        {
            this.source = source;
            this.copiesCount = copiesCount;
            deck = new List<T>();
            Generate();
        }

        private void Generate()
        {
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

        public T DrawExcept(T except)
        {
            if (deck.Count == 0 || deck.All(c => c.Equals(except)))
                Generate();

            var card = deck.Find(c => !c.Equals(except));

            if(card == null)
                card = deck[0];

            deck.Remove(card);
            return card;
        }

        public T RollExcept(T except)
        {
            T reroll = except;
            while(reroll.Equals(except))
            {
                reroll = source.GetRandomElement();
            }
            return reroll;
        }
    }
}