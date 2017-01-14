using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace WMNW.Core.GraphicX.Screen
{
    public class ScreenHandler : DrawableGameComponent
    {
        #region Events

        public event EventHandler OnScreenChange;

        #endregion

        #region Fields

        List<ScreenBase> _screens = null;

        readonly Stack<ScreenBase> _gameScreens = new Stack<ScreenBase> ();
        private static bool IsUpdateDisabled = false;

        #endregion

        #region Properties

        /// <summary>
        /// Singleton Acccessor
        /// </summary>
        static ScreenHandler Instance
        {
            get;
            set;
        }

        /// <summary>
        /// The level that this game component draws too
        /// </summary>
        public static int DrawLevel
        {
            get
            {
                return Instance.DrawOrder;
            }
        }

        /// <summary>
        /// When you try to get this Property you recieve the top level screen
        /// </summary>
        public static ScreenBase CurrentScreen
        {
            get
            {
                return Instance._gameScreens.Count == 0 ? null : Instance._gameScreens.Peek ();
            }
        }

        #endregion

        #region Constructor

        public ScreenHandler ( Game game )
            : base ( game )
        {
            Instance = this;

            //Set our current draw order
            Instance.DrawOrder = GameBase.DrawOrderStart;

            //Setup our Screens
            Instance._screens = new List<ScreenBase> ();

            Game.Window.ClientSizeChanged += delegate
            {
                if ( CurrentScreen != null )
                {
                    CurrentScreen.OnResize ();
                }
            };
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a screen for later use
        /// </summary>
        /// <param name="newScreen"></param>
        public static void Add( ScreenBase newScreen )
        {
            //Ask it to load its content
            newScreen.LoadContent ();
            //Add screen into our screen manager
            Instance._screens.Add ( newScreen );
        }

        /// <summary>
        /// Pop Screen will remove the top level screen from our stack
        /// </summary>
        private static void PopScreen()
        {
            //Get our top level screen
            var screen = Instance._gameScreens.Peek ();
            //Hide our screen
            screen.Hide ();
            //Unsubscribe this screen from our On Change method so it doesn't trigger anymore
            Instance.OnScreenChange -= screen.ScreenChange;
            //Remove our screen from our components
            Instance.Game.Components.Remove ( screen );
            //Pop off the screen from our stack
            Instance._gameScreens.Pop ();
        }

        private static void AddScreenToStack( ScreenBase screen )
        {
            //Push our added screen to top of stack
            Instance._gameScreens.Push ( screen );
            //Add it into our Game Components list
            Instance.Game.Components.Add ( screen );
            //Attach an event for On Screen Change
            Instance.OnScreenChange += screen.ScreenChange;
        }

        /// <summary>
        /// Change will cause the screen to change from one to the one passed
        /// </summary>
        /// <param name="name"></param>
        public static void Change( string name )
        {
            var screen = GetScreen ( name );

            if ( screen == null )
                return;

            //Remove all screens from our stack
            while ( Instance._gameScreens.Count > 0 )
                PopScreen ();

            //Alter the draw order a bit
            screen.DrawOrder = GameBase.DrawOrderStart;
            Instance.DrawOrder = GameBase.DrawOrderStart;

            AddScreenToStack ( screen );

            //Fire off the On Change Event
            if ( Instance.OnScreenChange != null )
                Instance.OnScreenChange ( Instance, null );
        }

        /// <summary>
        /// Gets a Screen based on the name passed
        /// </summary>
        /// <param name="name">Name of Screen to fetch</param>
        public static ScreenBase GetScreen( string name )
        {
            return Instance._screens.FirstOrDefault ( s => s.Name == name );
        }

        public static void PauseUpdate()
        {
            IsUpdateDisabled = true;
            CurrentScreen.DisableUpdate ();
        }

        public static void ResumeUpdate()
        {
            IsUpdateDisabled = false;
            CurrentScreen.EnableUpdate ();
        }

        public override void Update( GameTime gameTime )
        {
            if ( IsUpdateDisabled )
                return;
            base.Update ( gameTime );
        }

        public override void Draw( GameTime gameTime )
        {
            base.Draw ( gameTime );
        }

        #endregion
    }
}

