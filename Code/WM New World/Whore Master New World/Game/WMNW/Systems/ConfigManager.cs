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
        private static PathsConfig _paths;
        private static LogConfig _logs;
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

        public static PathsConfig Paths
        {
            get
            {
                return _paths;
            }
        }

        public static LogConfig Debug
        {
            get
            {
                return _logs;
            }
        }

        #endregion

        #region Construct

        public ConfigManager ()
        {
            InMan.CheckInstance ( _instance, "Config Manager", true );
            _instance = this;
            _screen = new ScreenConfig ();
            _paths = new PathsConfig ();

            _logs = new LogConfig ();
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
            _paths.Default ();
            _logs.Reset ();
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
            _paths.Load ( doc [ "Settings" ] );

            _logs.Load ( doc [ "Settings" ] );
            if ( Debug.LogLoad )
                LogManager.LogLoad ( "Settings.Config" );
        }

        private static void SaveConfig()
        {
            string configFile = PathMan.GetPathInGameFolder ( _configPath );
            XmlWriter wr = XmlSettingManager.CreateXmlWriter ( configFile );
            wr.WriteStartElement ( "Settings" );
            _screen.Save ( wr );
            _paths.Save ( wr );

            _logs.Save ( wr );
            wr.WriteEndElement ();
            wr.Flush ();
            wr.Close ();
            if ( Debug.LogLoad )
                LogManager.LogSave ( "Settings.Config" );
        }

        #endregion
    }
}

