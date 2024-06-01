namespace YMHDotNetCore.WinFormsApp
{
    partial class FormBlogList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dgvData = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            Edit = new DataGridViewButtonColumn();
            Delete = new DataGridViewButtonColumn();
            Title = new DataGridViewTextBoxColumn();
            colAuthor = new DataGridViewTextBoxColumn();
            colContent = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            SuspendLayout();
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dgvData.AllowUserToDeleteRows = false;
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvData.Columns.AddRange(new DataGridViewColumn[] { colId, Edit, Delete, Title, colAuthor, colContent });
            dgvData.Dock = DockStyle.Fill;
            dgvData.Location = new Point(0, 0);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersWidth = 62;
            dgvData.RowTemplate.Height = 33;
            dgvData.Size = new Size(800, 450);
            dgvData.TabIndex = 0;
            dgvData.CellContentClick += dgvData_CellContentClick;
            // 
            // colId
            // 
            colId.DataPropertyName = "BlogId";
            colId.HeaderText = "ID";
            colId.MinimumWidth = 8;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;
            // 
            // Edit
            // 
            Edit.HeaderText = "Edit";
            Edit.MinimumWidth = 8;
            Edit.Name = "Edit";
            Edit.ReadOnly = true;
            Edit.Resizable = DataGridViewTriState.True;
            Edit.SortMode = DataGridViewColumnSortMode.Automatic;
            Edit.Text = "Edit";
            Edit.UseColumnTextForButtonValue = true;
            // 
            // Delete
            // 
            Delete.HeaderText = "Delete";
            Delete.MinimumWidth = 8;
            Delete.Name = "Delete";
            Delete.ReadOnly = true;
            Delete.Text = "Delete";
            Delete.UseColumnTextForButtonValue = true;
            // 
            // Title
            // 
            Title.DataPropertyName = "BlogTitle";
            Title.HeaderText = "Title";
            Title.MinimumWidth = 8;
            Title.Name = "Title";
            Title.ReadOnly = true;
            // 
            // colAuthor
            // 
            colAuthor.DataPropertyName = "BlogAuthor";
            colAuthor.HeaderText = "Author";
            colAuthor.MinimumWidth = 8;
            colAuthor.Name = "colAuthor";
            colAuthor.ReadOnly = true;
            // 
            // colContent
            // 
            colContent.DataPropertyName = "BlogContent";
            colContent.HeaderText = "Content";
            colContent.MinimumWidth = 8;
            colContent.Name = "colContent";
            colContent.ReadOnly = true;
            // 
            // FormBlogList
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvData);
            Name = "FormBlogList";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Blog List";
            Load += FormBlogList_Load;
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvData;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewButtonColumn Edit;
        private DataGridViewButtonColumn Delete;
        private DataGridViewTextBoxColumn Title;
        private DataGridViewTextBoxColumn colAuthor;
        private DataGridViewTextBoxColumn colContent;
    }
}