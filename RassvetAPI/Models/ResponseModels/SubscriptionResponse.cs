using System;

namespace RassvetAPI.Models.ResponseModels
{
    public class SubscriptionResponse
    {
        public int ID { get; set; }
        public string BarcodeString { get; set; }
        public string SectionName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
