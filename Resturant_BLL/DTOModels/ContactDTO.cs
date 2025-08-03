using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.DTOModels
{
    public class ContactDTO
    {
        public EmailSenderDTO EmailSender { get; set; }
        public List<BranchDTO> branchDTOs { get; set; }
    }
}
