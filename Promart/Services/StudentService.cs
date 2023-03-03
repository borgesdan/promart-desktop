using Microsoft.EntityFrameworkCore;
using Promart.Database.Context;
using Promart.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promart.Services
{
    public class StudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Student?> GetById(int id)
            => await _context.Students
                .Where(s => s.Id == id)
                .Include(s => s.Workshops)
                .Include(s => s.FamilyRelationships)
                .FirstOrDefaultAsync();
    }
}
