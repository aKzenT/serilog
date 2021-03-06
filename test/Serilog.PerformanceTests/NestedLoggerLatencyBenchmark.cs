using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Serilog;
using System;
using System.Threading.Tasks;
using Serilog.PerformanceTests.Support;
using Xunit;
using Serilog.Events;

namespace Serilog.PerformanceTests
{
    /// <summary>
    /// Tests the overhead of writing through a nested logger.
    /// </summary>
    public class NestedLoggerLatencyBenchmark
    {
        ILogger _log, _nested;
        readonly LogEvent _event = Some.InformationEvent();

        [Setup]
        public void Setup()
        {
            _log = new LoggerConfiguration()
                .WriteTo.Sink(new NullSink())
                .CreateLogger();

            _nested = _log.ForContext<NestedLoggerLatencyBenchmark>();
        }

        [Benchmark(Baseline = true)]
        public Task RootLogger()
        {
            return _log.Write(_event);
        }

        [Benchmark]
        public Task NestedLogger()
        {
            return _nested.Write(_event);
        }
    }
}
  