using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityMVC.Models
{
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { set; get; }
        [Required(ErrorMessage = "Название не указана")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Название укзано некоректно")]
        public string Name { set; get; }
        public Course Course { set; get; }
    }
}
