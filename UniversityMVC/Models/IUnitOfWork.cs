using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityMVC.Models
{
    interface IUnitOfWork
    {
        IRepository<Course> Courses { get; }
        IRepository<Group> Groups { get; }
        IRepository<Student> Students { get; }

        public void Save();
    }
}
