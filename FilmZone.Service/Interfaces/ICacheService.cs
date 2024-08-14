using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FilmZone.Service.Interfaces
{
    public interface ICacheService
    {
        void Set(string key, object value);
        bool TryGetValue(string key, out object? value);
    }
}