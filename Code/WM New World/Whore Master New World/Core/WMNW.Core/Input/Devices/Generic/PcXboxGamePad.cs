using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WMNW.Core.Input.Classes;
using WMNW.Core.Input.Devices.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace WMNW.Core.Input.Devices.Generic
{
    /// <summary>Interfaces with an XBox 360 controller via XNA (XINPUT)</summary>
    internal class PcXboxGamePad : GamePad
    {
        #region Properties

        /// <summary>Index of the player this device represents</summary>
        readonly PlayerIndex _playerIndex;

        /// <summary>Whether the input device is connected to the system</summary>
        public override bool IsAttached
        {
            get
            {
                return _current.IsConnected;
            }
        }

        /// <summary>Human-readable name of the input device</summary>
        public override string Name
        {
            get
            {
                return "Xbox 360 game pad #" + ( ( int )_playerIndex + 1 );
            }
        }

        GamePadState _current;

        protected GamePadState Current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
            }
        }

        protected GamePadState Previous
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        /// <summary>Initializes a new XNA-based keyboard device</summary>
        public PcXboxGamePad ( PlayerIndex playerIndex )
        {
            _playerIndex = playerIndex;
            Current = Microsoft.Xna.Framework.Input.GamePad.GetState ( _playerIndex );
        }

        #endregion

        #region Methods

        /// <summary>Retrieves the current state of the game pad</summary>
        /// <returns>The current state of the game pad</returns>
        public override GamePadState GetState()
        {
            return Current;
        }

        /// <summary>Retrieves the current DirectInput joystick state</summary>
        /// <returns>The current state of the DirectInput joystick</returns>
        public override ExtendedGamePadState GetExtendedState()
        {
            return new ExtendedGamePadState ( ref _current );
        }


        /// <summary>Takes a snapshot of the current state of the input device</summary>
        /// <remarks>
        ///   This snapshot will be queued until the user calls the Update() method,
        ///   where the next polled snapshot will be taken from the queue and provided
        ///   to the user.
        /// </remarks>
        public override void TakeSnapshot()
        {
        }

        /// <summary>
        /// Check if a key was released this state that was down last
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool CurrentButtonReleased( Buttons button )
        {
            return Current.IsButtonUp ( button ) && Previous.IsButtonDown ( button );
        }

        /// <summary>
        /// Check if a key was down last state and up this one
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool CurrentButtonPressed( Buttons button )
        {
            return Current.IsButtonDown ( button ) && Previous.IsButtonUp ( button );
        }

        /// <summary>
        /// Check if key was down last state
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool ButtonDownPrevious( Buttons button )
        {
            return Previous.IsButtonDown ( button );
        }

        /// <summary>
        /// Check if key was up last state
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool ButtonUpPrevious( Buttons button )
        {
            return Previous.IsButtonUp ( button );
        }

        /// <summary>
        /// Return a full list of all keys pressed since last state
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Buttons> GetButtonsPressed()
        {
            return ( from f in typeof( Buttons ).GetFields ()
                             where f.IsLiteral
                             where
                                 CurrentButtonPressed ( ( Buttons )Enum.Parse ( typeof( Buttons ), f.Name, true ) ) &&
                                 ButtonUpPrevious ( ( Buttons )Enum.Parse ( typeof( Buttons ), f.Name, true ) )
                             select ( Buttons )Enum.Parse ( typeof( Buttons ), f.Name, true ) ).ToList ();
        }

        /// <summary>
        /// Return a full list of all keys released since last state
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Buttons> GetButtonsReleased()
        {
            return ( from f in typeof( Buttons ).GetFields ()
                             where f.IsLiteral
                             where
                                 CurrentButtonReleased ( ( Buttons )Enum.Parse ( typeof( Buttons ), f.Name, true ) ) &&
                                 ButtonDownPrevious ( ( Buttons )Enum.Parse ( typeof( Buttons ), f.Name, true ) )
                             select ( Buttons )Enum.Parse ( typeof( Buttons ), f.Name, true ) ).ToList ();
        }

        #endregion

        #region XNA Methods

        /// <summary>Updates the state of the input device</summary>
        /// <param name="gameTime"></param>
        /// <remarks>
        ///   <para>
        ///     If this method is called with no snapshots in the queue, it will take
        ///     an immediate snapshot and make it the current state. This way, you
        ///     can use the input devices without caring for the snapshot system if
        ///     you wish.
        ///   </para>
        ///   <para>
        ///     If this method is called while one or more snapshots are waiting in
        ///     the queue, this method takes the next snapshot from the queue and makes
        ///     it the current state.
        ///   </para>
        /// </remarks>
        public override void Update( GameTime gameTime )
        {
            #region Update States

            Previous = _current;
            _current = Microsoft.Xna.Framework.Input.GamePad.GetState ( _playerIndex );

            #endregion

            #region Handle Button Passing Events

            var buttons = GetButtonsPressed ();
            var buttonsReleased = GetButtonsReleased ();

            foreach ( var b in buttons )
                OnButtonPressed ( b );

            foreach ( var b in buttonsReleased )
                OnButtonReleased ( b );

            #endregion
        }

        #endregion

    }
}

