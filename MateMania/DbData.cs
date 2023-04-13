using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using MateMania.Models;
using Newtonsoft.Json;

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
            if (odpoved.IsSuccessStatusCode)
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
        public static async void NastavitTitul(int titul)
        {
            HttpResponseMessage odpoved = await client.PostAsJsonAsync($"/user/SetTitle/{nactenyUzivatel.Id}/{titul}", nactenyUzivatel);
            odpoved.EnsureSuccessStatusCode();
        }
        public static async void ZmenitSkolu(string nazevSkoly, int rocnik)
        {
            HttpResponseMessage odpoved = await client.PostAsJsonAsync($"/user/UpdateSchool/{nactenyUzivatel.Id}/{nazevSkoly}/{rocnik}", nactenyUzivatel);
            odpoved.EnsureSuccessStatusCode();
        }
        public static async void ZmenitStavUc(bool zmena)
        {
            HttpResponseMessage odpoved = await client.PostAsJsonAsync($"/user/UpdateTeacher/{nactenyUzivatel.Id}/{zmena}", nactenyUzivatel);
            odpoved.EnsureSuccessStatusCode();
        }

        public static async Task<List<UserModel>> NajitClenyTridy(int idTridy)
        {
            HttpResponseMessage odpoved = await client.GetAsync(client.BaseAddress + $"/user/FindClassmates/{idTridy}");
            if (odpoved.IsSuccessStatusCode)
            {
                var cleniTridy = await odpoved.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<UserModel>>(cleniTridy);
                return data;
            }
            return null;
        }
        public static async Task<ExamsModel> NajitOtazky(string pin)
        {
            HttpResponseMessage odpoved = await client.GetAsync(client.BaseAddress + $"/exam/Find/{pin}/{nactenyUzivatel.ChosenClassID}");
            if (odpoved.IsSuccessStatusCode)
            {
                return await odpoved.Content.ReadFromJsonAsync<ExamsModel>();
            }
            return null;
        }
        public static async Task<List<ExamsModel>> NajitOtazkyTridy()
        {
            HttpResponseMessage odpoved = await client.GetAsync(client.BaseAddress + $"/exam/Find/{nactenyUzivatel.ChosenClassID}");
            if (odpoved.IsSuccessStatusCode)
            {
                var otazkytridy = await odpoved.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ExamsModel>>(otazkytridy);
                return data;
            }
            return null;
        }

        public static async Task<List<ClassModel>> NajitMesta(string kraj, string okres)
        {
            HttpResponseMessage odpoved = await client.GetAsync(client.BaseAddress + $"/class/FindCities/{kraj}/{okres}/");
            if (odpoved.IsSuccessStatusCode)
            {
                var mesta = await odpoved.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ClassModel>>(mesta);
                return data;
            }
            else return null;
        }
        public static async Task<List<ClassModel>> NajitSkoly()
        {
            HttpResponseMessage odpoved = await client.GetAsync(client.BaseAddress + $"/api/classes/");
            if (odpoved.IsSuccessStatusCode)
            {
                var trida = await odpoved.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ClassModel>>(trida);
                return data;
            }
            else return null;
        }
        public static async Task<List<ClassModel>> NajitSkoly(string kraj, string okres, string mesto)
        {
            HttpResponseMessage odpoved = await client.GetAsync(client.BaseAddress + $"/class/Find/{kraj}/{okres}/{mesto}");
            if (odpoved.IsSuccessStatusCode)
            {
                var trida = await odpoved.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ClassModel>>(trida);
                return data;
            }
            else return null;
        }
        public static async Task<ClassModel> NajitUzivatelovuSkolu()
        {
            HttpResponseMessage odpoved = await client.GetAsync(client.BaseAddress + $"/class/Find/{nactenyUzivatel.ChosenClassID}");
            if (odpoved.IsSuccessStatusCode)
            {
                return await odpoved.Content.ReadFromJsonAsync<ClassModel>();
            }
            else return null;
        }
        public static async Task<List<ExamAnswersModel>> NajitOdpovedi(int idZadani)
        {
            HttpResponseMessage odpoved = await client.GetAsync(client.BaseAddress + $"/answer/Find/{nactenyUzivatel.Id}/{idZadani}");
            if (odpoved.IsSuccessStatusCode)
            {
                var odpovedi = await odpoved.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<ExamAnswersModel>>(odpovedi);
                return data;
            }
            else return null;
        }
        public static async void VytvoritZadani(ExamsModel vkladaneZadani)
        {
            //dodělat
            HttpResponseMessage odpoved = await client.PostAsJsonAsync("/exam/Create", vkladaneZadani);
            odpoved.EnsureSuccessStatusCode();
        }
        public static async void VytvoritTridu(ClassModel vkladanaTrida)
        {
            HttpResponseMessage odpoved = await client.PostAsJsonAsync("/class/Create", vkladanaTrida);
            odpoved.EnsureSuccessStatusCode();
        }
        public static async void PridatVysledek(ExamAnswersModel vkladanyVysledek)
        {
            HttpResponseMessage odpoved = await client.PostAsJsonAsync("/answer/Create", vkladanyVysledek);
            odpoved.EnsureSuccessStatusCode();
        }
    }
}
