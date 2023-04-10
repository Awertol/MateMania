using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MateMania
{
    public static class DbData
    {
        static HttpClient client = new HttpClient();
        public static UserModel nactenyUzivatel;

        public static void NastavClient()
        {
            client.BaseAddress = new Uri("https://bprestapitest.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public static async void RegistrovatUzivatele(string username, string password)
        {
            UserBase vkladanyUzivatel = new UserBase();
            vkladanyUzivatel.Nickname = username;
            vkladanyUzivatel.UserPassword = password;
            HttpResponseMessage odpoved = await client.PostAsJsonAsync("/user/RegisterUser", vkladanyUzivatel);
            odpoved.EnsureSuccessStatusCode();
        }
        public static async Task<UserModel> LoginUzivatele(string username, string password)
        {
            UserModel vracenyUzivatel = null;
            UserBase prihlasovanyUzivatel = new UserBase();
            prihlasovanyUzivatel.Nickname = username;
            prihlasovanyUzivatel.UserPassword = password;
            HttpResponseMessage odpoved = await client.GetAsync(client.BaseAddress + $"user/LoginUser/{prihlasovanyUzivatel.Nickname}/{prihlasovanyUzivatel.UserPassword}");
            if(odpoved.IsSuccessStatusCode)
            {
                vracenyUzivatel = await odpoved.Content.ReadFromJsonAsync<UserModel>();
                nactenyUzivatel = vracenyUzivatel;
            }
            return vracenyUzivatel;
        }
        public static async void ZmenitUdaj(string choice, string updatedValue)
        {
            HttpResponseMessage odpoved = await client.PostAsJsonAsync($"/user/Update{choice}/{nactenyUzivatel.Id}/{updatedValue}", nactenyUzivatel);
            odpoved.EnsureSuccessStatusCode();
        }
        public static async void ZmenitMedaili(int medaile)
        {
            HttpResponseMessage odpoved = await client.PostAsJsonAsync($"/user/UpdateMedal/{nactenyUzivatel.Id}/{medaile}/true", nactenyUzivatel);
            odpoved.EnsureSuccessStatusCode();
        }
        public static async void ZmenitTitul(int titul)
        {
            HttpResponseMessage odpoved = await client.PostAsJsonAsync($"/user/UpdateTitle/{nactenyUzivatel.Id}/{titul}", nactenyUzivatel);
            odpoved.EnsureSuccessStatusCode();
        }
    }
}
