using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityMVC.Models;

namespace UniversityMVC
{
    public static class SampleData
    {
        public static void Initialize(UniversityContext context)
        {
            if (!context.Courses.Any())
            {
                context.Courses.AddRange(
                    new Course
                    {
                        Id = 0,
                        Name = "Math",
                        Description = "Sample Text"
                    },
                    new Course
                    {
                        Id = 1,
                        Name = "Physics",
                        Description = "Sample Text"
                    },
                    new Course
                    {
                        Id = 2,
                        Name = "Biology",
                        Description = "Sample Text"
                    },
                    new Course
                    {
                        Id = 3,
                        Name = "CS",
                        Description = "Sample Text"
                    });
                context.SaveChanges();
            }

            if (!context.Groups.Any())
            {
                context.Groups.AddRange(
                    new Group
                    {
                        Id = 0,
                        Name = "C-100",
                        Course = context.Courses.Find(0)
                    },
                    new Group
                    {
                        Id = 1,
                        Name = "C-101",
                        Course = context.Courses.Find(2)
                    },
                    new Group
                    {
                        Id = 2,
                        Name = "C-102",
                        Course = context.Courses.Find(1)
                    },
                    new Group
                    {
                        Id = 3,
                        Name = "C-103",
                        Course = context.Courses.Find(3)
                    },
                    new Group
                    {
                        Id = 4,
                        Name = "C-104",
                        Course = context.Courses.Find(1)
                    }
                );
                context.SaveChanges();
            }

            if (!context.Students.Any())
            {
                context.Students.AddRange(
                    new Student
                    {
                        Id = 0,
                        FirstName = "First Name 0",
                        LastName = "Last Name 0",
                        Group = context.Groups.Find(0)
                    },
                    new Student
                    {
                        Id = 1,
                        FirstName = "First Name 1",
                        LastName = "Last Name 1",
                        Group = context.Groups.Find(0)
                    },
                    new Student
                    {
                        Id = 2,
                        FirstName = "First Name 2",
                        LastName = "Last Name 2",
                        Group = context.Groups.Find(1)
                    },
                    new Student
                    {
                        Id = 3,
                        FirstName = "First Name 3",
                        LastName = "Last Name 3",
                        Group = context.Groups.Find(2)
                    },
                    new Student
                    {
                        Id = 4,
                        FirstName = "First Name 4",
                        LastName = "Last Name 4",
                        Group = context.Groups.Find(3)
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
