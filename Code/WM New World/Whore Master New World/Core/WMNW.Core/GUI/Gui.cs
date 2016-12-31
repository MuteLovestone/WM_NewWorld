using System;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;
using System.Collections.Generic;
using WMNW.Core.Input.Devices.Generic;
using System.Linq;
using WMNW.Core.GUI.Controls;
using MouseButtons = WMNW.Core.Input.Classes.MouseButtons;

namespace WMNW.Core.GUI
{
    public class Gui
    {
        #region Variables

        #endregion

        #region Properties

        public List<ControlBase> Controls = null;

        protected PcMouse Mouse = ( ( PcMouse )GameBase.Input.GetMouse () );
        protected PcKeyboard Keyboard = ( ( PcKeyboard )GameBase.Input.GetKeyboard () );

        public ControlBase SelectedControl = null;

        #endregion

        #region Constructor

        public Gui ()
        {
            Controls = new List<ControlBase> ();
        }

        #endregion

        #region Methods

        public void Add( ControlBase control )
        {
            Remove ( control );
            Controls.Add ( control );

            SelectControl ( control );
        }

        public void Remove( ControlBase control )
        {
            if ( Controls.Exists ( c => c == control ) )
                Controls.Remove ( control );

            SelectControl ( control );
        }

        private void SelectControl( ControlBase control )
        {
            //Order the controls
            Controls.OrderBy ( x => x.ZIndex );

            //Select the first control found
            SelectedControl = control;

            //Unselect each control
            foreach ( var child in Controls )
            {
                child.Selected = false;

                foreach ( var c in control.Children )
                    c.Selected = false;
            }

            //Set our control as Selected
            if ( SelectedControl != null && SelectedControl.Selectable )
            {
                control.Selected = true;
                foreach ( var c in control.Children )
                    c.Selected = true;
            }
        }

        /// <summary>
        /// Load From Binary
        /// </summary>
        public void Load()
        {

        }

        /// <summary>
        /// Save To Binary
        /// </summary>
        public void Save()
        {

        }

        #endregion

        public void Update( GameTime gameTime )
        {
            //Do hit test and see if we hit selected control
            if ( !SelectedControl.Selectable || ( !Mouse.IsInside ( SelectedControl.Bounds ) && Mouse.ButtonPressed ( MouseButtons.Left ) ) )
            {
                var newControl = Controls.FirstOrDefault ( control => control.Bounds.Contains ( Mouse.CurrentPosition () ) && control.Selectable );
                if ( newControl != null )
                    SelectControl ( newControl );
            }

            Controls.OrderBy ( x => x.ZIndex );

            foreach ( var c in Controls )
                c.Update ( gameTime );
        }

        public void Draw( GameTime gameTime )
        {
            Controls.OrderBy ( x => x.ZIndex );

            foreach ( var c in Controls )
                c.Draw ( gameTime );

            foreach ( var c in Controls )
                c.DrawFinal ( gameTime );
        }
    }
}

