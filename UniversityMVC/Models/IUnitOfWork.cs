using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityMVC.Models
{
    interface IUnitOfWork
    {
        ICourseRepository Courses { get; }
        IGroupRepository Groups { get; }
        IStudentRepository Students { get; }

        public void Save();
    }
}
