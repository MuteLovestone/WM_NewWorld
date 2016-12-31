using System;
using InMan = WMNW.Core.InstanceManager;
using WMNW.Config;
using System.IO;
using System.Xml;
using PathMan = WMNW.Core.PathManager;

namespace WMNW.Systems
{
    public class ConfigManager
    {
        #region Fields

        private static ConfigManager _instance;
        private static ScreenConfig _screen;
        private const string _configPath = "/Content/Config/Settings.Config";
        public const string GameTitle = "Whore Master: New World";

        #endregion

        #region Properties

        public static ScreenConfig Screen
        {
            get
            {
                return _screen;
            }
        }

        #endregion

        #region Construct

        public ConfigManager ()
        {
            InMan.CheckInstance ( _instance, "Config Manager", true );
            _instance = this;
            _screen = new ScreenConfig ();
            if ( ConfigFileFound () )
                LoadConfig ();
            else
                SaveConfig ();
        }

        #endregion

        #region Public Logic

        public static void Save()
        {
            SaveConfig ();
        }

        public static void DefaultAll()
        {
            _screen.Default ();
        }

        #endregion

        #region Private Logic

        private static bool ConfigFileFound()
        {
            string configFile = PathMan.GetPathInGameFolder ( _configPath );
            if ( File.Exists ( configFile ) )
                return true;
            else
                return false;
        }

        private static void LoadConfig()
        {
            string configFile = PathMan.GetPathInGameFolder ( _configPath );
            XmlDocument doc = XmlSettingManager.GetXmlDocforFile ( configFile );
            _screen.Load ( doc [ "Settings" ] );
        }

        private static void SaveConfig()
        {
            string configFile = PathMan.GetPathInGameFolder ( _configPath );
            XmlWriter wr = XmlSettingManager.CreateXmlWriter ( configFile );
            wr.WriteStartElement ( "Settings" );
            _screen.Save ( wr );
            wr.WriteEndElement ();
            wr.Flush ();
            wr.Close ();
        }

        #endregion
    }
}

