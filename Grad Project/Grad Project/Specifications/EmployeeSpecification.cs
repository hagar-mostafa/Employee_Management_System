using Grad_Project.Models;
using System.Linq.Expressions;

namespace Grad_Project.Specifications
{
    public class EmployeeSpecification
    {
        public Expression<Func<Employee, bool>> Criteria { get; private set; }

        public int Take { get; set; }
        public int Skip { get; set; }
        public bool IsPagination { get; set; } = false;


        public EmployeeSpecification(EmployeeParams emp)
        {
            if (emp != null)
            {
                Criteria = (e => (!emp.JobTitleId.HasValue || e.JobTitleId == emp.JobTitleId) && (!emp.DepartmentId.HasValue || e.DepartmentId == emp.DepartmentId) &&
                (string.IsNullOrWhiteSpace(emp.SearchText) || e.Email.Contains(emp.SearchText) || e.FullName.Contains(emp.SearchText)));

            }


        }


        public void AddPagination(int pageSize, int pageNumber)
        {
            IsPagination = true;
            Take = pageSize;
            Skip = (pageNumber - 1) * pageSize;


        }







    }
}
