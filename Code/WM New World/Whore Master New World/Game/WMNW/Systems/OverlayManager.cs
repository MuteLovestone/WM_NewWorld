using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WMNW.Core.GUI;
using WMNW.Core.GUI.Controls;
using WMNW.Core.GraphicX.Screen;
using WMNW.Core.GraphicX;
using IMan = WMNW.Core.InstanceManager;

namespace WMNW.Systems
{
    public class OverlayManager
    {
        #region Fields

        private static OverlayManager _instance;
        private static Gui gui;
        private static bool isShowing = false;
        private static List<AchivementToDraw> achivementOrders = new List<AchivementToDraw> ();
        private static bool waitingForInput = false;
        private static bool OptionsShowing = false;
        private static bool MessageShowing = false;
        private static Label _achiveLabel;

        #endregion

        #region Properties

        #endregion

        #region Constructor

        public OverlayManager ()
        {
            IMan.CheckInstance ( _instance, "Overlay Manager", true );
            _instance = this;
            gui = new Gui ();
            isShowing = false;
            achivementOrders = new List<AchivementToDraw> ();
        }

        #endregion

        #region Logic

        public static string ShowOptions( string message, List<string> options )
        {
            //TODO: Make Functional
            ScreenHandler.PauseUpdate ();
            isShowing = true;
            do
            {
                waitingForInput = false;
            }
            while( waitingForInput );
            ScreenHandler.ResumeUpdate ();
            isShowing = false;
            return options [ 0 ];
        }

        public static void ShowMessage( string message, bool center )
        {
            #region Set Up Vars
            int posx = 0;
            int posy = 0;
            Vector2 lblsize = GraphicsHandler.MesureString ( "UIFont", message ) + new Vector2 ( 100, 50 );
            posx = ConfigManager.Screen.Width / 2 - ( int )lblsize.X / 2;
            posy = ConfigManager.Screen.Height / 2 - ( int )lblsize.Y / 2;
            #endregion
            Label b = new Label ( "UIFont", message, Color.White, new Vector2 ( posx, posy ), lblsize );
            b.MouseClicked += MessageLabelClicked;
            b.ChangeColor ( Color.Gold, Color.Black );
            b.Centered = center;
            b.ZIndex = 99999;
            gui.Add ( b );
            isShowing = true;
            MessageShowing = true;
            waitingForInput = true;
            ScreenHandler.PauseUpdate ();
            do
            {
                
            }
            while( waitingForInput );
            b.MouseClicked -= MessageLabelClicked;
            gui.Remove ( b );
            System.Threading.Thread.Sleep ( 100 );
            ScreenHandler.ResumeUpdate ();
            b = null;
            return;
        }

        public static void ShowAchievement( string acievementName )
        {
            isShowing = true;
            achivementOrders.Add ( new AchivementToDraw (){ Message = "New Message" } );
        }

        public void Update( GameTime gameTime )
        {
            if ( !isShowing )
                return;
            gui.Update ( gameTime );
        }

        public void Draw( GameTime gameTime )
        {
            if ( !isShowing )
                return;
            GraphicsHandler.Begin ();
            gui.Draw ( gameTime );
            GraphicsHandler.End ();
        }

        #endregion

        #region Private Control Events

        private static void MessageLabelClicked( object sender, WMMouseEventArgs e )
        {
            MessageShowing = false;
            waitingForInput = false;
            isShowing = false;
            ( ( Label )sender ).Enabled = false;
        }

        #endregion

        #region Private Logic

        private void UpdateAchivements()
        {
            if ( achivementOrders.Count == 0 && !MessageShowing && !OptionsShowing )
                isShowing = false;
            else
            {
                if ( achivementOrders.Count == 0 )
                    return;
                if ( achivementOrders.Count <= 1 )
                {
                    achivementOrders [ 0 ].TicksLeft--;
                }
                if ( achivementOrders [ 0 ].TicksLeft <= 0 )
                {
                    achivementOrders.RemoveAt ( 0 );
                }
            }
        }

        #endregion

        #region Private classes

        private class AchivementToDraw
        {
            #region Fields

            public int TicksLeft = 5000;
            public string Message = "";

            #endregion

        }

        #endregion
    }
}

