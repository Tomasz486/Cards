using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Cards
{
    /// <summary>
    ///     The main form class.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        ///     The message about non selected row on GridView.
        /// </summary>
        private const string NonSelectedRowOnGridViewMessage = "Nie wybrano karty.";

        /// <summary>
        ///     Confirm delete message.
        /// </summary>
        private const string ConfirmDeleteMessage = "Czy na pewno chcesz usunąć kartę?";

        /// <summary>
        ///     Confirm delete dialog title.
        /// </summary>
        private const string ConfirmDeleteDialogTitle = "Potwierdzenie usuwania";


        /// <summary>
        ///     The Data connector.
        /// </summary>
        private readonly SqlDataConnector DataConnector = new SqlDataConnector();

        /// <summary>
        ///     The main form's constructor.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            
            if (!this.DataConnector.CreateDataBaseIfNotExist(out string errorMessage))
            {
                MessageBox.Show(errorMessage);
            }

            this.GetDataToGridView();
        }

        /// <summary>
        ///     Get data to gridView.
        /// </summary>
        private void GetDataToGridView()
        {
            dataGridView1.DataSource = DataConnector.GetData(this.FilteredId.Text,
                this.FilteredSerialNumber.Text, this.FilteredAccountNumber.Text, out string errorMesage);

            if (errorMesage != null)
            {
                MessageBox.Show(errorMesage);
            }

            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
            dataGridView1.Update();
        }

        /// <summary>
        ///     The filtered text changed event.
        /// </summary>
        /// <param name="sender">
        ///     The sender,
        /// </param>
        /// <param name="e">
        ///     The event args,
        /// </param>
        private void Filtered_TextChanged(object sender, EventArgs e)
        {
            this.GetDataToGridView();
        }

        /// <summary>
        ///     The delete button click event.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The event args.
        /// </param>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show(NonSelectedRowOnGridViewMessage);
                return;
            }

            var id = dataGridView1.CurrentRow.Cells["Identyfikator"].Value.ToString();

            if (id == null)
            {
                MessageBox.Show(NonSelectedRowOnGridViewMessage);
                return;
            }

            if (MessageBox.Show(ConfirmDeleteMessage, ConfirmDeleteDialogTitle, MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }

            this.DataConnector.DeleteRowById(id,out string errorMessage);

            if (errorMessage != null)
            {
                MessageBox.Show(errorMessage);
            }

            this.GetDataToGridView();
        }

        /// <summary>
        ///     The delete button click event.
        /// </summary>
        /// <param name="sender">
        ///     The sender.
        /// </param>
        /// <param name="e">
        ///     The event args.
        /// </param>
        private void AddCardButton_Click(object sender, EventArgs e)
        {
            var addCardForm = new AddCardForm();
            addCardForm.ShowDialog();
            addCardForm.Dispose();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            this.GetDataToGridView();
        }
    }
}
