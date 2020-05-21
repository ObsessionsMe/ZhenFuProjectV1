using Repository.RepositoryService;
using Repository.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.ManageService
{
    public class DictionaryServices
    {
        IDictionaryRepository dictionaryRepository;

        public DictionaryServices(IDictionaryRepository _dictionaryRepository)
        {
            dictionaryRepository = _dictionaryRepository;
        }
    }
}
