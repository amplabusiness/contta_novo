using RoboEconet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoboEconet.Infra.Data.Interface
{
    public interface IPisConfinsRepository
    {
        Task<bool> Create(List<PisConfinsDto> pisConfins);
       
    }
}
