using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAnnotations;
using vcar.Core.Models;
using vcar.Core;

namespace vcar.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VcarContext _context;

        public UnitOfWork(VcarContext context)
        {
            _context = context;
        }
        public async Task Complete()
        {
            await _context.SaveChangesAsync();
        }
    }
}