using System;
using System.Linq;
using System.Windows.Forms;
using Cards.Tools;

namespace Cards
{
    /// <summary>
    ///     The add acrd form.
    /// </summary>
    public partial class AddCardForm : Form
    {
        /// <summary>
        ///     The message about non unique value.
        /// </summary>
        private const string NonUniqueValueMessage = " już istnieje:";

        /// <summary>
        ///     The Data connector.
        /// </summary>
        private readonly SqlDataConnector DataConnector = new SqlDataConnector();

        /// <summary>
        ///     The add form constructor.
        /// </summary>
        public AddCardForm()
        {
            InitializeComponent();
        }

        /// <summary>
        ///     The add button click event.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The event args.
        /// </param>
        private void AddCardButton_Click(object sender, EventArgs e)
        {
            if (this.Controls.OfType<TextBox>().Any(t => t.Text == string.Empty))
            {
                MessageBox.Show("Proszę wypełnić wszystkie pola.");
                return;
            }

            var generatedId = new SimpleGeneratorId().GenerateUniqueId();

            var dataTable = this.DataConnector.GetSelectedData(generatedId, this.SerialNumberTextBox.Text, this.AccountNumberTextBox.Text);

            var dupkicatedFieldName = new FormValidation().CompareDataFromFormAndDatabase(dataTable, this.SerialNumberTextBox.Text, this.AccountNumberTextBox.Text);
            
            if (dupkicatedFieldName != null)
            {
                MessageBox.Show($"{dupkicatedFieldName} {NonUniqueValueMessage}");
                return;
            }

            this.DataConnector.AddCard(generatedId, this.PinTextBox.Text, this.SerialNumberTextBox.Text, this.AccountNumberTextBox.Text);

            this.Close();
        }

        /// <summary>
        ///     Key press on accout number textbox.
        /// </summary>
        /// <param name="sender">
        ///     Thr sender.
        /// </param>
        /// <param name="e">
        ///     The event args
        /// </param>
        private void AccountNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        /// <summary>
        ///     Key press on pin textbox.
        /// </summary>
        /// <param name="sender">
        ///     Thr sender.
        /// </param>
        /// <param name="e">
        ///     The event args
        /// </param>
        private void PinTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        /// <summary>
        ///     Key press on serial number textbox.
        /// </summary>
        /// <param name="sender">
        ///     Thr sender.
        /// </param>
        /// <param name="e">
        ///     The event args
        /// </param>
        private void SerialNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
