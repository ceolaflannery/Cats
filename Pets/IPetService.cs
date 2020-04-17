using System.Threading.Tasks;

namespace Pets
{
    public interface IPetService
    {
        Task<string> GetPetDetails();
    }
}