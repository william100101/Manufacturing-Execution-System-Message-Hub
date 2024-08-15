using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace MESWebApplication
{
    public static class JSRuntimeExtensions
    {
        public static ValueTask<bool>PrettyPrint(this IJSRuntime jsRuntime)
        {
            return jsRuntime.InvokeAsync<bool>("PR.prettyPrint", null);
        }
        public static ValueTask<bool> EnableTabs(this IJSRuntime jsRuntime)
        {
            return jsRuntime.InvokeAsync<bool>("EnableTabs", null);
        }
    }

}
