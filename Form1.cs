using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotEnv.Core;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;


namespace Client_AI_API
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Get_Model();
            TextBox_ans.ReadOnly = true;
        }

        private async void Get_Model()
        {
            new EnvLoader().Load();
            var reader = new EnvReader();
            var url = reader["MODEL_API_URL"];
            using (var client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        // Десериализация JSON-ответа в объект
                        var responseObject = JsonConvert.DeserializeObject<dynamic>(content);

                        // Проверка на null
                        if (responseObject != null)
                        {
                            // Извлечение текста из свойства "result"
                            string answerText = responseObject;

                            // Разделение строки на массив моделей
                            string[] models = answerText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            // Удаление префиксов и создание нового массива
                            var filteredModels = models
                                .Skip(2) // Пропускаем первые два элемента
                                .Select(model => Regex.Replace(model, @"^\d+\.", "").Trim()) // Удаляем префиксы
                                .ToArray();

                            // Добавление оставшихся моделей в комбобокс
                            models_combobox.Items.AddRange(filteredModels);

                            // Установка первой модели по умолчанию
                            if (filteredModels.Length > 0)
                            {
                                models_combobox.SelectedIndex = 0; // Устанавливаем первую модель как выбранную
                            }
                        }
                        else
                        {
                            TextBox_ans.Text = "Ошибка: ответ не содержит ожидаемого свойства 'result'.";
                        }
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
                catch (JsonException ex)
                {
                    TextBox_ans.Text = $"Ошибка при десериализации JSON: {ex.Message}";
                }
            }
        }

        private async void Send_Text() 
        {
            new EnvLoader().Load();
            var reader = new EnvReader();
            string question = TextBox_chat.Text;
            string model = models_combobox.Text;
            TextBox_ans.Text = "Выполняется запрос...";
            var url = reader["TALK_API_URL"];
            var questionModel = new { question = question, model = model };
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

        private async void button_send_Click(object sender, EventArgs e)
        {
            Send_Text();
        }

        private void TextBox_chat_Click(object sender, EventArgs e)
        {
            TextBox_chat.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TextBox_chat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Предотвращаем звуковой сигнал при нажатии Enter
                Send_Text();
            }
        }
    }
}
