using System;
using System.ComponentModel.DataAnnotations;

namespace ShipmentsAPI.DtoModels
{
    public class CreateCmrDataDto
    {
        [Required]
        public Guid ShipmentId { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        public string SenderName { get; set; }
        public string SenderStreet { get; set; }
        public string SenderCity { get; set; }
        public string SenderCountry { get; set; }

        public string ConsigneeName { get; set; }
        public string ConsigneeStreet { get; set; }
        public string ConsigneeCity { get; set; }
        public string ConsigneeCountry { get; set; }

        public string Destination { get; set; }
        public string LoadingPlace { get; set; }

        public string Attachment1 { get; set; }
        public string Attachment2 { get; set; }

        public string GoodsMarks1 { get; set; }
        public string GoodsMarks2 { get; set; }
        public string GoodsMarks3 { get; set; }
        public string GoodsMarks4 { get; set; }
        public string GoodsMarks5 { get; set; }
        public string GoodsUN { get; set; }
        public string GoodsClassUN { get; set; }
        public string GoodsPGUN { get; set; }

        public bool IsDgd { get; set; }
        public bool IsLine3Active { get; set; }
        public bool IsLine4Active { get; set; }
        public bool IsLine5Active { get; set; }
        public bool IsAdrRegulated { get; set; }
        public bool IsOverpack { get; set; }

        public string GoodsQty { get; set; }
        public string GoodsNet { get; set; }
        public string GoodsWeight { get; set; }
        public string GoodsCBM { get; set; }

        public string SpedCompany { get; set; }
        public string SpedName { get; set; }
        public string SpedCarPlates { get; set; }
    }
}
