using System.Collections.Generic;

namespace Promart.Database.Entities
{
    public class Workshop
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public Workshop()
        {
            Students = new HashSet<Student>();
        }
    }
}
