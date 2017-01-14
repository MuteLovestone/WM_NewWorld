using System;
using WMNW.Core;
using System.Xml;
using WMNW.Systems;

namespace WMNW
{
    public class GameApplication
    {
        #region Fields

        /// <summary>
        /// The path manager instance.
        /// </summary>
        private static PathManager _pathManagerInstance;
        /// <summary>
        /// The xml setting manager instance.
        /// </summary>
        private static XmlSettingManager _xmlSettingManagerInstance;
        private static CoreGame _coreGameInstance;
        private static LogManager _logManager;

        #endregion

        #region Main Thread Point

        /// <summary>
        /// The entry point of the program, where the program control starts and ends.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        public static void Main( string[] args )
        {
            _pathManagerInstance = new PathManager ();
            _xmlSettingManagerInstance = new XmlSettingManager ();
            _coreGameInstance = new CoreGame ();
            _logManager = new LogManager ();
            #if !DEBUG
            try
            {
                #endif
                _coreGameInstance.Run ();
                #if !DEBUG
            }
            catch ( Exception e )
            {
                LogManager.LogError ( e );
            }
            #endif
            _logManager.SaveLogs ();
            Environment.Exit ( 0 );
        }

        #endregion
    }
}

