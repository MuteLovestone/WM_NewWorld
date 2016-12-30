using System;
using Microsoft.Xna.Framework;
using InMan = WMNW.Core.InstanceManager;

namespace WMNW
{
    public class CoreGame:Game
    {
        #region Fields

        private GraphicsDeviceManager _graphics;
        private static CoreGame _instance;

        #endregion

        #region Construct

        public CoreGame ()
        {
            InMan.CheckInstance ( _instance, "Core Game", true );
            _instance = this;
            _graphics = new GraphicsDeviceManager ( this );
            Content.RootDirectory = "Content";
        }

        #endregion

        #region Public Game Logic

        public static CoreGame GetInstance()
        {
            InMan.CheckInstance ( _instance, "Core Game" );
            return _instance;
        }

        #endregion

        #region XNA Impliment

        protected override void Draw( GameTime gameTime )
        {
            GraphicsDevice.Clear ( Color.CornflowerBlue );
            base.Draw ( gameTime );
        }

        protected override void Update( GameTime gameTime )
        {
            base.Update ( gameTime );
        }

        protected override void LoadContent()
        {
            base.LoadContent ();
        }

        #endregion

        #region Events Logic

        protected override void OnExiting( object sender, EventArgs args )
        {
            base.OnExiting ( sender, args );
            //Environment.Exit ( 0 );
        }

        #endregion
    }
}

