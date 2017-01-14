using System;

using IMan = WMNW.Core.InstanceManager;

namespace WMNW.Systems
{
    public class SelectionManager
    {
        #region Fields

        private static SelectionManager _instance;
        private static int SelectedMaster = 0;
        private static int SelectedBuilding = 0;
        private static int SelectedWorker = 0;

        #endregion

        #region Construct

        public SelectionManager ()
        {
            IMan.CheckInstance ( _instance, "Selection Manager", true );
            _instance = this;
        }

        #endregion

        #region Logic

        public static int SelectedMasterID()
        {
            return SelectedMaster;
        }

        public static void SelectMaster( int id )
        {
            SelectedMaster = id;
        }

        public static int SelectedBuildingID()
        {
            return SelectedBuilding;
        }

        public static void SelectBuilding( int id )
        {
            SelectedBuilding = id;
        }

        public static int SelectedWorkerID()
        {
            return SelectedWorker;
        }

        public static void SelectWorker( int id )
        {
            SelectedBuilding = id;
        }

        #endregion
    }
}

