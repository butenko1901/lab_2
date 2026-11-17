using ScientistsLab.Models;

namespace ScientistsLab.Strategies
{
    public interface ISearchStrategy
    {
        List<Scientist> Search(string filePath, SearchFilter filter);
    }
}
