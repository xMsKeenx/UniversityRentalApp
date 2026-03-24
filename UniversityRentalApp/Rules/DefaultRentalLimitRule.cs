using System;
using System.Collections.Generic;
using System.Text;
using UniversityRentalApp.Models;

namespace UniversityRentalApp.Rules
{
    public class DefaultRentalLimitRule : IRentalLimitRule
    {
        public int GetMaxRentals(UserType userType)
        {
            if (userType == UserType.Student)
            {
                return 2; 
            }
            else if (userType == UserType.Employee)
            {
                return 5;  
            }

            return 0;
        }
    }
}
