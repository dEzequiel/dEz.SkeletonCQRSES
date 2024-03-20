using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dEz.SkeletonCQRSES.ES.Core.Events
{
    public abstract class BaseEvent : Message
    {
        /// <summary>
        /// Version is used to replay the latest state of the aggregate/*.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Discriminator property used for polymortphic data binding 
        /// when serializing event objects.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        protected BaseEvent(string type)
        {
            this.Type = type;
        }
    }
}
