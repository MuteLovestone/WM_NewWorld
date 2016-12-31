using System;
using WMNW.Core;
using System.Xml;

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
            try
            {
                _coreGameInstance.Run ();
            }
            catch ( Exception e )
            {
                System.IO.File.WriteAllText ( PathManager.GetPathInGameFolder ( "/Logs/ErrorLog.txt" ), e.ToString () );
                Environment.Exit ( 0 );
            }
            System.IO.File.WriteAllText ( PathManager.GetPathInGameFolder ( "/Logs/ErrorLog.txt" ), "Game Finished without Errors" );
            Environment.Exit ( 0 );
        }

        #endregion
    }
}

