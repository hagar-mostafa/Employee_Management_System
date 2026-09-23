namespace Grad_Project.Models
{
    public class EmployeeParams
    {
        public string? SearchText { get; set; }
        public int? DepartmentId { get; set; }
        public int? JobTitleId { get; set; }




        private int _pageNumber = 1;

        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = value > 0 ? value : 1; }
        }


        private int _pageSize = 5; // Set to 5 so 25 employees = 5 pages

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 50 ? 50 : value; // Cap max size for performance
        }

    }
}
