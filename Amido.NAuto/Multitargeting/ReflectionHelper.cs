using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Amido.NAuto.MultiTargeting
{
    internal static class ReflectionHelper
    {
        public static bool IsAbstract(this Type type)
        {
#if NET40
            return type.IsAbstract;
#else
            return type.GetTypeInfo().IsAbstract;
#endif
        }

        public static bool IsPrimitive(this Type type)
        {
#if NET40
            return type.IsPrimitive;
#else
            return type.GetTypeInfo().IsPrimitive;
#endif
        }

        public static bool IsEnum(this Type type)
        {
#if NET40
            return type.IsPrimitive;
#else
            return type.GetTypeInfo().IsAbstract;
#endif
        }

        public static bool IsInterface(this Type type)
        {
#if NET40
            return type.IsInterface;
#else
            return type.GetTypeInfo().IsInterface;
#endif
        }

        public static Type BaseType(this Type type)
        {
#if NET40
            return type.BaseType;
#else
            return type.GetTypeInfo().BaseType;
#endif
        }

        public static ConstructorInfo[] GetConstructors(this Type type)
        {
#if NET40
            return type.GetConstructors();
#else
            return type.GetTypeInfo().DeclaredConstructors.ToArray();
#endif
        }

        public static PropertyInfo GetProperty(this Type type, string name)
        {
#if NET40
            return type.GetProperty(name);
#else
            return type.GetTypeInfo().GetDeclaredProperty(name);
#endif
        }

        public static PropertyInfo[] GetProperties(this Type type)
        {
#if NET40
            return type.GetProperties();
#else
            return type.GetTypeInfo().DeclaredProperties.ToArray();
#endif
        }

        public static Type[] GetInterfaces(this Type type)
        {
#if NET40
            return type.GetInterfaces();
#else
            return type.GetTypeInfo().ImplementedInterfaces.ToArray();
#endif
        }



        public static Type[] GetGenericArguments(this Type type)
        {
#if NET40
            return type.GetGenericArguments();
#else
            return type.GetTypeInfo().IsGenericTypeDefinition
                ? type.GetTypeInfo().GenericTypeParameters
                : type.GetTypeInfo().GenericTypeArguments;
#endif
        }

        public static string GetAssemblyPath(Type type)
        {
#if NET40
            return type.Assembly.CodeBase;
#else

            return new Uri(AppContext.BaseDirectory).LocalPath;
#endif
        }
    }
}