using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AmikojApi.Models
{
    public partial class CourseModel
    {
        [Key]
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string MyLangCode { get; set; }
        public string LearnLangCode { get; set; }
        public string CourseShortName { get; set; }
        public string CourseFullName { get; set; }
        public int NumOfChapters { get; set; }
        public int Level { get; set; }
    }
}
