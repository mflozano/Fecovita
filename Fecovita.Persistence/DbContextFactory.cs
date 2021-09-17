using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fecovita.Persistence
{
    public class DbContextFactory
    {
        /// <summary>
        /// The sync root.
        /// </summary>
        private static readonly object SyncRoot = new object();       

        /// <summary>
        /// The private instance
        /// </summary>
        private static FecovitaContext _instance { get; set; }

        public static FecovitaContext Instance
        {
            get
            {
                return GetCurrentInstance();
            }
        }

        /// <summary>
        /// Returns the current instance
        /// </summary>
        private static FecovitaContext GetCurrentInstance()
        {
           
            if (_instance == null)
            {
                lock (SyncRoot)
                {
                    // instance new context
                    _instance = new FecovitaContext();                             
                }
            }

            return _instance;
        }
    }
}

