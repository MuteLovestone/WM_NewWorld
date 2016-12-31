using System;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;
using WMNW.Core.GraphicX.Screen;
using WMNW.Core.GUI.Controls;

namespace WMNW.GameScreens
{
    public class LoadingScreen:ScreenBase
    {
        #region Fields

        private int _functionsCount = 1;
        private int _functionsTotal = 6;
        private ControlBase _backgroundControl;
        private Label _loadingText;
        private ProgressBar _loadingBar;
        private bool _finishedLoading = false;
        private const int _WaitTicks = 50;
        private int _currentTicks = 0;
        private const int _screensTotal = 6;

        #endregion

        #region Constructor

        public LoadingScreen ( CoreGame game )
            : base ( game, "Loading" )
        {
        }

        #endregion

        #region XNA Logic

        public override void LoadOnShow()
        {
            base.LoadOnShow ();
            #region Set Up Variables
            Vector2 backgroundSize = new Vector2 ( Systems.ConfigManager.Screen.Width - 2, Systems.ConfigManager.Screen.Height );
            Vector2 LoadingTextSize = new Vector2 ( Systems.ConfigManager.Screen.Width - 7, 150 );
            Vector2 LoadingTextPos = new Vector2 ( 3, Systems.ConfigManager.Screen.Height - 153 );
            Color TextColor = Color.White;
            Color BackColor = Color.Black;
            Color BorderColor = Color.Gold;
            #endregion
            #region Background Control
            _backgroundControl = new ScreenBack ( backgroundSize );
            _backgroundControl.ChangeColor ( BorderColor, BackColor );
            Gui.Add ( _backgroundControl );
            #endregion
            #region Loading Text
            _loadingText = new Label ( "UIFont", "Processing:         Building Game Screens", TextColor, LoadingTextPos, LoadingTextSize );
            _loadingText.ChangeColor ( BorderColor, BackColor );
            Gui.Add ( _loadingText );
            #endregion
            #region Loading Bar Text
            GetTotalLoadingCount ();
            Vector2 LoadingBarSize = new Vector2 ( Systems.ConfigManager.Screen.Width - 7, 25 );
            Vector2 LoadingBarPos = new Vector2 ( 3, Systems.ConfigManager.Screen.Height - 180 );
            _loadingBar = new ProgressBar ( LoadingBarPos, LoadingBarSize, Color.Blue );
            _loadingBar.ChangeColor ( BorderColor, BackColor );
            _loadingBar.MaximumValue = _functionsTotal;
            _loadingBar.MinimumValue = 0;
            _loadingBar.Value = _functionsCount;
            Gui.Add ( _loadingBar );
            #endregion
        }

        public override void DisposeOnHide()
        {
            base.DisposeOnHide ();
            _backgroundControl = null;
            _loadingBar = null;
            _loadingText = null;
        }

        public override void LoadContent()
        {
            
        }

        public override void Update( GameTime gameTime )
        {
            _loadingBar.Value = _functionsCount;
            BuildGameScreens ();
            base.Update ( gameTime );
            if ( _functionsCount == _functionsTotal )
                _finishedLoading = true;
            if ( _finishedLoading )
                ScreenHandler.Change ( "MainMenu" );
        }

        #endregion

        #region Event Logic

        #endregion

        #region Private Logic

        private void BuildGameScreens()
        {
            if ( _functionsCount >= _screensTotal )
                return;
            _currentTicks++;
            if ( _currentTicks >= _WaitTicks )
            {
                _currentTicks = 0;
                if ( _functionsCount == 1 )
                    ScreenHandler.Add ( new MainMenu ( CoreGame.GetInstance () ) );
                if ( _functionsCount == 2 )
                    ScreenHandler.Add ( new OptionsScreen ( CoreGame.GetInstance () ) );
                if ( _functionsCount == 3 )
                    ScreenHandler.Add ( new LoadGameScreen ( CoreGame.GetInstance () ) );
                if ( _functionsCount == 4 )
                    ScreenHandler.Add ( new NewGameScreen () );
                _functionsCount++;
            }
        }

        private void GetTotalLoadingCount()
        {
            _functionsTotal = _screensTotal;
        }

        #endregion
    }
}

