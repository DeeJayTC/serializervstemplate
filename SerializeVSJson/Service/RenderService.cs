using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorLight;

namespace SerializeVSJson.Service
{
    public class TemplateRenderService
    {
        private RazorLightEngine engine { get; set; }
        public TemplateRenderService()
        {
            engine = new RazorLightEngineBuilder()
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task<string> Render(string key, string template, object data)
        {
            return await engine.CompileRenderAsync(key, template, data);
        }
    }
}
