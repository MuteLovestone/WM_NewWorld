using System;
using WMNW.GameData.Details;
using System.Xml;

namespace WMNW.GameData
{
    public class CharacterBase
    {
        #region Fields

        private Naming _name = new Naming ();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName
        {
            get
            {
                return _name.FirstName;
            }
        }

        /// <summary>
        /// Gets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        public string MiddleName
        {
            get
            {
                return _name.MiddleName;
            }
        }

        /// <summary>
        /// Gets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName
        {
            get
            {
                return _name.LastName;
            }
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName
        {
            get
            {
                return _name.GetFullName ();
            }
        }

        #endregion

        #region Construct

        public CharacterBase ()
        {
            _name = new Naming ();
        }

        public CharacterBase ( XmlNode node )
        {
            Load ( node );
        }

        #endregion

        #region Game Logic

        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="f">First Name.</param>
        /// <param name="m">Middle Name.</param>
        /// <param name="l">Last Name.</param>
        public void SetName( string f, string m, string l )
        {
            _name.SetName ( f, m, l );
        }

        #endregion

        #region Save/Load Logic

        /// <summary>
        /// Save the specified wr.
        /// </summary>
        /// <param name="wr">Wr.</param>
        public virtual void Save( XmlWriter wr )
        {
            _name.Save ( wr );
        }

        /// <summary>
        /// Load the specified node.
        /// </summary>
        /// <param name="node">Node.</param>
        public virtual void Load( XmlNode node )
        {
            _name.Load ( node );
        }

        #endregion
    }
}

