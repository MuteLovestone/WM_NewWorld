using System;

namespace WMNW.GameData
{
    public class Master:CharacterBase
    {
        #region Fields

        private int _masterID = -1;

        #endregion

        #region Properties

        public int MasterID
        {
            get
            {
                return _masterID;
            }
            set
            {
                _masterID = value;
            }
        }

        #endregion

        #region Construct

        public Master ()
        {
        }

        #endregion

        #region Game Logic

        public virtual bool IsPlayer()
        {
            return false;
        }

        #endregion

        #region Save/Load Logic

        public override void Save( System.Xml.XmlWriter wr )
        {
            base.Save ( wr );
        }

        public override void Load( System.Xml.XmlNode node )
        {
            base.Load ( node );
        }

        #endregion
    }
}

