using dotnet_bot_accountant.Engine.Settings;
using System.Net;

namespace dotnet_bot_accountant.Engine.Models
{
    public class BlockModel
    {
        #region Fields
        public IPAddress Address { get; private set; }
        public string UserName { get; private set; }
        public DateTime BlockTs { get; private set; }
        public DateTime LastAttemptTs { get; private set; }
        public int CurrentAttempt { get; private set; } = 0;

        private int _attemptsLeft;
        private XmlSettings.ServiceSecurity _settings => Shared.Settings.Service.Security;
        private long _blockMs;
        private bool _isBlocked;

        #endregion

        #region Constructors
        public BlockModel(IPAddress address, string userName)
        {
            Address = address;
            UserName = userName;
        }
        #endregion

        #region Methods

        private double GetBlockMinutes(double remained) => Math.Round(remained / 60_000);
        
        public bool RaiseBadAttempt(out string message)
        {
            LastAttemptTs = DateTime.UtcNow;

            if (_isBlocked && !IsBlockTimeElapsed(out var remained))
            {
                var minutes = GetBlockMinutes(remained);
                message = $"Blocked. {minutes} remained";
                return true;
            }

            CurrentAttempt++;
            _attemptsLeft--;

            if (CurrentAttempt >= _settings.MaxAttemtps)
                CurrentAttempt = _settings.MaxAttemtps;

            if (_attemptsLeft < 0)
                _attemptsLeft = 0;

            if (!IsNeedtoBlock())
            {
                message = $"attempts left {_attemptsLeft}";
                return false;
            }

            _attemptsLeft = _settings.BlockAfterAttempt;
            _isBlocked = true;

            BlockTs = DateTime.UtcNow;
            _blockMs = CurrentAttempt * _settings.BlockPerAttempt;

            message = $"no attempts left, you blocked";

            return true;
        }

        public bool IsAllowed(out string message)
        {
            double remained = 0;

            var allowed = !_isBlocked || IsBlockTimeElapsed(out remained);

            message = (allowed) ? "" : $"Blocked. {GetBlockMinutes(remained)} remained";

            return allowed;
        }

        public bool IsBlockTimeElapsed(out double remained)
        {
            remained = 0;

            if (!_isBlocked)
                return true;

            var currentTs = DateTime.UtcNow;

            var diff = currentTs - BlockTs;

            var elapsed = diff.TotalMilliseconds >= _blockMs;

            remained = (elapsed) ? 0 : _blockMs - diff.TotalMilliseconds;

            _attemptsLeft = (elapsed) ? _settings.BlockAfterAttempt : _attemptsLeft;

            _isBlocked = (elapsed) ? false : _isBlocked;

            return elapsed;
        }

        private bool IsNeedtoBlock()
        {
            var needToBlock = CurrentAttempt % _settings.BlockAfterAttempt == 0 ||
                CurrentAttempt == _settings.MaxAttemtps ||
                _attemptsLeft <= 0;

            return needToBlock;
        }
        #endregion
    }
}
