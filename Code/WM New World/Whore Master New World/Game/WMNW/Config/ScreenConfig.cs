using System;
using System.Xml;

namespace WMNW.Config
{
    public class ScreenConfig
    {
        #region Fields

        private int _sWidth = 1024;
        private int _sHeight = 768;
        private bool _sFullScreen = false;

        #endregion

        #region Properties

        public int Width
        {
            get
            {
                return _sWidth;
            }
            set
            {
                _sWidth = value;
            }
        }

        public int Height
        {
            get
            {
                return _sHeight;
            }
            set
            {
                _sHeight = value;
            }
        }

        public bool FullScreen
        {
            get
            {
                return _sFullScreen;
            }
            set
            {
                _sFullScreen = value;
            }
        }

        #endregion

        #region Construct

        public ScreenConfig ()
        {
            Default ();
        }

        public ScreenConfig ( XmlNode node )
        {
            Load ( node );
        }

        #endregion

        #region Game Logic

        public void Default()
        {
            _sWidth = 1024;
            _sHeight = 768;
            _sFullScreen = false;
        }

        #endregion

        #region Save/Load Logic

        public void Save( XmlWriter wr )
        {
            wr.WriteStartElement ( "Screen" );
            wr.WriteElementString ( "Height", _sHeight );
            wr.WriteElementString ( "Width", _sWidth );
            wr.WriteElementString ( "FullScreen", _sFullScreen.ToString ().ToLower () );
            wr.WriteEndElement ();
        }

        public void Load( XmlNode node )
        {
            XmlNode screenNode = node [ "Screen" ];
            if ( screenNode == null )
            {
                Default ();
                return;
            }
            else
            {
                _sHeight = screenNode [ "Height" ].ConverToInt ();
                _sWidth = screenNode [ "Width" ].ConverToInt ();
                _sFullScreen = screenNode [ "FullScreen" ].ConvertToBool ();
            }
        }

        #endregion
    }
}

