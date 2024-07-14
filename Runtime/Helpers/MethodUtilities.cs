using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace Packages.Estenis.UnityExts_
{
    public static class MethodUtilities
    {
        public static MethodInfo GetMethodInfo(Type objectType,  string methodName)
        {
            var methods = objectType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            return methods.FirstOrDefault(m => m.Name == methodName);
        }

    }    

}

