using System.Drawing;
using System.Windows.Forms;

namespace Cards
{
    partial class AddCardForm
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
            this.AccountNumberLabel = new System.Windows.Forms.Label();
            this.PinLabel = new System.Windows.Forms.Label();
            this.SerialNumberLabel = new System.Windows.Forms.Label();
            this.AddCardButton = new System.Windows.Forms.Button();
            this.AccountNumberTextBox = new System.Windows.Forms.TextBox();
            this.PinTextBox = new System.Windows.Forms.TextBox();
            this.SerialNumberTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // AccountNumberLabel
            // 
            this.AccountNumberLabel.AutoSize = true;
            this.AccountNumberLabel.Location = new System.Drawing.Point(68, 71);
            this.AccountNumberLabel.Name = "AccountNumberLabel";
            this.AccountNumberLabel.Size = new System.Drawing.Size(104, 20);
            this.AccountNumberLabel.TabIndex = 0;
            this.AccountNumberLabel.Text = "Numer konta:";
            // 
            // PinLabel
            // 
            this.PinLabel.AutoSize = true;
            this.PinLabel.Location = new System.Drawing.Point(142, 101);
            this.PinLabel.Name = "PinLabel";
            this.PinLabel.Size = new System.Drawing.Size(35, 20);
            this.PinLabel.TabIndex = 1;
            this.PinLabel.Text = "Pin:";
            // 
            // SerialNumberLabel
            // 
            this.SerialNumberLabel.AutoSize = true;
            this.SerialNumberLabel.Location = new System.Drawing.Point(68, 135);
            this.SerialNumberLabel.Name = "SerialNumberLabel";
            this.SerialNumberLabel.Size = new System.Drawing.Size(112, 20);
            this.SerialNumberLabel.TabIndex = 2;
            this.SerialNumberLabel.Text = "Numer seryjny:";
            // 
            // AddCardButton
            // 
            this.AddCardButton.Location = new System.Drawing.Point(181, 186);
            this.AddCardButton.Name = "AddCardButton";
            this.AddCardButton.Size = new System.Drawing.Size(141, 29);
            this.AddCardButton.TabIndex = 3;
            this.AddCardButton.Text = "Dodaj kartę";
            this.AddCardButton.UseVisualStyleBackColor = true;
            this.AddCardButton.Click += new System.EventHandler(this.AddCardButton_Click);
            // 
            // AccountNumberTextBox
            // 
            this.AccountNumberTextBox.Location = new System.Drawing.Point(181, 68);
            this.AccountNumberTextBox.MaxLength = 10;
            this.AccountNumberTextBox.Name = "AccountNumberTextBox";
            this.AccountNumberTextBox.Size = new System.Drawing.Size(140, 26);
            this.AccountNumberTextBox.TabIndex = 4;
            this.AccountNumberTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.AccountNumberTextBox_KeyPress);
            // 
            // PinTextBox
            // 
            this.PinTextBox.Location = new System.Drawing.Point(181, 101);
            this.PinTextBox.MaxLength = 10;
            this.PinTextBox.Name = "PinTextBox";
            this.PinTextBox.Size = new System.Drawing.Size(140, 26);
            this.PinTextBox.TabIndex = 5;
            this.PinTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PinTextBox_KeyPress);
            // 
            // SerialNumberTextBox
            // 
            this.SerialNumberTextBox.Location = new System.Drawing.Point(181, 135);
            this.SerialNumberTextBox.MaxLength = 10;
            this.SerialNumberTextBox.Name = "SerialNumberTextBox";
            this.SerialNumberTextBox.Size = new System.Drawing.Size(140, 26);
            this.SerialNumberTextBox.TabIndex = 6;
            this.SerialNumberTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SerialNumberTextBox_KeyPress);
            // 
            // AddCardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 279);
            this.Controls.Add(this.SerialNumberTextBox);
            this.Controls.Add(this.PinTextBox);
            this.Controls.Add(this.AccountNumberTextBox);
            this.Controls.Add(this.AddCardButton);
            this.Controls.Add(this.SerialNumberLabel);
            this.Controls.Add(this.PinLabel);
            this.Controls.Add(this.AccountNumberLabel);
            this.Name = "AddCardForm";
            this.Text = "Dodaj  kartę";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label AccountNumberLabel;
        private Label PinLabel;
        private Label SerialNumberLabel;
        private Button AddCardButton;
        private TextBox AccountNumberTextBox;
        private TextBox PinTextBox;
        private TextBox SerialNumberTextBox;
    }
}