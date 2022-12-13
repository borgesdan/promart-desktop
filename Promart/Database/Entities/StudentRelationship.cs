namespace Promart.Database.Entities
{
    public class StudentRelationship
    {
        public int Id { get; set; }

        public string? FullName { get; set; }
        public FamilyRelationshipType? Relationship { get; set; }
        public string? Occupation { get; set; }
        public string? Education { get; set; }
        public string? Income { get; set; }

        public Student? Student { get; set; }
    }
}
