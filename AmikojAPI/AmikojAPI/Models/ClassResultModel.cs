using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AmikojApi.Models
{
    public partial class ClassResultModel
    {
        [Key]
        public int Id { get; set; }
        public string MyLangCode { get; set; }
        public string LearnLangCode { get; set; }
        public int ClassNumber { get; set; }
        public int ChapterNumber { get; set; }
        public int Score { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
    }
}
