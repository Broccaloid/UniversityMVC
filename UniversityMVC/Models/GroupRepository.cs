using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityMVC.Models
{
    public class GroupRepository : Repository<Group>, IGroupRepository
    {
        private UniversityContext UniversityContext
        {
            get
            {
                return context as UniversityContext;
            }
        }
        public GroupRepository(UniversityContext context) : base(context) { }

        public IEnumerable<Group> GetByCourse(int? courseId)
        {
            return UniversityContext.Groups.Where(g => g.Course.Id == courseId).ToList();
        }
    }
}
