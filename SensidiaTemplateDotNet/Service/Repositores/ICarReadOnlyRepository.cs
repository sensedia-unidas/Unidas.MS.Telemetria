namespace SensidiaTemplateDotNet.Service.Repositores
{
    using SensidiaTemplateDotNet.Domain.Cars;
    using System.Threading.Tasks;

    public interface ICarReadOnlyRepository
    {
        Task<Car> Get(Guid id);
    }
}
