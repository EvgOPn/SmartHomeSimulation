using System;
using System.Globalization;

namespace UserManagement.Models
{
    [Serializable]
    public class UserData
    {        
        public string Username = String.Empty;        
        public string PlayFabId = String.Empty;
        public string LastTimeOnline = String.Empty;

        private const float MinSecondsToAcceptOnline = 10;
        
        public bool UserIsOnline()
        {
            if (!DateTime.TryParse(LastTimeOnline, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime userLastOnlineTime))
            {
                return false;
            }
            
            DateTime currentDataTime = DateTime.UtcNow;
            TimeSpan timeDifferences = currentDataTime - userLastOnlineTime;
            bool userIsOnline = timeDifferences.TotalSeconds < MinSecondsToAcceptOnline;
            return userIsOnline;
        }
    }
}