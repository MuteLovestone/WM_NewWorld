using System;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;
using WMNW.Core.GraphicX.Screen;
using WMNW.Core.GUI.Controls;
using WMNW.Systems;

namespace WMNW.GameScreens
{
    public class LoadGameScreen:ScreenBase
    {
        #region Fields

        private ControlBase _background;
        private Button _BackButton;

        #endregion

        #region Construct

        public LoadGameScreen ( CoreGame game )
            : base ( game, "LoadGame" )
        {
        }

        #endregion

        #region XNA Logic

        public override void LoadContent()
        {
            
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
            _BackButton = new Button ( font, "Back", TC, new Vector2 ( posx, posy ), bSize );
            _BackButton.ChangeColor ( BC, BG );
            _BackButton.MouseClicked += BackButtonClicked;
            Gui.Add ( _BackButton );
            #endregion
        }

        public override void DisposeOnHide()
        {
            base.DisposeOnHide ();
            #region Null Controls
            _background = null;
            _BackButton = null;
            #endregion
        }

        #endregion

        #region Control events

        private void BackButtonClicked( object sender, WMMouseEventArgs e )
        {
            ScreenHandler.Change ( "MainMenu" );
        }

        #endregion
    }
}

