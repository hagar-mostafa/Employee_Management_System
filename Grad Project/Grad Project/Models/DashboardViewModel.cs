namespace Grad_Project.Models
{
    public class DashboardViewModel
    {
        public int TotalEmployees { get; set; }
        public int TotalDepartments { get; set; }
        public int TotalJobTitles { get; set; }
        public int HiredThisMonth { get; set; }
        public List<Employee> RecentEmployees { get; set; } = new List<Employee>();
    }
}
