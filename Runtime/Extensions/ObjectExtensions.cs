
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

public static class ObjectExtensions
{

    public static unsafe void* GetObjectAddress(this object obj)
    {
        TypedReference tr = __makeref(obj);
        IntPtr ptr = **(IntPtr**)(&tr);
        return (void*)ptr;//Pointer.Unbox(ptr);
    }

    public static unsafe void TransmuteTo(this object target, object source)
    {
        if (target is null || source is null || target.GetType() == source.GetType()) return;

        var s = (void**)source.GetObjectAddress();
        var t = (void**)target.GetObjectAddress();
        *t = *s;

        if(target.GetType() != source.GetType())
        {
            throw new AccessViolationException();
        }
    }

    public static T TransmuteTo<T>(this object target, T source)
    {
        target.TransmuteTo((object)source);
        return (T)target;
    }

    public static T TransmuteTo<T>(this object target) where T : new() =>
        target.TransmuteTo(new T());
}