using System;
using Microsoft.Xna.Framework;
using InMan = WMNW.Core.InstanceManager;

namespace WMNW
{
    //TASK: Add Sulex engine stuff to WMNW.Core
    public class CoreGame:Game
    {
        #region Fields

        /// <summary>
        /// The graphics.
        /// </summary>
        private GraphicsDeviceManager _graphics;
        /// <summary>
        /// The instance.
        /// </summary>
        private static CoreGame _instance;

        #endregion

        #region Construct

        /// <summary>
        /// Initializes a new instance of the <see cref="WMNW.CoreGame"/> class.
        /// </summary>
        public CoreGame ()
        {
            InMan.CheckInstance ( _instance, "Core Game", true );
            _instance = this;
            _graphics = new GraphicsDeviceManager ( this );
            Content.RootDirectory = "Content";
        }

        #endregion

        #region Public Game Logic

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>The instance.</returns>
        public static CoreGame GetInstance()
        {
            InMan.CheckInstance ( _instance, "Core Game" );
            return _instance;
        }

        #endregion

        #region XNA Impliment

        /// <summary>
        /// Draw the specified gameTime.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear ( Color.CornflowerBlue );
            base.Draw ( gameTime );
        }

        /// <summary>
        /// Update the specified gameTime.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        protected override void Update( GameTime gameTime )
        {
            base.Update ( gameTime );
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent ();
        }

        #endregion

        #region Events Logic

        /// <summary>
        /// Raises the exiting event.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="args">Arguments.</param>
        protected override void OnExiting( object sender, EventArgs args )
        {
            base.OnExiting ( sender, args );
            //Environment.Exit ( 0 );
        }

        #endregion
    }
}

