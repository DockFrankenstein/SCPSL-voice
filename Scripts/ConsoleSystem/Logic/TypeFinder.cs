﻿using System.Reflection;

namespace ConsoleSystem.Logic
{
    public static class TypeFinder
    {
        public static IEnumerable<MethodInfo> FindAttributes<t>() where t : Attribute =>
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => x.IsClass)
                .SelectMany(x => x.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static))
                .Where(x => x.GetCustomAttributes(typeof(t), false).FirstOrDefault() != null);

        public static List<MethodInfo> FindAttributesList<t>() where t : Attribute =>
            FindAttributes<t>().ToList();
    }
}