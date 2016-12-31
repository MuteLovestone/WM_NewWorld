using System;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX.Screen;
using WMNW.Core.GUI.Controls;
using WMNW.Systems;

namespace WMNW.GameScreens
{
    public class MainMenu:ScreenBase
    {
        #region Fields

        ScreenBack _backgroundControl;
        Button _newGameButton;
        Button _loadGameButton;
        Button _optionsButton;
        Button _exitButton;

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
            int posX = ConfigManager.Screen.Width / 2 - 150;
            int posY = ConfigManager.Screen.Height / 3 - 25;
            Vector2 buttonSize = new Vector2 ( 300, 50 );
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
            _newGameButton = new Button ( "UIFont", "New Game", TextColor, new Vector2 ( posX, posY ), buttonSize );
            _newGameButton.ChangeColor ( BorderColor, BackColor );
            _newGameButton.MouseClicked += NewGameButtonClicked;
            Gui.Add ( _newGameButton );
            #endregion
            #region Load Game Button
            posY += 52;
            _loadGameButton = new Button ( "UIFont", "Load Game", TextColor, new Vector2 ( posX, posY ), buttonSize );
            _loadGameButton.ChangeColor ( BorderColor, BackColor );
            _loadGameButton.MouseClicked += LoadGameButtonClicked;
            Gui.Add ( _loadGameButton );
            #endregion
            #region Options Button
            posY += 52;
            _optionsButton = new Button ( "UIFont", "Options", TextColor, new Vector2 ( posX, posY ), buttonSize );
            _optionsButton.ChangeColor ( BorderColor, BackColor );
            _optionsButton.MouseClicked += OptionsButtonClicked;
            Gui.Add ( _optionsButton );
            #endregion
            #region Exit Button
            posY += 52;
            _exitButton = new Button ( "UIFont", "Shutdown", TextColor, new Vector2 ( posX, posY ), buttonSize );
            _exitButton.ChangeColor ( BorderColor, BackColor );
            _exitButton.MouseClicked += ExitButtonClicked;
            Gui.Add ( _exitButton );
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
            ScreenHandler.Change ( "LoadGame" );
        }

        public void NewGameButtonClicked( object sender, WMMouseEventArgs e )
        {
            ScreenHandler.Change ( "NewGame" );
        }

        #endregion
    }
}

