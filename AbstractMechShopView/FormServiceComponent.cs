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
    public partial class FormServiceComponent : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public ServiceComponentViewModel Model { set { model = value; } get { return
model; } }
        private readonly IComponentService service;
        private ServiceComponentViewModel model;
        public FormServiceComponent(IComponentService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormServiceComponent_Load(object sender, EventArgs e)
        {
            try
            {
                List<AutoPartsViewModel> list = service.GetList();
                if (list != null)
                {
                    comboBoxComponent.DisplayMember = "AutoPartsName";
                    comboBoxComponent.ValueMember = "Id";
                    comboBoxComponent.DataSource = list;
                    comboBoxComponent.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxComponent.Enabled = false;
                comboBoxComponent.SelectedValue = model.ComponentId;
                textBoxCount.Text = model.Count.ToString();
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxComponent.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new ServiceComponentViewModel
                    {
                        ComponentId = Convert.ToInt32(comboBoxComponent.SelectedValue),
                        AutoPartsName = comboBoxComponent.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
                }
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

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
