using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using WMNW.Core.Input.Classes;
using WMNW.Core.GraphicX;

namespace WMNW.Core.GUI.Controls
{
    public class TextBox:ControlBase
    {
        #region Variables

        private bool _focused = false;

        private int _cursorPosition;
        private bool _cursorVisible = false;
        private double _cursorTimer = 0;
        private const int CursorRepeat = 600;

        private Vector2 _fontSpacing;

        private Rectangle _cursorRectangle;

        #endregion

        #region Properties

        public Color CursorColor;

        #endregion

        #region Consturctor

        public TextBox ( string font, Color color, Vector2 position, Vector2 size )
        {
            Font = font;
            TextColor = color;
            CursorColor = color;
            Position = position;

            Size = size;
            Text = "Click here to begin typeing...";
            _cursorPosition = Text.Length;
        }

        #endregion

        #region Xna Methods

        public override void Update( GameTime gameTime )
        {
            base.Update ( gameTime );

            //Update Our Visible Text
            UpdateVisibleText ();

            //Ensure we cannot move beyond our Text's length
            if ( _cursorPosition > Text.Length )
                _cursorPosition = Text.Length;

            //If mouse is inside and clicks our control it gains focus
            if ( Selected )
            {
                if ( Mouse.IsInside ( Bounds ) )
                {
                    if ( Mouse.ButtonPressed ( MouseButtons.Left ) )
                    {
                        _focused = true;
                        Keyboard.CharacterEntered -= KeyboardOnCharacterEntered;
                        Keyboard.CharacterEntered += KeyboardOnCharacterEntered;
                    }
                }
                else if ( !Mouse.IsInside ( Bounds ) )
                {//Deactivate our control and unsubscribe from keyboard event
                    if ( Mouse.GetMouseButtonsPressed ().Count () >= 1 )
                    {
                        Keyboard.CharacterEntered -= KeyboardOnCharacterEntered;

                        _cursorTimer = 0;
                        _cursorVisible = false;
                        _focused = false;
                    }
                }
            }

            #region Handle Cursor and Selection Position when drawing

            if ( String.IsNullOrEmpty ( Text ) )
                return;

            //Determine our real cursor position and selected start
            var tempText = Text.Replace ( _visibleText, "" );

            _fontSpacing = Vector2.Zero;

            if ( _cursorVisible )
            {            

                //If real position is between 0 and visible text length
                if ( _cursorPosition - tempText.Length > 0 && _cursorPosition - tempText.Length <= _visibleText.Length )
                    _fontSpacing = GraphicsHandler.MesureString ( Font, _visibleText.Substring ( 0, _cursorPosition - tempText.Length ) );

                _cursorRectangle = new Rectangle ( ( int )Position.X + ( int )_fontSpacing.X, ( int )Position.Y,
                    ( int )GraphicsHandler.GetFont ( Font ).Spacing == 0 ? 2 : ( int )GraphicsHandler.GetFont ( Font ).Spacing,
                    ( int )_fontSpacing.Y == 0 ? 20 : ( int )_fontSpacing.Y );
            }

            #endregion

            #region Handle Cursor Timer

            if ( !_focused )
                return;

            _cursorTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if ( _cursorTimer > CursorRepeat )
            {
                _cursorVisible = !_cursorVisible;
                _cursorTimer = 0;
            }

            #endregion

            if ( !Selected )
                _cursorVisible = false;
        }

        //Start Point always the same = None
        //Switch based on

        private void KeyboardOnCharacterEntered( Keys character )
        {
            if ( !Selected )
                return;
            switch ( character )
            {
                #region Key.Back
                case Keys.Back:
                    ResetCursorTimer ();
                    //Do not allow back to work if no text exists
                    if ( _cursorPosition == 0 )
                        break;

                    Text = Text.Remove ( _cursorPosition - 1, 1 );

                    if ( _cursorPosition - 1 == Text.Length )
                        _cursorPosition = Text.Length;
                    else
                        _cursorPosition = _cursorPosition - 1;

                    break;
                    #endregion
                    #region Key.Delete
                case Keys.Delete:
                    ResetCursorTimer ();
                    //Do not allow delete to work if at end of text
                    if ( Text.Length == _cursorPosition )
                        break;

                    Text = Text.Remove ( _cursorPosition, 1 );
                    break;
                    #endregion
                    #region Key.Left
                case Keys.Left:
                    ResetCursorTimer ();

                    if ( _visibleStartPos < _cursorPosition )
                    {
                        _cursorPosition = _cursorPosition - 1;
                        if ( _cursorPosition <= 0 )
                            _cursorPosition = 0;
                    }
                    break;
                    #endregion
                    #region Key.Right
                case Keys.Right:
                    ResetCursorTimer ();

                    _cursorPosition = _cursorPosition + 1;
                    if ( _cursorPosition >= Text.Length )
                        _cursorPosition = Text.Length;
                    break;
                    #endregion
                    #region Character Keys
                default:
                    var s = Keyboard.ConvertKeys ( character );

                    if ( s == null )
                        break;

                    ResetCursorTimer ();
                    Text = Text.Insert ( _cursorPosition, s );

                    if ( _cursorPosition + 1 == Text.Length )
                        _cursorPosition = Text.Length;
                    else
                        _cursorPosition = _cursorPosition + 1;
                    break;
                    #endregion
            }
        }

        private int _visibleStartPos = 0;

        private void UpdateVisibleText()
        {
            //If our text is larger than our box...
            if ( GraphicsHandler.MesureString ( Font, Text ).X >= Size.X )
            {
                var tempText = "";
                var num = Text.Length - 1;

                for ( var i = 0; i < Size.X; )
                {
                    tempText = tempText.Insert ( 0, Text.ToCharArray () [ num ].ToString () );
                    num = num - 1;

                    i = ( int )GraphicsHandler.MesureString ( Font, tempText ).X;
                }

                _visibleText = tempText;
                _visibleStartPos = num;
            }
            else
            {
                _visibleText = Text;
                _visibleStartPos = 0;
            }
        }

        private string _visibleText;

        /// <summary>
        /// Resets our timer to 0 and sets it to true 
        /// </summary>
        private void ResetCursorTimer()
        {
            _cursorTimer = 0;
            _cursorVisible = true;
        }


        public override void Draw( GameTime gameTime )
        {
            base.Draw ( gameTime );

            //Draw Text
            GraphicsHandler.DrawString ( Font, _visibleText, Position + TextOffset, TextColor );

            //Draw Cursor
            if ( _cursorVisible )
                GraphicsHandler.DrawFillRectangle ( _cursorRectangle, CursorColor );
        }

        #endregion
    }
}

