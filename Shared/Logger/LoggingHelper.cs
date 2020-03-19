using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Shared.Logger
{
    public static class LoggingHelper
    {
        public static int GetReplayId(string inputString)
        {
            if (!inputString.ToLower().Contains("replayid")) return 0;
            var replayIdIdx = inputString.ToLower().IndexOf("replayid", StringComparison.Ordinal);

            var replayIdStr = inputString.Substring(replayIdIdx, inputString.Length-replayIdIdx);
            var splitReplayIdstr = string.Empty;
            if (!string.IsNullOrEmpty(replayIdStr))
            {
                splitReplayIdstr = SplitPlatformEventMessage(replayIdStr, ':', "replayid");
            }

            var replayId = 0;
            if (!string.IsNullOrEmpty(splitReplayIdstr))
            {
                int.TryParse(splitReplayIdstr, out replayId);
            }

            return replayId;
        }

        private static string SplitPlatformEventMessage(string stringToSplit, char splitParameter, string searchTerm)
        {
            var retVal = string.Empty;
            if (string.IsNullOrEmpty(stringToSplit)) return retVal;
            var splitArray = stringToSplit.Split(splitParameter);
            return splitArray.Length != 2 ? retVal : Regex.Match(splitArray[1], @"\d+").Value;
        }
    }
}
