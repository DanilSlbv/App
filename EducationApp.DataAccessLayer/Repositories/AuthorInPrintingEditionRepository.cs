using EducationApp.DataAccessLayer.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAcessLayer.AppContext;
using System.Threading.Tasks;
using EducationApp.DataAccessLayer.Repositories.Base;
using System.Linq;

namespace EducationApp.DataAccessLayer.Repositories
{
    public class AuthorInPrintingEditionRepository:BaseEFRepository<AuthorInPrintingEditons>,IAuthorInPrintingEditionRepository
    {
        private readonly ApplicationContext _applicationContext;
        public AuthorInPrintingEditionRepository(ApplicationContext applicationContext) :base(applicationContext)
        {
            _applicationContext = applicationContext;
        }

        //public List<AuthorInPrintingEditons> GetAuthorsInPrintingEditons(string printingEditionId)
        //{
        //    var printingEdition=_applicationContext.AuthorInPrintingEditons.Select(x=>x.PrintingEditionId).Where(x=>x.p)
        //    return printingEdition;
        //}
    }
}
