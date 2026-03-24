using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityRentalApp.Models
{
    public abstract class Equipment
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public EquipmentStatus Status { get; private set; }
        public string UnavailabilityReason { get; private set; }

        protected Equipment(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Status = EquipmentStatus.Available;
            UnavailabilityReason = "";
        }

        
        public bool IsAvailable => Status == EquipmentStatus.Available;

        public void MarkRented()
        {
            Status = EquipmentStatus.Rented;
            UnavailabilityReason = "";
        }

        public void MarkReturned()
        {
            Status = EquipmentStatus.Available;
            UnavailabilityReason = "";
        }

        public void MarkUnavailable(string reason)
        {
            Status = EquipmentStatus.Unavailable;
            UnavailabilityReason = reason;
        }

  
        public virtual string GetDescription()
        {
            string info = $"{Name} | ID={Id} | Status={Status}";

            if (Status == EquipmentStatus.Unavailable)
            {
                info = info + $" | Reason={UnavailabilityReason}";
            }

            return info;
        }
    }
}
