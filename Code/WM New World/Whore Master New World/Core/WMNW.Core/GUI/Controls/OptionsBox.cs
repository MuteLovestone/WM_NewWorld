using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using WMNW.Core.GraphicX;

namespace WMNW.Core.GUI.Controls
{
    public class OptionsBox:ControlBase
    {
        #region Fields

        Vector2 screenSize;
        Vector2 btnSize;
        Vector2 msgSize;
        List<string> opts;

        #endregion

        #region EventsHandlers

        public delegate void OptionEventHandler ( object sender, OptionSelectedEvent e );

        public event OptionEventHandler OnOptionSelected;

        protected virtual void OptionSelected( OptionSelectedEvent e )
        {
            OnOptionSelected?.Invoke ( this, e );
        }

        #endregion

        #region Construct

        public OptionsBox ( Vector2 screenSize, List<string>options, Color textColor, string message )
        {
            this.screenSize = screenSize;
            this.msgSize = msgSize;
            Size = screenSize;
            opts = options;
            Vector2 maxTextSize = Vector2.Zero;
            Font = "UIFont";
            foreach ( string option in opts )
            {
                Vector2 optSize = GraphicsHandler.MesureString ( Font, option );
                if ( optSize.X > maxTextSize.X )
                    maxTextSize = optSize;
            }
            int posx = 2;
            int posy = 2;//( int )( screenSize.Y / 2 ) - ( int )( msgSize.Y / 2 );
            MultiLineBox msgBox = new MultiLineBox ( Font, message, textColor, new Vector2 ( posx, posy ), new Vector2 ( screenSize.X - 6, 250 ) );
            Children.Add ( msgBox );
            posy += ( int )( 250 + 2 );
            List<Button> optBtns = new List<Button> ();
            foreach ( string option in opts )
            {
                Button b = new Button ( Font, option, textColor );
                b.MouseClicked += B_MouseClicked;
                b.Selected = true;
                optBtns.Add ( b );
            }
            ButtonList bl = new ButtonList ( new Vector2 ( screenSize.X - 16, screenSize.Y - 256 ), maxTextSize, new Vector2 ( posx, posy ), optBtns );
            Children.Add ( bl );
        }

        void B_MouseClicked( object sender, WMMouseEventArgs e )
        {
            OptionSelected ( new OptionSelectedEvent ( ( ( Button )sender ).Text ) );
        }

        #endregion

        #region Overrides

        public override void Update( GameTime gameTime )
        {
            if ( !Enabled )
                return;
            base.Update ( gameTime );
            UpdateChildren ( gameTime );
        }

        public override void Draw( GameTime gameTime )
        {
            if ( !Enabled )
                return;
            base.Draw ( gameTime );
            DrawChildren ( gameTime );
        }

        public override void ChangeColor( Color? border, Color? background )
        {
            base.ChangeColor ( border, background );
            foreach ( ControlBase c in Children )
            {
                c.ChangeColor ( border, background );
            }
        }

        #endregion

    }

    public class OptionSelectedEvent
    {
        public string Option;

        public OptionSelectedEvent ( string option )
        {
            Option = option;
        }
    }
}

