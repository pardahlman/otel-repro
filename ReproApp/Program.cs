using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;

var services = new ServiceCollection();
services.ConfigureOpenTelemetryTracing(traceProviderBuilder => traceProviderBuilder.ConfigureBuilder(
    (serviceProvider, builder) =>
    {
        builder.AddHttpClientInstrumentation(o => {});
    }));
await using var serviceProvider = services.BuildServiceProvider();

// This call throws NotSupportedException
var traceProvider = serviceProvider.GetRequiredService<TracerProvider>();