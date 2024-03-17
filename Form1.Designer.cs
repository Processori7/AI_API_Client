namespace Client_AI_API
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            TextBox_chat = new RichTextBox();
            button_send = new Button();
            TextBox_ans = new RichTextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 30F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.Gainsboro;
            label1.Location = new Point(319, 27);
            label1.Name = "label1";
            label1.Size = new Size(383, 54);
            label1.TabIndex = 0;
            label1.Text = "Добро пожаловать!";
            // 
            // TextBox_chat
            // 
            TextBox_chat.BackColor = Color.DarkGray;
            TextBox_chat.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            TextBox_chat.Location = new Point(137, 338);
            TextBox_chat.Name = "TextBox_chat";
            TextBox_chat.Size = new Size(753, 77);
            TextBox_chat.TabIndex = 1;
            TextBox_chat.Text = "Введите сообщение!";
            TextBox_chat.Click += TextBox_chat_Click;
            // 
            // button_send
            // 
            button_send.Location = new Point(444, 435);
            button_send.Name = "button_send";
            button_send.Size = new Size(75, 23);
            button_send.TabIndex = 2;
            button_send.Text = "Отправить";
            button_send.UseVisualStyleBackColor = true;
            button_send.Click += button_send_Click;
            // 
            // TextBox_ans
            // 
            TextBox_ans.BackColor = Color.LightGray;
            TextBox_ans.Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point);
            TextBox_ans.Location = new Point(137, 94);
            TextBox_ans.Name = "TextBox_ans";
            TextBox_ans.Size = new Size(753, 238);
            TextBox_ans.TabIndex = 3;
            TextBox_ans.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Gray;
            ClientSize = new Size(1047, 496);
            Controls.Add(TextBox_ans);
            Controls.Add(button_send);
            Controls.Add(TextBox_chat);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Чат";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private RichTextBox TextBox_chat;
        private Button button_send;
        private RichTextBox TextBox_ans;
    }
}
