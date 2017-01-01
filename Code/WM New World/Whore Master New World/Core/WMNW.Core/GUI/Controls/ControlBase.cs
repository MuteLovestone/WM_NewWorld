using System;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;
using WMNW.Core.Input.Devices.Generic;
using System.Linq;
using System.Collections.Generic;
using MouseButtons = WMNW.Core.Input.Classes.MouseButtons;

namespace WMNW.Core.GUI.Controls
{
    public class ControlBase
    {
        #region Control Accessors

        protected PcMouse Mouse = ( ( PcMouse )GameBase.Input.GetMouse () );
        protected PcKeyboard Keyboard = ( ( PcKeyboard )GameBase.Input.GetKeyboard () );

        protected ButtonState ButtonState = ButtonState.Disabled;

        public List<ControlBase> Children = new List<ControlBase> ();

        public ControlBase Parent = null;

        public int ZIndex = 0;
        public string Name = "";
        public bool Selectable = true;

        #endregion

        #region Events

        public delegate void MouseEventHandler ( object sender, WMMouseEventArgs e );

        public delegate void SulexEventHandler ( object sender, WMEventArgs e );

        public event MouseEventHandler MouseClicked;
        public event SulexEventHandler SelectedEvent;

        protected virtual void OnMouseClick( WMMouseEventArgs e )
        {
            if ( MouseClicked != null )
                MouseClicked ( this, e );
        }

        protected virtual void OnSelected( WMEventArgs e )
        {
            if ( SelectedEvent != null )
                SelectedEvent ( this, e );
        }

        #endregion

        #region Font Stuff

        protected Color TextColor
        {
            get;
            set;
        }

        private string _font;

        public string Font
        {
            get
            {
                return _font;
            }
            set
            {
                FontChanged = true;
                _font = value;
            }
        }

        public bool Centered
        {
            get;
            set;
        }

        public bool FontChanged
        {
            get;
            set;
        }

        private string _text;

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                TextChanged = true;
                _text = value;
            }
        }

        public bool TextChanged
        {
            get;
            set;
        }

        public Vector2 TextOffset = Vector2.Zero;

        #endregion

        #region Border Stuff

        public int Border
        {
            get;
            set;
        }

        //Color
        public Color DisabledBorderColor
        {
            get;
            set;
        }

        public Color HoverBorderColor
        {
            get;
            set;
        }

        public Color DownBorderColor
        {
            get;
            set;
        }

        public Color NormalBorderColor
        {
            get;
            set;
        }

        //Texture
        public string DisabledBorderTexture
        {
            get;
            set;
        }

        public string HoverBorderTexture
        {
            get;
            set;
        }

        public string DownBorderTexture
        {
            get;
            set;
        }

        public string NormalBorderTexture
        {
            get;
            set;
        }

        #endregion

        #region Background Stuff

        //Color
        public Color DisabledBackgroundColor
        {
            get;
            set;
        }

        public Color HoverBackgroundColor
        {
            get;
            set;
        }

        public Color DownBackgroundColor
        {
            get;
            set;
        }

        public Color NormalBackgroundColor
        {
            get;
            set;
        }

        //Texture
        public string DisabledBackgroundTexture
        {
            get;
            set;
        }

        public string HoverBackgroundTexture
        {
            get;
            set;
        }

        public string DownBackgroundTexture
        {
            get;
            set;
        }

        public string NormalBackgroundTexture
        {
            get;
            set;
        }

        #endregion

        #region Bounds Stuff

        public Vector2 Position
        {
            get;
            set;
        }

        public Vector2 Size
        {
            get;
            set;
        }

        public Rectangle Bounds
        {
            get
            {
                return new Rectangle ( ( int )Position.X, ( int )Position.Y, ( int )Size.X, ( int )Size.Y );
            }
            set
            {
                Position = new Vector2 ( value.X, value.Y );
                Size = new Vector2 ( value.Width, value.Height );
            }
        }

        public bool Enabled
        {
            get;
            set;
        }

        protected bool TabStop
        {
            get;
            set;
        }

        protected int TabIndex
        {
            get;
            set;
        }

        public bool Selected
        {
            get;
            set;
        }

        #endregion

        #region Constructor

        public ControlBase ()
        {
            Border = 1;
            Enabled = true;
        }

        #endregion

        #region Interfaces

        public void BuildTextOffset()
        {
            if ( Centered && ( TextChanged || FontChanged ) )
            {
                TextChanged = false;
                FontChanged = false;
                TextOffset = Size / 2 - GraphicsHandler.MesureString ( Font, Text ) / 2;
            }
        }

        /// <summary>
        /// Changes Color of all borders and backgrounds to be the same
        /// </summary>
        /// <param name="border"></param>
        /// <param name="background"></param>
        public virtual void ChangeColor( Color? border, Color? background )
        {
            if ( border.HasValue )
            {
                DisabledBorderColor = border.Value;
                HoverBorderColor = border.Value;
                DownBorderColor = border.Value;
                NormalBorderColor = border.Value;
            }

            if ( background.HasValue )
            {
                DisabledBackgroundColor = background.Value;
                HoverBackgroundColor = background.Value;
                DownBackgroundColor = background.Value;
                NormalBackgroundColor = background.Value;
            }
        }

        /// <summary>
        /// Change textures of all borders and backgrounds to be the same
        /// </summary>
        /// <param name="border"></param>
        /// <param name="background"></param>
        public void ChangeTexture( string border, string background )
        {
            DisabledBorderTexture = border;
            HoverBorderTexture = border;
            DownBorderTexture = border;
            NormalBorderTexture = border;

            DisabledBackgroundTexture = background;
            HoverBackgroundTexture = background;
            DownBackgroundTexture = background;
            NormalBackgroundTexture = background;
        }

        public virtual void DrawFinal( GameTime gameTime )
        {
            //Draw Final Stuffs
        }

        public virtual void Draw( GameTime gameTime )
        {
            switch ( ButtonState )
            {
                case ButtonState.Disabled:
                    {
                        //Draw Background
                        if ( DisabledBackgroundTexture != null )
                            GraphicsHandler.Draw ( DisabledBackgroundTexture, Bounds, Color.White );
                        else
                            GraphicsHandler.DrawFillRectangle ( Bounds, DisabledBackgroundColor );

                        //Draw Border
                        if ( DisabledBorderTexture != null )
                            GraphicsHandler.Draw ( DisabledBorderTexture, Bounds, Color.White );
                        else
                            GraphicsHandler.DrawRectangle ( Bounds, DisabledBorderColor, Border );
                    }
                    break;
                case ButtonState.Hover:
                    {
                        if ( HoverBackgroundTexture != null )
                            GraphicsHandler.Draw ( HoverBackgroundTexture, Bounds, Color.White );
                        else
                            GraphicsHandler.DrawFillRectangle ( Bounds, HoverBackgroundColor );


                        if ( HoverBorderTexture != null )
                            GraphicsHandler.Draw ( HoverBorderTexture, Bounds, Color.White );
                        else
                            GraphicsHandler.DrawRectangle ( Bounds, HoverBorderColor, Border );
                    }
                    break;
                case ButtonState.Down:
                    {
                        if ( DownBackgroundTexture != null )
                            GraphicsHandler.Draw ( DownBackgroundTexture, Bounds, Color.White );
                        else
                            GraphicsHandler.DrawFillRectangle ( Bounds, DownBackgroundColor );

                        if ( DownBorderTexture != null )
                            GraphicsHandler.Draw ( DownBorderTexture, Bounds, Color.White );
                        else
                            GraphicsHandler.DrawRectangle ( Bounds, DownBorderColor, Border );
                    }
                    break;
                case ButtonState.Normal:
                    {
                        if ( NormalBackgroundTexture != null )
                            GraphicsHandler.Draw ( NormalBackgroundTexture, Bounds, Color.White );
                        else
                            GraphicsHandler.DrawFillRectangle ( Bounds, NormalBackgroundColor );

                        if ( NormalBackgroundTexture != null )
                            GraphicsHandler.Draw ( NormalBorderTexture, Bounds, Color.White );
                        else
                            GraphicsHandler.DrawRectangle ( Bounds, NormalBorderColor, Border );
                    }
                    break;
            }
        }

        public virtual void Update( GameTime gameTime )
        {
            if ( !Enabled )
                ButtonState = ButtonState.Disabled;
            else if ( Selected && Mouse.IsInside ( Bounds ) )
            {
                if ( Mouse.GetMouseButtonsDown ().Contains ( MouseButtons.Left ) )
                    ButtonState = ButtonState.Down;
                else
                    ButtonState = ButtonState.Hover;
            }
            else
                ButtonState = ButtonState.Normal;
        }

        public void UpdateChildren( GameTime gameTime )
        {
            Children.OrderBy ( x => x.ZIndex );

            foreach ( var child in Children )
                child.Update ( gameTime );
        }

        protected void DrawChildren( GameTime gameTime )
        {
            Children.OrderBy ( x => x.ZIndex );

            foreach ( var child in Children )
                child.Draw ( gameTime );
        }

        #endregion
    }

    /// <summary>
    /// Sends Mouse With Event Args
    /// </summary>
    public class WMMouseEventArgs
    {
        public WMMouseEventArgs ()
        {
            Buttons = ( ( PcMouse )GameBase.Input.GetMouse () ).GetMouseButtonsPressed ();
            Position = ( ( PcMouse )GameBase.Input.GetMouse () ).MousePosition ();
        }

        public MouseButtons[] Buttons;
        public Vector2 Position;
    }

    /// <summary>
    /// Empty Event Args
    /// </summary>
    public class WMEventArgs
    {
        public int SelectedItem;

    }
}

