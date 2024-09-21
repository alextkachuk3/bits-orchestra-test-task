namespace bits_orchestra_test_task.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsMarried { get; set; }
        public required string Phone { get; set; }
        public decimal Salary { get; set; }
    }

}
