using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AmikojApi.Models
{
    public partial class ChapterModel
    {
        [Key]
        public int Id { get; set; }
        public int ChapterNumber { get; set; }
        public int NoOfClasses { get; set; }
        public string MyLangCode { get; set; }
        public string LearnLangCode { get; set; }
        public string ChapterName { get; set; }
        public string ChapterDescription { get; set; }
        public string TranslateName { get; set; }
        public string TranslateDescription { get; set; }
    }
}
