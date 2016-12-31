using System;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;

namespace WMNW.Core.GUI.Controls
{
    public class ProgressBar:ControlBase
    {
        #region Variables

        float _min = 0;
        float _max = 100;
        float _value = 0;

        #endregion

        #region Properties

        public float Value
        {
            get
            {
                return _value;
            }
            set
            {
                if ( MinimumValue <= value && value <= MaximumValue )
                    _value = value;
            }
        }

        public float MinimumValue
        {
            get
            {
                return _min;
            }
            set
            {
                _min = value;
            }
        }

        public float MaximumValue
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
            }
        }

        protected string BarTexture
        {
            get;
            set;
        }

        protected Color BarColor
        {
            get;
            set;
        }


        #endregion

        #region Consturctor

        public ProgressBar ( Vector2 position, Vector2 size, Color barColor, string barTexture = null )
        {
            Position = position;
            Size = size;

            BarTexture = barTexture;
            BarColor = barColor;
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

            var barWidth = ( int )( ( Size.X - 2 ) / MaximumValue * Value );

            if ( BarTexture == null )
                GraphicsHandler.DrawFillRectangle (
                    new Rectangle ( ( int )Position.X + 1, ( int )Position.Y + 1, barWidth, ( int )Size.Y - 2 ), BarColor );
            else
                GraphicsHandler.Draw ( BarTexture,
                    new Rectangle ( ( int )Position.X, ( int )Position.Y, barWidth, ( int )Size.Y ), Color.White );
        }

        public void IncreaseBy( float amount )
        {
            if ( amount <= ( MaximumValue - Value ) )
                Value += amount;
            else
                Value = MaximumValue;
        }

        public void DecreaseBy( float amount )
        {
            if ( amount <= Value )
                Value -= amount;
            else
                Value = MinimumValue;
        }

        #endregion
    }
}

