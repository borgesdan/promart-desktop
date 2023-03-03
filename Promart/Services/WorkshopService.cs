using Microsoft.EntityFrameworkCore;
using Promart.Database;
using Promart.Database.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Promart.Services.Contracts;
using Promart.Database.Entities;

namespace Promart.Services
{
    public class WorkshopService
    {
        private readonly AppDbContext _context;        

        public WorkshopService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Workshop>> GetAll()
            => await _context.Workshops
            .Where(w => w.RegistryStatus == RegistryStatus.Active)              
            .ToListAsync();        
    }
}
