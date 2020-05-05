using Entity;
using RepositoryFactory.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.ClientService
{
   public class AttachmentService
    {
        private IAttachMentRepository attachMentRepository;
        public AttachmentService(IAttachMentRepository _attachMentRepository)
        {
            attachMentRepository = _attachMentRepository;
        }
        /// <summary>
        /// 获取所有有效商品
        /// </summary>
        /// <returns></returns>
        public List<AttachMentInfoEntity> FindAttachMentList(int type)
        {
            return attachMentRepository.FindList(x => x.AttachmentType == type);
        }
    }
}
