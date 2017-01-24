using System;
using System.Xml;
using System.IO;
using iMan = WMNW.Core.InstanceManager;
using pMan = WMNW.Core.PathManager;
using cMan = WMNW.Systems.ConfigManager;

namespace WMNW.Systems
{
    public class SaveManager
    {
        #region Fields

        private static SaveManager _instance;

        #endregion

        #region Properties

        #endregion

        #region Construct

        public SaveManager ()
        {
            iMan.CheckInstance ( _instance, "Save Manager", true );
            _instance = this;

        }

        #endregion

        #region Logic

        public static int SaveCount()
        {
            string savePath = GetSavesPath ();
            string[] saves = Directory.GetFiles ( savePath, "*.NWSave", SearchOption.TopDirectoryOnly );
            return saves.Length;
        }

        public static string GetSaveHeader( string saveName )
        {
            
            string header = "Save: \n";
            header += "Player Name: \n";
            header += "(MockUp) \n";
            header += "Current Gold: \n";
            header += "(MockUp) \n";
            header += "Total Workers: \n";
            header += "(Mock Up) \n";
            header += "Total Buildings: \n";
            header += "(MockUp) \n";
            return header;
        }

        public static void Save()
        {
            string fullpath = GetSavesPath () + MastersManager.GetPlayer ().FullName + ".NWSave";
            XmlWriter wr = XmlSettingManager.CreateXmlWriter ( fullpath );
            wr.WriteStartElement ( "Save" );
            MastersManager.Save ( wr );
            wr.WriteEndElement ();
            wr.Flush ();
            wr.Close ();
            if ( ConfigManager.Debug.LogLoad )
                LogManager.LogSave ( MastersManager.GetPlayer ().FullName + ".NWSave" );
        }

        public static void Load( string filename )
        {
            string fullPath = GetSavesPath () + filename + ".NWSave";
            try
            {
                XmlDocument doc = XmlSettingManager.GetXmlDocforFile ( fullPath );
                MastersManager.Load ( doc [ "Save" ] );
                if ( ConfigManager.Debug.LogLoad )
                    LogManager.LogLoad ( filename + ".NWSave" );
            }
            catch ( Exception e )
            {
                LogManager.LogError ( e );
            }
        }

        #endregion

        #region Private Logic

        private static string GetSavesPath()
        {
            string path = pMan.GetFilePath ( cMan.Paths.SavePath );
            LogManager.LogLoad ( path );
            return path;
        }

        #endregion
    }
}

