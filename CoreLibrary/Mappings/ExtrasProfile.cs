using AutoMapper;
using Domain.Entities;
using ExtrasDtoResponse = CoreLibrary.DTOs.Extras.Response.ExtrasDtoResponse;

namespace CoreLibrary.Mappings;

public class ExtrasProfile : Profile
{
    public ExtrasProfile()
    {
        CreateMap<Extras, DTOs.Extras.Response.ExtrasDtoResponse>();
       
    }
}
