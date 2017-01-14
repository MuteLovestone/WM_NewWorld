using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;

namespace WMNW.Core.GUI.Controls
{
    public class ButtonList:ControlBase
    {
        #region Fields

        private List<Button> _buttons = new List<Button> ();
        private Vector2 btnSize;

        #endregion

        #region Construct

        public ButtonList ( Vector2 size, Vector2 pos )
        {
            Size = size;
            Position = pos;
        }

        #endregion

        #region XNA Logic

        public override void Draw( GameTime gameTime )
        {
            base.Draw ( gameTime );
        }

        public override void Update( GameTime gameTime )
        {
            base.Update ( gameTime );
        }

        #endregion

        #region Overrides

        public override void ChangeColor( Color? border, Color? background )
        {
            base.ChangeColor ( border, background );
        }

        #endregion
    }
}

