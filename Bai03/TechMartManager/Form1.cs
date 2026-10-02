using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TechMartManager
{
    // Lớp Model chứa thông tin Sản phẩm
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ImagePath { get; set; }
    }

    public partial class Form1 : Form
    {
        // Khai báo danh sách và nguồn kết nối dữ liệu
        private BindingList<Product> _productList;
        private BindingSource _bindingSource;

        public Form1()
        {
            InitializeComponent();

            // [QUAN TRỌNG]: Lệnh này giúp Form tự động chạy hàm Form1_Load khi mở lên
            this.Load += Form1_Load;

            // Tắt tính năng tự động sinh cột để dùng các cột đã tự định nghĩa
            dgvProducts.AutoGenerateColumns = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Khởi tạo danh sách dữ liệu và BindingSource
            _productList = new BindingList<Product>();
            _bindingSource = new BindingSource { DataSource = _productList };

            // --- THÊM DỮ LIỆU MẪU BAN ĐẦU ---
            _productList.Add(new Product { ProductId = "SP001", ProductName = "Laptop Dell XPS 13", Category = "Laptop", UnitPrice = 25000000, Quantity = 5 });
            _productList.Add(new Product { ProductId = "SP002", ProductName = "iPhone 15 Pro Max", Category = "Điện thoại", UnitPrice = 28000000, Quantity = 10 });
            _productList.Add(new Product { ProductId = "SP003", ProductName = "Tai nghe Sony WH-1000XM5", Category = "Phụ kiện", UnitPrice = 8000000, Quantity = 15 });

            // Cập nhật dữ liệu cho ComboBox Danh mục
            cboCategory.Items.Clear();
            cboCategory.Items.AddRange(new string[] { "Điện thoại", "Laptop", "Phụ kiện" });
            if (cboCategory.Items.Count > 0)
                cboCategory.SelectedIndex = 0;

            // Cấu hình giao diện các cột của DataGridView
            SetupDataGridView();

            // Gán nguồn dữ liệu cho DataGridView
            dgvProducts.DataSource = _bindingSource;

            // Đăng ký các sự kiện Click nút bấm bằng code (đảm bảo không bị lỗi giao diện)
            btnChooseImage.Click += BtnChooseImage_Click;
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            dgvProducts.CellClick += DgvProducts_CellClick;
            txtSearch.TextChanged += TxtSearch_TextChanged;

            // Đăng ký sự kiện Menu (nếu bạn có thêm thanh MenuStrip)
            if (menuExportCsv != null) menuExportCsv.Click += MenuExportCsv_Click;
            if (menuExit != null) menuExit.Click += (s, ev) => Application.Exit();

            // Cập nhật dòng trạng thái "Tổng số sản phẩm"
            UpdateStatus();
        }

        // Cấu hình DataGridView (Định dạng cột, giá tiền)
        private void SetupDataGridView()
        {
            dgvProducts.Columns.Clear();

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductId",
                HeaderText = "Mã SP",
                Width = 90
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ProductName",
                HeaderText = "Tên SP",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvProducts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Category",
                HeaderText = "Danh Mục",
                Width = 110
            });

            var colPrice = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "UnitPrice",
                HeaderText = "Đơn Giá",
                Width = 120
            };
            colPrice.DefaultCellStyle.Format = "N0"; // Định dạng tiền tệ có dấu phẩy
            colPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProducts.Columns.Add(colPrice);

            var colQty = new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Quantity",
                HeaderText = "Số Lượng",
                Width = 90
            };
            colQty.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvProducts.Columns.Add(colQty);

            // Cài đặt cho phép chọn nguyên dòng thay vì từng ô
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.MultiSelect = false;
            dgvProducts.ReadOnly = true;
        }

        // Chức năng: Chọn ảnh đại diện
        private void BtnChooseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh đại diện sản phẩm";
                ofd.Filter = "File ảnh (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|Tất cả tập tin (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picAvatar.ImageLocation = ofd.FileName;
                }
            }
        }

        // Kiểm tra tính hợp lệ của dữ liệu đầu vào
        private bool ValidateInput()
        {
            errorProvider.Clear();
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(txtProductName.Text) || txtProductName.Text == "Tên sản phẩm")
            {
                errorProvider.SetError(txtProductName, "Tên sản phẩm không được để trống!");
                isValid = false;
            }

            if (!decimal.TryParse(txtUnitPrice.Text, out decimal price) || price <= 0)
            {
                errorProvider.SetError(txtUnitPrice, "Đơn giá phải là số hợp lệ và > 0!");
                isValid = false;
            }

            if (!int.TryParse(txtQuantity.Text, out int qty) || qty < 0)
            {
                errorProvider.SetError(txtQuantity, "Số lượng phải là số nguyên >= 0!");
                isValid = false;
            }

            return isValid;
        }

        // Chức năng: Thêm mới sản phẩm
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            string id = string.IsNullOrWhiteSpace(txtProductId.Text) || txtProductId.Text == "Mã sản phẩm"
                ? "SP" + (_productList.Count + 1).ToString("D3")
                : txtProductId.Text.Trim();

            if (_productList.Any(p => p.ProductId.Equals(id, StringComparison.OrdinalIgnoreCase)))
            {
                errorProvider.SetError(txtProductId, "Mã sản phẩm đã tồn tại!");
                return;
            }

            var product = new Product
            {
                ProductId = id,
                ProductName = txtProductName.Text.Trim(),
                Category = cboCategory.SelectedItem?.ToString() ?? "Điện thoại",
                UnitPrice = Convert.ToDecimal(txtUnitPrice.Text),
                Quantity = Convert.ToInt32(txtQuantity.Text),
                ImagePath = picAvatar.ImageLocation
            };

            _productList.Add(product);
            ClearForm();
            UpdateStatus();
            MessageBox.Show("Thêm mới sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Chức năng: Click vào 1 dòng để đưa dữ liệu lên ô nhập liệu
        private void DgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProducts.CurrentRow != null)
            {
                var p = dgvProducts.CurrentRow.DataBoundItem as Product;
                if (p != null)
                {
                    txtProductId.Text = p.ProductId;
                    txtProductName.Text = p.ProductName;
                    cboCategory.SelectedItem = p.Category;
                    txtUnitPrice.Text = p.UnitPrice.ToString("G0");
                    txtQuantity.Text = p.Quantity.ToString();
                    picAvatar.ImageLocation = p.ImagePath;
                }
            }
        }

        // Chức năng: Cập nhật sản phẩm
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null || !(dgvProducts.CurrentRow.DataBoundItem is Product p))
            {
                MessageBox.Show("Vui lòng chọn 1 sản phẩm cần cập nhật từ bảng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput()) return;

            p.ProductId = txtProductId.Text.Trim();
            p.ProductName = txtProductName.Text.Trim();
            p.Category = cboCategory.SelectedItem?.ToString();
            p.UnitPrice = Convert.ToDecimal(txtUnitPrice.Text);
            p.Quantity = Convert.ToInt32(txtQuantity.Text);
            p.ImagePath = picAvatar.ImageLocation;

            _bindingSource.ResetBindings(false); // Cập nhật lại giao diện DataGridView
            ClearForm();
            MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Chức năng: Xóa sản phẩm
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.CurrentRow == null || !(dgvProducts.CurrentRow.DataBoundItem is Product p))
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dr = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa sản phẩm '{p.ProductName}' (Mã: {p.ProductId}) không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                _productList.Remove(p);
                ClearForm();
                UpdateStatus();
            }
        }

        // Chức năng: Tìm kiếm (theo thời gian thực khi gõ phím)
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            if (keyword == "tìm kiếm theo tên" || keyword == "tim kiem theo ten") keyword = "";

            if (string.IsNullOrEmpty(keyword))
            {
                _bindingSource.DataSource = _productList;
            }
            else
            {
                var filtered = _productList
                    .Where(p => p.ProductName != null && p.ProductName.ToLower().Contains(keyword))
                    .ToList();
                _bindingSource.DataSource = filtered;
            }
        }

        // Chức năng: Xuất file CSV
        private void MenuExportCsv_Click(object sender, EventArgs e)
        {
            if (_productList.Count == 0)
            {
                MessageBox.Show("Danh sách sản phẩm trống, không thể xuất file!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Title = "Xuất danh sách sản phẩm ra CSV";
                sfd.Filter = "CSV Files (*.csv)|*.csv";
                sfd.FileName = "DanhSachSanPham.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.UTF8))
                        {
                            sw.WriteLine("Mã SP,Tên SP,Danh Mục,Đơn Giá,Số Lượng");
                            foreach (var p in _productList)
                            {
                                sw.WriteLine($"\"{p.ProductId}\",\"{p.ProductName}\",\"{p.Category}\",{p.UnitPrice},{p.Quantity}");
                            }
                        }
                        MessageBox.Show("Xuất file CSV thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Cập nhật số lượng hiển thị dưới StatusStrip
        private void UpdateStatus()
        {
            if (lblStatusCount != null)
            {
                lblStatusCount.Text = $"Tổng số sản phẩm: {_productList.Count}";
            }
        }

        // Reset dữ liệu ở khung bên trái
        private void ClearForm()
        {
            txtProductId.Clear();
            txtProductName.Clear();
            txtUnitPrice.Clear();
            txtQuantity.Clear();
            picAvatar.ImageLocation = null;
            errorProvider.Clear();
            txtProductName.Focus();
        }
    }
}