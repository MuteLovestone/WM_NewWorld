using System;
using System.Linq;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;

namespace WMNW.Core.GUI.Controls
{
    public class CheckBox:ControlBase
    {
        #region Properties

        public bool Checked
        {
            get;
            set;
        }

        public Vector2 CheckBoxSize
        {
            get;
            set;
        }

        #region Check Boxes

        //CheckBox Textures
        public string NormalCheckBoxTexture
        {
            get;
            set;
        }

        public string DisabledCheckBoxTexture
        {
            get;
            set;
        }

        public string HoverCheckBoxTexture
        {
            get;
            set;
        }

        public string DownCheckBoxTexture
        {
            get;
            set;
        }

        //Check Box Colors
        public Color NormalCheckBoxColor
        {
            get;
            set;
        }

        public Color DisabledCheckBoxColor
        {
            get;
            set;
        }

        public Color HoverCheckBoxColor
        {
            get;
            set;
        }

        public Color DownCheckBoxColor
        {
            get;
            set;
        }

        #endregion

        #region CheckBox Border

        //Check Box Borders
        public string NormalCheckBoxBorderTexture
        {
            get;
            set;
        }

        public string DisabledCheckBoxBorderTexture
        {
            get;
            set;
        }

        public string HoverCheckBoxBorderTexture
        {
            get;
            set;
        }

        public string DownCheckBoxBorderTexture
        {
            get;
            set;
        }

        //Check Box Border Colors
        public Color NormalCheckBoxBorderColor
        {
            get;
            set;
        }

        public Color DisabledCheckBoxBorderColor
        {
            get;
            set;
        }

        public Color HoverCheckBoxBorderColor
        {
            get;
            set;
        }

        public Color DownCheckBoxBorderColor
        {
            get;
            set;
        }

        #endregion

        #endregion

        #region Consturctor

        public CheckBox ( string font, string text, Color color, Vector2 position, Vector2 checkBoxSize, Vector2? size )
        {
            Font = font;
            Text = text;
            TextColor = color;
            Position = position;
            CheckBoxSize = checkBoxSize;

            Size = ( size ?? GraphicsHandler.MesureString ( font, text ) );
        }

        public CheckBox ( string font, string text, Color color, Vector2 checkBoxSize, Vector2? size )
        {
            Font = font;
            Text = text;
            TextColor = color;
            CheckBoxSize = checkBoxSize;

            Size = ( size ?? GraphicsHandler.MesureString ( font, text ) );
        }


        #endregion

        #region Xna Methods

        public override void Update( GameTime gameTime )
        {
            if ( !Enabled )
                return;
            base.Update ( gameTime );

            if ( Mouse.IsInside ( Bounds ) )
            {
                if ( Mouse.GetMouseButtonsPressed ().Count () >= 1 )
                {
                    OnMouseClick ( new WMMouseEventArgs () );

                    //Set itself oppisite
                    Checked = !Checked;
                }
            }
        }

        public override void Draw( GameTime gameTime )
        {
            base.Draw ( gameTime );

            switch ( ButtonState )
            {
                case ButtonState.Disabled:
                    {
                        //Draw CheckBox Border
                        if ( DisabledCheckBoxBorderTexture != null )
                            GraphicsHandler.Draw ( DisabledCheckBoxBorderTexture, CheckBoxArea (), Color.White );
                        else
                            GraphicsHandler.DrawRectangle ( CheckBoxArea (), DisabledCheckBoxBorderColor, 1 );

                        if ( Checked )
                        {
                            //Draw Check Box
                            if ( DisabledCheckBoxTexture != null )
                                GraphicsHandler.Draw ( DisabledCheckBoxTexture, CheckBoxArea (), Color.White );
                            else
                                GraphicsHandler.DrawFillRectangle ( CheckBoxArea ( 3, 3, -5, -5 ), DisabledCheckBoxColor );
                        }
                    }
                    break;
                case ButtonState.Hover:
                    {

                        if ( HoverCheckBoxBorderTexture != null )
                            GraphicsHandler.Draw ( HoverCheckBoxBorderTexture, CheckBoxArea (), Color.White );
                        else
                            GraphicsHandler.DrawRectangle ( CheckBoxArea (), HoverCheckBoxBorderColor, 1 );

                        if ( Checked )
                        {
                            if ( HoverCheckBoxTexture != null )
                                GraphicsHandler.Draw ( HoverCheckBoxTexture, CheckBoxArea (), Color.White );
                            else
                                GraphicsHandler.DrawFillRectangle ( CheckBoxArea ( 3, 3, -5, -5 ), HoverCheckBoxColor );
                        }
                    }
                    break;
                case ButtonState.Down:
                    {

                        if ( DownCheckBoxBorderTexture != null )
                            GraphicsHandler.Draw ( DownCheckBoxBorderTexture, CheckBoxArea (), Color.White );
                        else
                            GraphicsHandler.DrawRectangle ( CheckBoxArea (), DownCheckBoxBorderColor, 1 );

                        if ( Checked )
                        {
                            if ( DownCheckBoxTexture != null )
                                GraphicsHandler.Draw ( DownCheckBoxTexture, CheckBoxArea (), Color.White );
                            else
                                GraphicsHandler.DrawFillRectangle ( CheckBoxArea ( 3, 3, -5, -5 ), DownCheckBoxColor );
                        }
                    }
                    break;
                case ButtonState.Normal:
                    {
                        if ( NormalCheckBoxBorderTexture != null )
                            GraphicsHandler.Draw ( NormalCheckBoxBorderTexture, CheckBoxArea (), Color.White );
                        else
                            GraphicsHandler.DrawRectangle ( CheckBoxArea (), NormalCheckBoxBorderColor, 1 );

                        if ( Checked )
                        {
                            if ( NormalCheckBoxTexture != null )
                                GraphicsHandler.Draw ( NormalCheckBoxTexture, CheckBoxArea (), Color.White );
                            else
                                GraphicsHandler.DrawFillRectangle ( CheckBoxArea ( 3, 3, -5, -5 ), NormalCheckBoxColor );
                        }
                    }
                    break;
            }

            GraphicsHandler.DrawString ( Font, Text, new Vector2 ( CheckBoxSize.X, 0 ) + Position + TextOffset, TextColor );
        }

        /// <summary>
        /// Creates a Check Box Area based on passed values
        /// </summary>
        /// <param name="x">X Value</param>
        /// <param name="y">Y Value</param>
        /// <param name="w">Width</param>
        /// <param name="h">Height</param>
        /// <returns></returns>
        public Rectangle CheckBoxArea( int x = 0, int y = 0, int w = 0, int h = 0 )
        {
            return new Rectangle ( ( int )Position.X + x, ( int )Position.Y + y, ( int )CheckBoxSize.X + w, ( int )CheckBoxSize.Y + h );
        }

        public void ChangeColor( Color? Border, Color? background, Color? check )
        {
            ChangeColor ( Border, background );
            if ( Border.HasValue )
            {
                DisabledCheckBoxBorderColor = Border.Value;
                HoverCheckBoxBorderColor = Border.Value;
                DownCheckBoxBorderColor = Border.Value;
                NormalCheckBoxBorderColor = Border.Value;
            }
            if ( check.HasValue )
            {
                DisabledCheckBoxColor = check.Value;
                HoverCheckBoxColor = check.Value;
                DownCheckBoxColor = check.Value;
                NormalCheckBoxColor = check.Value;
            }
        }

        #endregion
    }
}

