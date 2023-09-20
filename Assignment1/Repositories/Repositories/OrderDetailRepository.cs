using Domains.Entities;
using Services.Repositories;

namespace Repositories.Repositories
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
