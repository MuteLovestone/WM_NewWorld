using System;
using System.Collections.Generic;

namespace WMNW.Core.Input.Classes
{
    /// <summary>Provides helper methods for collections</summary>
    internal static class CollectionHelper
    {

        /// <summary>Returns an item from a list if the index exists</summary>
        /// <typeparam name="TItemType">Type of the item that will be returned</typeparam>
        /// <param name="list">List the item will be taken from</param>
        /// <param name="index">Index from which the item will be taken</param>
        /// <returns>The item if the index existed, otherwise a default item</returns>
        public static TItemType GetIfExists<TItemType>( IList<TItemType> list, int index )
        {
            return list.Count > index ? list [ index ] : default(TItemType);
        }
    }
}

