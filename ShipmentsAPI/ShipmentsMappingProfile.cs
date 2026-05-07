using AutoMapper;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.Entities;
using System.Collections.Generic;

namespace ShipmentsAPI
{
    public class ShipmentsMappingProfile : Profile
    {
        public ShipmentsMappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<PurchaseOrder, PurchaseOrderDto>()
                .ForMember(x => x.IncotermName, o => o.MapFrom(x => x.Incoterm.ShortName))
                .ForMember(x => x.CustomerName, o => o.MapFrom(x => x.Customer.Name))
                .ForMember(x => x.CustomerShortName, o => o.MapFrom(x => x.Customer.ShortName));
            CreateMap<PurchaseOrder, PurchaseOrderBriefDto>()
                .ForMember(x => x.IncotermName, o => o.MapFrom(x => x.Incoterm.ShortName))
                .ForMember(x => x.CustomerShortName, o => o.MapFrom(x => x.Customer.ShortName))
                .ForMember(x => x.CustomerCity, o => o.MapFrom(x => string.Concat( x.Customer.CountryAddress, " / ", x.Customer.CityAddress)));
                
            CreateMap<Forwarder, ForwarderDto>();
            CreateMap<Incoterm, IncotermDto>();
            CreateMap<Shipment, ShipmentBriefDto>()
                .ForMember(x => x.ContainerNumber, o => o.MapFrom(x => x.ContainerNumber));
            CreateMap<Shipment, ShipmentDto>()
                .ForMember(x => x.WarehouseArea, o => o.MapFrom(x => x.WarehouseArea.Name))
                .ForMember(x => x.Status, o => o.MapFrom(x => x.Status.Name));
            //.ForMember(x => x.PurchaseOrders, o => o.MapFrom(MapOrdersInShipment));
            CreateMap<UpdateShipmentDto, Shipment>()
                .ForMember(x => x.Id, o => o.MapFrom(x => x.ShipmentId));
                
            CreateMap<CreateShipmentDto, Shipment>();
            CreateMap<Status, StatusDto>();
            CreateMap<WarehouseArea, WarehouseAreaDto>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<CreateForwarderDto, Forwarder>();
            CreateMap<CreateStatusDto, Status>();
            CreateMap<CreateWarehouseAreaDto, WarehouseArea>();
            CreateMap<CreatePurchaseOrderDto, PurchaseOrder>();
            CreateMap<CmrData, CmrDataDto>();
            CreateMap<CreateCmrDataDto, CmrData>();
        }

        //private List<PurchaseOrderDto> MapOrdersInShipment(Shipment shipment, ShipmentDto shipmentDto)
        //{
        //    var result = new List<PurchaseOrderDto>();
        //    foreach (var shipmentOrder in shipment.PurchaseOrders)
        //    {
        //        result.Add(new PurchaseOrderDto()
        //        {
        //            Id = shipmentOrder.Id,
        //            DeliveryDate = shipmentOrder.DeliveryDate,
        //            PONumber = shipmentOrder.PONumber,
        //            CustomerName = shipmentOrder.Customer.ShortName,
                    
        //            PalletQty = shipmentOrder.PalletQty 
        //        }); 
        //    }

        //    return result;
        //}

    }
}
