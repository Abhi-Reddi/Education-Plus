using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPlus.Models
{
    public class TeacherLecture
    {
        [Display(Name = "Teacher Id")]
        public int TeacherId { get; set; }

        [Display(Name = "Lecture Id")]
        [Required]
        public int LectureId { get; set; }

        public Teacher Teacher { get; set; } = new Teacher();
        public Lecture Lecture { get; set; } = new Lecture();
    }
}
