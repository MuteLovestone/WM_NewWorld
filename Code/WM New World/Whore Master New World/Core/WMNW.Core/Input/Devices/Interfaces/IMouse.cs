using System;
using Microsoft.Xna.Framework.Input;
using WMNW.Core.Input.Classes;

namespace WMNW.Core.Input.Devices.Interfaces
{
    #region Delegates

    /// <summary>Delegate used to report movement of the mouse cursor</summary>
    /// <param name="x">New X coordinate of the mouse cursor</param>
    /// <param name="y">New Y coordinate of the mouse cursor</param>
    public delegate void MouseMoveDelegate ( float x, float y );

    /// <summary>
    ///   Delegate used to report a press or released of one or more mouse buttons
    /// </summary>
    /// <param name="buttons">Button or buttons that have been pressed or released</param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public delegate void MouseButtonDelegate ( MouseButtons buttons, int x, int y );

    /// <summary>Delegate used to report a rotation of the mouse wheel</summary>
    /// <param name="ticks">Number of ticks the mouse wheel has been rotated</param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public delegate void MouseWheelDelegate ( float ticks, int x, int y );

    #endregion

    /// <summary>Specializd input devices for mouse-like controllers</summary>
    public interface IMouse : IInputDevice
    {
        #region Delegates

        /// <summary>Fired when the mouse has been moved</summary>
        event MouseMoveDelegate MouseMoved;

        /// <summary>Fired when one or more mouse buttons have been pressed</summary>
        event MouseButtonDelegate MouseButtonPressed;

        /// <summary>Fired when one or more mouse buttons have been released</summary>
        event MouseButtonDelegate MouseButtonReleased;

        /// <summary>Fired when the mouse wheel has been rotated</summary>
        event MouseWheelDelegate MouseWheelRotated;

        /// <summary>
        /// Fired when one or more mouse buttons have been clicked
        /// </summary>
        event MouseButtonDelegate MouseButtonClicked;

        #endregion

        #region Other Methds

        /// <summary>
        /// Retrieves the State of our Mouse
        /// </summary>
        /// <returns></returns>
        MouseState GetState();

        /// <summary>Moves the mouse cursor to the specified location</summary>
        /// <param name="x">New X coordinate of the mouse cursor</param>
        /// <param name="y">New Y coordinate of the mouse cursor</param>
        void MoveTo( float x, float y );

        #endregion

    }
}

