using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;
using Riok.Mapperly.Abstractions;

namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class MenueItemMapper
    {
        public partial MenueItemDTO MapToMenueItemDTO(MenueItem menueItem);
        public partial MenueItem MapToMenueItem(MenueItemDTO menueItem);
        public partial List<MenueItemDTO> MapToMenueItemDTOList(List<MenueItem> menueItems);
    }
}
