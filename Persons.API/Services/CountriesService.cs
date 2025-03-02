﻿using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persons.API.Constants;
using Persons.API.Database;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Countries;
using Persons.API.Dtos.Persons;
using Persons.API.Services.Interfaces;

namespace Persons.API.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly PersonsDbContext _context;
        private readonly IMapper _mapper;
        public CountriesService(PersonsDbContext personsDbContext, IMapper mapper)
        {
            _context = personsDbContext;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<CountryDtos>>> GetListAsync()
        {
            var countries = await _context.countries.OrderBy( x => x.AlphaCode3).ToListAsync();

            var countriesDtos = _mapper.Map<List<CountryDtos>>(countries);

            return new ResponseDto<List<CountryDtos>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro obtenidos correctamente",
                Data = countriesDtos
            };
        }

        public async Task<ResponseDto<CountryDtos>> GetOneByIdAsync( Guid id)
        {
            var country = await _context.countries.FirstOrDefaultAsync( x => x.Id == id );

            if (country == null)
            {
                return new ResponseDto<CountryDtos> 
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "El registro no fue encontrado"
                };
            }

             

            return new ResponseDto<CountryDtos>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro encontrado",
                Data = _mapper.Map<CountryDtos>(country)
            };
        }

        public async Task<ResponseDto<CountryActionResponseDto>> CreateAsync(CountryCreateDto dto)
        {
            var countryEntity = _mapper.Map<CountryEntity>(dto); 
            _context.countries.Add(countryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CountryActionResponseDto> 
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado correctamente",
                Data = _mapper.Map<CountryActionResponseDto>(countryEntity)
            };

        }

        public async Task<ResponseDto<CountryActionResponseDto>> EditAsync(CountryEditDto dto, Guid id)
        {
            var countryEntity = await _context.countries.FindAsync(id);

            if (countryEntity is null) 
            {
                return new ResponseDto<CountryActionResponseDto> 
                { 
                    StatusCode= HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            _mapper.Map<CountryEditDto, CountryEntity>(dto, countryEntity);
            _context.countries.Update(countryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CountryActionResponseDto>
            { 
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Editado",
                Data = _mapper.Map<CountryActionResponseDto>(countryEntity)
            };



        }

        public async Task<ResponseDto<CountryActionResponseDto>> DeleteAsync (Guid id)
        {
            var countryEntity = await _context.countries.FindAsync(id);

            if (countryEntity == null)
            {
                return new ResponseDto<CountryActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }
             //27 de febrero para que valide que no tenga personas con el pais 
            var personsInCountry = await _context.Persons.CountAsync(p => p.CountryId == id);

            if (personsInCountry > 0) 
            {
                return new ResponseDto<CountryActionResponseDto> 
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El pais tiene datos relacionado"
,
                };
            }

            _context.countries.Remove(countryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CountryActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro Eliminado",
                //Data = _mapper.Map 
            };
        }
    }
}
