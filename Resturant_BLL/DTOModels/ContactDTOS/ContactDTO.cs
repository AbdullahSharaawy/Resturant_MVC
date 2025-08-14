using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_BLL.DTOModels.BranchDTOS;

namespace Resturant_BLL.DTOModels.ContactDTOS
{
    public class ContactDTO
    {
        public EmailSenderDTO EmailSender { get; set; }
        public List<BranchDTO> branchDTOs { get; set; }
    }
}
