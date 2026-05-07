using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentsAPI.DtoModels;
using ShipmentsAPI.EFDbContext;
using ShipmentsAPI.Entities;
using ShipmentsAPI.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ShipmentsAPI.Services
{
    public interface ICustomerService
    {
        List<CustomerDto> GetCustomers();
        CustomerDto GetById(Guid id);
        Guid Create(CreateCustomerDto createCustomerDto);
        CustomerDto Update(Guid id, CreateCustomerDto dto);
        void Delete(Guid id);
        PageResult<CustomerDto> Search(QueryCustomers query);
    }

    public class CustomerService : ICustomerService
    {
        private readonly IMapper mapper;
        private readonly ShipmentsDbContext dbContext;

        public CustomerService(IMapper mapper, ShipmentsDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
        }

        public List<CustomerDto> GetCustomers()
        {
            var customers = dbContext.Customers
                .Include(x => x.PurchaseOrders)
                .ThenInclude( i => i.Incoterm)
                .ToList();
            var customersDtos = mapper.Map<List<CustomerDto>>(customers);
            return customersDtos;
        }
        public CustomerDto GetById(Guid id)
        {
            var customer = dbContext.Customers
                .Include(x => x.PurchaseOrders)
                .ThenInclude(i => i.Incoterm)
                .FirstOrDefault(x => x.Id == id);

            var customerDto = mapper.Map<CustomerDto>(customer);
            return customerDto;
        }
        public PageResult<CustomerDto> Search(QueryCustomers query)
        {
            var customers = dbContext.Customers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.SearchPhrase))
            {
                customers = customers.Where(x =>
                x.Name.Contains(query.SearchPhrase)
                || x.ShortName.Contains(query.SearchPhrase)
                || x.ClientNumber.Contains(query.SearchPhrase)
                || x.CityAddress.Contains(query.SearchPhrase)
                || x.StreetAddress.Contains(query.SearchPhrase));
            }

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                var columnsSelector = new Dictionary<string, Expression<Func<Customer, object>>>()
                {
                    { nameof(Customer.Name), t => t.Name },
                    { nameof(Customer.ShortName), t => t.ShortName },
                    { nameof(Customer.ClientNumber), t => t.ClientNumber },
                    { nameof(Customer.CityAddress), t => t.CityAddress },
                    { nameof(Customer.CountryAddress), t => t.CountryAddress },
                    { nameof(Customer.ZipCodeAddress), t => t.ZipCodeAddress },
                    { nameof(Customer.StreetAddress), t => t.StreetAddress },
                };

                var selectedColumn = columnsSelector[query.SortBy];

                customers = query.SortDirection == SortDirection.ASC ?
                    customers.OrderBy(selectedColumn)
                    : customers.OrderByDescending(selectedColumn);
            }
            var totalItemsCount = customers.Count();

            customers = customers
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize)
                .AsQueryable();

            var customersDto = mapper.Map<List<CustomerDto>>(customers);

            var result = new PageResult<CustomerDto>(customersDto, totalItemsCount, query.PageSize, query.PageNumber);

            return result;
        }

        public Guid Create(CreateCustomerDto createCustomerDto)
        {
            var customerNumberExists = dbContext.Customers.Where(i => i.ClientNumber == createCustomerDto.ClientNumber).Any();
            if (customerNumberExists) 
            {
                throw new BadRequestException($"Klient z numerem {createCustomerDto.ClientNumber} już istnieje.");
            }
            createCustomerDto.ShortName = createCustomerDto.ShortName.ToUpper();
            var newCustomer = mapper.Map<Customer>(createCustomerDto);
            newCustomer.Id = Guid.NewGuid();
            dbContext.Customers.Add(newCustomer);
            dbContext.SaveChanges();

            return newCustomer.Id;
        }

        public CustomerDto Update(Guid id, CreateCustomerDto dto)
        {
            var customer = dbContext.Customers
                .FirstOrDefault(x => x.Id == id);
            
            if (customer == null) throw new NotFoundException($"Klient z numerem Id: {id} nie isnieje.");
            
            var customerNumberExists = dbContext.Customers.Where(i => i.ClientNumber == dto.ClientNumber).Any(x=>x.Id != customer.Id);
            if (customerNumberExists)
            {
                throw new BadRequestException($"Inny klient z numerem {dto.ClientNumber} już istnieje.");
            }

            customer.Name = dto.Name;
            customer.ShortName = dto.ShortName;
            customer.StreetAddress = dto.StreetAddress;
            customer.CountryAddress = dto.CountryAddress;
            customer.CityAddress = dto.CityAddress;
            customer.ZipCodeAddress = dto.ZipCodeAddress;
            customer.ClientNumber = dto.ClientNumber;
            dbContext.Customers.Update(customer);
            dbContext.SaveChanges();

            var customerDto = mapper.Map<CustomerDto>(customer);

            return customerDto;
        }

        public void Delete(Guid id)
        {
            var customer = dbContext.Customers
                .Include(x => x.PurchaseOrders)
                .FirstOrDefault(x => x.Id == id);
            if (customer == null) throw new NotFoundException($"Klient z numerem Id: {id} nie isnieje.");
            dbContext.Customers.Remove(customer);
            dbContext.SaveChanges();
        }
    }
}
