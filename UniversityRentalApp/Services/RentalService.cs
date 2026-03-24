using System;
using System.Collections.Generic;
using System.Text;
using UniversityRentalApp.Models;
using UniversityRentalApp.Repositories;
using UniversityRentalApp.Rules;

namespace UniversityRentalApp.Services
{
    public class RentalService
    {
        
        private UserRepository _userRepo;
        private EquipmentRepository _equipmentRepo;
        private RentalRepository _rentalRepo;
        private IRentalLimitRule _limitRule;
        private IPenaltyRule _penaltyRule;

       
        public RentalService(
            UserRepository userRepo,
            EquipmentRepository equipmentRepo,
            RentalRepository rentalRepo,
            IRentalLimitRule limitRule,
            IPenaltyRule penaltyRule)
        {
            _userRepo = userRepo;
            _equipmentRepo = equipmentRepo;
            _rentalRepo = rentalRepo;
            _limitRule = limitRule;
            _penaltyRule = penaltyRule;
        }

        
        //business logic 
        public void RentEquipment(Guid userId, Guid equipmentId, int days)
        {
            User user = _userRepo.GetById(userId);
            Equipment equipment = _equipmentRepo.GetById(equipmentId);

            // check exist and available
            if (user == null) throw new Exception("User not found.");
            if (equipment == null) throw new Exception("Equipment not found.");
            if (!equipment.IsAvailable) throw new Exception("Equipment is not currently available.");

            // check the users limit
            int limit = _limitRule.GetMaxRentals(user.Type);
            int currentActiveRentals = 0;

            //count active rentals
            foreach (var r in _rentalRepo.GetAll())
            {
                if (r.UserId == userId && r.IsActive())
                {
                    currentActiveRentals++;
                }
            }

            if (currentActiveRentals >= limit)
            {
                throw new Exception($"Rental blocked: {user.FirstName} has reached the limit of {limit} active items.");
            }

            //Create the rental and mark equipment as rented and converts days to dueDate
            DateTime dueDate = DateTime.Now.AddDays(days);
            Rental newRental = new Rental(userId, equipmentId, DateTime.Now, dueDate);
            _rentalRepo.Add(newRental);

            equipment.MarkRented();
        }

        public void ReturnEquipment(Guid rentalId, DateTime returnDate)
        {
            Rental rental = _rentalRepo.GetById(rentalId);

            if (rental == null) throw new Exception("Rental not found.");
            if (!rental.IsActive()) throw new Exception("This rental is already closed.");

            Equipment equipment = _equipmentRepo.GetById(rental.EquipmentId);

            // Calculate penalty 
            decimal penalty = _penaltyRule.CalculatePenalty(rental.DueAt, returnDate);

            // Close the rental and free up the equipment
            rental.MarkAsReturned(returnDate, penalty);
            equipment.MarkReturned();
        }

       
        public void MarkEquipmentUnavailable(Guid equipmentId, string reason)
        {
            Equipment equipment = _equipmentRepo.GetById(equipmentId);
            if (equipment != null)
            {
                equipment.MarkUnavailable(reason);
            }
        }
    }
}
