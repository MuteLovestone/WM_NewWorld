using System;
using WMNW.GameData.Details;
using System.Xml;

namespace WMNW.GameData
{
    public class CharacterBase
    {
        #region Fields

        private Naming _name = new Naming ();
        private Genders _gender = Genders.None;
        private bool _isSlave = false;
        protected long _gold = 0;

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

        /// <summary>
        /// Gets the gender.
        /// </summary>
        /// <value>The gender.</value>
        public Genders Gender
        {
            get
            {
                return _gender;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is free.
        /// </summary>
        /// <value><c>true</c> if this instance is free; otherwise, <c>false</c>.</value>
        public bool IsFree
        {
            get
            {
                return !_isSlave;
            }
        }

        public long Gold
        {
            get
            {
                return _gold;
            }
            set
            {
                _gold = value;
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

        /// <summary>
        /// Sets the gender.
        /// </summary>
        /// <param name="newGender">New gender.</param>
        public void SetGender( Genders newGender )
        {
            _gender = newGender;
        }

        /// <summary>
        /// Sets the slave.
        /// </summary>
        public void SetSlave()
        {
            _isSlave = true;
        }

        /// <summary>
        /// Sets the free.
        /// </summary>
        public void SetFree()
        {
            _isSlave = false;
        }

        public bool HasVagina()
        {
            if ( _gender == Genders.Female ||
                 _gender == Genders.FemaleNeut ||
                 _gender == Genders.Futa ||
                 _gender == Genders.FutaNeut ||
                 _gender == Genders.FutaFull ||
                 _gender == Genders.HermFull ||
                 _gender == Genders.HermNeut ||
                 _gender == Genders.Herm )
                return true;
            else
                return false;
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
            wr.WriteElementString ( "Gender", ( int )_gender );
            wr.WriteElementString ( "Gold", _gold );
            wr.WriteElementString ( "IsSlave", _isSlave );
        }

        /// <summary>
        /// Load the specified node.
        /// </summary>
        /// <param name="node">Node.</param>
        public virtual void Load( XmlNode node )
        {
            _name.Load ( node );
            _gender = ( Genders )node [ "Gender" ].ConverToInt ();
            _gold = node [ "Gold" ].ConvertToLong ();
            _isSlave = node [ "IsSlave" ].ConvertToBool ();
        }

        #endregion
    }
}

