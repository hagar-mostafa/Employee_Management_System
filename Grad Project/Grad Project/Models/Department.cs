using System.ComponentModel.DataAnnotations;
using System;
#nullable disable
namespace Grad_Project.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Department Name")]
        public string Name { get; set; }

        // Navigation Property=> Employee has relation M:1 with Department
        public ICollection<Employee> Employees { get; set; }
    }
}
