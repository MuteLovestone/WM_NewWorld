using System;
using System.Xml;
using System.Collections.Generic;
using WMNW.GameData.Buildings;
using cfg = WMNW.Systems.ConfigManager;

namespace WMNW.GameData
{
    public class Master:CharacterBase
    {
        #region Fields

        private int _masterID = -1;
        private BuildingList buildings = new BuildingList ();

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

        public Master ( XmlNode node )
            : base ( node )
        {
            
        }

        #endregion

        #region Game Logic

        public virtual bool IsPlayer()
        {
            return false;
        }

        public void AddBuilding( Building b )
        {
            buildings.Add ( b );
        }

        public int GetBuildingsTotal()
        {
            return buildings.Count;
        }

        public Building GetBuilding( int id )
        {
            if ( id >= GetBuildingsTotal () )
                return null;
            else
                return buildings [ id ];
        }

        public int TotalDisposition()
        {
            int dis = 0;
            buildings.ForEach ( x => dis += x.Disposition );
            return dis;
        }

        public int TotalSuspicion()
        {
            int sus = 0;
            buildings.ForEach ( x => sus += x.Suspicion );
            return sus;
        }

        #endregion

        #region Static Logic

        public static string GetDispostionText( int disp )
        {
            string disText = "";
            if ( disp >= 100 )
                disText = "Saint";
            else if ( disp >= 80 )
                disText = "Benevolent";
            else if ( disp >= 50 )
                disText = "Nice";
            else if ( disp >= 10 )
                disText = "Pleasant";
            else if ( disp >= -10 )
                disText = "Neutral";
            else if ( disp >= -50 )
                disText = "Not Nice";
            else if ( disp >= -80 )
                disText = "Mean";
            else
                disText = "Evil";
            if ( cfg.Debug.ShowNumbers )
                disText += " ( " + disp + " ) ";
            return disText;
            
        }

        public static string GetSuspicionText( int susp )
        {
            string s = "";
            if ( susp >= 80 )
                s = "Town Scum";
            else if ( susp >= 50 )
                s = "Miscreant";
            else if ( susp >= 10 )
                s = "Suspect";
            else if ( susp >= -10 )
                s = "Unsuspected";
            else if ( susp >= -50 )
                s = "Lawful";
            else if ( susp >= -80 )
                s = "Philanthropist";
            else
                s = "Town Hero";
            if ( cfg.Debug.ShowNumbers )
                s += " ( " + susp + " ) ";
            return s;
        }

        #endregion

        #region Save/Load Logic

        public override void Save( System.Xml.XmlWriter wr )
        {
            base.Save ( wr );
            wr.WriteElementString ( "MasterID", _masterID );
            buildings.Save ( wr );
        }

        public override void Load( System.Xml.XmlNode node )
        {
            base.Load ( node );
            _masterID = node [ "MasterID" ].ConverToInt ();
            buildings.Load ( node );
        }

        #endregion
    }
}

