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
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("email", login.Email),
                        new KeyValuePair<string, string>("senha", login.Senha)
                    });

                    client.BaseAddress = new Uri("https://aluracar.herokuapp.com.br/login");
                    var resultado = await client.PostAsync("/login", content);

                    if (resultado.IsSuccessStatusCode)
                        MessagingCenter.Send(new Usuario(), "LoginSucesso");
                    else
                        MessagingCenter.Send(new LoginException("Credencial incorreta"), "FalhaLogin");
                }
            }
            catch
            {
                MessagingCenter.Send(new LoginException("Sem conexão"), "FalhaLogin");
            }
        }
    }

    public class LoginException : Exception
    {
        public LoginException(string message) : base(message) {}
    }
}
