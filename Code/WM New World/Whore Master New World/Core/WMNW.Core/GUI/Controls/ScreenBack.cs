using System;
using Microsoft.Xna.Framework;

namespace WMNW.Core.GUI.Controls
{
    public class ScreenBack:ControlBase
    {
        #region Construct

        public ScreenBack ( Vector2 size )
        {
            Size = size;
            Position = Vector2.Zero;
            Border = 2;
            Selectable = false;

        }

        #endregion

    }
}

