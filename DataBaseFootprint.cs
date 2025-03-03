using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footprint
{
    class DataBaseFootprint
    {
        public FootprintBaseEntities baza { get; set; }

        public DataBaseFootprint(FootprintBaseEntities baza)
        {
            this.baza = baza;
        }
    }
}
