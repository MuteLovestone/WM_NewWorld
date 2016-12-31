using System;

namespace WMNW.Core.Input.Classes
{
    /// <summary>Available buttons on a mouse</summary>
    [Flags]
    public enum MouseButtons
    {
        /// <summary>Left mouse button</summary>
        Left = 1,
        /// <summary>Middle mouse button</summary>
        Middle = 2,
        /// <summary>Right mouse button</summary>
        Right = 4,
        /// <summary>First extended mouse button</summary>
        X1 = 8,
        /// <summary>Second extended mouse button</summary>
        X2 = 16
    }
}

