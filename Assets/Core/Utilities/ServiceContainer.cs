using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services.Base;

namespace Core.Utilities
{
    public static class ServiceContainer
    {
        private static readonly List<IService> Services = new List<IService>();

        public static void Add(IService service)
        {
            if (!Services.Contains(service))
            {
                Services.Add(service);
            }
        }

        public static T GetService<T>() where T : class, IService
        {
            var service = Services.OfType<T>().FirstOrDefault();

            if (service is null)
            {
                throw new Exception($"Service of type {typeof(T).Name} not found!");
            }

            return service;
        }
    }
}
