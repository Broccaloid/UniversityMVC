using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityMVC.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UniversityContext context;
        public IRepository<Course> Courses { get; private set; }

        public IRepository<Group> Groups { get; private set; }

        public IRepository<Student> Students { get; private set; }

        public UnitOfWork(UniversityContext context)
        {
            this.context = context;
            Courses = new CourseRepository(context);
            Groups = new GroupRepository(context);
            Students = new StudentRepository(context);
        }

        public async void Save()
        {
            await context.SaveChangesAsync();
        }
    }
}
