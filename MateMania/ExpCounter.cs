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
                int gMedals = DbData.nactenyUzivatel.GoldMedals;
                int sMedals = DbData.nactenyUzivatel.SilverMedals;
                int bMedals = DbData.nactenyUzivatel.BronzeMedals;
                experience += gMedals * 200;
                experience += sMedals * 120;
                experience += bMedals * 40;
                return experience;
            }
        }
        public static int Level
        {
            get
            {
                if (Experience >= 0 && Experience <= 2000)
                {
                    return 1;
                }
                else if (Experience > 2000 && Experience <= 4500)
                {
                    return 2;
                }
                else if (Experience > 4500 && Experience <= 7000)
                {
                    return 3;
                }
                else if (Experience > 7000 && Experience <= 10000)
                {
                    return 4;
                }
                else if (Experience > 10000 && Experience <= 15000)
                {
                    return 5;
                }
                else if (Experience > 15000)
                {
                    return 6;
                }
                else
                {
                    return 0;
                }
            }
        }
        public static int[] LevelCaps = { 2000, 4500, 7000, 10000, 15000 };
    }
}
