using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityRentalApp.Models
{
    public class Camera : Equipment
    {
        public string Brand { get; private set; }
        public int Megapixels { get; private set; } 
        public bool InterchangeableLens { get; private set; }

        public Camera(string name, string brand, int megapixels, bool interchangeableLens)
            : base(name)
        {
            Brand = brand;
            Megapixels = megapixels;
            InterchangeableLens = interchangeableLens;
        }

        public override string GetDescription()
        {
            string baseInfo = base.GetDescription();

            string lensText = "Fixed Lens";
            if (InterchangeableLens)
            {
                lensText = "Interchangeable Lens";
            }

            return baseInfo + $" | Brand: {Brand} | Resolution: {Megapixels}MP | Lens: {lensText}";
        }
    }
}