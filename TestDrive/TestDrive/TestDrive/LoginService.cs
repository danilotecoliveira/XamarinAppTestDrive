using System;
using Xamarin.Forms;
using System.Net.Http;
using TestDrive.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TestDrive
{
    public class LoginService
    {
        public async Task FazerLogin(Login login)
        {

            using (HttpClient client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", login.Email),
                    new KeyValuePair<string, string>("senha", login.Senha)
                });

                client.BaseAddress = new Uri("https://aluracar.herokuapp.com");

                HttpResponseMessage resultado = new HttpResponseMessage();
                try
                {
                    resultado = await client.PostAsync("/login", content);
                }
                catch (Exception ex)
                {
                    MessagingCenter.Send(new LoginException("Sem conexão"), "FalhaLogin");
                }

                if (resultado.IsSuccessStatusCode)
                    MessagingCenter.Send(new Usuario(), "SucessoLogin");
                else
                    MessagingCenter.Send(new LoginException("Credencial incorreta"), "FalhaLogin");
            }
        }
    }

    public class LoginException : Exception
    {
        public LoginException(string message) : base(message) {}
    }
}
