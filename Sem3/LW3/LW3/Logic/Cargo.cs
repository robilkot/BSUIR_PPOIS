using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LW3.Logic
{
    public class Cargo
    {
        public string Name = string.Empty;
        public uint Weight { get; init; } = 200;

        public Cargo() { }
        public Cargo(string name, uint weight) { Name = name; Weight = weight; }
    }
}
