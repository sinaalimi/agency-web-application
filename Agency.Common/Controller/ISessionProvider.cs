﻿namespace Agency.Common.Controller
{
    public interface ISessionProvider
    {
        object Get(string key);
        T Get<T>(string key) where T : class;
        void Remove(string key);
        void RemoveAll();
        void Store<T>(string key, T value) where T : class;
    }
}
