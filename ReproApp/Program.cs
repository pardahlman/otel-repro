using System;
using System.Diagnostics;
using System.Net.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Trace;

var services = new ServiceCollection();
services.AddHttpClient();
services.ConfigureOpenTelemetryTracing(builder =>
    {
        builder.AddSqlClientInstrumentation(o => o.Filter = _ => false);
        builder.AddHttpClientInstrumentation(o => o.FilterHttpRequestMessage = _ => true);
    });
await using var serviceProvider = services.BuildServiceProvider();
_ = serviceProvider.GetRequiredService<TracerProvider>();

// Start recorded parent activity
var activity = new Activity("repro");
activity.ActivityTraceFlags = ActivityTraceFlags.Recorded;
activity.Start();
Console.WriteLine($"Parent activity started. Current activity: {Activity.Current.OperationName}, Recorded: {Activity.Current.Recorded}");

// Trigger SqlClient instrumentation
var connection = new SqlConnection("Server=localhost;User Id=sa;Password=yourStrong(!)Password;TrustServerCertificate=True");
await connection.OpenAsync();
var command = connection.CreateCommand();
command.CommandText = @"SELECT 1";
var reader = await command.ExecuteReaderAsync();
await command.DisposeAsync();
await reader.DisposeAsync();
await connection.DisposeAsync();
Console.WriteLine($"After SQL command. Current activity: {Activity.Current.OperationName}, Recorded: {Activity.Current.Recorded}");

// Trigger HttpClient instrumentation
var httpClient = serviceProvider.GetRequiredService<HttpClient>();
await httpClient.GetAsync("https://example.com");
Console.WriteLine($"After HTTP call. Current activity: {Activity.Current.OperationName}, Recorded: {Activity.Current.Recorded}");

activity.Stop();