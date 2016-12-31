using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using WMNW.Core.Input.Manager;
using WMNW.Core.GraphicX;
using WMNW.Core.GraphicX.Screen;

namespace WMNW.Core
{
    public class GameBase:Game
    {
        #region Fields

        protected GraphicsDeviceManager _graphics;
        private static InputManager _inputManager;
        private static ContentManager _contentMan;
        private static ScreenHandler _screenHandler;

        #endregion

        #region Properties

        public const int DrawOrderStart = 0;
        public const int DrawOrderIncrease = 50;
        public const int ReloadTimer = 5000;

        public static InputManager Input
        {
            get
            {
                return _inputManager;
            }
        }

        public static ContentManager ContentMan
        {
            get
            {
                return _contentMan;
            }
        }

        #endregion

        #region Construct

        public GameBase ()
        {
            _graphics = new GraphicsDeviceManager ( this );
            Content.RootDirectory = "Content";
            _contentMan = Content;
        }

        #endregion

        #region XNA Logic

        protected override void Draw( GameTime gameTime )
        {
            base.Draw ( gameTime );
            _screenHandler.Draw ( gameTime );
        }

        protected override void Update( GameTime gameTime )
        {
            base.Update ( gameTime );
            _inputManager.Update ( gameTime );
            _screenHandler.Update ( gameTime );
        }

        protected override void LoadContent()
        {
            base.LoadContent ();
        }

        protected override void Initialize()
        {
            base.Initialize ();
            _inputManager = new InputManager ( Services, false );
            GraphicsHandler.Initialize ( GraphicsDevice, Content );
            _screenHandler = new ScreenHandler ( this );
        }

        protected override void OnExiting( object sender, EventArgs args )
        {
            base.OnExiting ( sender, args );
            base.Exit ();
        }

        #endregion
    }
}

