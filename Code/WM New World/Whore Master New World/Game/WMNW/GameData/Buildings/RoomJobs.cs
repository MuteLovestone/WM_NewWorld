using System;
using System.Collections.Generic;

namespace WMNW.GameData.Buildings
{
    public class RoomJobs
    {
        #region Fields

        public List<Job> Jobs = new List<Job> ();
        public string DisplayName = "";

        #endregion

        public RoomJobs ( string displayName )
        {
            DisplayName = displayName;
        }
    }
}

