﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityMVC.Models
{
    public class Group
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public Course Course { set; get; }
    }
}
