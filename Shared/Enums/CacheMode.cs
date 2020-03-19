using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Enums
{
        public enum CacheMode
        {
            Cache_Off = 0,
            Simple_MiddleTier_RowVersion = 1,
            Simple_MiddleTier_UpdatedOn = 2,
            Advanced_DataTier_UpdatedOn = 3,
            Advanced_DataTier_RowVersion = 4,
            Advanced_DataTier_HashCode = 5
        }
}
