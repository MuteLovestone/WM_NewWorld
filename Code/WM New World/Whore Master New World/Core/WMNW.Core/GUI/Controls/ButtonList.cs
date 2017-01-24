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
        public Vector2 btnSize = new Vector2 ( 150, 25 );
        public Stance Layout = Stance.Vertical;
        private TrackBar scrollBar;
        private bool _showTrackBar = false;
        private int offset = 0;
        private bool offsetChanged = false;
        private int totalOffset = 0;
        private Vector2 _sizeBeforeScroll;

        #endregion

        #region Properties

        private Vector2 posOffset
        {
            get
            {
                if ( Layout == Stance.Horizontal )
                    return new Vector2 ( btnSize.X + 2, 0 );
                else
                    return new Vector2 ( 0, btnSize.Y + 2 );
            }
        }

        private Vector2 ScrollOffset
        {
            get
            {
                if ( _showTrackBar )
                {
                    if ( Layout == Stance.Horizontal )
                        return new Vector2 ( 0, 14 );
                    else
                        return new Vector2 ( 14, 0 );
                }
                else
                    return Vector2.Zero;
            }
        }

        private Vector2 TrueButtonSize
        {
            get
            {
                return btnSize - ScrollOffset;
            }
        }

        #endregion

        #region Construct

        public ButtonList ( Vector2 size, Vector2 buttonSize, Vector2 pos, List<Button> buttons, Stance layout = Stance.Vertical )
        {
            totalOffset = 0;
            Position = pos;
            btnSize = buttonSize;
            Size = size + new Vector2 ( 4, 4 );
            Layout = layout;
            _buttons = buttons;
            BuildChildrenButtons ();
        }

        #endregion

        #region XNA Logic

        public override void Draw( GameTime gameTime )
        {
            base.Draw ( gameTime );
            DrawChildren ( gameTime );
        }

        public override void Update( GameTime gameTime )
        {
            base.Update ( gameTime );
            if ( offsetChanged )
                BuildChildrenButtons ();
            UpdateChildren ( gameTime );
        }


        #endregion

        #region Logic

        private void BuildBar()
        {
            int value = 0;
            if ( scrollBar != null )
                value = scrollBar.Value;
            Vector2 barSize = Vector2.Zero;
            Vector2 barPos = Vector2.Zero;
            switch ( Layout )
            {
                case Stance.Vertical:
                    barSize = new Vector2 ( 10, TrueButtonSize.Y + ScrollOffset.Y - 4 );
                    barPos = new Vector2 ( Position.X + TrueButtonSize.X + 4, Position.Y + 2 );
                    break;
                case Stance.Horizontal:
                    barSize = new Vector2 ( TrueButtonSize.X + ScrollOffset.X - 4, 10 );
                    barPos = new Vector2 ( Position.X + 2, Position.Y + TrueButtonSize.Y + 4 );
                    break;
                default:
                    goto case Stance.Vertical;
            }
            scrollBar = new TrackBar ( barPos, barSize, Color.Gold, Vector2.Zero, "", Layout );
            scrollBar.Enabled = false;
            scrollBar.OnValueChanged += ScrollBar_OnValueChanged;
            if ( totalOffset > 0 )
            {
                scrollBar.MaximumValue = totalOffset;
                _showTrackBar = true;
                scrollBar.Enabled = true;
            }
            else
            {
                _showTrackBar = false;
                scrollBar.Enabled = false;
            }
            scrollBar.Value = value;
            Children.Add ( scrollBar );
        }

        void ScrollBar_OnValueChanged( object sender, EventArgs e )
        {
            offset = ( ( TrackBar )sender ).Value;
            offsetChanged = true;
        }

        private void BuildChildrenButtons()
        {
            Children.Clear ();
            offsetChanged = false;
            int counter = 0;
            int Overflow = 0;
            Vector2 pos = Position + new Vector2 ( 2, 2 );
            Vector2 posMod = posOffset;
            foreach ( Button btn in _buttons )
            {
                if ( counter >= offset )
                {
                    Vector2 btnxSize = TrueButtonSize;
                    Vector2 btnPos = pos;
                    Vector2 total = btnxSize + btnPos;
                    if ( IsOffControl ( total ) )
                    {
                        btn.Enabled = false;
                        Overflow++;
                    }
                    else
                    {
                        btn.Position = pos;
                        btn.Size = btnxSize;
                        btn.Enabled = true;
                    }
                    pos += posMod;
                }
                else
                {
                    btn.Enabled = false;
                    btn.Selected = false;
                    Overflow++;
                }
                counter++;
            }
            AddChildren ();
            totalOffset = Overflow;
            BuildBar ();
        }

        private void AddChildren()
        {
            Children.Clear ();
            foreach ( Button b in _buttons )
                if ( b.Enabled )
                    Children.Add ( b );
            Children.Add ( scrollBar );
        }

        private bool IsOffControl( Vector2 btnTotal )
        {
            Vector2 total2 = Position + Size;
            if ( Layout == Stance.Horizontal )
            {
                if ( btnTotal.X > total2.X )
                {
                    return true;
                }
                return false;
            }
            else
            {
                if ( btnTotal.Y > total2.Y )
                {
                    return true;
                }
                return false;
            }
        }

        #endregion

        #region AddButton

        public void AddButton( Button newButton )
        {
            _buttons.Add ( newButton );
        }

        #endregion

        #region Overrides

        public override void ChangeColor( Color? border, Color? background )
        {
            //Changes this Color
            base.ChangeColor ( border, background );
            // Changes all Buttons Colors
            _buttons.ForEach ( b => b.ChangeColor ( border, background ) );
            scrollBar.ChangeColor ( border, background );
        }

        #endregion
    }
}

