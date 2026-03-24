using System;
using System.Collections.Generic;
using System.Text;
using UniversityRentalApp.Models;

namespace UniversityRentalApp.Repositories
{
    public class RentalRepository
    {
        private List<Rental> _rentals = new List<Rental>();

        public void Add(Rental rental)
        {
            _rentals.Add(rental);
        }

        public Rental GetById(Guid id)
        {
            foreach (var rental in _rentals)
            {
                if (rental.Id == id)
                {
                    return rental;
                }
            }
            return null;
        }

        public List<Rental> GetAll()
        {
            return _rentals;
        }
    }
}
