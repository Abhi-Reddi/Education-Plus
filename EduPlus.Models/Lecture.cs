using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPlus.Models
{
    public class Lecture
    {
        [Display(Name = "Lecture Id")]
        public int LectureId { get; set; }

        [Required]
        public string Subject { get; set; } = string.Empty;

        [Required]
        [Range(60, int.MaxValue, ErrorMessage = "Minimum minutes should be at least 60.")]
        public int MinMinutes { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Max minutes should be greater or equal to minimum minutes.")]
        public int MaxMinutes { get; set; }
    }
}
