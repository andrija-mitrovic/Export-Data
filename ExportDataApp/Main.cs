using ExcelReportApp.Core.Data;
using ExcelReportApp.Core.Data.Repository;
using ExportDataApp.Core.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExportDataApp
{
    public partial class Main : Form
    {
        private ExportDataService _service;
        private DataTable _dt;
        private IProductRepository _repo;

        public Main()
        {
            InitializeComponent();

            _service = new ExportDataService();
            _repo = new ProductRepository();
            _dt = _repo.GetAllProducts(new SqlDbConnection());

            this.dgvView.DataSource = _dt;
            this.btnCsv.Click += BtnCsv_Click;
            this.btnExcel.Click += BtnExcel_Click;
            this.btnDbf.Click += BtnDbf_Click;
        }

        private void BtnDbf_Click(object sender, EventArgs e)
        {
            _service.ExportToDbf("Products", _dt);
            this.Close();
        }

        private void BtnExcel_Click(object sender, EventArgs e)
        {
            _service.ExportToExcel("Products.xlsx", _dt);
            this.Close();
        }

        private void BtnCsv_Click(object sender, EventArgs e)
        {
            _service.ExportToCsv("SELECT sifra, barco, naziv, jedmj, pakov, zempo, roktr FROM R_ROBA", "Products.csv");
            this.Close();
        }
    }
}
