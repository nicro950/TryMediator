#nullable enable
using System;
using System.Collections.Generic;

namespace MediatrTry
{
    public class AMediator
    {
        Dictionary<Type, Delegate> dictionary = new Dictionary<Type, Delegate>();

        public void RegisterHandler<T ,T2>(Func<T, T2> action)
            where T : IRequest<T2>
        {
            Type t2 = typeof(T);
            dictionary.Add(typeof(T), action);
        }

        public T Send<T>(IRequest<T> message)   
        {
            if (dictionary.TryGetValue(message.GetType(), out Delegate d))
            {
                return (T)d.DynamicInvoke(message);
            }
            return default;
        }
    }
}
