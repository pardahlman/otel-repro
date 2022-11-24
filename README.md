Running the repro app result in unhandled exception

```
System.NotSupportedException: Services cannot be configured after ServiceProvider has been created.
   at OpenTelemetry.Trace.TracerProviderBuilderBase.ConfigureServices(Action`1 configure)
   at OpenTelemetry.Trace.TracerProviderBuilderExtensions.ConfigureServices(TracerProviderBuilder tracerProviderBuilder, Action`1 configure)
   at OpenTelemetry.Trace.TracerProviderBuilderExtensions.AddHttpClientInstrumentation(TracerProviderBuilder builder, String name, Action`1 configureHttpClientInstrumentationOptions)
   at OpenTelemetry.Trace.TracerProviderBuilderExtensions.AddHttpClientInstrumentation(TracerProviderBuilder builder, Action`1 configureHttpClientInstrumentationOptions)
   at Program.<>c.<<Main>$>b__0_1(IServiceProvider serviceProvider, TracerProviderBuilder builder) in C:\code\github\otel-repro\ReproApp\Program.cs:line 8
```

Note that it is possible to register the http client instrumentation without the configuration lamda

```csharp
builder.AddHttpClientInstrumentation();
```