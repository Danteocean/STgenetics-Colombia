using CoreLibrary.DTOs.Extras.Response;
using CoreLibrary.DTOs.Sandwich.Response;

namespace CoreLibrary.DTOs.Menu.Response;

public class MenuDtoResponse
{
    public List<SandwichDtoResponse> sandwiches { get; set; }
    public List<ExtrasDtoResponse> extras { get; set; }
}