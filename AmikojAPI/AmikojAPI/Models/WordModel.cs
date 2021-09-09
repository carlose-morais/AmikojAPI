using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AmikojApi.Models
{
    public partial class WordModel
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ChapterNumber { get; set; }
        public int ClassNumber { get; set; }
        public string MyLangCode { get; set; }
        public string LearnLangCode { get; set; }
        public string Word { get; set; }
        public string Translation { get; set; }
        public string Tips { get; set; }
        public bool IsWord { get; set; }
    }
}
