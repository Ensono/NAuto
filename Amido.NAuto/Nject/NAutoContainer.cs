using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Amido.NAuto.MultiTargeting;

namespace Amido.NAuto.Nject
{
    public class NAutoContainer
    {
        public static readonly ConcurrentDictionary<Type, Type> Mappings = new ConcurrentDictionary<Type, Type>();

        public static void ClearMappings()
        {
            Mappings.Clear();
        }

        public NAutoContainer Register<TInteface, TImplementation>() where TImplementation : TInteface
        {
            Mappings.AddOrUpdate(typeof(TInteface), typeof(TImplementation), (k, v) => v);
            return this;
        }

        public NAutoContainer Register(Type interfaceType, Type implementationType)
        {
            Mappings.AddOrUpdate(interfaceType, implementationType, (k, v) => v);
            return this;
        }

        public TInterface Resolve<TInterface>()
        {
            return (TInterface)Resolve(typeof(TInterface));
        }

        public object Resolve(Type type)
        {
            if (!Mappings.ContainsKey(type))
            {
                throw new ArgumentException("Type not registered");
            }

            var implementationType = Mappings[type];

            var constructors = implementationType.GetConstructors();

            var constructorParameters = constructors[0].GetParameters();
            var parameterObjects = new List<object>();

            if (constructorParameters.Length > 0)
            {
                foreach (var parameter in constructors[0].GetParameters())
                {
                    parameterObjects.Add(Resolve(parameter.ParameterType));
                }

                return Activator.CreateInstance(implementationType, parameterObjects.ToArray());
            }

            return Activator.CreateInstance(implementationType);
        }
    }
}
