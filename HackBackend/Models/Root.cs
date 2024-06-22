using System.Collections.Generic;

namespace DistinctLocationsApi.Models
{
    public class Root
    {
        public string BillingCurrency { get; set; }
        public string CustomerEntityId { get; set; }
        public string CustomerEntityType { get; set; }
        public List<Item> Items { get; set; }
        public string NextPageLink { get; set; }
        public int Count { get; set; }
    }
}
