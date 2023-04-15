using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MateMania
{
    public static class ExpCounter
    {
        public static int Experience
        {
            get
            {
                int experience = 0;
                experience += DbData.nactenyUzivatel.GoldMedals * 200;
                experience += DbData.nactenyUzivatel.SilverMedals * 120;
                experience += DbData.nactenyUzivatel.BronzeMedals * 40;
                return experience;
            }
        }
        public static int[] LevelCaps = { 2000, 4500, 7000, 10000, 15000 };
        public static int Level
        {
            get
            {
                if (Experience >= 0 && Experience <= LevelCaps[0]) { return 1; }
                else if (Experience > LevelCaps[0] && Experience <= LevelCaps[1]) { return 2; }
                else if (Experience > LevelCaps[1] && Experience <= LevelCaps[2]) { return 3; }
                else if (Experience > LevelCaps[2] && Experience <= LevelCaps[3]) { return 4; }
                else if (Experience > LevelCaps[3] && Experience <= LevelCaps[4]) { return 5; }
                else if (Experience > LevelCaps[4]) { return 6; }
                else { return 0; }
            }
        }
    }
}
