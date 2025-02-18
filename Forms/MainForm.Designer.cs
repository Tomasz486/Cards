using System.Drawing;
using System.Windows.Forms;

namespace Cards
{
    partial class MainForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.deleteButton = new System.Windows.Forms.Button();
            this.FilteredId = new System.Windows.Forms.TextBox();
            this.addCard = new System.Windows.Forms.Button();
            this.FilterLabel = new System.Windows.Forms.Label();
            this.FilteredSerialNumber = new System.Windows.Forms.TextBox();
            this.FilteredAccountNumber = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(34, 42);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.Size = new System.Drawing.Size(536, 475);
            this.dataGridView1.TabIndex = 0;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(349, 540);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(205, 29);
            this.deleteButton.TabIndex = 1;
            this.deleteButton.Text = "Usuń zaznaczoną kartę";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // FilteredId
            // 
            this.FilteredId.Location = new System.Drawing.Point(111, 12);
            this.FilteredId.Name = "FilteredId";
            this.FilteredId.Size = new System.Drawing.Size(164, 26);
            this.FilteredId.TabIndex = 2;
            // 
            // addCard
            // 
            this.addCard.Location = new System.Drawing.Point(40, 540);
            this.addCard.Name = "addCard";
            this.addCard.Size = new System.Drawing.Size(235, 29);
            this.addCard.TabIndex = 3;
            this.addCard.Text = "Dodaj kartę\r\n";
            this.addCard.UseVisualStyleBackColor = true;
            this.addCard.Click += new System.EventHandler(this.AddCardButton_Click);
            // 
            // FilterLabel
            // 
            this.FilterLabel.AutoSize = true;
            this.FilterLabel.Location = new System.Drawing.Point(53, 15);
            this.FilterLabel.Name = "FilterLabel";
            this.FilterLabel.Size = new System.Drawing.Size(46, 20);
            this.FilterLabel.TabIndex = 4;
            this.FilterLabel.Text = "Filtry:";
            // 
            // FilteredSerialNumber
            // 
            this.FilteredSerialNumber.Location = new System.Drawing.Point(261, 12);
            this.FilteredSerialNumber.Name = "FilteredSerialNumber";
            this.FilteredSerialNumber.Size = new System.Drawing.Size(153, 26);
            this.FilteredSerialNumber.TabIndex = 6;
            // 
            // FilteredAccountNumber
            // 
            this.FilteredAccountNumber.Location = new System.Drawing.Point(412, 12);
            this.FilteredAccountNumber.Name = "FilteredAccountNumber";
            this.FilteredAccountNumber.Size = new System.Drawing.Size(150, 26);
            this.FilteredAccountNumber.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 610);
            this.Controls.Add(this.FilteredAccountNumber);
            this.Controls.Add(this.FilteredSerialNumber);
            this.Controls.Add(this.FilterLabel);
            this.Controls.Add(this.addCard);
            this.Controls.Add(this.FilteredId);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.dataGridView1);
            this.Name = "MainForm";
            this.Text = "Zarządzaj kartami";
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dataGridView1;

        private Button deleteButton;
        private TextBox FilteredId;
        private Button addCard;
        private Label FilterLabel;
        private TextBox FilteredSerialNumber;
        private TextBox FilteredAccountNumber;
    }
}
