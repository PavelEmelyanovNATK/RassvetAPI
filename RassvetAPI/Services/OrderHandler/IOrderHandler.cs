using RassvetAPI.Models.RequestModels;
using System.Threading.Tasks;

namespace RassvetAPI.Services.OrderHandler
{
    public interface IOrderHandler
    {
        /// <summary>
        /// Формирует заказ для переданного клиента на переданную услугу.
        /// </summary>
        /// <param name="offerId"></param>
        /// <param name="clientId"></param>
        /// <returns></returns>
        /// <exception cref="OfferNotFoundException"></exception>
        Task MakeOrderAsync(int offerId, int clientId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="managerId"></param>
        /// <returns></returns>
        /// <exception cref="OrderNotFoundException"></exception>
        Task ProcessOrderAsync(int orderId, int managerId);
    }
}
