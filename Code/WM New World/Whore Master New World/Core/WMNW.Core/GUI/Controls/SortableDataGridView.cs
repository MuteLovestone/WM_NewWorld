using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;

namespace WMNW.Core.GUI.Controls
{
    public class SortableDataGridView:ControlBase
    {
        #region Variables

        public List<string> HeaderColumns;
        public List<List<string>> Rows;

        public int Height;
        public int Width;

        #endregion

        #region Properties


        #endregion

        #region Constructor

        public SortableDataGridView ( string font, Color color, List<string> columns, List<List<string>> rows, int width, int height, Vector2 position )
            : base ()
        {
            Font = font;
            TextColor = color;
            Width = width;
            Height = height;
            HeaderColumns = columns;
            Rows = rows;
            Position = position;
        }

        #endregion

        #region XNA Methods

        public override void Update( GameTime gameTime )
        {
            if ( !Enabled )
                return;
            base.Update ( gameTime );
        }

        public override void Draw( GameTime gameTime )
        {
            base.Draw ( gameTime );

            var startPos = Position + TextOffset;

            //Draw Headers
            foreach ( var c in HeaderColumns )
            {
                GraphicsHandler.DrawString ( Font, c, new Vector2 ( startPos.X + ( Width / 2 ) / 2, startPos.Y ), TextColor );
                startPos = new Vector2 ( startPos.X + Width, startPos.Y );
            }

            startPos = new Vector2 ( startPos.X, startPos.Y + Height );

            //Draw Rows
            foreach ( var r in Rows )
            {
                startPos = new Vector2 ( Position.X + TextOffset.X, startPos.Y );

                foreach ( var s in r )
                {
                    GraphicsHandler.DrawString ( Font, s, startPos, TextColor );
                    startPos = new Vector2 ( startPos.X + Width, startPos.Y );
                }

                startPos = new Vector2 ( startPos.X, startPos.Y + Height );
            }


        }

        #endregion
    }
}

