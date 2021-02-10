using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityMVC.Models
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Имя не указано")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Фамилия не указана")]
        public string LastName { get; set; }
        public Group Group { get; set; }
    }
}
