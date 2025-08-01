﻿using Resturant_BLL.DTOModels;
using Resturant_DAL.Entities;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_BLL.Mapperly
{
    [Mapper]
    public partial class TableMapper
    {
        // [MapProperty(nameof(Department.Id), nameof(DepartmentDTO.ID))]
        //[MapProperty(nameof(Department.Name), nameof(DepartmentDTO.Name))]
        //[MapProperty(nameof(Department.Manager), nameof(DepartmentDTO.Manager))]
        //[MapProperty(nameof(Chief.ChiefID), nameof(ChiefDTO.ChiefID))]
        //[MapProperty(nameof(Chief.Name), nameof(ChiefDTO.Name))]
        //[MapProperty(nameof(Chief.PhoneNumber), nameof(ChiefDTO.PhoneNumber))]
        //[MapProperty(nameof(Chief.Email), nameof(ChiefDTO.Email))]
        //[MapProperty(nameof(Chief.Position), nameof(ChiefDTO.Position))]
        [MapProperty(nameof(table.Branch.City), nameof(TableDTO.City))]
        [MapProperty(nameof(table.Branch.BranchID), nameof(TableDTO.BranchID))]
        public partial TableDTO MapToTableDTO(table table);
        
        public partial List<TableDTO> MapToTableDTOList(List<table> tables);

        //[MapProperty(nameof(TableDTO.TableID), nameof(table.TableID))]
        //[MapProperty(nameof(TableDTO.TableNumber), nameof(table.TableNumber))]
        //[MapProperty(nameof(TableDTO.BranchID), nameof(table.BranchID))]
        public partial table MapToTable(TableDTO dto);



    }





}
