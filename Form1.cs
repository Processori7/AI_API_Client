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
                        // �������������� JSON-������ � ������
                        var responseObject = JsonConvert.DeserializeObject<dynamic>(content);

                        // �������� �� null
                        if (responseObject != null)
                        {
                            // ���������� ������ �� �������� "result"
                            string answerText = responseObject;

                            // ���������� ������ �� ������ �������
                            string[] models = answerText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                            // �������� ��������� � �������� ������ �������
                            var filteredModels = models
                                .Skip(2) // ���������� ������ ��� ��������
                                .Select(model => Regex.Replace(model, @"^\d+\.", "").Trim()) // ������� ��������
                                .ToArray();

                            // ���������� ���������� ������� � ���������
                            models_combobox.Items.AddRange(filteredModels);

                            // ��������� ������ ������ �� ���������
                            if (filteredModels.Length > 0)
                            {
                                models_combobox.SelectedIndex = 0; // ������������� ������ ������ ��� ���������
                            }
                        }
                        else
                        {
                            TextBox_ans.Text = "������: ����� �� �������� ���������� �������� 'result'.";
                        }
                    }
                    else
                    {
                        TextBox_ans.Text = $"������: {response.StatusCode}";
                    }
                }
                catch (HttpRequestException ex)
                {
                    TextBox_ans.Text = $"������ ��� �������� �������: {ex.Message}";
                }
                catch (JsonException ex)
                {
                    TextBox_ans.Text = $"������ ��� �������������� JSON: {ex.Message}";
                }
            }
        }

        private async void Send_Text() 
        {
            new EnvLoader().Load();
            var reader = new EnvReader();
            string question = TextBox_chat.Text;
            string model = models_combobox.Text;
            TextBox_ans.Text = "����������� ������...";
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
                        // �������������� JSON-������ � ������
                        var responseObject = JsonConvert.DeserializeObject<dynamic>(content);
                        // ���������� ������ �� �������� "result"
                        string answerText = responseObject.result;
                        TextBox_ans.Text = answerText;
                    }
                    else
                    {
                        TextBox_ans.Text = $"������: {response.StatusCode}";
                    }
                }
                catch (HttpRequestException ex)
                {
                    TextBox_ans.Text = $"������ ��� �������� �������: {ex.Message}";
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
                e.SuppressKeyPress = true; // ������������� �������� ������ ��� ������� Enter
                Send_Text();
            }
        }
    }
}
