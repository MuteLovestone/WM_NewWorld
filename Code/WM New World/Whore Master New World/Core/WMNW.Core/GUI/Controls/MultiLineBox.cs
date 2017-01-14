using System;
using System.Linq;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;

namespace WMNW.Core.GUI.Controls
{
    public class MultiLineBox:ControlBase
    {
        #region Properties

        private string[] _multiLineString;

        #endregion

        #region Consturctor

        public MultiLineBox ( string font, string text, Color color, Vector2 position, Vector2 size )
        {
            Font = font;
            Text = text;
            TextColor = color;
            Position = position;

            Size = size;
        }

        #endregion

        #region Xna Methods

        public override void Update( GameTime gameTime )
        {
            base.Update ( gameTime );
            if ( !TextChanged && !FontChanged )
                return;
            FontChanged = false;
            TextChanged = false;
            //Mesure how long our string can be hieght wise for performance
            var i = ( int )( ( Size.Y ) / GraphicsHandler.MesureString ( Font, Text.Replace ( "\n", "" ) ).Y );

            //Creates a multi line string that is as large as the box allows for
            _multiLineString = new string[i];

            //Set each item to an empty value
            for ( var index = 0; index < _multiLineString.Length; index++ )
                _multiLineString [ index ] = "";
            //Loop through each line value and lets try and get what text should be set for that line
            for ( var mls = 0; mls < _multiLineString.Length; mls++ )
            {
                //First we confirm that the multiLine variable is not empty
                //Next we Aggregate: This uses Text as the seed for current and then we replace anything found in the following items of the array with an empty string in the first value
                //Next it loops through all other values doing the same thing and replacing all instances of text found with "" in text to give us what the value of the changing text is
                string changingText =
                    _multiLineString.Where ( multiLine => multiLine != "" ).Aggregate ( Text, (current, multiLine ) => current.Substring ( multiLine.Length ) );

                //if Changing text contains \n lets remove everything after the n
                if ( changingText.Contains ( "\n" ) )
                {
                    //get index of our value
                    int index = changingText.IndexOf ( "\n" );
                    //remove our text after \n
                    if ( index > 0 )
                        changingText = changingText.Substring ( 0, index + 1 );
                    changingText = FitToScreen ( changingText );
                    _multiLineString [ mls ] = changingText;
                }
                else
                { 
                    //MultiLineString[mls] = "TEXT!";
                    while ( _multiLineString [ mls ] == "" )
                    {
                        changingText = changingText.Substring ( 0, changingText.LastIndexOf ( " " ) < 0 ? 0 : changingText.LastIndexOf ( " " ) );

                        //Mesure our X value of our changing text
                        var fontMesure = GraphicsHandler.MesureString ( Font, changingText ).X;

                        //If our font is bigger than 
                        if ( fontMesure < Size.X )
                            _multiLineString [ mls ] = changingText;
                        if ( fontMesure == 0 )
                            break;
                    }
                }
            }
        }

       
        private string FitToScreen( string changingText )
        {
            var fontMesure = GraphicsHandler.MesureString ( Font, changingText ).X;
            if ( fontMesure == 0 )
                return "";
            while ( fontMesure > Size.X )
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
            base.Draw ( gameTime );

            // GraphicsHandler.DrawString(Font, Text, Position + TextOffset, TextColor);
            if ( _multiLineString == null )
                return;
            for ( var index = 0; index < _multiLineString.Length; index++ )
            {//Draw each one
                var multiLine = _multiLineString [ index ].Replace ( "\n", "" );
                //Determine where we draw our string by adding to position + where we are at in the loop times the size of font height
                GraphicsHandler.DrawString ( Font, multiLine,
                    Position + new Vector2 ( 0, index * GraphicsHandler.MesureString ( Font, _multiLineString [ index ].Replace ( "\n", "" ) ).Y ) + TextOffset,
                    TextColor );
            }
        }

        #endregion
    }
}

