# Neuroglia StartupTasks
.NET Standard library that provides abstractions and implementations to manage startup tasks

# Usage

[Nuget Package](https://www.nuget.org/packages/Neuroglia.StartupTasks/)

```
  dotnet add package Neuroglia.StartupTasks
```

## Sample Code

### Create a new StartupTask

```C#
 public class MyStartupTask
        : StartupTask
 {

      public MyStartupTask(ILogger<MyStartupTask> logger, IStartupTaskManager startupTaskManager)
          : base(startupTaskManager)
      {
          this.Logger = logger;
      }

      protected ILogger Logger { get; }

      protected override async Task ExecuteAsync(CancellationToken cancellationToken)
      {
          this.Logger.LogInformation("Starting the application...");
          await Task.Delay(5000);
          this.Logger.LogInformation("Application started");
      }

  }
```

### Register the StartupTask with the DependencyInjection

```C#
public void ConfigureServices(IServiceCollection services)
{

    ...

    services.AddStartupTask<MyStartupTask>();

    //Optionally add the StartupTask health check:
    services.AddHealthChecks()
      .AddCheck<StartupTasksHealthCheck>("Startup Tasks");

    ...
}
```

### Optionally register the StartupTaskMiddleware, used for health checks

```C#
 public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
 {
    if (env.IsDevelopment())
        app.UseDeveloperExceptionPage();

    app.UseHealthChecks("/healthz");

    app.UseStartupTasks();

    ...
 }
```

# Contributing

Please see [CONTRIBUTING.md](https://github.com/neuroglia-io/StartupTasks/blob/master/CONTRIBUTING.md) for instructions on how to contribute.
