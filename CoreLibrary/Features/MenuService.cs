using AutoMapper;
using CoreLibrary.DTOs.Extras.Response;
using CoreLibrary.DTOs.Menu.Response;
using CoreLibrary.DTOs.Sandwich.Response;
using CoreLibrary.Interface.Services;
using Domain.Querys;
using Domain.Wrappers;
using Microsoft.Extensions.Configuration;

namespace CoreLibrary.Features
{
    public class MenuService : IMenuService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public MenuService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        private string ConnectionString => _configuration.GetConnectionString("DefaultConnection")
     ?? throw new Exception("Missing DefaultConnection");


        public async Task<Response<List<ExtrasDtoResponse>>> GetExtras()
        {
            try
            {
                GetExtras getExtras = new GetExtras(ConnectionString);
                var data = await getExtras.Get();

                if (data == null || data.Count == 0)
                    return new Response<List<ExtrasDtoResponse>>
                    { State = "Error", Message = "No extras found", Succeeded = false };

                var result = _mapper.Map<List<ExtrasDtoResponse>>(data);

                return new Response<List<ExtrasDtoResponse>>(result)
                { State = "Ok", Message = "Successful response", Succeeded = true };
            }
            catch (Exception ex)
            {
                return new Response<List<ExtrasDtoResponse>> { State = "Error", Message = ex.Message, Succeeded = false };
            }
        }

        public async Task<Response<List<SandwichDtoResponse>>> GetSandwich()
        {
            try
            {
                GetSandwich getSandwich = new GetSandwich(ConnectionString);
                var data = await getSandwich.Get();

                if (data == null || data.Count == 0)
                    return new Response<List<SandwichDtoResponse>>
                    { State = "Error", Message = "No sandwiches found", Succeeded = false };

                var result = _mapper.Map<List<SandwichDtoResponse>>(data);

                return new Response<List<SandwichDtoResponse>>(result)
                { State = "Ok", Message = "Successful response", Succeeded = true };
            }
            catch (Exception ex)
            {
                return new Response<List<SandwichDtoResponse>>
                { State = "Error", Message = ex.Message, Succeeded = false };
            }
        }

        public async Task<Response<MenuDtoResponse>> GetMenu()
        {
            try
            {
                GetExtras getExtras = new GetExtras(ConnectionString);
                GetSandwich getSandwich = new GetSandwich(ConnectionString);

                var extrasTask = getExtras.Get();
                var sandwichesTask = getSandwich.Get();

                await Task.WhenAll(extrasTask, sandwichesTask);

                var extras = extrasTask.Result;
                var sandwiches = sandwichesTask.Result;

                if (extras.Count == 0 || sandwiches.Count == 0)
                {
                    return new Response<MenuDtoResponse> { State = "Error", Message = "Menu data could not be loaded", Succeeded = false };
                }

                var result = new MenuDtoResponse
                {
                    extras = _mapper.Map<List<ExtrasDtoResponse>>(extras),
                    sandwiches = _mapper.Map<List<SandwichDtoResponse>>(sandwiches)
                };

                return new Response<MenuDtoResponse>(result) { State = "Ok", Message = "Successful response", Succeeded = true };
            }
            catch (Exception ex)
            {
                return new Response<MenuDtoResponse> { State = "Error", Message = ex.Message, Succeeded = false };
            }
        }
    }
}
