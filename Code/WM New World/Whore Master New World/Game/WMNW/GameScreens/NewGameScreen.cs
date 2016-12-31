using System;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;
using WMNW.Core.GraphicX.Screen;
using WMNW.Core.GUI.Controls;

namespace WMNW.GameScreens
{
    public class NewGameScreen:ScreenBase
    {
        #region Fields

        private ScreenBack _background;
        private Button _backButton;
        private Button _acceptButton;

        #endregion

        #region Construct

        public NewGameScreen ()
            : base ( CoreGame.GetInstance (), "NewGame" )
        {
        }

        #endregion

        #region XNA Logic

        public override void DisposeOnHide()
        {
            base.DisposeOnHide ();
            _background = null;
            _backButton = null;
            _acceptButton = null;
        }

        public override void LoadOnShow()
        {
            base.LoadOnShow ();
            #region Set Up Variables
            Vector2 backgroundSize = new Vector2 ( Systems.ConfigManager.Screen.Width - 2, Systems.ConfigManager.Screen.Height );
            Color BC = Color.Gold;
            Color BG = Color.Black;
            int posx = 0;
            int posy = 0;
            Color TC = Color.White;
            string font = "UIFont";
            Vector2 bSize = new Vector2 ( 150, 50 );
            #endregion
            #region Background
            _background = new ScreenBack ( backgroundSize );
            _background.ChangeColor ( BC, BG );
            Gui.Add ( _background );
            #endregion
            #region Back Button
            posx += 4;
            posy = ( int )backgroundSize.Y - 54;
            _backButton = new Button ( font, "Back", TC, new Vector2 ( posx, posy ), bSize );
            _backButton.ChangeColor ( BC, BG );
            _backButton.MouseClicked += BackButtonClicked;
            Gui.Add ( _backButton );
            #endregion
            #region Accept Button
            posx = ( int )backgroundSize.X - ( int )bSize.X - 3;
            _acceptButton = new Button ( font, "Start New Life", TC, new Vector2 ( posx, posy ), bSize );
            _acceptButton.ChangeColor ( BC, BG );
            _acceptButton.MouseClicked += AcceptButtonClicked;
            Gui.Add ( _acceptButton );
            #endregion
        }

        public override void LoadContent()
        {
            
        }

        #endregion

        #region Control Events and Private Logic

        private void BackButtonClicked( object sender, WMMouseEventArgs e )
        {
            ScreenHandler.Change ( "MainMenu" );
        }

        private void AcceptButtonClicked( object sender, WMMouseEventArgs e )
        {
            ScreenHandler.Change ( "Building" );
        }

        #endregion
    }
}

