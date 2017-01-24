using System;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;
using MouseButtons = WMNW.Core.Input.Classes.MouseButtons;

namespace WMNW.Core.GUI.Controls
{
    public enum Stance
    {
        Vertical,
        Horizontal
    }

    public class TrackBar:ControlBase
    {
        #region Properties

        private int _value = 20;
        private int _maxValue = 100;
        private int _minValue = 0;
        private Stance _stance;
        private Button _sliderBtn;

        /// <summary>
        /// Current Value of Slider
        /// </summary>
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                if ( ( value < _minValue || value > _maxValue ) )
                    return;

                _value = value;

                if ( Enabled )
                    OnValueChangedEvent ( new EventArgs () );
            }
        }

        #region Events

        public delegate void WMEventHandler ( object sender, EventArgs e );

        public event WMEventHandler OnValueChanged;

        protected virtual void OnValueChangedEvent( EventArgs e )
        {
            if ( OnValueChanged != null )
                OnValueChanged ( this, e );
        }

        #endregion

        /// <summary>
        /// Maximum Value of our Track Bar
        /// </summary>
        public int MaximumValue
        {
            get
            {
                return _maxValue;
            }
            set
            {
                _maxValue = value;
            }
        }

        /// <summary>
        /// Minimum Value of our Track Bar
        /// </summary>
        public int MinimumValue
        {
            get
            {
                return _minValue;
            }
            set
            {
                _minValue = value;
            }
        }

        /// <summary>
        /// Length of our Line. X = Line Length. Y = Line Width
        /// </summary>
        public Vector2 LineSize
        {
            get;
            set;
        }

        /// <summary>
        /// Color of our Line
        /// </summary>
        public Color LineColor
        {
            get;
            set;
        }

        /// <summary>
        /// Texture for our Line
        /// </summary>
        public string LineTexture
        {
            get;
            set;
        }

        /// <summary>
        /// Padding our our TrackBar
        /// </summary>
        public Vector2 Padding
        {
            get;
            set;
        }

        #endregion

        #region Consturctor

        public TrackBar ( Vector2 position, Vector2 lineSize, Color? lineColor, Vector2 padding, string lineTexture = "",
                          Stance stance = Stance.Horizontal )
        {
            Position = position;
            LineSize = lineSize;
            Padding = padding;

            if ( lineColor.HasValue ) //Use Color if its available else check string
                LineColor = lineColor.Value;
            else
                LineTexture = lineTexture;

            _stance = stance;

            //Calculate Position of knob along the line based on percentage of Value versus max value
            var percent = ( Value * 100 ) / MaximumValue;
            //Set our position accounting for Padding, Line Size, and Position
            if ( _stance == Stance.Horizontal )
            {
                var realPos = LineSize.X * percent + 1 / 100;
                _sliderBtn =
                    new Button (
                    new Vector2 ( ( int )( ( ( int )Position.X + Padding.X ) + realPos ),
                        ( int )( ( int )Position.Y + Padding.Y ) ), new Vector2 ( 10, 10 ) ) {
                    Parent = this
                };
                _sliderBtn.ChangeColor ( null, Color.White );
                _sliderBtn.Name = "_sliderBtn";
                Children.Add ( _sliderBtn );
            }
            else
            {
                var realPos = LineSize.Y * ( percent + 1 ) / 100;
                _sliderBtn =
                    new Button (
                    new Vector2 ( ( int )( ( ( int )Position.X + LineSize.X * Padding.X - Padding.X ) ),
                        ( int )( ( int )Position.Y + Padding.Y ) + realPos ), new Vector2 ( 10, 10 ) ) {
                    Parent = this
                };
                _sliderBtn.ChangeColor ( null, Color.White );
                _sliderBtn.Name = "_sliderBtn";
                Children.Add ( _sliderBtn );
            }
            Size = lineSize;
        }

        #endregion

        #region Xna Methods

        private bool _moving = false;

        public override void Update( GameTime gameTime )
        {
            if ( !Enabled )
                return;
            base.Update ( gameTime );
            if ( Selected )
            {
                //Now allow mouse to move the knob if its pressed down inside the knob
                if ( Mouse.IsInside ( _sliderBtn.Bounds ) )
                if ( Mouse.ButtonDown ( MouseButtons.Left ) )
                    _moving = true;
            }

            //Stop allowing movement when released
            if ( Mouse.ButtonUp ( MouseButtons.Left ) )
                _moving = false;
            
            if ( _moving )
            {
                if ( _stance == Stance.Horizontal )
                {
                    //Grab some values for use later
                    var pos = ( int )Mouse.MousePosition ().X;
                    var w = ( int )LineSize.X;

                    //If mouse position is out of range set the maximum value
                    if ( pos < Position.X + Padding.X )
                        pos = ( int )( Position.X + Padding.X );
                    if ( pos > ( w + Position.X + Padding.X ) )
                        pos = ( int )( w + Position.X + Padding.X );

                    //Set our Slider Position as we move the mouse
                    _sliderBtn.Position = new Vector2 ( pos - 5,
                        ( int )( ( int )Position.Y + LineSize.Y * Padding.Y - Padding.Y ) );
                    var px = ( ( float )_maxValue / w ); //Get a percent of how large the line is compared to max value
                    Value = ( int )( Math.Ceiling ( ( pos - Position.X - Padding.X ) * px ) );
                    //Create value based on Mouse position and the percentage we just ontained
                }
                else
                {
                    //Grab some values for use later
                    var pos = ( int )Mouse.MousePosition ().Y;
                    var h = ( int )LineSize.Y;

                    //If mouse position is out of range set the maximum value
                    if ( pos < Position.Y + Padding.Y )
                        pos = ( int )( Position.Y + Padding.Y );
                    if ( pos > ( h + Position.Y + Padding.Y ) )
                        pos = ( int )( h + Position.Y + Padding.Y );

                    // Set our Slider Position as we move the mouse
                    _sliderBtn.Position = new Vector2 ( ( ( int )Position.X + LineSize.X * Padding.X - Padding.X ), pos - 5 );
                    float px = ( ( float )_maxValue / h );//h / _maxValue; //Get a percent of how large the line is compared to max value
                    Value = ( int )( Math.Ceiling ( ( pos - Position.Y - Padding.Y ) * px ) );
                }
            }

            UpdateChildren ( gameTime );

        }

        public override void Draw( GameTime gameTime )
        {
            if ( !Enabled )
                return;
            base.Draw ( gameTime );
            if ( _stance == Stance.Horizontal )
            {
                //Draw The Line
                if ( LineTexture != null )
                    GraphicsHandler.Draw ( LineTexture, new Vector2 ( Position.X + Padding.X, ( int )( Position.Y + LineSize.Y * Padding.Y ) ), Color.White );
                else
                    GraphicsHandler.DrawHLine ( new Point ( ( int )( Position.X + Padding.X ), ( int )( Position.Y + LineSize.Y * Padding.Y ) ), ( int )LineSize.X, 1, LineColor );
            }
            else
            {
                //Draw The Line
                if ( LineTexture != null )
                    GraphicsHandler.Draw ( LineTexture, new Vector2 ( Position.X + Padding.X, ( int )( Position.Y + LineSize.Y * Padding.Y ) ), Color.White );
                else
                    GraphicsHandler.DrawVLine ( new Point ( ( int )( Position.X + Padding.X ), ( int )( Position.Y + LineSize.X * Padding.Y ) ), ( int )LineSize.X, ( int )LineSize.Y, LineColor );
                //GraphicsHandler.DrawVLine ( new Point ( ( int )( Position.X + Padding.X ), ( int )( Position.Y + LineSize.X * Padding.Y ) ), ( int )LineSize.Y, 1, LineColor );
                
            }

            DrawChildren ( gameTime );
        }

        #endregion
    }
}

