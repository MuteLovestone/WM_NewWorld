using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WMNW.Core.GUI;
using WMNW.Core.GUI.Controls;
using WMNW.Core.GraphicX.Screen;
using WMNW.Core.GraphicX;
using System.Threading.Tasks;
using IMan = WMNW.Core.InstanceManager;

namespace WMNW.Systems
{
    public sealed class OverlayManager:IDisposable
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
        private static Label _messageBox;
        private static string _selectedOption1;
        private static OptionsBox _optBox;

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

        public static async Task<string> ShowOptions( string message, List<string> options )
        {
            //TODO: Make Functional
            ScreenHandler.PauseUpdate ();
            waitingForInput = true;
            isShowing = true;
            _optBox = new OptionsBox ( new Vector2 ( ConfigManager.Screen.Width, ConfigManager.Screen.Height ),
                options,
                Color.White,
                message );
            _optBox.OnOptionSelected += _optBox_OnOptionSelected;
            _optBox.ChangeColor ( Color.Gold, Color.Black );
            gui.Add ( _optBox );
            do
            {
            }
            while( waitingForInput );
            ScreenHandler.ResumeUpdate ();
            isShowing = false;
            return _selectedOption1;
        }

        static void _optBox_OnOptionSelected( object sender, OptionSelectedEvent e )
        {
            waitingForInput = false;
            _selectedOption1 = e.Option;
            gui.Remove ( _optBox );
        }


        public static void ShowMessage( string message, bool center )
        {
            #region Set Up Vars
            int posx = 0;
            int posy = 0;
            Vector2 lblsize = GraphicsHandler.MesureString ( "UIFont", message ) + new Vector2 ( 100, 50 );
            if ( lblsize.X >= ConfigManager.Screen.Width - 4 )
            {
                string tempMessage = message;
                do
                {
                    string message1 = message.Substring ( 0, tempMessage.LastIndexOf ( " " ) + 1 );
                    string message2 = message.Replace ( message1, "" );
                    string msgTemp = message1 + "\n" + message2;
                    Vector2 tempAddon = new Vector2 ( 100, 50 );
                    Vector2 newSize = GraphicsHandler.MesureString ( "UIFont", msgTemp ) + tempAddon;
                    if ( newSize.X < ConfigManager.Screen.Width - 4 )
                    {
                        message = msgTemp;
                        lblsize = newSize;
                    }
                }
                while ( lblsize.X >= ConfigManager.Screen.Width - 4 );
            }
            posx = ConfigManager.Screen.Width / 2 - ( int )lblsize.X / 2;
            posy = ConfigManager.Screen.Height / 2 - ( int )lblsize.Y / 2;
            #endregion
            // Create Message box
            _messageBox = new Label ( "UIFont", message, Color.White, new Vector2 ( posx, posy ), lblsize );
            _messageBox.MouseClicked += MessageLabelClicked;
            _messageBox.ChangeColor ( Color.Gold, Color.Black );
            _messageBox.Centered = center;
            _messageBox.ZIndex = 99999;
            // Pause Screen Updating
            ScreenHandler.PauseUpdate ();
            // Add Message Box
            gui.Add ( _messageBox );
            // Set Options
            isShowing = true;
            MessageShowing = true;
            waitingForInput = true;
        }

        public static void ShowAchievement( string acievementName )
        {
            isShowing = true;
            achivementOrders.Add ( new AchivementToDraw (){ Message = "New Message" } );
        }

        public void Update( GameTime gameTime )
        {
            if ( !isShowing )
            {
                ScreenHandler.ResumeUpdate ();
                return;
            }
            gui.Update ( gameTime );
        }

        public void Draw( GameTime gameTime )
        {
            if ( !isShowing )
            {
                return;
            }
            GraphicsHandler.Begin ();
            gui.Draw ( gameTime );
            GraphicsHandler.End ();
        }

        #endregion

        #region Private Control Events

        private static void MessageLabelClicked( object sender, WMMouseEventArgs e )
        {
            ( ( Label )sender ).Enabled = false;
            gui.Controls.Clear ();
            MessageShowing = false;
            waitingForInput = false;
            isShowing = false;
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

        #region Public Logic

        public void Dispose()
        {
            _instance = null;
            gui = null;
            achivementOrders = null;
            _achiveLabel = null;
            _messageBox = null;
        }

        #endregion
    }
}

