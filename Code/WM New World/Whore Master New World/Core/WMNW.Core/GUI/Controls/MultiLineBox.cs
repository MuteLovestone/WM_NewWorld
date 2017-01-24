using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;

namespace WMNW.Core.GUI.Controls
{
    public class MultiLineBox:ControlBase
    {
        #region Properties

        List<string> _multiLines = new List<string> ();
        TrackBar scrollBar;
        int offset = 0;

        #endregion

        #region Properties

        private Vector2 TextSize
        {
            get
            {
                return Size - new Vector2 ( 10, 0 );
            }
        }

        #endregion

        #region Consturctor

        public MultiLineBox ( string font, string text, Color color, Vector2 position, Vector2 size )
        {
            Font = font;
            Text = text;
            TextColor = color;
            Position = position;
            Size = size;
            scrollBar = new TrackBar ( new Vector2 ( position.X + size.X - 12, position.Y + 2 ), new Vector2 ( 10, size.Y - 4 ), Color.Gold, Vector2.Zero, "", Stance.Vertical );
            scrollBar.OnValueChanged += ScrollBar_OnValueChanged;
            Children.Add ( scrollBar );
        }

        void ScrollBar_OnValueChanged( object sender, EventArgs e )
        {
            offset = ( ( TrackBar )sender ).Value;
        }

        #endregion

        #region Xna Methods

        public override void Update( GameTime gameTime )
        {
            if ( !Enabled )
                return;
            base.Update ( gameTime );
            UpdateChildren ( gameTime );
            if ( !TextChanged && !FontChanged )
                return;
            FontChanged = false;
            TextChanged = false;

            //TASK: Update to use a ScrollBar

            //Mesure how long our string can be hieght wise for performance
            var i = ( int )( ( Size.Y ) / GraphicsHandler.MesureString ( Font, Text.Replace ( "\n", "" ) ).Y );
            #region Process Lines
            bool isProccessing = true;
            bool doNextLine = false;
            do
            {
                doNextLine = false;
                string changingText =
                    _multiLines.Where ( multiLine => multiLine != "" ).Aggregate ( Text, (current, multiLine ) => current.Substring ( multiLine.Length ) );
                if ( changingText.Contains ( "\n" ) )
                {
                    //get index of our value
                    int index = changingText.IndexOf ( "\n" );
                    //remove our text after \n
                    if ( index > 0 )
                        changingText = changingText.Substring ( 0, index + 1 );
                    changingText = FitToScreen ( changingText );
                    _multiLines.Add ( changingText );
                }
                else
                {
                    do
                    {
                        changingText = changingText.Substring ( 0, changingText.LastIndexOf ( " " ) < 0 ? 0 : changingText.LastIndexOf ( " " ) );

                        //Mesure our X value of our changing text
                        var fontMesure = GraphicsHandler.MesureString ( Font, changingText ).X;

                        //If our font is bigger than 
                        if ( fontMesure < TextSize.X )
                        {
                            _multiLines.Add ( changingText );
                            doNextLine = true;
                        }
                        if ( fontMesure == 0 )
                        {
                            isProccessing = false;
                            doNextLine = true;
                        }
                    }
                    while( !doNextLine );
                }
            }
            while ( isProccessing );
            if ( _multiLines.Count > i )
            {
                scrollBar.MaximumValue = _multiLines.Count - i;
                scrollBar.Enabled = true;
            }
            else
                scrollBar.Enabled = false;
            #endregion
            #region Format Lines
            for ( int c = 0; c < _multiLines.Count; c++ )
            {
                _multiLines [ c ] = _multiLines [ c ].Replace ( "\n", "" );
            }
            #endregion
        }

       
        private string FitToScreen( string changingText )
        {
            var fontMesure = GraphicsHandler.MesureString ( Font, changingText ).X;
            if ( fontMesure == 0 )
                return "";
            while ( fontMesure > TextSize.X )
            {
                changingText = changingText.Substring ( 0, changingText.LastIndexOf ( " " ) < 0 ? 0 : changingText.LastIndexOf ( " " ) );
                fontMesure = GraphicsHandler.MesureString ( Font, changingText ).X;
            }
            if ( fontMesure == 0 )
                return "";
            else
                return changingText;
        }

        public override void Draw( GameTime gameTime )
        {
            if ( !Enabled )
                return;
            base.Draw ( gameTime );

            // GraphicsHandler.DrawString(Font, Text, Position + TextOffset, TextColor);

            for ( var index = 0; index < _multiLines.Count; index++ )
            {
                if ( index < offset )
                    continue;
                //Draw each one
                var multiLine = _multiLines [ index ];
                Vector2 textSize = GraphicsHandler.MesureString ( Font, _multiLines [ index ] );
                Vector2 pos = Position + new Vector2 ( 0, ( index - offset ) * textSize.Y ) + TextOffset;
                if ( pos.Y + textSize.Y > Position.X + Size.Y )
                    continue;
                //Determine where we draw our string by adding to position + where we are at in the loop times the size of font height
                GraphicsHandler.DrawString ( Font, multiLine, pos, TextColor );
            }
        }

        public override void ChangeColor( Color? border, Color? background )
        {
            base.ChangeColor ( border, background );
            scrollBar.ChangeColor ( border, background );
        }

        #endregion
    }
}

