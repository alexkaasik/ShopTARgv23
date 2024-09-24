using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARgv23.Core.Dto
{
    public class SpaceshipDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public DateTime BuiltDate { get; set; }

        public int CargoWeight { get; set; }

        public int Crew { get; set; }

        public int EnginePower { get; set; }

        // Only in db
        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}

