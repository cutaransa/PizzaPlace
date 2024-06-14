using PizzaPlace.Core.Models;
using PizzaPlace.Core.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PizzaPlace.Persistence.Repositories
{
    public class FileRepository : IFileRepository
    {
        private ApplicationDbContext _context;

        public FileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<File> GetFiles()
        {
            return _context.Files
                .ToList();
        }

        public File GetFile(int fileId)
        {
            return _context.Files
                .SingleOrDefault(m => m.FileId == fileId);
        }

        public void Add(File file)
        {
            _context.Files.Add(file);
        }
    }
}
