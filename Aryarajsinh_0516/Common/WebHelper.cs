using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Aryarajsinh_0516.Common
{

    public class WebHelper

    {

        public static async Task<string> HttpRequestResponse(string url)

        {

            try

            {

                using (HttpClient client = new HttpClient())

                {

                    client.BaseAddress = new Uri("http://localhost:57080/");

                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)

                    {

                        return response.Content.ReadAsStringAsync().Result;

                    }

                    return null;

                }

            }

            catch (Exception e)

            {

                throw e;

            }

        }

        public static async Task<string> HttpClientPostRequest(string url, string jsonContent)

        {

            using (HttpClient client = new HttpClient())

            {

                client.BaseAddress = new Uri("http://localhost:57080/");

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync(url, new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)

                {

                    return await response.Content.ReadAsStringAsync();

                }

                return null;

            }

        }

        public static async Task<string> HttpRequestResponse(string url, string JwtToken)

        {

            try

            {

                using (HttpClient client = new HttpClient())

                {

                    client.BaseAddress = new Uri("http://localhost:57080/");

                    client.DefaultRequestHeaders.Accept.Clear();

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)

                    {

                        return response.Content.ReadAsStringAsync().Result;

                    }

                    return null;

                }

            }

            catch (Exception e)

            {

                throw e;

            }

        }

        public static async Task<string> HttpClientPostRequests(string url, string jsonContent, string JWtToken)

        {

            using (HttpClient client = new HttpClient())

            {

                client.BaseAddress = new Uri("http://localhost:57080/");

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWtToken);
                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsync(url, new StringContent(jsonContent, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)

                {

                    return await response.Content.ReadAsStringAsync();

                }

                return null;

            }

        }

    }

}