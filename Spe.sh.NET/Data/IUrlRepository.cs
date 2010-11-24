using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spe.sh.NET.Data
{
    public interface IUrlRepository : IDisposable
    {
        void SetupInitialData();
        string AddUrl(Uri uri);
        Uri ResolveUrl(string code, string ip, string referer);
    }
}
