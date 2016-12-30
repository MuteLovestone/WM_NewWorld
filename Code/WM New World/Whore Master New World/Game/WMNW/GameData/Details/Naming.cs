using System;
using System.Xml;

namespace WMNW.GameData.Details
{
    public class Naming
    {
        #region Fields

        private string _fName = "";
        private string _mName = "";
        private string _lName = "";

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>The first name.</value>
        public string FirstName
        {
            get
            {
                return _fName;
            }
            set
            {
                _fName = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the middle.
        /// </summary>
        /// <value>The name of the middle.</value>
        public string MiddleName
        {
            get
            {
                return _mName;
            }
            set
            {
                _mName = value;
            }
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>The last name.</value>
        public string LastName
        {
            get
            {
                return _lName;
            }
            set
            {
                _lName = value;
            }
        }

        #endregion

        #region Construct

        /// <summary>
        /// Initializes a new instance of the <see cref="WMNW.GameData.Details.Naming"/> class.
        /// </summary>
        public Naming ()
        {
            SetName ( "", "", "" );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WMNW.GameData.Details.Naming"/> class.
        /// </summary>
        /// <param name="f">First Name.</param>
        /// <param name="m">Middle Name.</param>
        /// <param name="l">Last Name.</param>
        public Naming ( string f, string m, string l )
        {
            SetName ( f, m, l );
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WMNW.GameData.Details.Naming"/> class.
        /// </summary>
        /// <param name="node">Node to load from.</param>
        public Naming ( XmlNode node )
        {
            Load ( node );
        }

        #endregion

        #region Game Logic

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <returns>The full name.</returns>
        public string GetFullName()
        {
            string name = FirstName;
            if ( MiddleName != "" )
                name += " " + MiddleName;
            if ( LastName != "" )
                name += " " + LastName;
            return name;
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents the current <see cref="WMNW.GameData.Details.Naming"/>.
        /// </summary>
        /// <returns>A <see cref="System.String"/> that represents the current <see cref="WMNW.GameData.Details.Naming"/>.</returns>
        public override string ToString()
        {
            return string.Format ( "FirstName={0}, MiddleName={1}, LastName={2}", FirstName, MiddleName, LastName );
        }

        /// <summary>
        /// Sets the name.
        /// </summary>
        /// <param name="f">First Name.</param>
        /// <param name="m">Middle Name.</param>
        /// <param name="l">Last Name.</param>
        public void SetName( string f, string m, string l )
        {
            _fName = f;
            _mName = m;
            _lName = l;
        }

        #endregion

        #region Save/Load Logic

        /// <summary>
        /// Save the specified wr.
        /// </summary>
        /// <param name="wr">Wr.</param>
        public void Save( XmlWriter wr )
        {
            wr.WriteStartElement ( "Name" );
            wr.WriteElementString ( "First", _fName );
            wr.WriteElementString ( "Middle", _mName );
            wr.WriteElementString ( "Last", _lName );
            wr.WriteEndElement ();
        }

        /// <summary>
        /// Load the specified node.
        /// </summary>
        /// <param name="node">Node.</param>
        public void Load( XmlNode node )
        {
            XmlNode nameNode = node [ "Name" ];
            _fName = nameNode.GetText ();
            _mName = nameNode.GetText ();
            _lName = nameNode.GetText ();
        }

        #endregion
    }
}

