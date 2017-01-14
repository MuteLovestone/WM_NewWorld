using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;
using WMNW.Core.GraphicX.Screen;
using WMNW.Core.GUI.Controls;
using WMNW.Systems;
using Player = WMNW.GameData.Player;

namespace WMNW.GameScreens
{
    public class NewGameScreen:ScreenBase
    {
        #region Fields

        private ScreenBack _background;
        private Button _backButton;
        private Button _acceptButton;
        private Label _fnLabel;
        private Label _mnLabel;
        private Label _lnLabel;
        private TextBox _fnText;
        private TextBox _mnText;
        private TextBox _lnText;
        private ComboBox _genderBox;
        private Label _genderLabel;

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
            if ( Gui != null )
            {
                if ( Gui.Controls != null )
                {
                    Gui.Controls.Clear ();
                    Gui.SelectedControl = null;
                }
            }
            base.DisposeOnHide ();
            _background = null;
            _backButton = null;
            _acceptButton = null;
            _fnText = null;
            _fnLabel = null;
            _mnText = null;
            _mnLabel = null;
            _lnText = null;
            _lnLabel = null;
            if ( _genderBox != null )
            {
                _genderBox.SelectedBtn = null;
                _genderBox.Children.Clear ();
            }
            _genderBox = null;
            _genderLabel = null;
        }

        public override void LoadOnShow()
        {
            base.LoadOnShow ();
            MastersManager.Reset ();
            #region Set Up Variables
            Vector2 backgroundSize = new Vector2 ( Systems.ConfigManager.Screen.Width - 2, Systems.ConfigManager.Screen.Height );
            Color BC = Color.Gold;
            Color BG = Color.Black;
            int posx = 0;
            int posy = 0;
            Color TC = Color.White;
            string font = "UIFont";
            Vector2 bSize = new Vector2 ( 150, 50 );
            Vector2 lblSize = new Vector2 ( 100, 25 );
            Vector2 tbSize = new Vector2 ( 300, 25 );
            Vector2 tbOffset = new Vector2 ( 5, 2 );
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
            #region First Name Label
            posx = 4;
            posy = 4;
            _fnLabel = new Label ( font, "First", TC, new Vector2 ( posx, posy ), lblSize );
            _fnLabel.ChangeColor ( BC, BG );
            Gui.Add ( _fnLabel );
            #endregion
            #region First Name Text
            posx += 102;
            _fnText = new TextBox ( font, TC, new Vector2 ( posx, posy ), tbSize );
            _fnText.Text = "Player";
            _fnText.ChangeColor ( BC, BG );
            _fnText.TextOffset = tbOffset;
            Gui.Add ( _fnText );
            #endregion
            #region Gender Label
            posx += 302;
            _genderLabel = new Label ( font, "Gender", TC, new Vector2 ( posx, posy ), lblSize );
            _genderLabel.ChangeColor ( BC, BG );
            Gui.Add ( _genderLabel );
            #endregion
            #region Gender Box
            #region Gender Buttons
            List<Button> _gender = new List<Button> ();
            _gender.Add ( new Button ( font, "Male", TC ) );
            _gender.Add ( new Button ( font, "Female", TC ) );
            _gender.Add ( new Button ( font, "Futa", TC ) );
            #endregion
            posx += 102;
            _genderBox = new ComboBox ( font, TC, new Vector2 ( posx, posy ), lblSize, _gender );
            _genderBox.ChangeColor ( BC, BG );
            _genderBox.SelectedEvent += GenderBoxSelectionChanged;
            _genderBox.Size = lblSize;
            Gui.Add ( _genderBox );
            _gender = null;
            #endregion
            #region Middle Name Label
            posx = 4;
            posy += 27;
            _mnLabel = new Label ( font, "Middle", TC, new Vector2 ( posx, posy ), lblSize );
            _mnLabel.ChangeColor ( BC, BG );
            Gui.Add ( _mnLabel );
            #endregion
            #region Middle Name Text
            posx += 102;
            _mnText = new TextBox ( font, TC, new Vector2 ( posx, posy ), tbSize );
            _mnText.Text = "";
            _mnText.ChangeColor ( BC, BG );
            _mnText.TextOffset = tbOffset;
            Gui.Add ( _mnText );
            #endregion
            #region Last Name Label
            posx = 4;
            posy += 27;
            _lnLabel = new Label ( font, "Last", TC, new Vector2 ( posx, posy ), lblSize );
            _lnLabel.ChangeColor ( BC, BG );
            Gui.Add ( _lnLabel );
            #endregion
            #region Last Name Text
            posx += 102;
            _lnText = new TextBox ( font, TC, new Vector2 ( posx, posy ), tbSize );
            _lnText.ChangeColor ( BC, BG );
            _lnText.TextOffset = tbOffset;
            _lnText.Text = "Player";
            Gui.Add ( _lnText );
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
            MastersManager.GetPlayer ().SetName ( _fnText.Text, _mnText.Text, _lnText.Text );
            if ( _genderBox.SelectedBtn.Text == "Male" )
                MastersManager.GetPlayer ().SetGender ( WMNW.GameData.Details.Genders.Male );
            else if ( _genderBox.SelectedBtn.Text == "Female" )
                MastersManager.GetPlayer ().SetGender ( WMNW.GameData.Details.Genders.Female );
            else
                MastersManager.GetPlayer ().SetGender ( WMNW.GameData.Details.Genders.Futa );
            SelectionManager.SelectMaster ( 0 );
            SelectionManager.SelectBuilding ( 0 );
            SelectionManager.SelectWorker ( 0 );
            MastersManager.GetPlayer ().AddBuilding ( new WMNW.GameData.Buildings.Building ( "Home", WMNW.GameData.Buildings.BuildingType.House ) );
            MastersManager.GetPlayer ().AddBuilding ( new WMNW.GameData.Buildings.Building ( "Brothel", WMNW.GameData.Buildings.BuildingType.Brothel ) );
            ScreenHandler.Change ( "Building" );
        }

        private void GenderBoxSelectionChanged( object sender, WMEventArgs e )
        {
            _genderBox.SelectedItem = e.SelectedItem;
        }

        #endregion
    }
}

