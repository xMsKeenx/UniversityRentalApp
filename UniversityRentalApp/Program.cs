using System;
using System.Collections.Generic;
using System.Text;
using UniversityRentalApp.Models;
using UniversityRentalApp.Repositories;
using UniversityRentalApp.Rules;
using UniversityRentalApp.Services;

namespace UniversityRentalApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //we set up data and rules
            UserRepository userRepo = new UserRepository();
            EquipmentRepository equipmentRepo = new EquipmentRepository();
            RentalRepository rentalRepo = new RentalRepository();
            IRentalLimitRule limitRule = new DefaultRentalLimitRule();
            IPenaltyRule penaltyRule = new DefaultPenaltyRule();

            // Dependency Inversion Principle
            RentalService service = new RentalService(userRepo, equipmentRepo, rentalRepo, limitRule, penaltyRule);

            Console.WriteLine("=== UNIVERSITY EQUIPMENT RENTAL SYSTEM ===");
            Console.WriteLine("Running Demonstration Scenario...\n");

            //Adding equipment items
            Console.WriteLine("1. Adding equipment items...");
            Laptop laptop = new Laptop("MSI", "Dell", "i7", 16, 512);
            Projector projector = new Projector("Epson Room 101", "Epson", 4000, true);
            Camera camera = new Camera("Canon EOS", "Canon", 24, true);

            equipmentRepo.Add(laptop);
            equipmentRepo.Add(projector);
            equipmentRepo.Add(camera);
            Console.WriteLine("Equipment added successfully.\n");

            //Adding users 
            Console.WriteLine("2. Adding users...");
            User student = new User("Tank", "Bell", UserType.Student);
            User employee = new User("Piotr", "Gako", UserType.Employee);

            userRepo.Add(student);
            userRepo.Add(employee);
            Console.WriteLine("Users added successfully.\n");

            
            Console.WriteLine("3. Correct rental operation...");
            try
            {
                service.RentEquipment(student.Id, laptop.Id, 3);
                Console.WriteLine($"{student.FirstName} successfully rented: {laptop.Name}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine();

            //invalid operation 
            Console.WriteLine("4. Attempted invalid operation (exceeding student limit of 2)...");
            try
            {
                //rent a second item (this should work) 
                service.RentEquipment(student.Id, projector.Id, 2);
                Console.WriteLine($"{student.FirstName} successfully rented: {projector.Name}. He now has 2 items.");

                //rent a third item (this must fail)
                Console.WriteLine($"Attempting to rent a 3rd item ({camera.Name})...");
                service.RentEquipment(student.Id, camera.Id, 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"EXPECTED ERROR: {ex.Message}");
            }
            Console.WriteLine();

            
            Console.WriteLine("5. Return completed on time...");

            // We need to find active laptop rental to return it
            Rental laptopRental = null;
            foreach (var r in rentalRepo.GetAll())
            {
                if (r.UserId == student.Id && r.EquipmentId == laptop.Id && r.IsActive())
                {
                    laptopRental = r;
                }
            }

            if (laptopRental != null)
            {
                // Returning (on time)
                service.ReturnEquipment(laptopRental.Id, DateTime.Now);
                Console.WriteLine($"{student.FirstName} returned the {laptop.Name} on time. No penalty!");
            }
            Console.WriteLine();

           //Delayed
            Console.WriteLine("6. Delayed return with penalty...");

            // Employee rents the camera for 1 day
            service.RentEquipment(employee.Id, camera.Id, 1);

            Rental cameraRental = null;
            foreach (var r in rentalRepo.GetAll())
            {
                if (r.UserId == employee.Id && r.EquipmentId == camera.Id && r.IsActive())
                {
                    cameraRental = r;
                }
            }

            if (cameraRental != null)
            {
                //but returns it 4 days after the due date
                DateTime lateDate = cameraRental.DueAt.AddDays(4);
                service.ReturnEquipment(cameraRental.Id, lateDate);

                //Check penalty
                Console.WriteLine($"{employee.FirstName} returned the {camera.Name} 4 days late!");
                Console.WriteLine($"Penalty applied: {cameraRental.Penalty} PLN");
            }
            Console.WriteLine();

            Console.WriteLine("=== End of Demonstration ===");
            Console.ReadLine(); 
        }
    }
}
