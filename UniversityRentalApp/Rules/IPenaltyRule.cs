using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityRentalApp.Rules
{
    public interface IPenaltyRule
    {
        decimal CalculatePenalty(DateTime dueAt, DateTime returnedAt);
    }
}
