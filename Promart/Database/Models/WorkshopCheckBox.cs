using Promart.Database.Entities;

namespace Promart.Database.Models
{
    public class WorkshopCheckBox
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public WorkshopCheckBox() { }

        public WorkshopCheckBox(Workshop workshop)
        {
            Id = workshop.Id;
            Name = workshop.Name;
        }

        public override string ToString()
        {
            return Name ?? string.Empty;
        }
    }
}
