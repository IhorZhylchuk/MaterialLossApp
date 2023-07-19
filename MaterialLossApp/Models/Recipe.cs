using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialLossApp.Models
{
    public class Recipe:Default
    {
       
        public override int Id { get; set; }
        public override string Name { get; set; } = string.Empty;
    }
    public class Opakowania: Default
    {
        public override int Id { get; set; }
        public override string Name { get; set; } = string.Empty;
        public  int MaterialNumber { get; set; }
        public double Capacity { get; set; }
    }
    public abstract class Default
    {
        public abstract int Id { get; set; }
        public abstract string Name { get; set; }

    }
}
