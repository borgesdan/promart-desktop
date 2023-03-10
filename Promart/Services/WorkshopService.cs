using Microsoft.EntityFrameworkCore;
using Promart.Database;
using Promart.Database.Context;
using Promart.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Promart.Services
{
    public class WorkshopService : IDisposable
    {
        private readonly AppDbContext _context;        

        public WorkshopService(AppDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<IEnumerable<Workshop>> GetAll()
            => await _context.Workshops
            .Where(w => w.RegistryStatus == RegistryStatus.Active)              
            .ToListAsync();        
    }
}
