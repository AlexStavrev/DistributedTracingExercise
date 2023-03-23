using System.Diagnostics;
using System.Reflection;
using OpenTelemetry;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Monitoring;

public static class Monitoring
{
    public static readonly ActivitySource ActivitySource = new("RPS_" + Assembly.GetEntryAssembly()?.GetName().Name, "1.0.0");

    static Monitoring()
    {
        Sdk.CreateTracerProviderBuilder()
            .AddZipkinExporter()
            .AddConsoleExporter()
            .AddSource(ActivitySource.Name)
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(serviceName: ActivitySource.Name))
            .Build();
    }
}