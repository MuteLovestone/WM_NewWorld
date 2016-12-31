using System;
using Microsoft.Xna.Framework.Input;
using WMNW.Core.Input.Classes;

namespace WMNW.Core.Input.Devices.Interfaces
{
    #region Delegates

    /// <summary>Delegate use to report presses and releases of game pad buttons</summary>
    /// <param name="buttons">Button or buttons that have been pressed or released</param>
    public delegate void GamePadButtonDelegate ( Buttons buttons );

    /// <summary>Delegate use to report presses and releases of game pad buttons</summary>
    /// <param name="buttons1">Button or buttons that have been pressed or released</param>
    /// <param name="buttons2">Button or buttons that have been pressed or released</param>
    public delegate void ExtendedGamePadButtonDelegate ( ulong buttons1, ulong buttons2 );

    #endregion

    /// <summary>Specialized input device for game pad-like controllers</summary>
    public interface IGamePad : IInputDevice
    {
        #region Delegates

        /// <summary>Called when one or more buttons on the game pad have been pressed</summary>
        event GamePadButtonDelegate ButtonPressed;
        /// <summary>Called when one or more buttons on the game pad have been released</summary>
        event GamePadButtonDelegate ButtonReleased;

        /// <summary>Called when one or more buttons on the game pad have been pressed</summary>
        event ExtendedGamePadButtonDelegate ExtendedButtonPressed;
        /// <summary>Called when one or more buttons on the game pad have been released</summary>
        event ExtendedGamePadButtonDelegate ExtendedButtonReleased;

        #endregion

        #region Properties

        /// <summary>Retrieves the current state of the game pad</summary>
        /// <returns>The current state of the game pad</returns>
        GamePadState GetState();

        /// <summary>Retrieves the current DirectInput joystick state</summary>
        /// <returns>The current state of the DirectInput joystick</returns>
        ExtendedGamePadState GetExtendedState();

        #endregion
    }
}

