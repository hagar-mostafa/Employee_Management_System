using Grad_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Grad_Project.Data
{
    public class DataSeeder
    {
        public static async Task SeedDataAsync(AppDbContext context)
        {
            if (await context.Departments.AnyAsync() || await context.Employees.AnyAsync())
            {
                return;
            }

            // 1. Create Departments
            var deptIT = new Department { Name = "Information Technology" };
            var deptHR = new Department { Name = "Human Resources" };
            var deptFinance = new Department { Name = "Finance" };
            var deptMarketing = new Department { Name = "Marketing" };
            var deptSales = new Department { Name = "Sales" };
            var deptEngineering = new Department { Name = "Engineering" };
            var deptSupport = new Department { Name = "Customer Support" };
            var deptOps = new Department { Name = "Operations" };
            var deptLegal = new Department { Name = "Legal" };
            var deptRnD = new Department { Name = "Research & Development" };

            context.Departments.AddRange(deptIT, deptHR, deptFinance, deptMarketing, deptSales,
                                         deptEngineering, deptSupport, deptOps, deptLegal, deptRnD);

            // 2. Create Job Titles
            var jobSE = new JobTitle { Title = "Software Engineer" };
            var jobHRM = new JobTitle { Title = "HR Manager" };
            var jobFA = new JobTitle { Title = "Financial Analyst" };
            var jobMS = new JobTitle { Title = "Marketing Specialist" };
            var jobSR = new JobTitle { Title = "Sales Representative" };
            var jobDevOps = new JobTitle { Title = "DevOps Engineer" };
            var jobSupport = new JobTitle { Title = "Support Specialist" };
            var jobOpsMgr = new JobTitle { Title = "Operations Manager" };
            var jobLegal = new JobTitle { Title = "Legal Counsel" };
            var jobDataSci = new JobTitle { Title = "Data Scientist" };

            context.JobTitles.AddRange(jobSE, jobHRM, jobFA, jobMS, jobSR,
                                       jobDevOps, jobSupport, jobOpsMgr, jobLegal, jobDataSci);

            await context.SaveChangesAsync();

            // 3. Seed Employees
            var employees = new List<Employee>
            {
                // ==========================================================
                // Department: Information Technology (21 Employees)
                // ==========================================================
                new Employee { FullName = "Ahmed Youssef", Email = "ahmed.y@company.com", PhoneNumber = "01011112222", HireDate = new DateTime(2021, 6, 15), Salary = 25000, DepartmentId = deptIT.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Sara Mahmoud", Email = "sara.m@company.com", PhoneNumber = "01122223333", HireDate = new DateTime(2022, 1, 20), Salary = 28000, DepartmentId = deptIT.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Omar Khaled", Email = "omar.k@company.com", PhoneNumber = "01233334444", HireDate = new DateTime(2020, 11, 10), Salary = 32000, DepartmentId = deptIT.Id, JobTitleId = jobDevOps.Id },
                new Employee { FullName = "Mona Ali", Email = "mona.a@company.com", PhoneNumber = "01544445555", HireDate = new DateTime(2023, 3, 5), Salary = 21000, DepartmentId = deptIT.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Kareem Tarek", Email = "kareem.t@company.com", PhoneNumber = "01055556666", HireDate = new DateTime(2019, 8, 12), Salary = 40000, DepartmentId = deptIT.Id, JobTitleId = jobDevOps.Id },
                new Employee { FullName = "Nada Wael", Email = "nada.w@company.com", PhoneNumber = "01066667777", HireDate = new DateTime(2021, 2, 15), Salary = 26000, DepartmentId = deptIT.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Ziad Taher", Email = "ziad.t@company.com", PhoneNumber = "01177778888", HireDate = new DateTime(2023, 9, 1), Salary = 30000, DepartmentId = deptIT.Id, JobTitleId = jobSE.Id },

                 // ---------------------------------------------------

                new Employee { FullName = "Hagar Mostafa", Email = "hagar.m@company.com", PhoneNumber = "01288889999", HireDate = new DateTime(2020, 5, 20), Salary = 33000, DepartmentId = deptIT.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Mohamed Ahmed", Email = "mohamed.a@company.com", PhoneNumber = "01599990000", HireDate = new DateTime(2022, 7, 11), Salary = 22000, DepartmentId = deptIT.Id, JobTitleId = jobSupport.Id },
                new Employee { FullName = "Omar Eslam", Email = "omar.e@company.com", PhoneNumber = "01000001111", HireDate = new DateTime(2018, 12, 1), Salary = 45000, DepartmentId = deptIT.Id, JobTitleId = jobDevOps.Id },
                new Employee { FullName = "Abdelrahman Sobhy", Email = "abdelrahman.s@company.com", PhoneNumber = "01111112222", HireDate = new DateTime(2023, 1, 15), Salary = 20000, DepartmentId = deptIT.Id, JobTitleId = jobSupport.Id },
                // ---------------------------------------------------

                new Employee { FullName = "Youssef Kamal", Email = "youssef.k@company.com", PhoneNumber = "01222223333", HireDate = new DateTime(2019, 4, 30), Salary = 38000, DepartmentId = deptIT.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Mai Ezz", Email = "mai.e@company.com", PhoneNumber = "01533334444", HireDate = new DateTime(2021, 8, 25), Salary = 29000, DepartmentId = deptIT.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Mostafa Nabil", Email = "mostafa.n@company.com", PhoneNumber = "01044445555", HireDate = new DateTime(2022, 11, 5), Salary = 24000, DepartmentId = deptIT.Id, JobTitleId = jobSupport.Id },
                new Employee { FullName = "Salma Diab", Email = "salma.d@company.com", PhoneNumber = "01155556666", HireDate = new DateTime(2020, 2, 14), Salary = 31000, DepartmentId = deptIT.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Amr Wagdy", Email = "amr.w@company.com", PhoneNumber = "01266667777", HireDate = new DateTime(2017, 9, 10), Salary = 50000, DepartmentId = deptIT.Id, JobTitleId = jobDevOps.Id },
                new Employee { FullName = "Farah Hassan", Email = "farah.h@company.com", PhoneNumber = "01577778888", HireDate = new DateTime(2023, 6, 1), Salary = 19000, DepartmentId = deptIT.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Hazem Zaki", Email = "hazem.z@company.com", PhoneNumber = "01088889999", HireDate = new DateTime(2021, 3, 18), Salary = 27000, DepartmentId = deptIT.Id, JobTitleId = jobSupport.Id },
                new Employee { FullName = "Laila Sami", Email = "laila.s@company.com", PhoneNumber = "01199990000", HireDate = new DateTime(2019, 10, 22), Salary = 35000, DepartmentId = deptIT.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Mahmoud Hussein", Email = "mahmoud.h@company.com", PhoneNumber = "01200001111", HireDate = new DateTime(2022, 4, 7), Salary = 23000, DepartmentId = deptIT.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Noha Yasser", Email = "noha.y@company.com", PhoneNumber = "01511112222", HireDate = new DateTime(2020, 1, 30), Salary = 34000, DepartmentId = deptIT.Id, JobTitleId = jobDevOps.Id },

                // ==========================================================
                // Department: Human Resources (3 Employees)
                // ==========================================================
                new Employee { FullName = "Nour Hisham", Email = "nour.h@company.com", PhoneNumber = "01166667777", HireDate = new DateTime(2021, 9, 1), Salary = 35000, DepartmentId = deptHR.Id, JobTitleId = jobHRM.Id },
                new Employee { FullName = "Ramy Said", Email = "ramy.s@company.com", PhoneNumber = "01277778888", HireDate = new DateTime(2022, 5, 18), Salary = 18000, DepartmentId = deptHR.Id, JobTitleId = jobHRM.Id },
                new Employee { FullName = "Hoda Kamal", Email = "hoda.k@company.com", PhoneNumber = "01588889999", HireDate = new DateTime(2020, 4, 12), Salary = 22000, DepartmentId = deptHR.Id, JobTitleId = jobHRM.Id },

                // ==========================================================
                // Department: Finance (5 Employees)
                // ==========================================================
                new Employee { FullName = "Hassan Ezz", Email = "hassan.e@company.com", PhoneNumber = "01588889999", HireDate = new DateTime(2018, 2, 25), Salary = 45000, DepartmentId = deptFinance.Id, JobTitleId = jobFA.Id },
                new Employee { FullName = "Yasmine Nabil", Email = "yasmine.n@company.com", PhoneNumber = "01099990000", HireDate = new DateTime(2020, 7, 30), Salary = 42000, DepartmentId = deptFinance.Id, JobTitleId = jobFA.Id },
                new Employee { FullName = "Tarek Mostafa", Email = "tarek.m@company.com", PhoneNumber = "01100001111", HireDate = new DateTime(2023, 1, 15), Salary = 30000, DepartmentId = deptFinance.Id, JobTitleId = jobFA.Id },
                new Employee { FullName = "Dina Shawky", Email = "dina.s@company.com", PhoneNumber = "01211112222", HireDate = new DateTime(2019, 11, 20), Salary = 38000, DepartmentId = deptFinance.Id, JobTitleId = jobFA.Id },
                new Employee { FullName = "Amr Diab", Email = "amr.d@company.com", PhoneNumber = "01022223333", HireDate = new DateTime(2022, 8, 9), Salary = 29000, DepartmentId = deptFinance.Id, JobTitleId = jobFA.Id },

                // ==========================================================
                // Department: Marketing (4 Employees)
                // ==========================================================
                new Employee { FullName = "Farah Wael", Email = "farah.w@company.com", PhoneNumber = "01211112222", HireDate = new DateTime(2021, 11, 22), Salary = 22000, DepartmentId = deptMarketing.Id, JobTitleId = jobMS.Id },
                new Employee { FullName = "Mazen Adel", Email = "mazen.a@company.com", PhoneNumber = "01522223333", HireDate = new DateTime(2022, 8, 9), Salary = 24000, DepartmentId = deptMarketing.Id, JobTitleId = jobMS.Id },
                new Employee { FullName = "Sherif Sami", Email = "sherif.s@company.com", PhoneNumber = "01033334444", HireDate = new DateTime(2019, 4, 14), Salary = 38000, DepartmentId = deptMarketing.Id, JobTitleId = jobMS.Id },
                new Employee { FullName = "Aya Hussein", Email = "aya.h@company.com", PhoneNumber = "01144445555", HireDate = new DateTime(2023, 6, 11), Salary = 19000, DepartmentId = deptMarketing.Id, JobTitleId = jobMS.Id },

                // ==========================================================
                // Department: Sales (6 Employees)
                // ==========================================================
                new Employee { FullName = "Mahmoud Hassan", Email = "mahmoud.h@company.com", PhoneNumber = "01255556666", HireDate = new DateTime(2020, 9, 19), Salary = 26000, DepartmentId = deptSales.Id, JobTitleId = jobSR.Id },
                new Employee { FullName = "Laila Fathy", Email = "laila.f@company.com", PhoneNumber = "01566667777", HireDate = new DateTime(2021, 2, 28), Salary = 27500, DepartmentId = deptSales.Id, JobTitleId = jobSR.Id },
                new Employee { FullName = "Ibrahim Zaki", Email = "ibrahim.z@company.com", PhoneNumber = "01077778888", HireDate = new DateTime(2017, 10, 5), Salary = 35000, DepartmentId = deptSales.Id, JobTitleId = jobSR.Id },
                new Employee { FullName = "Salma Yasser", Email = "salma.y@company.com", PhoneNumber = "01188889999", HireDate = new DateTime(2022, 12, 1), Salary = 21000, DepartmentId = deptSales.Id, JobTitleId = jobSR.Id },
                new Employee { FullName = "Mostafa Galal", Email = "mostafa.g@company.com", PhoneNumber = "01299990000", HireDate = new DateTime(2023, 7, 20), Salary = 19500, DepartmentId = deptSales.Id, JobTitleId = jobSR.Id },
                new Employee { FullName = "Youssef Nader", Email = "youssef.n@company.com", PhoneNumber = "01011113333", HireDate = new DateTime(2019, 3, 15), Salary = 31000, DepartmentId = deptSales.Id, JobTitleId = jobSR.Id },

                // ==========================================================
                // Department: Engineering (8 Employees)
                // ==========================================================
                new Employee { FullName = "Bassem Youssef", Email = "bassem.y@company.com", PhoneNumber = "01122224444", HireDate = new DateTime(2018, 5, 20), Salary = 45000, DepartmentId = deptEngineering.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Noha Ramy", Email = "noha.r@company.com", PhoneNumber = "01233335555", HireDate = new DateTime(2020, 3, 25), Salary = 38000, DepartmentId = deptEngineering.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Karam Wagdy", Email = "karam.w@company.com", PhoneNumber = "01544446666", HireDate = new DateTime(2021, 1, 30), Salary = 34000, DepartmentId = deptEngineering.Id, JobTitleId = jobDevOps.Id },
                new Employee { FullName = "Hazem Emam", Email = "hazem.e@company.com", PhoneNumber = "01055557777", HireDate = new DateTime(2019, 10, 14), Salary = 41000, DepartmentId = deptEngineering.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Shady Mohamed", Email = "shady.m@company.com", PhoneNumber = "01166668888", HireDate = new DateTime(2022, 2, 28), Salary = 29000, DepartmentId = deptEngineering.Id, JobTitleId = jobDevOps.Id },
                new Employee { FullName = "Reem Essam", Email = "reem.e@company.com", PhoneNumber = "01277779999", HireDate = new DateTime(2023, 5, 5), Salary = 25000, DepartmentId = deptEngineering.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Waleed Safwat", Email = "waleed.s@company.com", PhoneNumber = "01088880000", HireDate = new DateTime(2017, 12, 10), Salary = 48000, DepartmentId = deptEngineering.Id, JobTitleId = jobSE.Id },
                new Employee { FullName = "Habiba Tarek", Email = "habiba.t@company.com", PhoneNumber = "01199991111", HireDate = new DateTime(2021, 8, 15), Salary = 33000, DepartmentId = deptEngineering.Id, JobTitleId = jobDevOps.Id },

                // ==========================================================
                // Department: Customer Support (2 Employees)
                // ==========================================================
                new Employee { FullName = "Mariam Fathy", Email = "mariam.f@company.com", PhoneNumber = "01500002222", HireDate = new DateTime(2022, 4, 10), Salary = 17500, DepartmentId = deptSupport.Id, JobTitleId = jobSupport.Id },
                new Employee { FullName = "Ali Sobhy", Email = "ali.s@company.com", PhoneNumber = "01011114444", HireDate = new DateTime(2021, 11, 5), Salary = 18000, DepartmentId = deptSupport.Id, JobTitleId = jobSupport.Id },

                // ==========================================================
                // Department: Operations (4 Employees)
                // ==========================================================
                new Employee { FullName = "Khaled Zaki", Email = "khaled.z@company.com", PhoneNumber = "01122225555", HireDate = new DateTime(2019, 12, 5), Salary = 48000, DepartmentId = deptOps.Id, JobTitleId = jobOpsMgr.Id },
                new Employee { FullName = "Rasha Magdy", Email = "rasha.m@company.com", PhoneNumber = "01233336666", HireDate = new DateTime(2020, 6, 20), Salary = 42000, DepartmentId = deptOps.Id, JobTitleId = jobOpsMgr.Id },
                new Employee { FullName = "Essam El-Din", Email = "essam.e@company.com", PhoneNumber = "01544447777", HireDate = new DateTime(2018, 9, 15), Salary = 50000, DepartmentId = deptOps.Id, JobTitleId = jobOpsMgr.Id },
                new Employee { FullName = "Ghada Nabil", Email = "ghada.n@company.com", PhoneNumber = "01055558888", HireDate = new DateTime(2023, 2, 10), Salary = 31000, DepartmentId = deptOps.Id, JobTitleId = jobOpsMgr.Id },

                // ==========================================================
                // Department: Legal (1 Employee)
                // ==========================================================
                new Employee { FullName = "Magdy Yacoub", Email = "magdy.y@company.com", PhoneNumber = "01166669999", HireDate = new DateTime(2016, 3, 1), Salary = 65000, DepartmentId = deptLegal.Id, JobTitleId = jobLegal.Id },

                // ==========================================================
                // Department: Research & Development (3 Employees)
                // ==========================================================
                new Employee { FullName = "Osama Mounir", Email = "osama.m@company.com", PhoneNumber = "01277770000", HireDate = new DateTime(2020, 10, 10), Salary = 52000, DepartmentId = deptRnD.Id, JobTitleId = jobDataSci.Id },
                new Employee { FullName = "Nadine Ashraf", Email = "nadine.a@company.com", PhoneNumber = "01588881111", HireDate = new DateTime(2021, 5, 25), Salary = 47000, DepartmentId = deptRnD.Id, JobTitleId = jobDataSci.Id },
                new Employee { FullName = "Saeed Fawzy", Email = "saeed.f@company.com", PhoneNumber = "01099992222", HireDate = new DateTime(2022, 11, 15), Salary = 41000, DepartmentId = deptRnD.Id, JobTitleId = jobDataSci.Id }
            };

            context.Employees.AddRange(employees);
            await context.SaveChangesAsync();
        }
    }
}