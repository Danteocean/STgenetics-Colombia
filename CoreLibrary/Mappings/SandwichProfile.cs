using AutoMapper;
using CoreLibrary.DTOs.Sandwich.Response;
using Domain.Entities;

namespace CoreLibrary.Mappings;

public class SandwichProfile : Profile
{
    public SandwichProfile()
    {
        CreateMap<Sandwich, SandwichDtoResponse>();
    }
}