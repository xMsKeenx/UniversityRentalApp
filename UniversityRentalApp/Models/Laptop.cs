using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityRentalApp.Models
{
  
    public class Laptop : Equipment
    {
        public string Brand { get; private set; }
        public string CpuModel { get; private set; }
        public int RamGb { get; private set; }
        public int SsdGb { get; private set; }

        public Laptop(string name, string brand, string cpuModel, int ramGb, int ssdGb)
            : base(name)
        {
            Brand = brand;
            CpuModel = cpuModel;
            RamGb = ramGb;
            SsdGb = ssdGb;
        }

     
        public override string GetDescription()
        {
            string baseInfo = base.GetDescription();
            return $"{baseInfo} | Brand={Brand} | CPU={CpuModel} | RAM={RamGb}GB | SSD={SsdGb}GB";
        }
    }
}
