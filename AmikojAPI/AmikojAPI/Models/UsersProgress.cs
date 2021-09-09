using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace AmikojApi.Models
{
    public partial class UsersProgress
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Key]
        public string UserId { get; set; }
        public string MyLangCode { get; set; }
        public string LearnLangCode { get; set; }
        public int ChapterNumber { get; set; }
        public int LastCompleteClassNumber { get; set; }
        public double CurrentScore { get; set; }
        public bool IsComplete { get; set; }
        public int Level { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
