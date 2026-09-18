using System.ComponentModel.DataAnnotations;
using System;
#nullable disable
namespace Grad_Project.Models
{
    public class JobTitle
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Job title is required")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        [Display(Name = "Job Title")]
        public string Title { get; set; }

        // Navigation Property => Employee  has relation M : 1 with JopTitle
        public ICollection<Employee> Employees { get; set; }
    }
}
