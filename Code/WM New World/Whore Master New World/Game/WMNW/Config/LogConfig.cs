using System;
using System.Xml;

namespace WMNW.Config
{
    public class LogConfig
    {
        #region Fields

        private bool _all;
        private bool _showNumbers;
        private bool _save;
        private bool _load;

        #endregion

        #region Properties

        public bool LogAll
        {
            get
            {
                return _all;
            }
            set
            {
                _all = value;
            }
        }

        public bool ShowNumbers
        {
            get
            {
                if ( LogAll )
                    return true;
                else
                    return _showNumbers;
            }
            set
            {
                _showNumbers = value;
            }
        }

        public bool LogSave
        {
            get
            {
                if ( LogAll )
                    return true;
                return _save;
            }
            set
            {
                _save = value;
            }
        }

        public bool LogLoad
        {
            get
            {
                if ( LogAll )
                    return true;
                return _load;
            }
            set
            {
                _load = value;
            }
        }

        #endregion

        #region Construct

        public LogConfig ()
        {
        }

        public LogConfig ( XmlNode node )
        {
            Load ( node );
        }

        #endregion

        #region Logic

        public void Reset()
        {
            _all = false;
            _showNumbers = false;
            _save = false;
            _load = false;
        }

        #endregion

        #region Save/Load Logic

        public void Load( XmlNode node )
        {
            XmlNode lNode = node [ "Debug" ];
            if ( lNode == null )
            {
                Reset ();
                return;
            }
            _all = lNode [ "LogAll" ].ConvertToBool ();
            _showNumbers = lNode [ "LogAll" ].ConvertToBool ();
            _save = lNode [ "LogSave" ].ConvertToBool ();
            _load = lNode [ "LogLoad" ].ConvertToBool ();
        }

        public void Save( XmlWriter wr )
        {
            wr.WriteStartElement ( "Debug" );
            wr.WriteElementString ( "LogAll", _all );
            wr.WriteElementString ( "ShowNumbers", _showNumbers );
            wr.WriteElementString ( "LogSaves", _save );
            wr.WriteElementString ( "LogLoad", _load );
            wr.WriteEndElement ();
        }

        #endregion
    }
}

