using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopSpeed.Application.Contracts.Presistence
{
    public interface IunitOFWork :IDisposable
    {
        public IBrandRepository brand { get; }

        public IVehicleTypeRepository vehicleType { get; }
        Task SaveAsync();

    }
}
