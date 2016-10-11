// using System;
// using System.Diagnostics;
// using System.Threading;
// using Hudl.Common.Clock;
// using Hudl.Config;
// using Hudl.Mjolnir.External;
// using Hudl.Mjolnir.Key;

// namespace Atom.CircuitBreaker.Metrics
// {
//     internal class StandardCommandMetrics : ICommandMetrics
//     {
//         private readonly IClock _clock;
//         private readonly ResettingNumbersBucket _resettingNumbersBucket;
//         private readonly IConfigurableValue<long> _snapshotTtlMillis;
//         private readonly GroupKey _key;
//         private readonly IStats _stats;

//         internal StandardCommandMetrics(GroupKey key, IConfigurableValue<long> windowMillis, IConfigurableValue<long> snapshotTtlMillis, IStats stats)
//             : this(key, windowMillis, snapshotTtlMillis, new SystemClock(), stats) {}

//         internal StandardCommandMetrics(GroupKey key, IConfigurableValue<long> windowMillis, IConfigurableValue<long> snapshotTtlMillis, IClock clock, IStats stats)
//         {
//             if (key == null)
//             {
//                 throw new ArgumentNullException("key");
//             }

//             _key = key;
//             _clock = clock;
//             _snapshotTtlMillis = snapshotTtlMillis;
//             _resettingNumbersBucket = new ResettingNumbersBucket(_clock, windowMillis);

//             if (stats == null)
//             {
//                 throw new ArgumentNullException("stats");
//             }
//             _stats = stats;
//         }

//         private string StatsPrefix
//         {
//             get { return "mjolnir metrics " + _key; }
//         }

//         public void MarkCommandSuccess()
//         {
//             _stats.Event(StatsPrefix + " Mark", CounterMetric.CommandSuccess.ToString(), null);
//             _resettingNumbersBucket.Increment(CounterMetric.CommandSuccess);
//         }

//         public void MarkCommandFailure()
//         {
//             _stats.Event(StatsPrefix + " Mark", CounterMetric.CommandFailure.ToString(), null);
//             _resettingNumbersBucket.Increment(CounterMetric.CommandFailure);
//         }

//         private long _lastSnapshotTimestamp = 0;
//         private MetricsSnapshot _lastSnapshot = new MetricsSnapshot(0, 0);

//         public MetricsSnapshot GetSnapshot()
//         {
//             var stopwatch = Stopwatch.StartNew();
//             try
//             {
//                 var lastSnapshotTime = _lastSnapshotTimestamp;
//                 var currentTime = _clock.GetMillisecondTimestamp();

//                 if (_lastSnapshot == null || currentTime - lastSnapshotTime > _snapshotTtlMillis.Value)
//                 {
//                     // Try to update the _lastSnapshotTimestamp. If we update it, this thread will take on the authority of updating
//                     // the snapshot. CompareExchange returns the original result, so if it's different from currentTime, we successfully exchanged.
//                     if (Interlocked.CompareExchange(ref _lastSnapshotTimestamp, currentTime, _lastSnapshotTimestamp) != currentTime)
//                     {
//                         var createwatch = Stopwatch.StartNew();
//                         try
//                         {
//                             // TODO rob.hruska 11/8/2013 - May be inaccurate if counts are incremented as we're querying these.
//                             var success = _resettingNumbersBucket.GetCount(CounterMetric.CommandSuccess);
//                             var failure = _resettingNumbersBucket.GetCount(CounterMetric.CommandFailure);
//                             var total = success + failure;

//                             int errorPercentage;
//                             if (total == 0)
//                             {
//                                 errorPercentage = 0;
//                             }
//                             else
//                             {
//                                 errorPercentage = (int) (success == 0 ? 100 : (failure / (double) total) * 100);
//                             }

//                             _lastSnapshot = new MetricsSnapshot(total, errorPercentage);
//                         }
//                         finally
//                         {
//                             createwatch.Stop();
//                             _stats.Elapsed(StatsPrefix + " CreateSnapshot", null, createwatch.Elapsed);
//                         }
//                     }
//                 }

//                 return _lastSnapshot;
//             }
//             finally
//             {
//                 _stats.Elapsed(StatsPrefix + " GetSnapshot", null, stopwatch.Elapsed);
//             }
//         }

//         public void Reset()
//         {
//             // TODO For stats purposes on sparse operations, it might be nice to have this called at regular intervals.
//             var stopwatch = Stopwatch.StartNew();
//             try
//             {
//                 _resettingNumbersBucket.Reset();
//             }
//             finally
//             {
//                 _stats.Elapsed(StatsPrefix + " Reset", null, stopwatch.Elapsed);
//             }
//         }
//     }
// }