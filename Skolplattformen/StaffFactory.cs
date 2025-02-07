using Skolplattformen.Models;
namespace Skolplattformen
{
    public class StaffFactory
    {
        public Staff NewStaff(string name, int jobtitle, decimal salary, bool ismentor, DateOnly dateofhire, int unit)
        {
            var newStaff = new Staff()
            {
                StaffName = name,
                JobTitleId = jobtitle,
                Salary = salary,
                IsMentor = ismentor,
                DateOfHire = dateofhire,
                UnitId = unit
            };

            return newStaff;

        }
    }
}
