using Microsoft.Extensions.Options;
using System.Threading;
using System;

namespace Atom.CircuitBreaker.Util
{
    internal class GaugeTimer
    {
        // ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
        // Don't let these get garbage collected.
        private readonly Timer _timer;
        private readonly IOptionsMonitor<GaugeTimerSettings> _gaugeSetting;
        // ReSharper restore PrivateFieldCanBeConvertedToLocalVariable

        /// <summary>
        /// Constructs a new GaugeTimer that invokes the provided handler.
        /// 
        /// If the interval millis override is provided, it'll be used. Otherwise a
        /// ConfigurableValue for "mjolnir.gaugeIntervalMillis" || 5000 will be used.
        /// 
        /// intervalMillisOverride should typically only be used for testing.
        /// </summary>
        /// <param name="onTick">Event handler to invoke on tick</param>
        /// <param name="intervalMillisOverride">Interval override (for unit testing)</param>
        internal GaugeTimer(TimerCallback callback, IOptionsMonitor<GaugeTimerSettings> gaugeSetting)
        {
            if (gaugeSetting == null) throw new ArgumentNullException(nameof(gaugeSetting));

            _gaugeSetting = gaugeSetting ;
            _gaugeSetting.OnChange(p => UpdateStatsGaugeInterval(p.Interval));
            
            _timer = new Timer(callback, null, 0, _gaugeSetting.CurrentValue.Interval);            
            
        }

        private void UpdateStatsGaugeInterval(int millis)
        {
            if (_timer == null)            
                return;
                    
            _timer.Change(0, millis); 
            
        }
    }
}
