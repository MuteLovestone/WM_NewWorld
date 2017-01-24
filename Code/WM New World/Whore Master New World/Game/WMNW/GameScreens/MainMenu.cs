using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX.Screen;
using WMNW.Core.GUI.Controls;
using WMNW.Systems;

namespace WMNW.GameScreens
{
    public sealed class MainMenu:ScreenBase
    {
        #region Fields

        ScreenBack _backgroundControl;
        Button _newGameButton;
        Button _loadGameButton;
        Button _optionsButton;
        Button _exitButton;
        ButtonList _menuBox;

        #endregion

        #region Construct

        public MainMenu ( CoreGame game )
            : base ( game, "MainMenu" )
        {
        }

        #endregion

        #region XNA Logic

        public override void LoadOnShow()
        {
            base.LoadOnShow ();
            #region Set Up Vars
            Vector2 backgroundSize = new Vector2 ( ConfigManager.Screen.Width - 2, ConfigManager.Screen.Height );
            int btnCount = 4;
            int btnListSpacer = 2;
            Vector2 buttonSize = new Vector2 ( 300, 50 );
            int posX = ConfigManager.Screen.Width / 2 - ( ( int )buttonSize.X / 2 );
            int posY = ConfigManager.Screen.Height / 2 - ( ( int )buttonSize.Y * btnCount + ( 2 * ( btnCount + 1 ) ) ) / 2;
            Color TextColor = Color.White;
            Color BorderColor = Color.Gold;
            Color BackColor = Color.Black;
            #endregion
            #region Background Control
            _backgroundControl = new ScreenBack ( backgroundSize );
            _backgroundControl.ChangeColor ( BorderColor, BackColor );
            Gui.Add ( _backgroundControl );
            #endregion
            #region New Game Button
            _newGameButton = new Button ( "UIFont", "New Game", TextColor );
            _newGameButton.MouseClicked += NewGameButtonClicked;
            #endregion
            #region Load Game Button
            _loadGameButton = new Button ( "UIFont", "Load Game", TextColor );
            _loadGameButton.MouseClicked += LoadGameButtonClicked;
            if ( SaveManager.SaveCount () > 0 )
                _loadGameButton.Enabled = true;
            else
                _loadGameButton.Enabled = false;
            #endregion
            #region Options Button
            _optionsButton = new Button ( "UIFont", "Options", TextColor );
            _optionsButton.MouseClicked += OptionsButtonClicked;
            #endregion
            #region Exit Button
            _exitButton = new Button ( "UIFont", "Shutdown", TextColor );
            _exitButton.MouseClicked += ExitButtonClicked;
            #endregion
            #region Menu List 
            Vector2 ListSize = new Vector2 ( buttonSize.X, buttonSize.Y * btnCount + ( ( btnCount - 3 ) * btnListSpacer ) );
            List<Button> btns = new List<Button> ();
            btns.Add ( _newGameButton );
            btns.Add ( _loadGameButton );
            btns.Add ( _optionsButton );
            btns.Add ( _exitButton );
            _menuBox = new ButtonList ( ListSize, buttonSize, new Vector2 ( posX, posY ), btns, Stance.Vertical );
            _menuBox.ChangeColor ( BorderColor, BackColor );
            Gui.Add ( _menuBox );
            #endregion
        }

        public override void DisposeOnHide()
        {
            base.DisposeOnHide ();
            _backgroundControl = null;
            _newGameButton = null;
            _loadGameButton = null;
            _optionsButton = null;
            _exitButton = null;
            _menuBox = null;
        }

        public override void LoadContent()
        {
            
        }

        #endregion

        #region Control Events

        public void ExitButtonClicked( object sender, WMMouseEventArgs e )
        {
            Game.Exit ();
        }

        public void OptionsButtonClicked( object sender, WMMouseEventArgs e )
        {
            ScreenHandler.Change ( "Options" );
        }

        public void LoadGameButtonClicked( object sender, WMMouseEventArgs e )
        {
            CoreGame.GetInstance ().NotYetImplimentedMessage ();
            //ScreenHandler.Change ( "LoadGame" );
        }

        public void NewGameButtonClicked( object sender, WMMouseEventArgs e )
        {
            ScreenHandler.Change ( "NewGame" );
        }

        #endregion
    }
}

