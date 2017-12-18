# sematext-metrics

A .Net Standard Client for [Sematext Custom Metrics](https://sematext.com/docs/monitoring/custom-metrics/)

A free account to generate API tokens is available at [www.sematext.com](https://apps.sematext.com/users-web/register.do)

## Installation

### Package Manager

```powershell
    Install-Package Sematext-Metrics
```

### .Net CLI

```powershell
    dotnet add package Sematext-Metrics
```

## Initialization

Once you create an account and application via the [sematext web ui](https://sematext.com) you will have an app token to use when you initialize the `MetricsClient`

```csharp
    var client = new MetricsClient(<YOUR APP TOKEN>);
```

## Usage

### Send a single metric

```csharp
    // create a new metric
    var metric = new Metric(timestamp: TimeHelper.EpochFromUtc,
                            name: "coffee-consumed",
                            value: 4,
                            aggregateType: AggregateTypes.Sum,
                            filter1: "floor=1",
                            filter2: "strength=bold");

    // send the metric to sematext
    await client.SendAsync(metric);
```

### Send multiple metrics

```csharp
    // create a list to hold multple metrics
    var metrics = new List<Metric>();

    // create the first metric
    metrics.Add(new Metric(timestamp: TimeHelper.EpochFromUtc,
                            name: "coffee-consumed",
                            value: 4,
                            aggregateType: AggregateTypes.Sum,
                            filter1: "floor=1",
                            filter2: "strength=bold"));

    // create the second metric
    metrics.Add(new Metric(timestamp: TimeHelper.EpochFromUtc,
                            name: "coffee-consumed",
                            value: 1,
                            aggregateType: AggregateTypes.Sum,
                            filter1: "floor=3",
                            filter2: "strength=medium"));

    // send the metrics to sematext
    await client.SendAsync(metrics);
```

## Configuration

To send metrics to a different api endpoint (if you are running an on-premise SPM setup) you can override the default endpoint when you initialize the `MetricsClient`

```csharp
    var customEndpoint = new Uri("http://spm-receiver.example.com/spm-receiver/custom/receive.raw");
    var client = new MetricsClient(<YOUR APP TOKEN>, customEndpoint);
```

## Running the unit tests

If you want to run the unit tests in the solution you first need to set an environment variable named `SEMATEXT_APP_TOKEN`. This environment variable is used in the `TestCaseBase` class when it creates the `MetricsClient` that is used to run the unit tests.

### Powershell

```powershell
    [Environment]::SetEnvironmentVariable("SEMATEXT_APP_TOKEN", "<YOUR APP TOKEN>", "User")
```

## Further Reading

https://sematext.com/docs/monitoring/custom-metrics/