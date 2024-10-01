using EduPlus.Models;
using System.Collections.Generic;

namespace EduPlus.WebUI.Models
{
    public class TeacherLecturesViewModel
    {
        public Teacher Teacher { get; set; }
        public List<Lecture> AssociatedLectures { get; set; } = new List<Lecture>();
        public List<Lecture> NonAssociatedLectures { get; set; } = new List<Lecture>();
    }
}
