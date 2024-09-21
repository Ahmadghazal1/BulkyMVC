using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            Category = new CategoryRepository(context);
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
