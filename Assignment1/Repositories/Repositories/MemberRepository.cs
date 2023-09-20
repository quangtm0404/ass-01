using Domains.Entities;
using Services.Repositories;

namespace Repositories.Repositories
{
    public class MemberRepository : GenericRepository<Member>, IMemberRepository
    {
        public MemberRepository(AppDbContext dbcontext) : base(dbcontext) { }
    }
}
