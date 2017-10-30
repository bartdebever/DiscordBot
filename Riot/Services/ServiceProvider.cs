using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Riot.Services.Interfaces;

namespace Riot.Services
{
    public interface IRiotServiceProvider : IServiceProvider, IEnumerable<object>
    {
        T GetService<T>();
    }

    public class ServiceProvider : IRiotServiceProvider
    {
        public class ServiceProviderBuilder
        {
            private ConcurrentDictionary<Type, object> dictionary = new ConcurrentDictionary<Type, object>();

            public ServiceProvider Build()
            {
                return new ServiceProvider(dictionary);
            }

            public ServiceProviderBuilder AddManual<T>(T obj)
            {
                dictionary.TryAdd(typeof(T), obj);
                return this;
            }

            public ServiceProviderBuilder LoadFrom(Assembly assembly)
            {
                // TODO: https://github.com/MathieuPomerleau/JhinBotCore/blob/master/JhinBot/Services/Implementation/ServiceProvider.cs
            }
        }

        public T GetService<T>()
        {

        }
    }
}
