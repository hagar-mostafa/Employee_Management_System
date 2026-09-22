using Grad_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Grad_Project.Specifications
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<Employee> CreateQuery(IQueryable<Employee> baseQuery, EmployeeSpecification spec)
        {
            IQueryable<Employee> curQuery = baseQuery;
            if (spec.Criteria != null)
            {
                curQuery = curQuery.Where(spec.Criteria);

            }

            curQuery = curQuery.Include(emp => emp.Department);
            curQuery = curQuery.Include(emp => emp.JobTitle);
            if (spec.IsPagination)
            {
                curQuery = curQuery.Skip(spec.Skip).Take(spec.Take);
            }
            return curQuery;
        }
    }
}
