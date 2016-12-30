using System;
using WMNW.Core;
using System.IO;

namespace System.Xml
{
    /// <summary>
    /// This class is used to manage and give short cuts to loading and writing xml files
    /// </summary>
    public class XmlSettingManager
    {
        #region Fields

        /// <summary>
        /// The instance.
        /// </summary>
        private static XmlSettingManager _instance;
        /// <summary>
        /// The sets.
        /// </summary>
        private static XmlWriterSettings _sets;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="System.Xml.XmlSettingManager"/> class.
        /// </summary>
        public XmlSettingManager ()
        {
            InstanceManager.CheckInstance ( _instance, "Xml Setting Manager", true );
            _instance = this;
            _sets = new XmlWriterSettings (){ Indent = true };
        }

        #endregion

        #region Game Logic

        /// <summary>
        /// Creates the xml writer.
        /// </summary>
        /// <returns>The xml writer.</returns>
        /// <param name="path">Path.</param>
        public static XmlWriter CreateXmlWriter( string path )
        {
            InstanceManager.CheckInstance ( _instance, "Xml Setting Manager" );
            Directory.CreateDirectory ( Path.GetDirectoryName ( path ) );
            return XmlWriter.Create ( path, _sets );

        }

        /// <summary>
        /// Gets the xml docfor file.
        /// </summary>
        /// <returns>The xml docfor file.</returns>
        /// <param name="path">Path.</param>
        public static XmlDocument GetXmlDocforFile( string path )
        {
            XmlDocument doc = new XmlDocument ();
            doc.Load ( path );
            return doc;
        }

        #endregion
    }
}

