﻿using System;
using Color = Microsoft.Xna.Framework.Color;

namespace System.Xml
{
    /// <summary>
    /// This class is used to extend the XmlWriter class
    /// </summary>
    public static class XMLWriterExtend
    {
        /// <summary>
        /// Writes the element string.
        /// </summary>
        /// <param name="wr">Wr.</param>
        /// <param name="name">Name.</param>
        /// <param name="data">Data.</param>
        public static void WriteElementString( this XmlWriter wr, string name, object data )
        {
            wr.WriteElementString ( name, data.ToString () );
        }

        public static int ConverToInt( this XmlNode node )
        {
            try
            {
                return int.Parse ( node.InnerText );
            }
            catch
            {
                return 0;
            }
        }

        public static bool ConvertToBool( this XmlNode node )
        {
            string nodeText = node.InnerText.ToLower ();
            if ( nodeText == "true" )
                return true;
            else
                return false;
        }

        public static short ConvertToShort( this XmlNode node )
        {
            try
            {
                return short.Parse ( node.InnerText );
            }
            catch
            {
                return 0;
            }
        }

        public static long ConvertToLong( this XmlNode node )
        {
            try
            {
                return long.Parse ( node.InnerText );
            }
            catch
            {
                return 0;
            }
        }

        public static byte ConvertToByte( this XmlNode node )
        {
            try
            {
                return byte.Parse ( node.InnerText );
            }
            catch
            {
                return 0;
            }
        }

        public static Color ConvertToColor( this XmlNode node )
        {
            byte r = node [ "R" ].ConvertToByte ();
            byte g = node [ "G" ].ConvertToByte ();
            byte b = node [ "B" ].ConvertToByte ();
            byte a = node [ "A" ].ConvertToByte ();
            return new Color ( r, g, b, a );
        }
        //TODO: Add ConvertTo(ValueType) when need to read values from node
    }
}
