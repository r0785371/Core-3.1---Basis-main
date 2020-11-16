using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ViewModels
{
     public class AssetCounterViewModel
    {
        public int ExpiredItems { get; set; }
        public int WarrentyItems { get; set; }

        public int GetBackupNeeded { get; set; }

        public int GetAssetWarningPeriod { get; set; }

        public int GetAssetWarningPeriodOrange { get; set; }

        public int GetAssetWarningPeriodRed { get; set; }

        public int GetBackupNeededThisWeek { get; set; }

        public int GetBackupNeededPeviousWeeks { get; set; }

        public int GetBackupOperationalSiteSibPreviousWeek { get; set; }
        public int GetBackupOperationalSiteSibThisWeek { get; set; }

    }
}
