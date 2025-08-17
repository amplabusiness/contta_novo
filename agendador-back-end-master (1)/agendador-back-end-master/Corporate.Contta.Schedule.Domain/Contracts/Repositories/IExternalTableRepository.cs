using Corporate.Contta.Schedule.Domain.Entities.ExternalTable;
using System.Threading.Tasks;

namespace Corporate.Contta.Schedule.Domain.Contracts.Repositories
{
    public interface IExternalTableRepository : IRepositorio<IcmsSt>
    {
        Task Insert(IcmsSt icmsSt);
    }
}
