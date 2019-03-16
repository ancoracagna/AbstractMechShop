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
    public partial class FormService : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }
        private readonly IMechService service;
        private int? id;
        private List<ServiceComponentViewModel> serviceComponents;
        public FormService(IMechService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormServiceComponent>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.ServiceId = id.Value;
                    }
                    serviceComponents.Add(form.Model);
                }
                LoadData();
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        serviceComponents.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void ChangeBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormServiceComponent>();
                form.Model =
               serviceComponents[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    serviceComponents[dataGridView.SelectedRows[0].Cells[0].RowIndex] =
                   form.Model;
                    LoadData();
                }
            }
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void FormService_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ServiceViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.ServiceName;
                        textBoxPrice.Text = view.Price.ToString();
                        serviceComponents = view.ServiceComponents;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                serviceComponents = new List<ServiceComponentViewModel>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (serviceComponents != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = serviceComponents;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (serviceComponents == null || serviceComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<ServiceComponentBindingModel> productComponentBM = new
               List<ServiceComponentBindingModel>();
                for (int i = 0; i < serviceComponents.Count; ++i)
                {
                    productComponentBM.Add(new ServiceComponentBindingModel
                    {
                        Id = serviceComponents[i].Id,
                        ServiceId = serviceComponents[i].ServiceId,
                        ComponentId = serviceComponents[i].ComponentId,
                        Count = serviceComponents[i].Count
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new ServiceBindingModel
                    {
                        Id = id.Value,
                        ServiceName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        ServiceComponents = productComponentBM
                    });
                }
                else
                {
                    service.AddElement(new ServiceBindingModel
                    {
                        ServiceName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        ServiceComponents = productComponentBM
                    });
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
