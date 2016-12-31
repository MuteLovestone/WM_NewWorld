using System;
using Microsoft.Xna.Framework;
using WMNW.Core.Input.Devices.Interfaces;
using WMNW.Core.Input.Classes;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Windows.Forms;
using WMNW.Core.Input.Devices.Generic;

namespace WMNW.Core.Input.Manager
{
    public class InputManager : IInputService, IGameComponent, IUpdateable
    {
        #region Interface Implementations

        #region Implementation of IInputService

        public void TakeSnapshot()
        {
        }

        /// <summary>Number of snapshots currently in the queue</summary>
        public int SnapshotCount
        {
            get
            {
                return _snapshotCount;
            }
        }

        #endregion

        #region Implementation of IUpdateable

        /// <summary>
        /// Update using Interface
        /// </summary>
        /// <param name="gameTime">Not Needed</param>
        void IUpdateable.Update( GameTime gameTime )
        {
            Update ( gameTime );
        }

        /// <summary>
        /// Fired when the Enabled Property Changes its value
        /// </summary>
        public event EventHandler<EventArgs> EnabledChanged { add { } remove { }
        }

        /// <summary>
        /// Fired when the UpdateOrder Property Changes its value
        /// </summary>
        public event EventHandler<EventArgs> UpdateOrderChanged;

        /// <summary>
        /// This Component is always enabled
        /// </summary>
        bool IUpdateable.Enabled
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        ///   Indicates when the game component should be updated relative to other game
        ///   components. Lower values are updated first.
        /// </summary>
        public int UpdateOrder
        {
            get
            {
                return _updateOrder;
            }
            set
            {
                if ( value == _updateOrder )
                    return;
                _updateOrder = value;
                OnUpdateOrderChanged ();
            }
        }

        #endregion

        #region Implementation of IGameComponent

        /// <summary>
        /// Implements IGameComponent
        /// </summary>
        void IGameComponent.Initialize()
        {
        }

        #endregion

        #endregion

        #region Fields

        /// <summary>
        /// Controls the order in which this game component is updated relitive to other game components
        /// </summary>
        int _updateOrder = int.MinValue;

        /// <summary>
        /// Number of state snap shots currently queued
        /// </summary>
        private int _snapshotCount;

        /// <summary>
        /// Game Service Container
        /// </summary>
        private readonly GameServiceContainer _gameServices;

        /// <summary>Collection of all mice known to the system</summary>
        private ReadOnlyCollection<IMouse> _mice;

        /// <summary>
        /// Collection of all Keyboards known to the system
        /// </summary>
        ReadOnlyCollection<IKeyboard> _keyboard;

        /// <summary>
        /// Collection of all GamePads known to the system
        /// </summary>
        ReadOnlyCollection<IGamePad> _gamePad;


        /// <summary>
        /// Determines if we are running under Windows Forms environment or not
        /// </summary>
        private bool _winForms;

        #endregion

        #region Properties

        /// <summary>All mice known to the system</summary>
        public ReadOnlyCollection<IMouse> Mice
        {
            get
            {
                return _mice;
            }
        }

        /// <summary>All keyboards known to the system</summary>
        public ReadOnlyCollection<IKeyboard> Keyboard
        {
            get
            {
                return _keyboard;
            }
        }

        /// <summary>All GamePads known to the system</summary>
        public ReadOnlyCollection<IGamePad> GamePad
        {
            get
            {
                return _gamePad;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new input manager
        /// </summary>
        /// <param name="services"></param>
        /// <param name="winForms"></param>
        public InputManager ( GameServiceContainer services, bool winForms = false )
        {
            if ( services != null )
            {
                _gameServices = services;
                _gameServices.AddService ( typeof( IInputService ), this );
            }

            _winForms = winForms;

            SetupMouse ();
            SetupKeyboard ();
            SetupGamePad ();
        }

        #endregion

        #region XNA Methods

        public IMouse GetMouse()
        {
            return CollectionHelper.GetIfExists ( _mice, 0 );
        }

        public IKeyboard GetKeyboard()
        {
            return CollectionHelper.GetIfExists ( _keyboard, 0 );
        }

        public IGamePad GetGamePad()
        {
            return CollectionHelper.GetIfExists ( _gamePad, 0 );
        }


        /// <summary>
        /// Update all our Input Components
        /// </summary>
        public void Update( GameTime gameTime )
        {
            if ( _snapshotCount > 0 )
                --_snapshotCount;

            foreach ( var m in _mice )
                m.Update ( gameTime );

            foreach ( var k in _keyboard )
                k.Update ( gameTime );

            foreach ( var g in _gamePad )
                g.Update ( gameTime );

        }

        #endregion

        #region Methods

        /// <summary>Fires the UpdateOrderChanged event</summary>
        protected void OnUpdateOrderChanged()
        {
            if ( UpdateOrderChanged != null )
                UpdateOrderChanged ( this, EventArgs.Empty );
        }

        #endregion

        #region Mouse Handler

        void SetupMouse()
        {
            var mice = new List<IMouse> ();
            #if WINDOWS || LINUX || MAC
            if ( _winForms )
            {
                mice.Add ( new PcMouse ( true ) );
                Console.WriteLine ( "Mouse Detected!" );
            }
            else
            {
                mice.Add ( new PcMouse () );
                Console.WriteLine ( "Mouse Detected!" );
            }
            #else
            mice.Add(new NoMouse());
            Console.WriteLine("Mouse Not Found");
            #endif
            _mice = new ReadOnlyCollection<IMouse> ( mice );
        }

        #endregion

        #region Keyboard Handler

        void SetupKeyboard()
        {
            var keyboard = new List<IKeyboard> ();
            #if WINDOWS || LINUX || MAC
            if ( _winForms )
            {
                keyboard.Add ( new PcKeyboard ( true ) );
                Console.WriteLine ( "Keyboard Detected!" );
            }
            else
            {
                keyboard.Add ( new PcKeyboard () );
                Console.WriteLine ( "Keyboard Detected!" );
            }
            #else
            keyboard.Add(new NoKeyboard());
            Console.WriteLine("Keyboard Not Found");
            #endif
            _keyboard = new ReadOnlyCollection<IKeyboard> ( keyboard );
        }

        #endregion

        #region GamePad Handler

        void SetupGamePad()
        {
            var gamePad = new List<IGamePad> ();

            #if WINDOWS || LINUX || MAC || XBOX360
            if ( Microsoft.Xna.Framework.Input.GamePad.GetState ( 0 ).IsConnected )
            {
                gamePad.Add ( new PcXboxGamePad ( 0 ) );
                Console.WriteLine ( "GamePad Detected" );
            }
            else
            {
                gamePad.Add ( new NoGamePad () );
                Console.WriteLine ( "GamePad Not Found" );
            }
            #else
            gamePad.Add(new NoGamePad());
            Console.WriteLine("GamePad Not Found");
            #endif
            _gamePad = new ReadOnlyCollection<IGamePad> ( gamePad );
        }

        #endregion
    }
}

