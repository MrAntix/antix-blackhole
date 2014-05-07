using System;
using System.Runtime.Serialization;

namespace Antix.Blackhole
{
    [Serializable]
    public class BlackholePathNotFoundException : Exception
    {
        public BlackholePathNotFoundException(string path) :
            base(string.Format(BlackholeResources.PathNotFoundException, path))

        {
        }

        protected BlackholePathNotFoundException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}