using System;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;
using WMNW.Core.GraphicX.Screen;
using WMNW.Systems;
using WMNW.Core.GUI.Controls;

namespace WMNW.GameScreens
{
    public class OptionsScreen:ScreenBase
    {
        #region Fields

        private ScreenBack _background;
        private Button _backButton;
        private Button _revertButton;
        private Button _defaultButton;
        private Button _acceptButton;
        private Label _heightLabel;
        private IntTextBox _heightText;
        private IntTextBox _widthText;
        private Label _widthLabel;
        private CheckBox _fullscreenBox;

        #endregion

        #region Construct

        public OptionsScreen ( CoreGame game )
            : base ( game, "Options" )
        {
        }

        #endregion

        #region XNA Logic

        public override void LoadOnShow()
        {
            base.LoadOnShow ();
            #region Build Vars
            Vector2 ButtonSize = new Vector2 ( 150, 50 );
            Vector2 ScreenSize = new Vector2 ( ConfigManager.Screen.Width - 2, ConfigManager.Screen.Height );
            Color TextColor = Color.White;
            Color BG = Color.Black;
            Color BC = Color.Gold;
            int posX = 4;
            int posY = ConfigManager.Screen.Height - 54;
            Vector2 LabelSize = new Vector2 ( 100, 25 );
            Vector2 IntBoxSize = new Vector2 ( 300, 25 );
            #endregion
            #region Background Control
            _background = new ScreenBack ( ScreenSize );
            _background.ChangeColor ( BC, BG );
            Gui.Add ( _background );
            #endregion
            #region Back Button
            _backButton = new Button ( "UIFont", "Back", TextColor, new Vector2 ( posX, posY ), ButtonSize );
            _backButton.ChangeColor ( BC, BG );
            _backButton.MouseClicked += BackButtonClicked;
            Gui.Add ( _backButton );
            #endregion
            #region Revert Button
            posX += 152;
            _revertButton = new Button ( "UIFont", "Revert", TextColor, new Vector2 ( posX, posY ), ButtonSize );
            _revertButton.ChangeColor ( BC, BG );
            _revertButton.MouseClicked += RevertButtonClicked;
            Gui.Add ( _revertButton );
            #endregion
            #region Default Button
            posX += 152;
            _defaultButton = new Button ( "UIFont", "Default", TextColor, new Vector2 ( posX, posY ), ButtonSize );
            _defaultButton.ChangeColor ( BC, BG );
            _defaultButton.MouseClicked += DefaultButtonClicked;
            Gui.Add ( _defaultButton );
            #endregion
            #region Accept Button
            posX += 152;
            _acceptButton = new Button ( "UIFont", "Accept", TextColor, new Vector2 ( posX, posY ), ButtonSize );
            _acceptButton.ChangeColor ( BC, BG );
            _acceptButton.MouseClicked += AcceptButtonClicked;
            Gui.Add ( _acceptButton );
            #endregion
            #region Height Label
            posX = 4;
            posY = 4;
            _heightLabel = new Label ( "UIFont", "Height ", TextColor, new Vector2 ( posX, posY ), LabelSize );
            _heightLabel.ChangeColor ( BC, BG );
            Gui.Add ( _heightLabel );
            #endregion
            #region Height Text
            posX += 102;
            _heightText = new IntTextBox ( "UIFont", ConfigManager.Screen.Height, TextColor, new Vector2 ( posX, posY ), IntBoxSize );
            _heightText.ChangeColor ( BC, BG );
            _heightText.TextOffset = new Vector2 ( 2, 2 );
            Gui.Add ( _heightText );
            #endregion
            #region Width Label
            posX = 4;
            posY += 27;
            _widthLabel = new Label ( "UIFont", "Width", TextColor, new Vector2 ( posX, posY ), LabelSize );
            _widthLabel.ChangeColor ( BC, BG );
            Gui.Add ( _widthLabel );
            #endregion
            #region Width Text
            posX += 102;
            _widthText = new IntTextBox ( "UIFont", ConfigManager.Screen.Width, TextColor, new Vector2 ( posX, posY ), IntBoxSize );
            _widthText.ChangeColor ( BC, BG );
            _widthText.TextOffset = new Vector2 ( 2, 2 );
            Gui.Add ( _widthText );
            #endregion
            #region FullScreen Box
            posX = 4;
            posY += 27;
            _fullscreenBox = new CheckBox ( "UIFont", "FullScreen", TextColor, new Vector2 ( posX, posY ), new Vector2 ( 23, 23 ), IntBoxSize );
            _fullscreenBox.ChangeColor ( BC, BG, Color.Green );
            _fullscreenBox.Checked = ConfigManager.Screen.FullScreen;
            _fullscreenBox.TextOffset = new Vector2 ( 5, 0 );
            Gui.Add ( _fullscreenBox );
            #endregion
        }

        public override void DisposeOnHide()
        {
            base.DisposeOnHide ();
            #region Null Controls
            _background = null;
            _backButton = null;
            _revertButton = null;
            _defaultButton = null;
            _acceptButton = null;
            _heightText = null;
            _heightLabel = null;
            _widthLabel = null;
            _widthText = null;
            _fullscreenBox = null;
            #endregion
        }

        public override void LoadContent()
        {
            
        }

        #endregion

        #region Control Events

        private void AcceptButtonClicked( object sender, WMMouseEventArgs e )
        {
            SetValues ();
            CoreGame.GetInstance ().UpdateScreenSize ();
            ScreenHandler.Change ( "MainMenu" );
        }

        private void BackButtonClicked( object sender, WMMouseEventArgs e )
        {
            ScreenHandler.Change ( "MainMenu" );
        }

        private void RevertButtonClicked( object sender, WMMouseEventArgs e )
        {
            SetText ();
        }

        private void DefaultButtonClicked( object sender, WMMouseEventArgs e )
        {
            ConfigManager.DefaultAll ();
            SetText ();
        }

        private void SetText()
        {
            _fullscreenBox.Checked = ConfigManager.Screen.FullScreen;
            _heightText.Value = ConfigManager.Screen.Height;
            _widthText.Value = ConfigManager.Screen.Width;
        }

        private void SetValues()
        {
            ConfigManager.Screen.FullScreen = _fullscreenBox.Checked;
            ConfigManager.Screen.Height = _heightText.Value;
            ConfigManager.Screen.Width = _widthText.Value;
        }

        #endregion
    }
}

