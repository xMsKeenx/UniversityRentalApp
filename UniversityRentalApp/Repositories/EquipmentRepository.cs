using System;
using System.Collections.Generic;
using System.Text;
using UniversityRentalApp.Models;

namespace UniversityRentalApp.Repositories
{
    public class EquipmentRepository
    {
        private List<Equipment> _equipmentList = new List<Equipment>();

        public void Add(Equipment equipment)
        {
            _equipmentList.Add(equipment);
        }

        public Equipment GetById(Guid id)
        {
            foreach (var item in _equipmentList)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public List<Equipment> GetAll()
        {
            return _equipmentList;
        }
    }
}
