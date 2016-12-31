using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WMNW.Core.Input.Classes;
using WMNW.Core.Input.Devices.Interfaces;

namespace WMNW.Core.Input.Devices.Generic
{
    public class PcMouse :  IMouse
    {
        #region Delegates

        /// <summary>Fired when the mouse has been moved</summary>
        public event MouseMoveDelegate MouseMoved;

        /// <summary>Fired when one or more mouse buttons have been pressed</summary>
        public event MouseButtonDelegate MouseButtonPressed;

        /// <summary>Fired when one or more mouse buttons have been released</summary>
        public event MouseButtonDelegate MouseButtonReleased;

        /// <summary>Fired when the mouse wheel has been rotated</summary>
        public event MouseWheelDelegate MouseWheelRotated;

        /// <summary>
        /// Fired when one or more mouse buttons have been clicked
        /// </summary>
        public event MouseButtonDelegate MouseButtonClicked;

        #endregion

        #region Fields

        private bool _winForms;

        #endregion

        #region Properties

        public bool IsAttached
        {
            get
            {
                return true;
            }
        }

        public string Name
        {
            get
            {
                return "PC Mouse";
            }
        }

        protected MouseState Current
        {
            get;
            set;
        }

        protected MouseState Previous
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        public PcMouse ( bool winForms = false )
        {
            _winForms = winForms;
            Current = _winForms ? ControlMouse.GetState () : Mouse.GetState ();
        }

        #endregion

        #region Xna Methods

        #endregion

        #region Other Methods

        public void Update( GameTime gameTime )
        {
            #region Update State

            Previous = Current;
            Current = _winForms ? ControlMouse.GetState () : Mouse.GetState ();

            #endregion

            #region Handle Passing Events

            if ( new Vector2 ( Previous.X, Previous.Y ) != new Vector2 ( Current.X, Current.Y ) )
                OnMouseMoved ( Current.X, Current.Y );
            if ( Previous.ScrollWheelValue != Current.ScrollWheelValue )
                OnWheelRotated ( Current.ScrollWheelValue / 120, Current.X, Current.Y );

            var down = GetMouseButtonsDown ();
            if ( down.Length > 0 )
            {
                foreach ( var b in down )
                    OnButtonDown ( b, Current.X, Current.Y );
            }

            var released = GetMouseButtonsReleased ();
            if ( released.Length > 0 )
            {
                foreach ( var b in released )
                    OnButtonUp ( b, Current.X, Current.Y );
            }


            var pressed = GetMouseButtonsPressed ();
            if ( pressed.Length > 0 )
            {
                foreach ( var b in pressed )
                    OnButtonClicked ( b, Current.X, Current.Y );
            }

            #endregion
        }

        public MouseState GetState()
        {
            return Current;
        }

        public void MoveTo( float x, float y )
        {
            Mouse.SetPosition ( ( int )x, ( int )y );
        }

        public Vector2 MousePosition()
        {
            return new Vector2 ( Current.Position.X, Current.Position.Y );
        }

        #endregion

        #region Mouse Events


        /// <summary>Records a mouse button press in the event queue</summary>
        /// <param name="buttons">Buttons that have been pressed</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected void OnButtonDown( MouseButtons buttons, int x, int y )
        {
            if ( MouseButtonPressed != null )
                MouseButtonPressed ( buttons, x, y );
        }

        /// <summary>Records a mouse button release in the event queue</summary>
        /// <param name="buttons">Buttons that have been released</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected void OnButtonUp( MouseButtons buttons, int x, int y )
        {
            if ( MouseButtonReleased != null )
                MouseButtonReleased ( buttons, x, y );
        }

        /// <summary>Records a mouse wheel rotation in the event queue</summary>
        /// <param name="ticks">Ticks the mouse wheel has been rotated</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected void OnWheelRotated( float ticks, int x, int y )
        {
            if ( MouseWheelRotated != null )
                MouseWheelRotated ( ticks, x, y );
        }

        /// <summary>Records a mouse cursor movement in the event queue</summary>
        /// <param name="x">X coordinate the mouse cursor has been moved to</param>
        /// <param name="y">Y coordinate the mouse cursor has been moved to</param>
        protected void OnMouseMoved( float x, float y )
        {
            if ( MouseMoved != null )
                MouseMoved ( x, y );
        }

        /// <summary>
        /// Records a Mouse button and relitive position of the mouse
        /// </summary>
        /// <param name="buttons"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        protected void OnButtonClicked( MouseButtons buttons, int x, int y )
        {
            if ( MouseButtonClicked != null )
                MouseButtonClicked ( buttons, x, y );
        }

        #region General Mouse Methods

        /// <summary>
        /// Checks if the mouse was just released
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool ButtonReleased( MouseButtons button )
        {
            switch ( button )
            {
                case MouseButtons.Left:
                    return Current.LeftButton == ButtonState.Released &&
                    Previous.LeftButton == ButtonState.Pressed;
                case MouseButtons.Right:
                    return Current.RightButton == ButtonState.Released &&
                    Previous.RightButton == ButtonState.Pressed;
                case MouseButtons.Middle:
                    return Current.MiddleButton == ButtonState.Released &&
                    Previous.MiddleButton == ButtonState.Pressed;
                case MouseButtons.X1:
                    return Current.MiddleButton == ButtonState.Released &&
                    Previous.XButton1 == ButtonState.Pressed;
                case MouseButtons.X2:
                    return Current.MiddleButton == ButtonState.Released &&
                    Previous.XButton2 == ButtonState.Pressed;
            }

            return false;
        }

        /// <summary>
        /// Checks if the passed mouse button is being held down
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool ButtonDown( MouseButtons button )
        {
            switch ( button )
            {
                case MouseButtons.Left:
                    return Current.LeftButton == ButtonState.Pressed;
                case MouseButtons.Right:
                    return Current.RightButton == ButtonState.Pressed;
                case MouseButtons.Middle:
                    return Current.MiddleButton == ButtonState.Pressed;
                case MouseButtons.X1:
                    return Current.XButton1 == ButtonState.Pressed;
                case MouseButtons.X2:
                    return Current.XButton2 == ButtonState.Pressed;
            }

            return false;
        }

        /// <summary>
        /// Checks if the passed mouse button is up
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool ButtonUp( MouseButtons button )
        {
            switch ( button )
            {
                case MouseButtons.Left:
                    return Current.LeftButton == ButtonState.Released;
                case MouseButtons.Right:
                    return Current.RightButton == ButtonState.Released;
                case MouseButtons.Middle:
                    return Current.MiddleButton == ButtonState.Released;
                case MouseButtons.X1:
                    return Current.XButton1 == ButtonState.Released;
                case MouseButtons.X2:
                    return Current.XButton2 == ButtonState.Released;
            }

            return false;
        }

        /// <summary>
        /// Checks if the passed mouse button was just pressed down
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public bool ButtonPressed( MouseButtons button )
        {
            switch ( button )
            {
                case MouseButtons.Left:
                    return Current.LeftButton == ButtonState.Pressed &&
                    Previous.LeftButton == ButtonState.Released;
                case MouseButtons.Right:
                    return Current.RightButton == ButtonState.Pressed &&
                    Previous.RightButton == ButtonState.Released;
                case MouseButtons.Middle:
                    return Current.MiddleButton == ButtonState.Pressed &&
                    Previous.MiddleButton == ButtonState.Released;
                case MouseButtons.X1:
                    return Current.XButton1 == ButtonState.Pressed &&
                    Previous.XButton1 == ButtonState.Released;
                case MouseButtons.X2:
                    return Current.XButton1 == ButtonState.Pressed &&
                    Previous.XButton1 == ButtonState.Released;
            }

            return false;
        }

        /// <summary>
        /// Check if the passed mouse key is currently in the passed mouse state
        /// </summary>
        /// <param name="mouseKeyState"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool MouseCheck( KeyState mouseKeyState, MouseButtons key )
        {
            switch ( mouseKeyState )
            {
                case KeyState.KeyUp:
                    return ButtonUp ( key );
                case KeyState.KeyDown:
                    return ButtonDown ( key );
                case KeyState.KeyReleased:
                    return ButtonReleased ( key );
                case KeyState.KeyPressed:
                    return ButtonPressed ( key );
            }

            return false;
        }

        /// <summary>
        /// State of a key
        /// </summary>
        public enum KeyState : byte
        {
            KeyUp,
            KeyDown,
            KeyReleased,
            KeyPressed
        }


        #endregion

        #region X and Y Returns

        /// <summary>
        /// Gets the mouses position in the current frame
        /// </summary>
        /// <returns></returns>
        public Vector2 CurrentPosition()
        {
            return Current.Position.ToVector2 ();
        }

        /// <summary>
        /// Gets the mouses position in the last frame
        /// </summary>
        /// <returns></returns>
        public Vector2 PreviousPosition()
        {
            return Previous.Position.ToVector2 ();
        }

        /// <summary>
        /// Calculates the difference in positions from Previous frames and the Current frames mouse position
        /// </summary>
        /// <returns></returns>
        public Vector2 DifferencePosition()
        {
            return CurrentPosition () - PreviousPosition ();
        }

        #endregion

        #region Mass Mouse Methods

        /// <summary>
        /// Gets a list of all mouse buttons currently held down
        /// </summary>
        /// <returns></returns>
        public MouseButtons[] GetMouseButtonsDown()
        {
            var mb = new MouseButtons[0];
            var i = 0;

            if ( ButtonDown ( MouseButtons.Left ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.Left;
                i++;
            }
            if ( ButtonDown ( MouseButtons.Middle ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.Middle;
                i++;
            }
            if ( ButtonDown ( MouseButtons.Right ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.Right;
                i++;
            }
            if ( ButtonDown ( MouseButtons.X1 ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.X1;
                i++;
            }
            if ( ButtonDown ( MouseButtons.X2 ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.X2;
            }

            return mb;
        }

        /// <summary>
        /// Gets a list of all mouse buttons currently pressed
        /// </summary>
        /// <returns></returns>
        public MouseButtons[] GetMouseButtonsPressed()
        {
            var mb = new MouseButtons[0];
            var i = 0;

            if ( ButtonPressed ( MouseButtons.Left ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.Left;
                i++;
            }
            if ( ButtonPressed ( MouseButtons.Middle ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.Middle;
                i++;
            }
            if ( ButtonPressed ( MouseButtons.Right ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.Right;
                i++;
            }
            if ( ButtonPressed ( MouseButtons.X1 ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.X1;
                i++;
            }
            if ( ButtonPressed ( MouseButtons.X2 ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.X2;
            }

            return mb;
        }

        /// <summary>
        /// Gets a list of all mouse buttons currently released
        /// </summary>
        /// <returns></returns>
        public MouseButtons[] GetMouseButtonsReleased()
        {
            var mb = new MouseButtons[0];
            var i = 0;

            if ( ButtonReleased ( MouseButtons.Left ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.Left;
                i++;
            }
            if ( ButtonReleased ( MouseButtons.Middle ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.Middle;
                i++;
            }
            if ( ButtonReleased ( MouseButtons.Right ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.Right;
                i++;
            }
            if ( ButtonReleased ( MouseButtons.X1 ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.X1;
                i++;
            }
            if ( ButtonReleased ( MouseButtons.X2 ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.X2;
            }

            return mb;
        }

        /// <summary>
        /// Gets a list of all mouse buttons currently up
        /// </summary>
        /// <returns></returns>
        public MouseButtons[] GetMouseButtonsUp()
        {
            var mb = new MouseButtons[0];
            var i = 0;

            if ( ButtonUp ( MouseButtons.Left ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.Left;
                i++;
            }
            if ( ButtonUp ( MouseButtons.Middle ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.Middle;
                i++;
            }
            if ( ButtonUp ( MouseButtons.Right ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.Right;
                i++;
            }
            if ( ButtonUp ( MouseButtons.X1 ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.X1;
                i++;
            }
            if ( ButtonUp ( MouseButtons.X2 ) )
            {
                Array.Resize ( ref mb, i + 1 );
                mb [ i ] = MouseButtons.X2;
            }

            return mb;
        }

        /// <summary>
        /// Checks if Mouse Position is located inside the area sent
        /// </summary>
        /// <param name="area">Area of our Control</param>
        /// <returns>bool</returns>
        public bool IsInside( Rectangle area )
        {
            return area.Contains ( Current.Position );
        }

        #endregion

        #endregion
    }
}

