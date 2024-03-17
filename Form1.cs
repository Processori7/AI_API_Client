using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DotEnv.Core;


namespace Client_AI_API
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TextBox_ans.ReadOnly = true;
        }

        private async void button_send_Click(object sender, EventArgs e)
        {
            new EnvLoader().Load();
            var reader = new EnvReader();
            string question = TextBox_chat.Text;
            TextBox_ans.Text = "Выполняется запрос...";
            var url = reader["API_URL"];
            var questionModel = new { question = question };
            var json = JsonConvert.SerializeObject(questionModel);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.PostAsync(url, data);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        // Десериализация JSON-ответа в объект
                        var responseObject = JsonConvert.DeserializeObject<dynamic>(content);
                        // Извлечение текста из свойства "result"
                        string answerText = responseObject.result;
                        TextBox_ans.Text = answerText;
                    }
                    else
                    {
                        TextBox_ans.Text = $"Ошибка: {response.StatusCode}";
                    }
                }
                catch (HttpRequestException ex)
                {
                    TextBox_ans.Text = $"Ошибка при отправке запроса: {ex.Message}";
                }
            }
        }

        private void TextBox_chat_Click(object sender, EventArgs e)
        {
            TextBox_chat.Text = "";
        }
    }
}
