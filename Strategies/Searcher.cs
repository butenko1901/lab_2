using ScientistsLab.Models;

namespace ScientistsLab.Strategies
{
    public class Searcher
    {
        private ISearchStrategy strategy;

        public Searcher(ISearchStrategy strategy)
        {
            this.strategy = strategy;
        }

        public void SetStrategy(ISearchStrategy strategy)
        {
            this.strategy = strategy;
        }

        public List<Scientist> Search(string filePath, SearchFilter filter)
        {
            return strategy.Search(filePath, filter);
        }
    }
}
