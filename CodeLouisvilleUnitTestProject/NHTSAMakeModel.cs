﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CodeLouisvilleUnitTestProject
{
   // [Serializable]
    internal class NHTSAMakeModel
    {
        public int MakeId { get; set; }

        public string MakeName { get; set; }

        public int ModelId { get; set; }

        public string ModelName { get; set; }
    }
}
