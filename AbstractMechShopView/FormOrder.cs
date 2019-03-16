using AbstractShopServiceDAL.BindingModel;
using AbstractShopServiceDAL.Interfaces;
using AbstractShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace AbstractMechShopView
{
    public partial class FormOrder : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IClientService serviceC;
        private readonly IMechService serviceP;
        private readonly IMainService serviceM;
        public FormOrder(IClientService serviceC, IMechService serviceP,IMainService serviceM)
        {
            InitializeComponent();
            this.serviceC = serviceC;
            this.serviceP = serviceP;
            this.serviceM = serviceM;

        }

        private void FormOrder_Load(object sender, EventArgs e)
        {
            try
            {
                List<CustomerViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxClient.DisplayMember = "CustomerFIO";
                    comboBoxClient.ValueMember = "Id";
                    comboBoxClient.DataSource = listC;
                    comboBoxClient.SelectedItem = null;
                }
                List<ServiceViewModel> listP = serviceP.GetList();
                if (listP != null)
                {
                    comboBoxService.DisplayMember = "ServiceName";
                    comboBoxService.ValueMember = "Id";
                    comboBoxService.DataSource = listP;
                    comboBoxService.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void CalcSum()
        {
            if (comboBoxService.SelectedValue != null &&
           !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxService.SelectedValue);
                    ServiceViewModel product = serviceP.GetElement(id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * product.Price).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxClient.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxService.SelectedValue == null)
            {
                MessageBox.Show("Выберите услугу", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceM.CreateOrder(new OrderBindingModel
                {
                    ClientId = Convert.ToInt32(comboBoxClient.SelectedValue),
                    ServiceId = Convert.ToInt32(comboBoxService.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToInt32(textBoxSum.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }

        }

        private void comboBoxService_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
