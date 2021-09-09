using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AmikojApi.Models
{
    public partial class ClassesModel
    {
        [Key]
        public int Id { get; set; }
        public string MyLangCode { get; set; }
        public string LearnLangCode { get; set; }
        public int ChapterId { get; set; }
        public int ChapterNumber { get; set; }
        public int ClassNumber { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public string Characters { get; set; }
    }
}
