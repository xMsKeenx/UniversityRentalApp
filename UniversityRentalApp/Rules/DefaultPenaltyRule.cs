using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityRentalApp.Rules
{
    public class DefaultPenaltyRule : IPenaltyRule
    {
        public decimal CalculatePenalty(DateTime dueAt, DateTime returnedAt)
        {
           
            if (returnedAt <= dueAt)
            {
                return 0m; 
            }

            
            TimeSpan lateTime = returnedAt - dueAt;
            int lateDays = (int)Math.Ceiling(lateTime.TotalDays);

        
            decimal penaltyPerDay = 10m; //10 zl per late day

            return lateDays * penaltyPerDay;
        }
    }
}
