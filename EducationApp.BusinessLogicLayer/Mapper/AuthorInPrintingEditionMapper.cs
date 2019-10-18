using EducationApp.BusinessLogicLayer.Models.PrintingEdition;
using EducationApp.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EducationApp.BusinessLogicLayer.Mapper
{
    public static class AuthorInPrintingEditionMapper
    {
        public static AuthorInPrintingEditons MapToAuthorInPrintingEditions(long authorInPrintingEditionId,long printingEditionId,long authorId)
        {
            return new AuthorInPrintingEditons
            {
                Id=authorInPrintingEditionId,
                AuthorId = authorId,
                PrintingEditionId = printingEditionId,
            };
        }
    }
}
