using SensidiaTemplateDotNet.Domain.Cars;
using System.Diagnostics;
using System.Security.Principal;

namespace SensidiaTemplateDotNet.Service.Repositores
{
   
    public interface ICarWriteOnlyRepository
    {
        Task Add(Car car);
        Task Update(Car car, PickUpCar pickUp);
        //Task Update(Car car, DropOffCar dropOff);
        Task Delete(Car car);
    }
}
