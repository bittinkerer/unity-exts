using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System;
using System.Reflection;
using UnityEngine;

namespace Packages.Estenis.UnityExts_
{
    public class MethodUtilities
    {
        /// <summary>
        /// Hijack a method:
        /// https://blog.adamfurmanek.pl/2021/12/11/custom-memory-allocation-in-c-part-18/index.html
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="sourceMethod"></param>
        /// <param name="targetType"></param>
        /// <param name="targetMethod"></param>
        public static void HijackMethod(Type sourceType, string sourceMethod, Type targetType, string targetMethod)
        {
            var source = sourceType.GetMethod(
                sourceMethod, 
                BindingFlags.Static | BindingFlags.NonPublic, 
                null,
                new Type[] { typeof(UnityEngine.Object), typeof(bool) },
                null);
            var target = targetType.GetMethod(targetMethod);
            string summary = (source == null ? "src:null" : "src:not_null") + (target == null ? "target:null" : "target:not_null");
            Debug.LogWarning($"{summary}");

            RuntimeHelpers.PrepareMethod(source.MethodHandle);
            RuntimeHelpers.PrepareMethod(target.MethodHandle);
            Debug.LogWarning("Prepare done!");

            var offset = 2 * IntPtr.Size;
            IntPtr sourceAddress = Marshal.ReadIntPtr(source.MethodHandle.Value, offset);
            IntPtr targetAddress = Marshal.ReadIntPtr(target.MethodHandle.Value, offset);

            var is32Bit = IntPtr.Size == 4;
            byte[] instruction;

            if (is32Bit)
            {
                instruction = new byte[] {
                    0x68, // push <value>
                }
                 .Concat(BitConverter.GetBytes((int)targetAddress))
                 .Concat(new byte[] {
                    0xC3 //ret
                 }).ToArray();
            }
            else
            {
                instruction = new byte[] {
                    0x48, 0xB8 // mov rax <value>
                }
                .Concat(BitConverter.GetBytes((long)targetAddress))
                .Concat(new byte[] {
                    0x50, // push rax
                    0xC3  // ret
                }).ToArray();
            }

            Marshal.Copy(instruction, 0, sourceAddress, instruction.Length);
        }
    }
}