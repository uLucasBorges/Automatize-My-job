using GoodlistInsert.Models;

namespace GoodlistInsert.Interfaces
{
    public interface IListService
    {
       void CreateListScript(vwObjectQuery complet, IFormFile file);
       void CreateUpdateListScript(vwObjectQuery complet, IFormFile file);

    }
}
