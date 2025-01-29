using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;


namespace DGTIT.Checador.Utilities {
    internal class InternalClock {

        private readonly System.Windows.Forms.Timer _timer;
        private readonly EventLog eventLog;
        private DateTime _currentTime;
        public DateTime CurrentTime { get => _currentTime; }
        public Action<DateTime> OnTimeChange { get; set; }
        
        public InternalClock(DateTime targetTime, EventLog eventLog) {
            this.eventLog = eventLog;

            // Set initial time
            _currentTime = targetTime;

            // Initialize the timer to tick every second (1000 ms)
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = (int) TimeSpan.FromSeconds(1).TotalMilliseconds;
            _timer.Tick += new EventHandler(UpdateTime);
        }

        private void UpdateTime(object sender, EventArgs e) {
            try {
                _currentTime = _currentTime.AddSeconds(1);
                Console.WriteLine("(-) CurrentTime: " + _currentTime.ToLongTimeString());
            }
            catch(Exception err) {
                Console.WriteLine("Fail at update the internal clock: " + err.Message );
                this.eventLog.WriteEntry($"Fail at update the internal clock: [{err.Message}]", EventLogEntryType.Error);
            }

            try {
                OnTimeChange?.Invoke(_currentTime);
            }
            catch(Exception err) {
                Console.WriteLine($"Fail to notify the time elapsed: {err.Message}");
                this.eventLog.WriteEntry($"Fail to notify the time elapsed: [{err.Message}]", EventLogEntryType.Error);
            }
        }

        public void StartClock() {
            _timer.Start();
        }

        public void StopClock() {
            _timer.Stop();
            _timer.Dispose();
        }

        public void SyncClock(DateTime time) {
            try {
                _currentTime = time;
                Console.WriteLine($"Clock synchronized to: {_currentTime.ToLongTimeString()}");
                this.eventLog.WriteEntry($"Clock synchronized to: {_currentTime.ToLongTimeString()}", EventLogEntryType.Information);
            }
            catch (Exception err) {
                Console.WriteLine($"Failed to sync time. Current: {_currentTime.ToLongTimeString()}, Target: {time.ToLongTimeString()}. Error: {err.Message}");
                this.eventLog.WriteEntry($"Failed to sync time. Current: {_currentTime.ToLongTimeString()}, Target: {time.ToLongTimeString()}. Error: {err.Message}", EventLogEntryType.Error);
            }
        }
        
    }
}
