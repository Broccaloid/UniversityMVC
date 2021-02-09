using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityMVC.Models
{
    public interface IGroupRepository : IRepository<Group>
    {
        IEnumerable<Group> GetByCourse(int courseId);
    }
}
