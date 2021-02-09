using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityMVC.Models
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private UniversityContext UniversityContext
        {
            get
            {
                return context as UniversityContext;
            }
        }
        public StudentRepository(UniversityContext context) : base(context) { }

        public IEnumerable<Student> GetByGroup(int groupId)
        {
            return UniversityContext.Students.Where(s => s.Group.Id == groupId);
        }
    }
}
