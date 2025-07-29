using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Entities
{
    public class EventType
    {
        public EventType(int eventTypeID, string type, string specialNotes, decimal cost, DateTime createdOn, string createdBy)
        {
            EventTypeID = eventTypeID;
            Type = type;
            SpecialNotes = specialNotes;
            Cost = cost;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
        }

        [Key]
        public int EventTypeID { get; private set; }
        public string Type { get; private set; }
        public string SpecialNotes { get; private set; }
        public decimal Cost { get; private set; }

        public DateTime CreatedOn { get; private set; }
        public string CreatedBy { get; private set; }
        public DateTime? ModifiedOn { get;  set; }
        public string? ModifiedBy { get;  set; }
        public DateTime? DeletedOn { get;  set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get;  set; }
    }
}
