using Adage.Tessitura;
using System;
using System.Collections.Generic;
using System.Web;

namespace PublicTheater.Custom.CoreFeatureSet.Helper
{
    
    public static class CacheHelper
	{
		private static readonly object LockObject = new object();

		//Only used when unit testing
		private static Dictionary<String, Object> _itemsToCache = new Dictionary<string, object>();

        public static int WebCacheMinute
        {
            get { return Config.GetValue(TheaterTemplate.Shared.Common.TheaterSharedAppSettings.WEB_CACHE_MINUTES, 10); }
        }
		/// <summary>
		/// Gets an item from the cache, adds it to the cache if it is not present
		/// </summary>
		/// <typeparam name="T">The type of item that is to be added to the cache</typeparam>
		/// <param name="cacheKey">A unique key to identify this item within the cache</param>
		/// <param name="creator">A function that will create the item if it cannot be found in the cache</param>
		/// <param name="cacheLengthMinutes">The number of minutes to store the item in the cache</param>
		/// <returns>The cached object</returns>
        public static T GetCachedItem<T>(string cacheKey, Func<T> creator, int cacheLengthMinutes) where T : class
		{
			T returnValue = null;

			if (HttpContext.Current == null)
			{ 
				if (!_itemsToCache.ContainsKey(cacheKey))
				{
					returnValue = creator();

					if (returnValue == null)
						throw new InvalidOperationException("The create new item function cannot return null");

					//This is not being locked because this will only be called in unit tests and therefore will likely uncounter threading issues
					_itemsToCache.Add(cacheKey, returnValue);
				}
			}
			else
			{
				returnValue = HttpContext.Current.Cache[cacheKey] as T;
				if (returnValue == null)
				{
					lock (LockObject)
					{
						returnValue = HttpContext.Current.Cache[cacheKey] as T;
						if (returnValue == null)
						{
							returnValue = creator();

							if (returnValue == null)
								throw new InvalidOperationException("The create new item function cannot return null");

							HttpContext.Current.Cache.Add(cacheKey, returnValue, null, DateTime.Now.AddMinutes(cacheLengthMinutes), System.Web.Caching.Cache.NoSlidingExpiration,
								System.Web.Caching.CacheItemPriority.Normal, null);
						}
					}
				}
			}

			return returnValue;
		}

        /// <summary>
        /// Gets an item from the cache, adds it to the cache if it is not present
        /// </summary>
        /// <typeparam name="T">The type of item that is to be added to the cache</typeparam>
        /// <param name="cacheKey">A unique key to identify this item within the cache</param>
        /// <param name="creator">A function that will create the item if it cannot be found in the cache</param>
        /// <returns>The cached object</returns>
        public static T GetCachedItem<T>(string cacheKey, Func<T> creator) where T : class
        {
            return GetCachedItem(cacheKey, creator, WebCacheMinute);
        }

        /// <summary>
        /// Gets an item from the cache, adds it to the cache if it is not present
        /// </summary>
        /// <typeparam name="T">The type of item that is to be added to the cache</typeparam>
        /// <param name="cacheKey">A unique key to identify this item within the cache</param>
        /// <param name="cacheObject">The item if it cannot be found in the cache</param>
        /// <param name="cacheLengthMinutes">The number of minutes to store the item in the cache</param>
        /// <returns>The cached object</returns>
        public static T GetCachedItem<T>(string cacheKey, T cacheObject, int cacheLengthMinutes) where T : class
        {
            T returnValue = null;

            if (HttpContext.Current == null)
            {
                if (!_itemsToCache.ContainsKey(cacheKey))
                {
                    returnValue = cacheObject;

                    if (returnValue == null)
                        throw new InvalidOperationException("The create new item function cannot return null");

                    //This is not being locked because this will only be called in unit tests and therefore will likely uncounter threading issues
                    _itemsToCache.Add(cacheKey, returnValue);
                }
            }
            else
            {
                returnValue = HttpContext.Current.Cache[cacheKey] as T;
                if (returnValue == null)
                {
                    lock (LockObject)
                    {
                        returnValue = HttpContext.Current.Cache[cacheKey] as T;
                        if (returnValue == null)
                        {
                            returnValue = cacheObject;

                            if (returnValue == null)
                                throw new InvalidOperationException("The create new item function cannot return null");

                            HttpContext.Current.Cache.Add(cacheKey, returnValue, null, DateTime.Now.AddMinutes(cacheLengthMinutes), System.Web.Caching.Cache.NoSlidingExpiration,
                                System.Web.Caching.CacheItemPriority.Normal, null);
                        }
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Gets an item from the cache, adds it to the cache if it is not present
        /// </summary>
        /// <typeparam name="T">The type of item that is to be added to the cache</typeparam>
        /// <param name="cacheKey">A unique key to identify this item within the cache</param>
        /// <param name="cacheObject">The item if it cannot be found in the cache</param>
        /// <returns>The cached object</returns>
        public static T GetCachedItem<T>(string cacheKey, T cacheObject) where T : class
        {
            return GetCachedItem<T>( cacheKey, cacheObject, WebCacheMinute);
        }
	}

}
