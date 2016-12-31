using System;
using WMNW.Core.Input.Classes;
using System.Collections.Generic;
using WMNW.Core.Input.Devices.Interfaces;
using System.Collections.ObjectModel;
using Microsoft.Xna.Framework;

namespace WMNW.Core.Input.Manager
{
    /// <summary>
    /// Standard Interface to all of the games input
    /// </summary>
    public interface IInputService
    {
        #region Properties

        /// <summary>Number of snapshots currently in the queue</summary>
        int SnapshotCount
        {
            get;
        }

        /// <summary>All mice known to the system</summary>
        ReadOnlyCollection<IMouse> Mice
        {
            get;
        }

        /// <summary>Returns the primary mouse input device</summary>
        /// <returns>The primary mouse</returns>
        IMouse GetMouse();

        #endregion

        #region Xna Methods

        /// <summary>Updates the state of all input devices</summary>
        /// <remarks>
        ///   <para>
        ///     If this method is called with no snapshots in the queue, it will
        ///     query the state of all input devices immediately, raising events
        ///     for any changed states. This way, you can ignore the entire
        ///     snapshot system if you just want basic input device access.
        ///   </para>
        ///   <para>
        ///     If this method is called while one or more snapshots are waiting in
        ///     the queue, this method takes the next snapshot from the queue and makes
        ///     it the current state of all active devices.
        ///   </para>
        /// </remarks>
        void Update( GameTime gameTime );

        #endregion

        #region Other Methods

        /// <summary>Takes a snapshot of the current state of all input devices</summary>
        /// <remarks>
        ///   This snapshot will be queued until the user calls the Update() method,
        ///   where the next polled snapshot will be taken from the queue and provided
        ///   to the user.
        /// </remarks>
        void TakeSnapshot();

        #endregion
    }
}

