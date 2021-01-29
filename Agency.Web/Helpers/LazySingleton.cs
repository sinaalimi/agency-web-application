using System;
using System.Threading;

namespace Agency.Web.Helpers
{
    public sealed class LazySingleton
    {
        private static readonly  Lazy<LazySingleton> _instance = new Lazy<LazySingleton>(() => new LazySingleton(),
            LazyThreadSafetyMode.ExecutionAndPublication);

        private LazySingleton() { }

        public static  LazySingleton Instance => _instance.Value;

       

    }
}