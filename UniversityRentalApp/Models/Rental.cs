using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityRentalApp.Models
{
    public class Rental
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid EquipmentId { get; private set; }

        public DateTime RentedAt { get; private set; }
        public DateTime DueAt { get; private set; }

        public DateTime? ReturnedAt { get; private set; } //could be null
        public decimal Penalty { get; private set; }

        public Rental(Guid userId, Guid equipmentId, DateTime rentedAt, DateTime dueAt)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            EquipmentId = equipmentId;
            RentedAt = rentedAt;
            DueAt = dueAt;
            Penalty = 0; 
        }

        
        public bool IsActive()
        {
            if (ReturnedAt == null)
            {
                return true;
            }
            return false;
        }

   
        public bool IsOverdue(DateTime currentDate)
        {
            if (IsActive() && currentDate > DueAt)
            {
                return true;
            }
            return false;
        }

      
        public void MarkAsReturned(DateTime returnDate, decimal penaltyAmount)
        {
            ReturnedAt = returnDate;
            Penalty = penaltyAmount;
        }
    }
}
