using System;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;
using System.Linq;

namespace WMNW.Core.GUI.Controls
{
    public class Button:ControlBase
    {
        #region Properties

        #endregion

        #region Consturctor

        public Button ( string font, string text, Color color, Vector2 position, Vector2? size )
        {
            Font = font;
            Text = text;
            TextColor = color;
            Position = position;
            Centered = true;
            Size = size ?? GraphicsHandler.MesureString ( font, text );
            BuildTextOffset ();
        }

        public Button ( string font, string text, Color color )
        {
            Font = font;
            Text = text;
            TextColor = color;
            Centered = true;
            //BuildTextOffset ();
        }

        public Button ( Vector2 position, Vector2 size )
        {
            Position = position;
            Size = size;
            Centered = true;
            BuildTextOffset ();
        }

        #endregion

        #region Xna Methods

        public override void Update( GameTime gameTime )
        {
            if ( !Enabled )
                return;
            base.Update ( gameTime );
            if ( TextChanged || FontChanged )
            {
                BuildTextOffset ();
                TextChanged = false;
                FontChanged = false;
            }
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
            if ( !Enabled )
                return;
            base.Draw ( gameTime );

            if ( Font != null || Text != null )
                GraphicsHandler.DrawString ( Font, Text, Position + TextOffset, TextColor );
        }

        #endregion
    }
}

