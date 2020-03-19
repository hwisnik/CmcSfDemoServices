using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Enums
{
    public enum ExtendedPrivileges
    {
        IsPCAAdministrator = 0,
        IsSuperAdministrator = 1,
        IsAdministrator = 2,
        CanChangeUserExtendedPrivileges = 3,
        CanSelfEdit = 4,
        CanSelfPromote = 5,
        CanEditMessages = 6,
        CanEditApplicationTerms = 7,
        CanEditFormTerms = 8,
        CanAddTickets = 9,
        CanViewCacheBrowse = 10,
        CanViewSessions = 11,
        CanViewQaMenu = 12
    }

    
}

