using System;
using System.Collections.Generic;
using System.Xml;

namespace WMNW.GameData.Buildings
{
    public class BuildingList:List<Building>
    {
        #region Constructor

        public BuildingList ()
        {
        }

        public BuildingList ( XmlNode node )
        {
            Load ( node );
        }

        #endregion

        #region Save/Load Logic

        public void Save( XmlWriter wr )
        {
            wr.WriteStartElement ( "Buildings" );
            this.ForEach ( x => x.Save ( wr ) );
            wr.WriteEndElement ();
        }

        public void Load( XmlNode node )
        {
            XmlNode bNode = node [ "Buildings" ];
            XmlNodeList bList = bNode.ChildNodes;
            foreach ( XmlNode lNode in bList )
            {
                Add ( new Building ( lNode ) );
            }
        }

        #endregion
    }
}

