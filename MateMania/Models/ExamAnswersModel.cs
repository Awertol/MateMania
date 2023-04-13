using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateMania.Models
{
    public class ExamAnswersModel
    {
        public int AnswerID { get; set; }
        public int UserID { get; set; }
        public int ExamID { get; set; }
        public int Result { get; set; }
        public int MaxPossible { get; set; }
    }
}
