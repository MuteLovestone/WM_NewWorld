using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WMNW.Core.Input.Classes;
using WMNW.Core.Input.Devices.Interfaces;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using System.Linq;
using System.Collections.Generic;

namespace WMNW.Core.Input.Devices.Generic
{
    public class PcKeyboard : IKeyboard
    {
        #region Delegates

        public event KeyDelegate KeyPressed;
        public event KeyDelegate KeyReleased;
        public event CharacterDelegate CharacterEntered;

        #endregion

        #region Fields

        /// <summary>
        ///  Timer that holds how long it was since our key was held down
        /// </summary>
        double _repeatUpdateTimer = 0;

        /// <summary>
        /// Timer that holds how long it was sicne our last quick update
        /// </summary>
        double _quickUpdateTimer = 0;

        const int RepeatStandard = 300;
        const int QuickRepeatStandard = 170;

        private bool _winForms;

        #endregion

        #region Properties

        protected KeyboardState Current
        {
            get;
            set;
        }

        protected KeyboardState Previous
        {
            get;
            set;
        }

        public bool CapsLock
        {
            get;
            set;
        }

        public bool ScrollLock
        {
            get;
            set;
        }

        public bool NumLock
        {
            get;
            set;
        }

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
                return "PC Keyboard";
            }
        }

        public KeyboardState GetState()
        {
            return Current;
        }

        #endregion

        #region Constructor

        public PcKeyboard ( bool winForms = false )
        {
            _winForms = winForms;
            Current = _winForms ? ControlKeyboard.GetState () : Keyboard.GetState ();

            #if WINDOWS
            CapsLock = Control.IsKeyLocked((System.Windows.Forms.Keys)Keys.CapsLock);
            ScrollLock = Control.IsKeyLocked((System.Windows.Forms.Keys)Keys.Scroll);
            NumLock = Control.IsKeyLocked((System.Windows.Forms.Keys)Keys.NumLock);
            #endif
        }

        #endregion

        #region Xna Methods

        public void Update( GameTime gameTime )
        {
            #region Manage WinForms

            if ( _winForms )
            {
                if ( Control.ModifierKeys == System.Windows.Forms.Keys.Control )
                    ControlKeyboard.Add ( Keys.LeftControl );
                else
                    ControlKeyboard.Remove ( Keys.RightControl );

                if ( Control.ModifierKeys == System.Windows.Forms.Keys.Alt )
                    ControlKeyboard.Add ( Keys.LeftAlt );
                else
                    ControlKeyboard.Remove ( Keys.LeftAlt );

                if ( Control.ModifierKeys == System.Windows.Forms.Keys.Shift )
                    ControlKeyboard.Add ( Keys.LeftShift );
                else
                    ControlKeyboard.Remove ( Keys.RightControl );
            }

            #endregion

            #region Update State

            Previous = Current;
            Current = _winForms ? ControlKeyboard.GetState () : Keyboard.GetState ();

            #endregion

            #region Handle Passing Events

            var keys = GetKeysPressed ();
            var downKeys = GetKeysDown ();

            #region Alter Specials

            if ( keys.Contains ( Keys.CapsLock ) )
                CapsLock = !CapsLock;
            if ( keys.Contains ( Keys.Scroll ) )
                ScrollLock = !ScrollLock;
            if ( keys.Contains ( Keys.NumLock ) )
                NumLock = !NumLock;


            #endregion

            #region Handle Quick Repeats

            //Handles Updating Keys that should run quicker than others
            if ( downKeys.Contains ( Keys.Back ) || downKeys.Contains ( Keys.Delete ) || downKeys.Contains ( Keys.Left ) || downKeys.Contains ( Keys.Right ) )
            {
                _quickUpdateTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

                if ( _quickUpdateTimer > QuickRepeatStandard )
                {
                    #region Keys Down

                    if ( downKeys.Contains ( Keys.Back ) )
                        OnCharacterEntered ( Keys.Back );

                    if ( downKeys.Contains ( Keys.Delete ) )
                        OnCharacterEntered ( Keys.Delete );

                    if ( downKeys.Contains ( Keys.Left ) )
                        OnCharacterEntered ( Keys.Left );

                    if ( downKeys.Contains ( Keys.Right ) )
                        OnCharacterEntered ( Keys.Right );

                    #endregion

                    #region Keys Pressed

                    if ( downKeys.Contains ( Keys.Delete ) )
                        OnKeyPressed ( Keys.Delete );

                    if ( downKeys.Contains ( Keys.Left ) )
                        OnKeyPressed ( Keys.Left );

                    if ( downKeys.Contains ( Keys.Right ) )
                        OnKeyPressed ( Keys.Right );

                    #endregion

                    //Reset Timer
                    _quickUpdateTimer = 0;
                }
            }

            #endregion

            if ( keys.Count () > 0 )
            {
                #region Handle Individual Key Presses For Character Entered

                foreach ( var k in keys )
                    OnCharacterEntered ( k );

                //Reset our repeat timer
                _repeatUpdateTimer = 0;
                _quickUpdateTimer = 0;

                #endregion
            }
            if ( downKeys.Count () != 0 )
            {
                #region Handle Keys Down

                //Add our game time since last update to our repeat timer
                _repeatUpdateTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

                //And if we have reached our repeat standard then write the key
                if ( _repeatUpdateTimer > RepeatStandard )
                {
                    foreach ( var k in downKeys )
                        OnCharacterEntered ( k );

                    //Reset our repeat timer
                    _repeatUpdateTimer = 0;
                }

                #endregion
            }

            #region Send All Released Keys

            foreach ( var key in GetKeysReleased() )
                OnKeyReleased ( key );

            #endregion

            #region Send All Pressed Keys

            foreach ( var key in GetKeysDownNow() )
                OnKeyPressed ( key );

            #endregion

            #endregion
        }

        #endregion

        #region Keyboard Events

        /// <summary>Records a key press in the event queue</summary>
        /// <param name="key">Key that has been pressed</param>
        protected void OnKeyPressed( Keys key )
        {
            if ( KeyPressed != null )
                KeyPressed ( key );
        }

        /// <summary>Records a key release in the event queue</summary>
        /// <param name="key">Key that has been released</param>
        protected void OnKeyReleased( Keys key )
        {
            if ( KeyReleased != null )
                KeyReleased ( key );
        }

        /// <summary>Records a character in the event queue</summary>
        /// <param name="character">Character that has been entered</param>
        protected void OnCharacterEntered( Keys character )
        {
            if ( CharacterEntered != null )
                CharacterEntered ( character );
        }

        #endregion

        #region General Keyboard Methods

        /// <summary>
        /// Check if a key was released this state that was down last
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ButtonReleased( Keys key )
        {
            return Current.IsKeyUp ( key ) && Previous.IsKeyDown ( key );
        }

        /// <summary>
        /// Check if a key was down last state and up this one
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ButtonPressed( Keys key )
        {
            return Current.IsKeyDown ( key ) && Previous.IsKeyUp ( key );
        }

        /// <summary>
        /// Check if a key is being held down
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ButtonDown( Keys key )
        {
            return Current.IsKeyDown ( key );
        }

        /// <summary>
        /// Check if a key is up
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ButtonUp( Keys key )
        {
            return Current.IsKeyUp ( key );
        }

        /// <summary>
        /// Check if key was down last state
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ButtonDownPrevious( Keys key )
        {
            return Previous.IsKeyDown ( key );
        }

        /// <summary>
        /// Check if key was up last state
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ButtonUpPrevious( Keys key )
        {
            return Previous.IsKeyUp ( key );
        }

        /// <summary>
        /// Return a full list of all keys currently held down
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Keys> GetKeysDown()
        {
            return ( from f in typeof( Keys ).GetFields ()
                             where f.IsLiteral
                             where
                                 ButtonDown ( ( Keys )Enum.Parse ( typeof( Keys ), f.Name, true ) ) &&
                                 ButtonDownPrevious ( ( Keys )Enum.Parse ( typeof( Keys ), f.Name, true ) )
                             select ( Keys )Enum.Parse ( typeof( Keys ), f.Name, true ) ).ToList ();
        }

        /// <summary>
        /// Return a full list of all keys that went down this state
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Keys> GetKeysDownNow()
        {
            return ( from f in typeof( Keys ).GetFields ()
                             where f.IsLiteral
                             where
                                 ButtonDown ( ( Keys )Enum.Parse ( typeof( Keys ), f.Name, true ) ) &&
                                 ButtonUpPrevious ( ( Keys )Enum.Parse ( typeof( Keys ), f.Name, true ) )
                             select ( Keys )Enum.Parse ( typeof( Keys ), f.Name, true ) ).ToList ();
        }


        /// <summary>
        /// Return a full list of all keys pressed since last state
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Keys> GetKeysPressed()
        {
            return ( from f in typeof( Keys ).GetFields ()
                             where f.IsLiteral
                             where
                                 ButtonPressed ( ( Keys )Enum.Parse ( typeof( Keys ), f.Name, true ) ) &&
                                 ButtonUpPrevious ( ( Keys )Enum.Parse ( typeof( Keys ), f.Name, true ) )
                             select ( Keys )Enum.Parse ( typeof( Keys ), f.Name, true ) ).ToList ();
        }

        /// <summary>
        /// Return a full list of all keys released since last state
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Keys> GetKeysReleased()
        {
            return ( from f in typeof( Keys ).GetFields ()
                             where f.IsLiteral
                             where
                                 ButtonReleased ( ( Keys )Enum.Parse ( typeof( Keys ), f.Name, true ) ) &&
                                 ButtonDownPrevious ( ( Keys )Enum.Parse ( typeof( Keys ), f.Name, true ) )
                             select ( Keys )Enum.Parse ( typeof( Keys ), f.Name, true ) ).ToList ();
        }

        /// <summary>
        /// Checks if the passed Key equals the passed State 
        /// </summary>
        /// <param name="keyboardKeyState"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool KeyCheck( KeyState keyboardKeyState, Keys key )
        {
            switch ( keyboardKeyState )
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

        #region Get Odd Keys

        public bool IsShiftDown()
        {
            return ButtonDown ( Keys.LeftShift ) || ButtonDown ( Keys.RightShift );
        }

        public bool IsAltDown()
        {
            return ButtonDown ( Keys.LeftAlt ) || ButtonDown ( Keys.RightAlt );
        }

        public bool IsControlDown()
        {
            return ButtonDown ( Keys.LeftControl ) || ButtonDown ( Keys.RightControl );
        }

        #endregion


        #region Convert Keys to String

        /// <summary>
        /// Converts a sent key into a string value and returns it
        /// </summary>
        /// <param name="myKey"></param>
        /// <returns></returns>
        public string ConvertKeys( Keys myKey )
        {
            string key = null;
            var shift = ( CapsLock && !IsShiftDown () ) || ( !CapsLock ) && IsShiftDown ();

            switch ( myKey )
            {
            //Alphabet keys
                case Keys.A:
                    key = shift ? "A" : "a";
                    break;
                case Keys.B:
                    key = shift ? "B" : "b";
                    break;
                case Keys.C:
                    key = shift ? "C" : "c";
                    break;
                case Keys.D:
                    key = shift ? "D" : "d";
                    break;
                case Keys.E:
                    key = shift ? "E" : "e";
                    break;
                case Keys.F:
                    key = shift ? "F" : "f";
                    break;
                case Keys.G:
                    key = shift ? "G" : "g";
                    break;
                case Keys.H:
                    key = shift ? "H" : "h";
                    break;
                case Keys.I:
                    key = shift ? "I" : "i";
                    break;
                case Keys.J:
                    key = shift ? "J" : "j";
                    break;
                case Keys.K:
                    key = shift ? "K" : "k";
                    break;
                case Keys.L:
                    key = shift ? "L" : "l";
                    break;
                case Keys.M:
                    key = shift ? "M" : "m";
                    break;
                case Keys.N:
                    key = shift ? "N" : "n";
                    break;
                case Keys.O:
                    key = shift ? "O" : "o";
                    break;
                case Keys.P:
                    key = shift ? "P" : "p";
                    break;
                case Keys.Q:
                    key = shift ? "Q" : "q";
                    break;
                case Keys.R:
                    key = shift ? "R" : "r";
                    break;
                case Keys.S:
                    key = shift ? "S" : "s";
                    break;
                case Keys.T:
                    key = shift ? "T" : "t";
                    break;
                case Keys.U:
                    key = shift ? "U" : "u";
                    break;
                case Keys.V:
                    key = shift ? "V" : "v";
                    break;
                case Keys.W:
                    key = shift ? "W" : "w";
                    break;
                case Keys.X:
                    key = shift ? "X" : "x";
                    break;
                case Keys.Y:
                    key = shift ? "Y" : "y";
                    break;
                case Keys.Z:
                    key = shift ? "Z" : "z";
                    break;

            //Decimal keys
                case Keys.D0:
                    key = IsShiftDown () ? ")" : "0";
                    break;
                case Keys.D1:
                    key = IsShiftDown () ? "!" : "1";
                    break;
                case Keys.D2:
                    key = IsShiftDown () ? "@" : "2";
                    break;
                case Keys.D3:
                    key = IsShiftDown () ? "#" : "3";
                    break;
                case Keys.D4:
                    key = IsShiftDown () ? "$" : "4";
                    break;
                case Keys.D5:
                    key = IsShiftDown () ? "%" : "5";
                    break;
                case Keys.D6:
                    key = IsShiftDown () ? "^" : "6";
                    break;
                case Keys.D7:
                    key = IsShiftDown () ? "&" : "7";
                    break;
                case Keys.D8:
                    key = IsShiftDown () ? "*" : "8";
                    break;
                case Keys.D9:
                    key = IsShiftDown () ? "(" : "9";
                    break;

            //Decimal numpad keys
                case Keys.NumPad0:
                    if ( !NumLock )
                        break;
                    key = "0";
                    break;
                case Keys.NumPad1:
                    if ( !NumLock )
                        break;
                    key = "1";
                    break;
                case Keys.NumPad2:
                    if ( !NumLock )
                        break;
                    key = "2";
                    break;
                case Keys.NumPad3:
                    if ( !NumLock )
                        break;
                    key = "3";
                    break;
                case Keys.NumPad4:
                    if ( !NumLock )
                        break;
                    key = "4";
                    break;
                case Keys.NumPad5:
                    if ( !NumLock )
                        break;
                    key = "5";
                    break;
                case Keys.NumPad6:
                    if ( !NumLock )
                        break;
                    key = "6";
                    break;
                case Keys.NumPad7:
                    if ( !NumLock )
                        break;
                    key = "7";
                    break;
                case Keys.NumPad8:
                    if ( !NumLock )
                        break;
                    key = "8";
                    break;
                case Keys.NumPad9:
                    if ( !NumLock )
                        break;
                    key = "9";
                    break;

            //Special keys
                case Keys.OemTilde:
                    key = shift ? "~" : "`";
                    break;
                case Keys.OemSemicolon:
                    key = shift ? ":" : ";";
                    break;
                case Keys.OemQuotes:
                    key = shift ? "\"" : "\'";
                    break;
                case Keys.OemQuestion:
                    key = shift ? "?" : "/";
                    break;
                case Keys.OemPlus:
                    key = shift ? "+" : "=";
                    break;
                case Keys.OemPipe:
                    key = shift ? "|" : "\\";
                    break;
                case Keys.OemPeriod:
                    key = shift ? ">" : ".";
                    break;
                case Keys.OemOpenBrackets:
                    key = shift ? "{" : "[";
                    break;
                case Keys.OemCloseBrackets:
                    key = shift ? "}" : "]";
                    break;
                case Keys.OemMinus:
                    key = shift ? "_" : "-";
                    break;
                case Keys.OemComma:
                    key = shift ? "<" : ",";
                    break;
                case Keys.Space:
                    key = " ";
                    break;
                case Keys.Multiply:
                    key = "*";
                    break;
                case Keys.Add:
                    key = "+";
                    break;
                case Keys.Decimal:
                    key = ".";
                    break;
            }

            return key;
        }

        #endregion
    }
}

