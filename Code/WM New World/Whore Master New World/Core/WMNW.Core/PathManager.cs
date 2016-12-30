using System;
using InMan = WMNW.Core.InstanceManager;
using Path = System.IO.Path;
using Assembly = System.Reflection.Assembly;

namespace WMNW.Core
{
    /// <summary>
    /// This class is used to manage the game path and is used for CrossPlatform compatability
    /// </summary>
    public class PathManager
    {
        #region Fields

        /// <summary>
        /// The instance.
        /// </summary>
        private static PathManager _instance;
        /// <summary>
        /// The game path.
        /// </summary>
        private static string _gamePath;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WMNW.Core.PathManager"/> class.
        /// </summary>
        public PathManager ()
        {
            InMan.CheckInstance ( _instance, "Path Manager", true );
            _instance = this;
            _gamePath = Path.GetDirectoryName ( Assembly.GetCallingAssembly ().Location );

        }

        #endregion

        #region Game Logic

        /// <summary>
        /// Gets the path in game folder.
        /// </summary>
        /// <returns>The path in game folder.</returns>
        /// <param name="pathInGameFolder">Path in game folder.</param>
        public static string GetPathInGameFolder( string pathInGameFolder )
        {
            System.IO.Directory.CreateDirectory ( Path.GetDirectoryName ( ReplacePathForSystem ( _gamePath + pathInGameFolder ) ) );
            return ( ReplacePathForSystem ( _gamePath + pathInGameFolder ) );
        }

        /// <summary>
        /// Gets the path outside game folder.
        /// </summary>
        /// <returns>The path outside game folder.</returns>
        /// <param name="path">Path.</param>
        public static string GetPathOutsideGameFolder( string path )
        {
            return ( ReplacePathForSystem ( path ) );
        }

        #endregion

        #region Private Game Logic

        private static string ReplacePathForSystem( string path )
        {
            string sysSep = Path.DirectorySeparatorChar.ToString ();
            string fPath = path.Replace ( @"\\", sysSep );
            fPath = fPath.Replace ( "//", sysSep );
            fPath = fPath.Replace ( @"\", sysSep );
            fPath = fPath.Replace ( "/", sysSep );
            return fPath;
        }

        #endregion
    }
}

