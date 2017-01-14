using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;

namespace WMNW.Core.GUI.Controls
{
    public class ComboBox:ControlBase
    {
        #region Properties

        public bool IsActive = false;
        public int SelectedItem = 0;
        public Button SelectedBtn;

        #endregion

        #region Consturctor

        public ComboBox ( string font, string text, Color color, Vector2 position, Vector2 size, List<Button> buttons )
        {
            Font = font;
            Text = text;
            TextColor = color;
            Position = position;

            SelectedBtn = new Button ( font, text, color );

            //Add all buttons to chilren
            foreach ( var btn in buttons )
                Children.Add ( btn );
            Centered = true;
            Size = size;
        }

        public ComboBox ( string font, Color color, Vector2 position, Vector2 size, IList<Button> buttons )
        {
            Font = font;
            TextColor = color;
            Position = position;
            if ( buttons.Count == 0 )
                return;
            SelectedBtn = new Button ( font, buttons [ 0 ].Text, color );

            //Add all buttons to chilren
            foreach ( var btn in buttons )
                Children.Add ( btn );

            Centered = true;
            Size = size;
        }

        #endregion

        #region Xna Methods

        public override void Update( GameTime gameTime )
        {
            base.Update ( gameTime );
            if ( Children.Count == 0 )
                return;
            //Mesure our Font to help determine our height
            //Size = Size;
            SelectedBtn.Text = Children [ SelectedItem ].Text;
            SelectedBtn.Size = Size;
            SelectedBtn.Position = Position;
            if ( Centered )
                SelectedBtn.BuildTextOffset ();
            //Enable all children
            foreach ( var child in Children )
            {
                child.Enabled = true;
                child.Size = Size;
            }

            if ( !Mouse.IsInside ( SelectedBtn.Bounds ) )
            {
                //Fire an event if we click on any of the children
                for ( var i = 0; i < Children.Count; i++ )
                {
                    var child = Children [ i ];
                    if ( !Mouse.IsInside ( child.Bounds ) )
                        continue;
                    if ( Mouse.GetMouseButtonsPressed ().Count () < 1 )
                        continue;
                    IsActive = false;
                    OnSelected ( new WMEventArgs { SelectedItem = i } );
                }
            }

            if ( Selected )
            {
                //If we click Inside Selceted Button..
                if ( Mouse.IsInside ( SelectedBtn.Bounds ) )
                {
                    if ( Mouse.GetMouseButtonsPressed ().Count () >= 1 )
                    {//Activate control to be open 
                        IsActive = true;
                        OnMouseClick ( new WMMouseEventArgs () );
                    }
                } //Deactivate it when mouse is pressed somewhere else on screen
                else if ( !Mouse.IsInside ( SelectedBtn.Bounds ) )
                {
                    if ( Mouse.GetMouseButtonsPressed ().Count () >= 1 )
                        IsActive = false;
                }
            }

            if ( !IsActive )
                return;
            var totalSize = Size.Y;
            foreach ( var child in Children )
            {
                if ( SelectedBtn.Text == child.Text )
                {
                    child.Position = Position;
                    child.Size = Size;
                    child.Enabled = false;
                    continue;
                }
                child.Position = Position + new Vector2 ( 0, totalSize );
                child.Size = Size;
                totalSize = ( int )( totalSize + Size.Y );
            }

            UpdateChildren ( gameTime );
        }

        public override void Draw( GameTime gameTime )
        {
            base.Draw ( gameTime );
            if ( SelectedBtn == null )
                return;
            SelectedBtn.Draw ( gameTime );
        }

        public override void DrawFinal( GameTime gameTime )
        {
            if ( IsActive )
                DrawChildren ( gameTime );
        }

        public override void ChangeColor( Color? border, Color? background )
        {
            base.ChangeColor ( border, background );
            foreach ( var c in Children )
            {
                c.ChangeColor ( border, background );
            }
        }

        #endregion
    }
}

