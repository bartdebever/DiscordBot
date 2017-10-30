using System;
using System.Collections.Generic;
using RestSharp;
using Riot.LeagueOfLegends;

namespace Riot.Attributes
{
    /// <summary>
    /// Attach endpoint information to a method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class EndpointAttribute : Attribute
    {
        public readonly int Version;
        public readonly string EndpointTemplate;
        /// <summary>
        /// Endpoint must be called with this method.
        /// </summary>
        /// <remarks>
        /// Currently, only Riot API has GET, POST, and PUT, we therefore only allow these methods.
        /// Attempting to use other methods will cause NotSupportException.
        /// </remarks>
        public readonly Method Method;

        public EndpointAttribute(int version, string endpointTemplate, Method method = Method.GET)
        {
            if ((method != Method.GET) || (method != Method.POST) || (method != Method.PUT))
            {
                throw new NotSupportedException($"Cannot attach RiotEndpointAttribute to target method (expected GET, POST, PUT from RestSharp.Method, got RestSharp.Method.{nameof(method)})");
            }

            this.Version = version;
            this.EndpointTemplate = endpointTemplate;
            this.Method = method;
        }
    }
}
