namespace Task01API.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string? MgrName { get; set; }
        public virtual ICollection<Student>? Students { get; set; } 
    }
}
