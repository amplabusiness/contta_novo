using System.Collections.Generic;

namespace Corporate.Contta.Schedule.Domain.Entities.FullNfeAgg
{
    public class Sale
    {
        public Sale()
        {
            Products = new List<Products>();
        }

        public Taxes Taxes { get; set; }

        public IList<Products> Products { get; set; }

        public Receiver Receiver { get; set; }
    }
}
