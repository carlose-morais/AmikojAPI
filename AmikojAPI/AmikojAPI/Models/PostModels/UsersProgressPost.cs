using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmikojApi.Models
{
    public class UsersProgressPost
    {
        public string UserId { get; set; }
        public string MyLangCode { get; set; }
        public string LearnLangCode { get; set; }
        public int ChapterId { get; set; }
        public int LastCompleteClassId { get; set; }
        public double CurrentScore { get; set; }
        public bool IsComplete { get; set; }
        public int Level { get; set; }
    }
}
