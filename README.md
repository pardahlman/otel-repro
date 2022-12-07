Start MSSQL server in Docker

```
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

Run the application. It outputs:

```
Parent activity started. Current activity: repro, Recorded: True
After SQL command. Current activity: OpenTelemetry.Instrumentation.SqlClient.Execute, Recorded: False
After HTTP call. Current activity: OpenTelemetry.Instrumentation.SqlClient.Execute, Recorded: False
```

Change order of call to SQL and HTTP call. It outputs:

```
Parent activity started. Current activity: repro, Recorded: True
After HTTP call. Current activity: repro, Recorded: True
After SQL command. Current activity: OpenTelemetry.Instrumentation.SqlClient.Execute, Recorded: False

```