using PizzaPlace.Core.Models;
using System.Collections.Generic;

namespace PizzaPlace.Core.Repositories
{
    public interface IFileRepository
    {
        IEnumerable<File> GetFiles();
        File GetFile(int fileId);
        void Add(File file);
    }
}
