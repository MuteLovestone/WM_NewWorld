using System;
using System.Linq;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;

namespace WMNW.Core.GUI.Controls
{
    public class Label:ControlBase
    {
        #region Consturctor

        public Label ( string font, string text, Color color, Vector2 position, Vector2? size )
            : base ()
        {
            Font = font;
            Text = text;
            TextColor = color;
            Position = position;
            Centered = true;
            Size = size ?? GraphicsHandler.MesureString ( font, text );
            BuildTextOffset ();
        }

        #endregion

        #region Xna Methods

        public override void Update( GameTime gameTime )
        {
            if ( !Enabled )
                return;
            base.Update ( gameTime );
            BuildTextOffset ();
            if ( Selected )
            {
                if ( Mouse.IsInside ( Bounds ) )
                {
                    if ( Mouse.GetMouseButtonsPressed ().Count () >= 1 )
                        OnMouseClick ( new WMMouseEventArgs () );
                }
            }
        }

        public override void Draw( GameTime gameTime )
        {
            base.Draw ( gameTime );


            GraphicsHandler.DrawString ( Font, Text, Position + TextOffset, TextColor );

        }

        public void BuildTextOffset()
        {
            if ( Centered && ( TextChanged || FontChanged ) )
            {
                TextChanged = false;
                FontChanged = false;
                TextOffset = Size / 2 - GraphicsHandler.MesureString ( Font, Text ) / 2;
            }
        }

        #endregion
    }
}

