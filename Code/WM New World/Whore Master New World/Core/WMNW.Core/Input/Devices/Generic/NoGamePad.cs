using System;
using WMNW.Core.Input.Devices.Interfaces;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using WMNW.Core.Input.Classes;

namespace WMNW.Core.Input.Devices.Generic
{
    /// <summary>Dummy that takes the place of unfilled player slots</summary>
    internal class NoGamePad : IGamePad
    {
        #region Delegates

        /// <summary>Called when one or more buttons on the game pad have been pressed</summary>
        public event GamePadButtonDelegate ButtonPressed { add { } remove { }
        }

        /// <summary>Called when one or more buttons on the game pad have been released</summary>
        public event GamePadButtonDelegate ButtonReleased { add { } remove { }
        }

        /// <summary>Called when one or more buttons on the game pad have been pressed</summary>
        public event ExtendedGamePadButtonDelegate ExtendedButtonPressed { add { } remove { }
        }

        /// <summary>Called when one or more buttons on the game pad have been released</summary>
        public event ExtendedGamePadButtonDelegate ExtendedButtonReleased { add { } remove { }
        }

        #endregion

        #region Fields

        /// <summary>Neutral axis states for the extended game pad state</summary>
        private float[/*24*/] _axes;
        /// <summary>Neutral slider states for the extended game pad state</summary>
        private float[/*8*/] _sliders;
        /// <summary>Neutral button states for the extended game pad state</summary>
        private bool[/*128*/] _buttons;
        /// <summary>Neutral PoV controller states for the extended game pad state</summary>
        private int[/*4*/] _povs;

        #endregion

        #region Properties

        /// <summary>Retrieves the current state of the game pad</summary>
        /// <returns>The current state of the game pad</returns>
        public GamePadState GetState()
        {
            return new GamePadState ();
        }

        /// <summary>Retrieves the current DirectInput joystick state</summary>
        /// <returns>The current state of the DirectInput joystick</returns>
        public ExtendedGamePadState GetExtendedState()
        {
            return new ExtendedGamePadState (
                0, _axes,
                0, _sliders,
                0, _buttons,
                0, _povs
            );
        }

        /// <summary>Whether the input device is connected to the system</summary>
        public bool IsAttached
        {
            get
            {
                return false;
            }
        }

        /// <summary>Human-readable name of the input device</summary>
        public string Name
        {
            get
            {
                return "No game pad installed";
            }
        }

        #endregion

        #region Constructor

        /// <summary>Initializes a new game pad dummy</summary>
        public NoGamePad ()
        {
            _axes = new float[24];
            _sliders = new float[8];
            _buttons = new bool[128];
            _povs = new int[] { -1, -1, -1, -1 };
        }

        #endregion

        #region Xna Methods

        /// <summary>Updates the state of the input device</summary>
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
        public void Update( GameTime gameTime )
        {
        }

        #endregion

        #region Other Methods

        /// <summary>Takes a snapshot of the current state of the input device</summary>
        /// <remarks>
        ///   This snapshot will be queued until the user calls the Update() method,
        ///   where the next polled snapshot will be taken from the queue and provided
        ///   to the user.
        /// </remarks>
        public void TakeSnapshot()
        {
        }

        #endregion
    }
}

