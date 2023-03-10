using Microsoft.EntityFrameworkCore;
using Promart.Database.Context;
using Promart.Database.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Promart.Services
{
    public class StudentService : IDisposable
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }        

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<Student?> GetById(int id, bool includeWorkshops = true, bool includeFamilyRelationships = true)
        {
            var students = _context.Students.Where(s => s.Id == id);

            if (includeWorkshops)
                students = students.Include(s => s.Workshops);

            if (includeFamilyRelationships)
                students = students.Include(s => s.FamilyRelationships);                

            return await students.FirstOrDefaultAsync();
        }            
    }
}
