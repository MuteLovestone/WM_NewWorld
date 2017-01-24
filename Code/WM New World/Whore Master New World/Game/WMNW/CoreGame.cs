using System;
using System.Threading;
using Microsoft.Xna.Framework;
using InMan = WMNW.Core.InstanceManager;
using WMNW.Systems;
using WMNW.Core.GraphicX.Screen;
using WMNW.GameScreens;

namespace WMNW
{
    public sealed class CoreGame:WMNW.Core.GameBase
    {
        #region Fields

        /// <summary>
        /// The instance.
        /// </summary>
        private static CoreGame _instance;

        private static ConfigManager _configManager;
        private static SaveManager _saveManager;
        private static OverlayManager _overlayManager;
        private static SelectionManager _selectionManager;

        #endregion

        #region Construct

        /// <summary>
        /// Initializes a new instance of the <see cref="WMNW.CoreGame"/> class.
        /// </summary>
        public CoreGame ()
        {
            InMan.CheckInstance ( _instance, "Core Game", true );
            _instance = this;
            _configManager = new ConfigManager ();
            IsMouseVisible = true;
            Window.Title = ConfigManager.GameTitle;

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
            _overlayManager.Draw ( gameTime );
        }

        /// <summary>
        /// Update the specified gameTime.
        /// </summary>
        /// <param name="gameTime">Game time.</param>
        protected override void Update( GameTime gameTime )
        {
            base.Update ( gameTime );
            _overlayManager.Update ( gameTime );
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        protected override void LoadContent()
        {
            base.LoadContent ();
        }

        protected override void Initialize()
        {
            base.Initialize ();
            _saveManager = new SaveManager ();
            _overlayManager = new OverlayManager ();
            _selectionManager = new SelectionManager ();
            UpdateScreenSize ();
            InitalizeScreens ();
        }

        #endregion

        #region Private Logic

        public void NotYetImplimentedMessage()
        {
            OverlayManager.ShowMessage ( "This Feature is not yet Implimented.", true );
        }

        public void UpdateScreenSize()
        {
            _graphics.IsFullScreen = ConfigManager.Screen.FullScreen;
            _graphics.PreferredBackBufferHeight = ConfigManager.Screen.Height;
            _graphics.PreferredBackBufferWidth = ConfigManager.Screen.Width;
            _graphics.ApplyChanges ();
        }

        public void InitalizeScreens()
        {
            ScreenHandler.Add ( new LoadingScreen ( this ) );
            ChangeToStartingScreen ();
        }

        private void ChangeToStartingScreen()
        {
            MastersManager.Reset ();
            ScreenHandler.Change ( "Loading" );
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
            ConfigManager.Save ();
            _overlayManager.Dispose ();
            base.Exit ();

        }

        #endregion
    }
}

