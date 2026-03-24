using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityRentalApp.Models
{
    public class Projector : Equipment
    {
        public string Brand { get; private set; }
        public int Lumens { get; private set; }
        public bool IsPortable { get; private set; }

        public Projector(string name, string brand, int lumens, bool isPortable)
            : base(name)
        {
            Brand = brand;
            Lumens = lumens;
            IsPortable = isPortable;
        }

        public override string GetDescription()
        {
            string baseInfo = base.GetDescription();

            string portableText = "Stationary";
            if (IsPortable)
            {
                portableText = "Portable";
            }

            return baseInfo + $" | Brand: {Brand} | Lumens: {Lumens} | Type: {portableText}";
        }
    }
}
