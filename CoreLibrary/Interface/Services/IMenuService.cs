using CoreLibrary.DTOs.Extras.Response;
using CoreLibrary.DTOs.Menu.Response;
using CoreLibrary.DTOs.Sandwich.Response;
using Domain.Wrappers;

namespace CoreLibrary.Interface.Services;

public interface IMenuService
{
    Task<Response<MenuDtoResponse>> GetMenu();

    Task<Response<List<SandwichDtoResponse>>> GetSandwich();

    Task<Response<List<ExtrasDtoResponse>>> GetExtras();
}
