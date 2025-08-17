using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using RoboEconet.Infra;
using RoboEconet.Infra.Adapter;
using RoboEconet.Infra.Data.Interface;
using RoboEconet.Models;

namespace RoboEconet.Services
{
    public class PisConfinsService : IPisConfinsRepository
    {
        private IPisConfinsRepository _pisConfinsRepository;

        public PisConfinsService()
        {
            _pisConfinsRepository = Nucleo.Container.Resolve<IPisConfinsRepository>();
        }

        public async Task<bool> Create(List<PisConfinsDto> pisConfins)
        {
            EntidadePisConfinsToEntidadeMongodb entidadePisConfins = new EntidadePisConfinsToEntidadeMongodb();
            try
            {
                //var listPisConfis = entidadePisConfins.CretaEntidadeMongoPisConfins(pisConfins);
                var pisConfinsMongo = await _pisConfinsRepository.Create(pisConfins);
                return pisConfinsMongo;

            }
            catch (System.Exception ex)
            {

                throw;
            }

            return false;


        }
    }
}
