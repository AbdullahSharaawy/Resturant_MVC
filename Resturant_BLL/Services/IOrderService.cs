using Resturant_BLL.DTOModels.OrderDTOS;
using Resturant_DAL.Entities;
using System.Linq.Expressions;

namespace Resturant_BLL.Services
{
    public interface IOrderService
    {
        public Task<List<AdminOrderDTO>> GetList();
        public Task<AdminOrderDTO?> GetById(int id);
        public Task<AdminOrderDTO?> CreateOrderByAdmin(AdminOrderDTO Neworder);
        public Task<AdminOrderDTO?> Update(AdminOrderDTO newOrder);
        public Task<List<AdminOrderDTO>> GetOrdersByUserId(string userId);
        public Task<List<AdminOrderDTO>?> FilterBy(Expression<Func<Order, bool>> filter);
        public Task<DraftOrderDTO?>? GetDraftOrderById(int id);
        public Task<ShippedOrderDTO?> GetShippedOrder(int id);
        public Task<Order> CreateDraftOrder();
        public Task<ConfirmedOrderDTO?> ConfirmOrder(ConfirmedOrderDTO orderDTO);
        public Task<bool> Delete(int id);
        public Task<List<ReadOrderDTO>> GetMyOrdersByUserId(string userId);
        public Task<List<ReadOrderDTO>?> FilterMyOrdersBy(Expression<Func<Order, bool>> filter);
    }
}
