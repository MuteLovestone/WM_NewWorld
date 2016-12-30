using System;

namespace WMNW.Core
{
    /// <summary>
    /// This class is used to make sure that the instances are only created once
    /// </summary>
    public class InstanceManager
    {
        #region Fields

        #endregion

        #region Construct

        #endregion

        #region Logic

        /// <summary>
        /// Checks the instance.
        /// </summary>
        /// <param name="instance">Instance.</param>
        /// <param name="name">Name.</param>
        /// <param name="forceNull">If set to <c>true</c> force instance to be null.</param>
        public static void CheckInstance( object instance, string name, bool forceNull = false )
        {
            if ( forceNull )
            {
                if ( instance != null )
                    throw new InstanceNotNullException ( name );
                else
                    return;
            }
            else
            {
                if ( instance == null )
                    throw new InstanceNotYetCreatedException ( name );
                else
                    return;
            }
        }

        #endregion
    }

    public class InstanceNotNullException:Exception
    {
        public InstanceNotNullException ( string name )
            : base ( "The Instance of " + name + " has already been Created, and can not be created again" )
        {
            
        }
    }

    public class InstanceNotYetCreatedException:Exception
    {
        public InstanceNotYetCreatedException ( string name )
            : base ( "The Instance of " + name + " has yet to be created and must be created before called" )
        {
            
        }
    }
}

