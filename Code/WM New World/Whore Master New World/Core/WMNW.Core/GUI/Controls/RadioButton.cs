using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;

namespace WMNW.Core.GUI.Controls
{
    public class RadioButton:ControlBase
    {
        #region Properties


        #endregion

        #region Consturctor

        public RadioButton ( string font, string text, Color color, Vector2 position, List<CheckBox> checkBoxes )
        {
            Font = font;
            Text = text;
            TextColor = color;
            Position = position;

            foreach ( var cb in checkBoxes )
                Children.Add ( cb );
        }

        #endregion

        #region Xna Methods

        public override void Update( GameTime gameTime )
        {
            base.Update ( gameTime );

            UpdateChildren ( gameTime );
        }

        public override void Draw( GameTime gameTime )
        {
            base.Draw ( gameTime );

            GraphicsHandler.DrawString ( Font, Text, Position + TextOffset, TextColor );

            DrawChildren ( gameTime );
        }

        #endregion
    }
}

