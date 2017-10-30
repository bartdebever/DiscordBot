using System;
using Riot.Enums;

namespace Riot.Attributes
{
    /// <summary>
    /// Required policy to make API request to the endpoint.
    /// If your token does not have these policy enabled, Riot.NET will preemptively block you from making requests.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RequiredPolicyAttribute : Attribute
    {
        public readonly Policy[] Policies;

        public RequiredPolicyAttribute(params Policy[] policies)
        {
            this.Policies = policies;
        }
    }
}
