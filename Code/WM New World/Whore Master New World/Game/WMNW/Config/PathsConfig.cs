using System;
using System.Xml;

namespace WMNW.Config
{
    public class PathsConfig
    {
        #region Fields

        private string _savePath = "";
        private string _workersPath = "";

        #endregion

        #region Properties

        public string SavePath
        {
            get
            {
                return _savePath;
            }
            set
            {
                _savePath = value;
            }
        }

        public string WorkerPath
        {
            get
            {
                return _workersPath;
            }
            set
            {
                _workersPath = value;
            }
        }

        #endregion

        #region Constructor

        public PathsConfig ()
        {
        }

        #endregion

        #region Logic

        public void Default()
        {
            _savePath = @"../Saves/";
            _workersPath = @"../Content/Workers/";
        }

        #endregion

        #region Save/Load Logic

        public void Load( XmlNode node )
        {
            XmlNode pNode = node [ "Paths" ];
            if ( pNode == null )
            {
                Default ();
                return;
            }
            _savePath = pNode [ "SavePath" ].InnerText;
            _workersPath = pNode [ "WorkersPath" ].InnerText;
            if ( SavePath == " " || SavePath == "" )
                _savePath = @"../Saves/";
            if ( _workersPath == "" || _workersPath == " " )
                _workersPath = @"../Content/Workers/";
        }

        public void Save( XmlWriter wr )
        {
            wr.WriteStartElement ( "Paths" );
            wr.WriteElementString ( "SavePath", _savePath );
            wr.WriteElementString ( "WorkersPath", _workersPath );
            wr.WriteEndElement ();
        }

        #endregion
    }
}

