using System;
using System.Collections.Generic;
using System.Text;
using UniversityRentalApp.Models;

namespace UniversityRentalApp.Rules
{
    public interface IRentalLimitRule
    {
     int GetMaxRentals(UserType userType);
    }
}
