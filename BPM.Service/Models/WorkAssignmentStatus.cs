using System;

namespace BPM.Service.Models
{
    [Flags]
    public enum WorkAssignmentStatus
    {
        Zero = 0,
        Financial = 1,
        Test = 2,
        Check = 4
    }
}