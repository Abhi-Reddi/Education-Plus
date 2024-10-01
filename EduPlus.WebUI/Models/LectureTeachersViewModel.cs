using EduPlus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduPlus.WebUI.Models
{
    public class LectureTeachersViewModel
    {
        public Lecture Lecture { get; set; } 
        public List<Teacher> AssociatedTeachers { get; set; } = new List<Teacher>();
        public List<Teacher> NonAssociatedTeachers { get; set; } = new List<Teacher>();
    }
}