using System.ComponentModel.DataAnnotations;

namespace RassvetAPI.Models.RequestModels
{
    public class SubscriptionOrderRequest
    {
        [Required]
        public int OfferID { get; set; }
    }
}
