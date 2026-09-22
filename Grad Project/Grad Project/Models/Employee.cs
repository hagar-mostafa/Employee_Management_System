using Grad_Project.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
#nullable disable
namespace Grad_Project.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        [MaxLength(150)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Enter a valid phone number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Hire date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(5000, 100000, ErrorMessage = "Salary must be between 5000 and 100,000")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        // FK → Department
        [Required(ErrorMessage = "Please select a department")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        // FK → JobTitle
        [Required(ErrorMessage = "Please select a job title")]
        [Display(Name = "Job Title")]
        public int JobTitleId { get; set; }

        [Display(Name = "Profile Image")]
        public string ProfileImagePath { get; set; }

        // Navigation Properties
        public Department? Department { get; set; }
        public JobTitle? JobTitle { get; set; }
    }
}