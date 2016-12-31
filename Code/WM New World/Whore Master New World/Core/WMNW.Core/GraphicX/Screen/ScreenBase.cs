using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using WMNW.Core.Input.Devices.Generic;
using WMNW.Core.GUI;

namespace WMNW.Core.GraphicX.Screen
{
    public abstract class ScreenBase : DrawableGameComponent
    {
        #region Field

        #endregion

        #region Properties

        public readonly string Name = null;

        public GameComponentCollection Componenets
        {
            get
            {
                return Game.Components;
            }
        }

        public ContentManager Content
        {
            get
            {
                return GameBase.ContentMan;
            }
        }

        public new GameBase Game
        {
            get;
            set;
        }

        public PcKeyboard Keyboard = null;
        public Gui Gui = null;

        #endregion

        #region Constructor

        protected ScreenBase ( Game game, string name )
            : base ( game )
        {
            Game = ( GameBase )game;
            Name = name;
            Hide ();

            Keyboard = ( PcKeyboard )GameBase.Input.GetKeyboard ();
            Gui = new Gui ();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Event fires when screen is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ScreenChange( object sender, EventArgs e )
        {
            //If this screen is the current screen show it, else hide it
            if ( ScreenHandler.CurrentScreen == this )
                Show ();
            else
                Hide ();

        }

        /// <summary>
        /// Show our Screen while also Enableing it
        /// </summary>
        void Show()
        {
            Visible = true;
            Enabled = true;

            LoadOnShow ();
        }

        /// <summary>
        /// Hide our Screen while also Disableing it
        /// </summary>
        public void Hide()
        {
            Visible = false;
            Enabled = false;

            DisposeOnHide ();
        }

        public new abstract void LoadContent();

        /// <summary>
        /// Method is intended to load anything needed when a screen shows itself
        /// </summary>
        public virtual void LoadOnShow()
        {

        }

        /// <summary>
        /// Method is intended to Dispose uneeded items when a screen hides to help improve performance
        /// </summary>
        public virtual void DisposeOnHide()
        {

        }

        /// <summary>
        /// Method intended to run when Window is Resized
        /// </summary>
        public virtual void OnResize()
        {
        }

        public override void Update( GameTime gameTime )
        {
            Gui.Update ( gameTime );
            base.Update ( gameTime );
        }

        public override void Draw( GameTime gameTime )
        {            

            //Draw Without Camera
            GraphicsHandler.Begin ();
            Gui.Draw ( gameTime );
            GraphicsHandler.End ();
            base.Draw ( gameTime );
        }

        #endregion
    }
}

