using System;

namespace WMNW.GameData.Buildings
{
    public class Job
    {
        #region Fields

        public string DisplayName = "";
        public string JobString = "";

        #endregion

        public Job ( string dis, string job )
        {
            DisplayName = dis;
            JobString = job;
        }
    }
}

