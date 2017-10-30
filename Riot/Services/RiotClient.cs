using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using RestSharp;
using Riot.Attributes;
using Riot.Services.Interfaces;

namespace Riot.Services
{
    /// <summary>
    /// Endpoint class containing the information of a method's attributes
    /// </summary>
    public class Endpoint
    {
        public readonly string EndpointTemplate;
        public readonly GroupAttribute GroupAttribute;
        public readonly EndpointAttribute EndpointAttribute;
        public readonly DisabledRegionsAttribute DisabledRegionsAttribute;
        public readonly RequiredPolicyAttribute RequiredPolicyAttribute;

        public Endpoint(GroupAttribute group, EndpointAttribute endpoint, DisabledRegionsAttribute disabledRegions, RequiredPolicyAttribute requiredPolicies)
        {
            this.GroupAttribute = group;
            this.EndpointAttribute = endpoint;
            this.DisabledRegionsAttribute = disabledRegions;
            this.RequiredPolicyAttribute = requiredPolicies;
            this.EndpointTemplate = $"{group.Game}/{group.Group}/v{endpoint.Version}/{endpoint.EndpointTemplate}";
        }
    }

    public class RiotClient
    {
        private string baseUrl = "api.riotgames.com";

        public T Execute<T>(RestRequest request, Dictionary<string, object> dictionary = null, object body = null) where T : new()
        {
            RestClient client = new RestClient($"https://na1.{baseUrl}");
            request.AddHeader("X-Riot-Token", /*TOKEN HERE AS STRING*/);
            request.AddBody(body);

            foreach (var entry in dictionary ?? new Dictionary<string, object>())
            {
                request.AddParameter(entry.Key, entry.Value);
            }

            return client.Execute<T>(request).Data;
        }

        public static IList<Endpoint> FetchEndpoints()
        {
            IList<Endpoint> endpoints = new List<Endpoint>();
            IEnumerable<Type> classes = from Type in Assembly.GetEntryAssembly().GetTypes()
                                        where Type.IsSubclassOf(typeof(IEndpoint))
                                        select Type;

            // iterates over classes that implements IEndpoint interface
            foreach (Type cls in classes)
            {
                GroupAttribute group = cls.GetCustomAttribute<GroupAttribute>();

                foreach (MethodInfo method in cls.GetMethods())
                {
                    EndpointAttribute endpoint = method.GetCustomAttribute<EndpointAttribute>();
                    DisabledRegionsAttribute disabledRegions = method.GetCustomAttribute<DisabledRegionsAttribute>();
                    RequiredPolicyAttribute requiredPolicies = method.GetCustomAttribute<RequiredPolicyAttribute>();

                    endpoints.Add(new Endpoint(group, endpoint, disabledRegions, requiredPolicies));
                }
            }

            return endpoints;
        }
    }
}
