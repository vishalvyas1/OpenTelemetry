using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace OpenTelemetryExample
{
    public class OtelManager
    {
        public static void StartOpenTelemetry()
        {
            try
            {
                var otConfig = new OtConfig();
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
                var tracerProviderBuilder = Sdk.CreateTracerProviderBuilder().AddAspNetInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSource(otConfig.OtSource)
                    .SetSampler(new AlwaysOnSampler())
                    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(otConfig.OtService))
                    .AddJaegerExporter();
                tracerProviderBuilder.Build();
            }
            catch (Exception e)
            {

                Console.WriteLine($"Encountered an error while initializing OpenTelemtry Configuration with message with message [{e.Message}]");
            }

            
        }

        public class OtConfig
        {
            public string OtSource => "VishalVyasApp";

            public string OtService => "my-app";
        }

    }
}
