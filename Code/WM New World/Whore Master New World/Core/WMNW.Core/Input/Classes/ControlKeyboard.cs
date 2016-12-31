using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Xna.Framework.Input
{
    // Matches the same signatures as Microsoft.Xna.Framework.Input.Keyboard

    public static class ControlKeyboard
    {
        // Since we only have access to KeyboardState's Array constructor, cache arrays to
        // prevent generating garbage on each set or lookup.

        static Keys[] _currentKeys = new Keys[0];
        static Dictionary<int, Keys[]> _arrayCache = new Dictionary<int, Keys[]> ();

        public static bool Control
        {
            get;
            set;
        }

        public static bool Alt
        {
            get;
            set;
        }

        public static bool Shift
        {
            get;
            set;
        }

        public static KeyboardState GetState()
        {
            return new KeyboardState ( _currentKeys );
        }

        public static KeyboardState GetState( PlayerIndex playerIndex )
        {
            return new KeyboardState ( _currentKeys );
        }

        public static void SetKeys( List<Keys> keys )
        {
            if ( !_arrayCache.TryGetValue ( keys.Count, out _currentKeys ) )
            {
                _currentKeys = new Keys[keys.Count];
                _arrayCache.Add ( keys.Count, _currentKeys );
            }

            keys.CopyTo ( _currentKeys );
        }

        public static void Add( Keys key )
        {
            Array.Resize ( ref _currentKeys, _currentKeys.Length + 1 );
            _currentKeys [ _currentKeys.Length - 1 ] = key;
        }

        public static void Remove( Keys key )
        {
            _currentKeys = _currentKeys.Where ( val => val != key ).ToArray ();
        }
    }
}

