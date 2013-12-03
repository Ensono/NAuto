using System;
using System.Collections.Generic;

namespace Amido.NAuto.Nject
{
    public class NAutoContainer
    {
        public static Dictionary<Type, Type> Mappings = new Dictionary<Type, Type>();

        public NAutoContainer Register<TInteface, TImplementation>() where TImplementation : TInteface
        {
            Mappings.Add(typeof(TInteface), typeof(TImplementation));
            return this;
        }

        public NAutoContainer Register(Type interfaceType, Type implementationType)
        {
            Mappings.Add(interfaceType, implementationType);
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
