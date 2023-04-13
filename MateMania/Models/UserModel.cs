using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateMania.Models
{
    public class UserBase
    {
        public string Nickname { get; set; }
        public string UserPassword { get; set; }
    }
    public class UserModel : UserBase
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public int? ChosenClassID { get; set; }
        public int IsTeacher { get; set; }
        public int BronzeMedals { get; set; }
        public int SilverMedals { get; set; }
        public int GoldMedals { get; set; }
        public int Avatar { get; set; }
        public int Title1 { get; set; }
        public int Title2 { get; set; }
        public int Title3 { get; set; }
        public int Title4 { get; set; }
        public int Title5 { get; set; }
        public int Title6 { get; set; }
        public int ChosenTitle { get; set; }
    }
    public static class OfflineOnline
    {
        public static bool stavPripojeni = false;
    }
}
