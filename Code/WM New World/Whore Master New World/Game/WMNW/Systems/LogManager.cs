using System;
using System.IO;
using System.Collections.Generic;
using WMNW.Core;

namespace WMNW.Systems
{
    public class LogManager
    {
        #region Fields

        private static LogManager _instance;
        private static List<string> _loadLog = new List<string> ();
        private static List<string> _saveLog = new List<string> ();
        private static List<string> _mainLog = new List<string> ();
        private static List<string> _errorLog = new List<string> ();

        #endregion

        #region Construct

        public LogManager ()
        {
            InstanceManager.CheckInstance ( _instance, "Log Manager", true );
            _instance = this;
        }

        #endregion

        #region Public Logic

        public static void LogLoad( string fileName )
        {
            _loadLog.Add ( "File: " + fileName + " Loaded Successfully." );
        }

        public static void LogSave( string fileName )
        {
            _saveLog.Add ( "File: " + fileName + " Saved Successfully." );
        }

        public static void LogMain( string message )
        {
            _mainLog.Add ( message );
        }

        public static void LogError( Exception e )
        {
            _errorLog.Add ( "_____________________________________________" );
            _errorLog.Add ( e.Message );
            _errorLog.Add ( "\tTrace Log:" );
            _errorLog.Add ( e.StackTrace );
        }

        public void SaveLogs()
        {
            SaveLoadLog ();
            SaveSaveLog ();
            SaveMainLog ();
            SaveErrorLog ();
        }

        #endregion

        #region Private Logic

        private void SaveLoadLog()
        {
            string fullPath = PathManager.GetFilePath ( "../Logs/" ) + "File Load Log.txt";
            File.WriteAllLines ( fullPath, _loadLog.ToArray () );
        }

        private void SaveSaveLog()
        {
            string fullPath = PathManager.GetFilePath ( "../Logs/" ) + "File Save Log.txt";
            File.WriteAllLines ( fullPath, _saveLog.ToArray () );
        }

        private void SaveMainLog()
        {
            string fullPath = PathManager.GetFilePath ( "../Logs/" ) + "Game Log.txt";
            File.WriteAllLines ( fullPath, _mainLog.ToArray () );
        }

        private void SaveErrorLog()
        {
            string fullPath = PathManager.GetFilePath ( "../Logs/" ) + "Error Log.txt";
            File.WriteAllLines ( fullPath, _errorLog.ToArray () );
        }

        #endregion
    }
}

