using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Beastie.Net.Extensions.Constants
{
    public enum CachingTime
    {
        /// <summary>
        ///     No Cache time
        /// </summary>
        /// <value>No Cache time</value>
        None = 0,

        /// <summary>
        ///     Cache for 1 minute
        /// </summary>
        /// <value>Cache for 1 minute</value>
        BelowLow = 1,

        /// <summary>
        ///     Cache for 5 minute
        /// </summary>
        /// <value>Cache for 5 minute</value>
        Low = 5,

        /// <summary>
        ///     Cache for 10 minutes
        /// </summary>
        /// <value>Cache for 10 minutes</value>
        BelowNormal = 10,

        /// <summary>
        ///     Cache for 20 minutes
        /// </summary>
        /// <value>Cache for 20 minutes</value>
        Normal = 20,

        /// <summary>
        ///     Cache for 30 minutes
        /// </summary>
        /// <value>Cache for 30 minutes</value>
        AboveNormal = 30,

        /// <summary>
        ///     Cache for 1 hour
        /// </summary>
        /// <value>Cache for 1 hour</value>
        High = 60,

        /// <summary>
        ///     Cache for 4 hours
        /// </summary>
        /// <value>Cache for 4 hours</value>
        AboveHigh = 240,

        /// <summary>
        ///     Cache for 1 day
        /// </summary>
        /// <value>Cache for 1 day</value>
        Day = 1440
    }
}