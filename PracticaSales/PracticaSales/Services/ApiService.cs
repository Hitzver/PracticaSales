using Newtonsoft.Json;
using PracticaSales.Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PracticaSales.Services
{
    public class ApiService
    {
        public async Task<Response> GetList<T> (string urlBase , string prefix, string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{prefix}{controller}"; // string.Format("{0}{1}", prefix, controller); 
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(answer);
                return new Response
                {
                    IsSuccess = true,
                    Result = list
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    IsSuccess =false,
                    Message = err.Message
                };
            }
        }
    }
}
