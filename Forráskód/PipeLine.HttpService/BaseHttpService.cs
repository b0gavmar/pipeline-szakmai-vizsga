using Newtonsoft.Json;
using PipeLine.Backend.Assemblers;
using PipeLine.Backend.Assemblers.BaseAssemblers;
using PipeLine.Shared.Models;
using PipeLine.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PipeLine.HttpService
{
    public class BaseHttpService<Tmodel, TDto> : IBaseHttpService<Tmodel>
       where Tmodel : class, IDbEntity<Tmodel>, new()
       where TDto : class, new()
    {
        protected readonly HttpClient _httpClient;
        protected IAssembler<Tmodel, TDto>? _assembler;

        protected string path = "api/" + (new Tmodel().GetType().Name);

        public BaseHttpService()
        {
            _httpClient = new HttpClient();
        }

        public BaseHttpService(IHttpClientFactory? httpClientFactory, IAssembler<Tmodel, TDto> assembler)
        {
            if (assembler == null) throw new ArgumentNullException(nameof(assembler));
            if (httpClientFactory == null) throw new ArgumentNullException(nameof(httpClientFactory));
            _httpClient = httpClientFactory.CreateClient("PipeLineApi");
            _assembler = assembler;
        }

        public async Task<Response> CreateAsync(Tmodel model)
        {
            Response defaultResponse = new();
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync(path, _assembler.ToDto(model));

                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    string content = await httpResponse.Content.ReadAsStringAsync();
                    Response? response = JsonConvert.DeserializeObject<Response>(content);
                    if (response is null)
                    {
                        defaultResponse.ClearAndAddError("A mentés http kérés hibát okozott!");
                    }
                    else return response;
                }
                else if (!httpResponse.IsSuccessStatusCode)
                {
                    httpResponse.EnsureSuccessStatusCode();
                }
                else
                {
                    string content = await httpResponse.Content.ReadAsStringAsync();
                    Response? response = JsonConvert.DeserializeObject<Response>(content);
                    if (response is null)
                    {
                        defaultResponse.ClearAndAddError("A módosítás http kérés hibát okozott!");
                    }
                    else return response;
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            defaultResponse.ClearAndAddError("Az adatok mentése nem lehetséges!");
            return defaultResponse;
        }

        public async Task<Response> DeleteAsync(Guid id)
        {
            Response defaultResponse = new();
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.DeleteAsync($"{path}/{id}");
                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    string content = await httpResponse.Content.ReadAsStringAsync();
                    Response? response = JsonConvert.DeserializeObject<Response>(content);
                    if (response is null)
                    {
                        defaultResponse.ClearAndAddError("The deletion produced an http error!");
                    }
                    else return response;
                }
                else if (!httpResponse.IsSuccessStatusCode)
                {
                    httpResponse.EnsureSuccessStatusCode();
                }
                else
                {
                    string content = await httpResponse.Content.ReadAsStringAsync();
                    Response? response = JsonConvert.DeserializeObject<Response>(content);
                    if (response is null)
                    {
                       defaultResponse.ClearAndAddError("The change produced an http error!");
                    }
                    else return response;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            defaultResponse.ClearAndAddError("The data cannot be deleted!");

            return defaultResponse;
        }

        public async Task<List<Tmodel>> GetAllAsync()
        {
            try
            {
                List<TDto>? resultDto = await _httpClient.GetFromJsonAsync<List<TDto>>(path);
                if (resultDto != null)
                    return resultDto.Select(dto => _assembler!.ToModel(dto)).ToList();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return new List<Tmodel>();
        }

        public Task<Tmodel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> UpdateAsync(Tmodel model)
        {
            Response defaultResponse = new();
            try
            {
                HttpResponseMessage httpResponse = await _httpClient.PutAsJsonAsync(path, _assembler.ToDto(model));
                if (httpResponse.StatusCode == HttpStatusCode.BadRequest)
                {
                    string content = await httpResponse.Content.ReadAsStringAsync();
                    Response? response = JsonConvert.DeserializeObject<Response>(content);
                    if (response is null)
                    {
                        defaultResponse.ClearAndAddError("The change produced an http error!");
                    }
                    else return response;
                }
                else if (!httpResponse.IsSuccessStatusCode)
                {
                    httpResponse.EnsureSuccessStatusCode();
                }
                else
                {
                    string content = await httpResponse.Content.ReadAsStringAsync();
                    Response? response = JsonConvert.DeserializeObject<Response>(content);
                    if (response is null)
                    {
                        defaultResponse.ClearAndAddError("The change produced an http error!");

                    }
                    else return response;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            defaultResponse.ClearAndAddError("The data cannot be updated!");
            
            return defaultResponse;
        }
    }
}
