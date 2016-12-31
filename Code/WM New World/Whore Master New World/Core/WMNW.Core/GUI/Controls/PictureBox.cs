using System;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;

namespace WMNW.Core.GUI.Controls
{
    public class PictureBox:ControlBase
    {
        #region Properties

        public string Texture2D;
        public Rectangle Bounds2 = Rectangle.Empty;

        #endregion

        #region Consturctor

        /// <summary>
        /// Creates a picture to place somewhere
        /// </summary>
        /// <param name="position">Position of our Box</param>
        /// <param name="texture">Texture of our Image</param>
        /// <param name="bounds">Cropping of Picture into box</param>
        public PictureBox ( string texture, Rectangle size, Rectangle? bounds )
            : base ()
        {
            Position = new Vector2 ( size.X, size.Y );
            Size = new Vector2 ( size.Width, size.Height );

            Texture2D = texture;

            if ( !bounds.HasValue )
                return;
            Bounds2 = bounds.Value;
        }

        /// <summary>
        /// Creates a picture to place somewhere
        /// </summary>
        /// <param name="position">Position of our Box</param>
        /// <param name="texture">Texture of our Image</param>
        /// <param name="bounds">Cropping of Picture into box</param>
        public PictureBox ( string texture, Vector2 position, Rectangle? bounds )
            : base ()
        {
            Position = position;
            Texture2D = texture;

            if ( !bounds.HasValue )
                return;
            Bounds2 = bounds.Value;
        }

        #endregion

        #region Xna Methods

        public override void Update( GameTime gameTime )
        {
            base.Update ( gameTime );
        }

        public override void Draw( GameTime gameTime )
        {
            base.Draw ( gameTime );

            if ( Bounds == Rectangle.Empty )
                GraphicsHandler.Draw ( Texture2D, Position, Color.Red );
            else
                GraphicsHandler.Draw ( Texture2D,
                    new Rectangle ( ( int )Position.X, ( int )Position.Y, ( int )Size.X, ( int )Size.Y ),
                    Bounds, Color.Red );
        }

        #endregion
    }
}

