using System;
using System.Xml;

namespace WMNW.GameData
{
    public class Player:Master
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Construct

        public Player ()
        {
            _gold = 4000;
        }

        public Player ( XmlNode node )
            : base ( node )
        {
            
        }

        #endregion

        #region Game Logic

        public override bool IsPlayer()
        {
            return true;
        }

        #endregion

        #region Save/Load Logic

        /// <summary>
        /// Load the specified node.
        /// </summary>
        /// <param name="node">Node.</param>
        public override void Load( XmlNode node )
        {
            base.Load ( node );
        }

        /// <summary>
        /// Save the specified wr.
        /// </summary>
        /// <param name="wr">Wr.</param>
        public override void Save( XmlWriter wr )
        {
            base.Save ( wr );
        }

        #endregion
    }
}

