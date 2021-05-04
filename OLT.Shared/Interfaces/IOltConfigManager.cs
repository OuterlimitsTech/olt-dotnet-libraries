using System;

namespace OLT.Core
{
    public interface IOltConfigManager : IOltInjectableSingleton
    {
        T GetValue<T>(string key, T defaultValue) where T : IConvertible;
        T GetValue<T>(string key) where T : IConvertible;
        T GetSection<T>(string sectionName);
    }
}