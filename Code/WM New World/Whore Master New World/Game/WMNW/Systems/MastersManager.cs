using System;
using WMNW.GameData;
using System.Collections.Generic;
using System.Xml;
using InMan = WMNW.Core.InstanceManager;

namespace WMNW.Systems
{
    public class MastersManager
    {
        #region Fields

        /// <summary>
        /// The instance.
        /// </summary>
        private static MastersManager _instance;
        /// <summary>
        /// The masters list.
        /// </summary>
        private static List<Master> _mastersList;

        #endregion

        #region Properties

        #endregion

        #region Construct

        /// <summary>
        /// Initializes a new instance of the <see cref="WMNW.Systems.MastersManager"/> class.
        /// </summary>
        public MastersManager ()
        {
            InMan.CheckInstance ( _instance, "Masters Manager", true );
            _instance = this;
            _mastersList = new List<Master> ();
            //TASK: Document
        }

        #endregion

        #region Game Logic

        /// <summary>
        /// Gets the player.
        /// </summary>
        /// <returns>The player.</returns>
        public static Player GetPlayer()
        {
            return _mastersList [ 0 ];
        }

        /// <summary>
        /// Gets the master.
        /// </summary>
        /// <returns>The master.</returns>
        /// <param name="id">Identifier.</param>
        public static Master GetMaster( int id )
        {
            if ( id >= _mastersList.Count - 1 )
                return null;
            if ( id <= -1 )
                return null;
            if ( id == 0 )
                return GetPlayer ();
            else
                return _mastersList [ id ];
        }

        /// <summary>
        /// Adds the master.
        /// </summary>
        /// <param name="masterToAdd">Master to add.</param>
        public static void AddMaster( Master masterToAdd )
        {
            masterToAdd.MasterID = _mastersList.Count;
            _mastersList.Add ( masterToAdd );
        }

        /// <summary>
        /// Adds the player.
        /// </summary>
        /// <param name="playerToAdd">Player to add.</param>
        public static void AddPlayer( Player playerToAdd )
        {
            playerToAdd.MasterID = _mastersList.Count;
            _mastersList.Add ( playerToAdd );
        }

        /// <summary>
        /// Reset this instance.
        /// </summary>
        public static void Reset()
        {
            _mastersList.Clear ();
            AddPlayer ( new Player () );
        }

        #endregion

        #region Save/Load Logic

        /// <summary>
        /// Load the specified node.
        /// </summary>
        /// <param name="node">Node.</param>
        public static void Load( XmlNode node )
        {
            XmlNode playerNode = node [ "Player" ];
            AddPlayer ( new Player ( playerNode ) );
            XmlNodeList rivalNodes = node [ "Rivals" ].ChildNodes;
            foreach ( XmlNode rivalNode in rivalNodes )
            {
                AddMaster ( new Master ( rivalNode ) );
            }
        }

        /// <summary>
        /// Save the specified wr.
        /// </summary>
        /// <param name="wr">Wr.</param>
        public static void Save( XmlWriter wr )
        {
            wr.WriteStartElement ( "Masters" );
            wr.WriteStartElement ( "Player" );
            GetPlayer ().Save ( wr );
            wr.WriteEndElement ();
            wr.WriteStartElement ( "Rivals" );
            for ( int i = 1; i < _mastersList.Count; i++ )
            {
                wr.WriteStartElement ( "Rival" );
                _mastersList [ i ].Save ( wr );
                wr.WriteEndElement ();
            }
            wr.WriteEndElement ();
            wr.WriteEndElement ();
        }

        #endregion
    }
}

