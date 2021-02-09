using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityMVC.Models
{
    public interface IStudentRepository : IRepository<Student>
    {
        IEnumerable<Student> GetByGroup(int groupId);
    }
}
