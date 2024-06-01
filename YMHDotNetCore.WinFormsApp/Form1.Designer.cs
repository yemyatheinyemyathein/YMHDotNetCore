namespace YMHDotNetCore.WinFormsApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnClick = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textAuthor = new TextBox();
            textTitle = new TextBox();
            textContent = new TextBox();
            button1 = new Button();
            btnUpdate = new Button();
            SuspendLayout();
            // 
            // btnClick
            // 
            btnClick.BackColor = Color.Green;
            resources.ApplyResources(btnClick, "btnClick");
            btnClick.ForeColor = Color.Transparent;
            btnClick.Name = "btnClick";
            btnClick.UseVisualStyleBackColor = false;
            btnClick.Click += btnSave_Click;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // textAuthor
            // 
            resources.ApplyResources(textAuthor, "textAuthor");
            textAuthor.Name = "textAuthor";
            // 
            // textTitle
            // 
            resources.ApplyResources(textTitle, "textTitle");
            textTitle.Name = "textTitle";
            // 
            // textContent
            // 
            resources.ApplyResources(textContent, "textContent");
            textContent.Name = "textContent";
            // 
            // button1
            // 
            button1.BackColor = Color.Gray;
            resources.ApplyResources(button1, "button1");
            button1.ForeColor = Color.Transparent;
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = false;
            button1.Click += btnCancel_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.LightSeaGreen;
            resources.ApplyResources(btnUpdate, "btnUpdate");
            btnUpdate.ForeColor = Color.Transparent;
            btnUpdate.Name = "btnUpdate";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnUpdate);
            Controls.Add(button1);
            Controls.Add(textContent);
            Controls.Add(textTitle);
            Controls.Add(textAuthor);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnClick);
            Name = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnClick;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textAuthor;
        private TextBox textTitle;
        private TextBox textContent;
        private Button button1;
        private Button btnUpdate;
    }
}
