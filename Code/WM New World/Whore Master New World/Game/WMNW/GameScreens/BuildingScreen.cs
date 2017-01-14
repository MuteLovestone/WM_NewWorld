using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;
using WMNW.Core.GraphicX.Screen;
using WMNW.Core.GUI.Controls;
using cfg = WMNW.Systems.ConfigManager;
using MMan = WMNW.Systems.MastersManager;
using sMan = WMNW.Systems.SaveManager;
using UIMan = WMNW.Systems.OverlayManager;
using slcMan = WMNW.Systems.SelectionManager;
using WMNW.GameData;
using WMNW.GameData.Buildings;

namespace WMNW.GameScreens
{
    public class BuildingScreen:ScreenBase
    {
        #region Fields

        private ScreenBack _background;
        private MultiLineBox _detailsBox;
        private Button _saveGameButton;
        private Button _quitGameButton;
        private Button _visitTownButton;
        private Button _endTurnButton;
        private Button _manageBuildingButton;
        private Button _viewBuildingsButton;

        #endregion

        #region Constructor

        public BuildingScreen ()
            : base ( CoreGame.GetInstance (), "Building" )
        {
        }

        #endregion

        #region XNA Logic

        public override void LoadOnShow()
        {
            base.LoadOnShow ();
            #region Set Up Vars
            Vector2 scSize = cfg.Screen.GetScreenSize ();
            Vector2 btnSize = new Vector2 ( 100, 25 );
            Vector2 mlbSize = new Vector2 ( scSize.X / 4, scSize.Y - 35 );
            int posx = 0;
            int posy = 0;
            Color tc = Color.White;
            Color bc = Color.Gold;
            Color bg = Color.Black;
            string font = "UIFont";
            #endregion
            #region Background
            _background = new ScreenBack ( scSize );
            _background.ChangeColor ( bc, bg );
            Gui.Add ( _background );
            #endregion
            #region Save Game Button
            posx += 4;
            posy += 4;
            _saveGameButton = new Button ( font, "Save", tc, new Vector2 ( posx, posy ), btnSize );
            _saveGameButton.ChangeColor ( bc, bg );
            _saveGameButton.MouseClicked += SaveButtonClicked;
            Gui.Add ( _saveGameButton );
            #endregion
            #region Quit Game Button
            posx += 102;
            _quitGameButton = new Button ( font, "Quit Game", tc, new Vector2 ( posx, posy ), btnSize );
            _quitGameButton.ChangeColor ( bc, bg );
            _quitGameButton.MouseClicked += QuitGameButtonClicked;
            Gui.Add ( _quitGameButton );
            #endregion
            #region Details Box
            posx = 4;
            posy += 27;
            _detailsBox = new MultiLineBox ( font, GenerateDetails (), tc, new Vector2 ( posx, posy ), mlbSize );
            _detailsBox.ChangeColor ( bc, bg );
            _detailsBox.TextOffset = new Vector2 ( 5, 2 );
            Gui.Add ( _detailsBox );
            #endregion
            #region Visit Town Button
            posx = ( int )scSize.X - 102;
            posy = 4;
            _visitTownButton = new Button ( font, "Visit Town", tc, new Vector2 ( posx, posy ), btnSize );
            _visitTownButton.ChangeColor ( bc, bg );
            _visitTownButton.MouseClicked += _visitTownButton_MouseClicked;
            Gui.Add ( _visitTownButton );
            #endregion
            #region End Turn Button
            posx -= 102;
            _endTurnButton = new Button ( font, "End Turn", tc, new Vector2 ( posx, posy ), btnSize );
            _endTurnButton.ChangeColor ( bc, bg );
            _endTurnButton.MouseClicked += _endTurnButton_MouseClicked;
            Gui.Add ( _endTurnButton );
            #endregion
            #region Manage Building Button

            #endregion
            #region View Buildings Button

            #endregion
        }

        public override void DisposeOnHide()
        {
            base.DisposeOnHide ();
            _background = null;
            _detailsBox = null;
            _saveGameButton = null;
            _quitGameButton = null;
            _visitTownButton = null;
            _endTurnButton = null;
            _manageBuildingButton = null;
            _viewBuildingsButton = null;
        }

        public override void LoadContent()
        {
            
        }

        #endregion

        #region Control Events

        private void SaveButtonClicked( object sender, WMMouseEventArgs e )
        {
            Thread b = new Thread ( SaveGame );
            b.Start ();
        }

        private void QuitGameButtonClicked( object sender, WMMouseEventArgs e )
        {
            Thread b = new Thread ( QuitGameThread );
            b.Start ();
        }

        void _endTurnButton_MouseClicked( object sender, WMMouseEventArgs e )
        {
            Thread b = new Thread ( CoreGame.GetInstance ().NotYetImplimentedMessage );
            b.Start ();
            //ScreenHandler.Change ( "EndTurnLoading" );
        }

        void _visitTownButton_MouseClicked( object sender, WMMouseEventArgs e )
        {
            Thread b = new Thread ( CoreGame.GetInstance ().NotYetImplimentedMessage );
            b.Start ();
            //ScreenHandler.Change ( "Town" );
        }

        #endregion

        #region private Logic

        private void SaveGame()
        {
            sMan.Save ();
            UIMan.ShowMessage ( "Game Saved", true );
        }

        private void QuitGameThread()
        {
            List<string> opts = new List<string> ();
            opts.Add ( "Yes" );
            opts.Add ( "No" );
            if ( UIMan.ShowOptions ( "Would you Like to Save Your Game", opts ) == "Yes" )
            {
                SaveGame ();
            }
            ScreenHandler.Change ( "MainMenu" );
        }

        private string GenerateDetails()
        {
            int masterId = slcMan.SelectedMasterID ();
            Master mast = MMan.GetMaster ( slcMan.SelectedMasterID () );
            string details = "";
            details += "Owners Name:\n";
            details += mast.FullName + "\n";
            details += "-----------------------------\n";
            details += "Owner's Gender:\n";
            details += mast.Gender.ToString () + "\n";
            details += "-----------------------------\n";
            details += "Worlds Disposition of owner:\n";
            details += Master.GetDispostionText ( mast.TotalSuspicion () ) + "\n";
            details += "-----------------------------\n";
            details += "World Suspicion of Owner:\n";
            details += Master.GetSuspicionText ( mast.TotalSuspicion () ) + "\n";
            details += "-----------------------------\n";
            details += "Owner's Worth:\n";
            details += mast.Gold.ToString () + " Gold\n";
            details += "-----------------------------\n";
            Building building = mast.GetBuilding ( slcMan.SelectedBuildingID () );
            details += "Building:\n";
            details += building.Name + "\n";
            details += "-----------------------------\n";
            details += "Building Type:\n";
            details += building.Type + "\n";
            details += "-----------------------------\n";
            details += "Building Clean Rating:\n";
            details += Building.GetCleanRating ( building.Filth ) + "\n";
            details += "-----------------------------\n";
            details += "Customer Happiness:\n";
            details += Building.GetHappinessText ( building.Happiness ) + "\n";
            details += "-----------------------------\n";
            details += "Security Level:\n";
            details += building.SecurityLevel + "\n";
            details += "-----------------------------\n";
            details += "Building Fame Rating:\n";
            details += Building.GetFameRating ( building.Fame ) + "\n";
            details += "-----------------------------\n";
            details += "Beasts Residing in Building:\n";
            details += building.Beasts.ToString () + "\n";
            details += "----------------------------- ";
            return details;
        }

        #endregion
    }
}

