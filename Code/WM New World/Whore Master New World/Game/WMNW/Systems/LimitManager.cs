using System;

namespace WMNW.Systems
{
    public class LimitManager
    {
        public static int LimitBuildingFame( int fame )
        {
            if ( fame < 0 )
                fame = 0;
            if ( fame > 100 )
                fame = 100;
            return fame;
        }

        public static int LimitBuildingFilth( int filth )
        {
            if ( filth > 500 )
                filth = 500;
            if ( filth < -500 )
                filth = -500;
            return filth;
        }

        public static int LimitBuildingHappiness( int hap )
        {
            if ( hap <= 0 )
                hap = 0;
            else if ( hap >= 100 )
                hap = 100;
            return hap;
        }
    }
}

