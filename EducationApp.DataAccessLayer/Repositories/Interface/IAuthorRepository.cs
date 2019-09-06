using EducationApp.DataAccessLayer.Entities;

namespace EducationApp.DataAccessLayer.Repositories.Interface
{
    interface IAuthorRepository : IBaseEFRepository<Author>
    {
        void Update(Author Author);
    }
}
