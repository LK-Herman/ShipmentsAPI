using AutoMapper;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.EFDbContext;
using ShipmentsAPI.Entities;
using System;
using System.Linq;

namespace ShipmentsAPI.Services
{
    public interface ICmrDataService
    {
        CmrDataDto GetByShipmentAndCustomer(Guid shipmentId, Guid customerId);
        CmrDataDto Upsert(CreateCmrDataDto dto);
    }

    public class CmrDataService : ICmrDataService
    {
        private readonly IMapper mapper;
        private readonly ShipmentsDbContext dbContext;

        public CmrDataService(IMapper mapper, ShipmentsDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public CmrDataDto GetByShipmentAndCustomer(Guid shipmentId, Guid customerId)
        {
            var record = dbContext.CmrData
                .FirstOrDefault(x => x.ShipmentId == shipmentId && x.CustomerId == customerId);

            if (record == null)
                return null;

            return mapper.Map<CmrDataDto>(record);
        }

        public CmrDataDto Upsert(CreateCmrDataDto dto)
        {
            var existing = dbContext.CmrData
                .FirstOrDefault(x => x.ShipmentId == dto.ShipmentId && x.CustomerId == dto.CustomerId);

            if (existing == null)
            {
                var newRecord = mapper.Map<CmrData>(dto);
                newRecord.Id = Guid.NewGuid();
                dbContext.CmrData.Add(newRecord);
                dbContext.SaveChanges();
                return mapper.Map<CmrDataDto>(newRecord);
            }

            mapper.Map(dto, existing);
            dbContext.CmrData.Update(existing);
            dbContext.SaveChanges();
            return mapper.Map<CmrDataDto>(existing);
        }
    }
}
