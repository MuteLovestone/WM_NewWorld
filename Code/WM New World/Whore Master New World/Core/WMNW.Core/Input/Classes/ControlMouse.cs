using System;

namespace Microsoft.Xna.Framework.Input
{
    public static class ControlMouse
    {
        public static int X = 0;
        public static int Y = 0;
        public static int Delta = 0;
        public static ButtonState LeftBtn;
        public static ButtonState MiddleBtn;
        public static ButtonState RightBtn;
        public static ButtonState XBtn1;
        public static ButtonState XBtn2;

        public static MouseState GetState()
        {
            return new MouseState ( X, Y, Delta, LeftBtn, MiddleBtn, RightBtn, XBtn1, XBtn2 );
        }

        public static Vector2 MousePos
        {
            get
            {
                return new Vector2 ( X, Y );
            }
            set
            {
                X = ( int )value.X;
                Y = ( int )value.Y;
            }
        }

        public static bool LDoubleClick
        {
            get;
            set;
        }

        public static bool RDoubleClick
        {
            get;
            set;
        }

        public static bool MDoubleClick
        {
            get;
            set;
        }

        public static bool XBtn2DoubleClick
        {
            get;
            set;
        }

        public static bool XBtn1DoubleClick
        {
            get;
            set;
        }
    }
}

