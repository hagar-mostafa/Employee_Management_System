using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Grad_Project.Models
{
    public class EmployeeCreateViewModel
    {

        [Display(Name = "Profile Photo")]
        public IFormFile? ImageFile { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        [Display(Name = "Full Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
        public string FullName { get; set; } = default!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(150)]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Enter a valid phone number")]
        [RegularExpression(@"^(010|011|012|015)\d{8}$", ErrorMessage = "Phone number must be a valid Egyptian mobile number")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = default!;

        [Required(ErrorMessage = "Hire date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        [Range(5000, 100000, ErrorMessage = "Salary must be between 5000 and 100,000")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Please select a department")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Please select a job title")]
        [Display(Name = "Job Title")]
        public int JobTitleId { get; set; }
    }
}