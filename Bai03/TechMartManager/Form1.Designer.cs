namespace TechMartManager
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            menuExportCsv = new ToolStripMenuItem();
            menuExit = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            lblStatusCount = new ToolStripStatusLabel();
            tblMainLayout = new TableLayoutPanel();
            pnlInput = new Panel();
            txtProductId = new TextBox();
            txtProductName = new TextBox();
            cboCategory = new ComboBox();
            txtUnitPrice = new TextBox();
            txtQuantity = new TextBox();
            picAvatar = new PictureBox();
            label1 = new Label();
            btnChooseImage = new Button();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            txtSearch = new TextBox();
            errorProvider = new ErrorProvider(components);
            dgvProducts = new DataGridView();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            tblMainLayout.SuspendLayout();
            pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(982, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuExportCsv, menuExit });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.X;
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // menuExportCsv
            // 
            menuExportCsv.Name = "menuExportCsv";
            menuExportCsv.ShortcutKeys = Keys.Control | Keys.E;
            menuExportCsv.Size = new Size(224, 26);
            menuExportCsv.Text = "Export CSV";
            // 
            // menuExit
            // 
            menuExit.Name = "menuExit";
            menuExit.Size = new Size(224, 26);
            menuExit.Text = "Exit";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { lblStatusCount });
            statusStrip1.Location = new Point(0, 527);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(982, 26);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // lblStatusCount
            // 
            lblStatusCount.Name = "lblStatusCount";
            lblStatusCount.Size = new Size(145, 20);
            lblStatusCount.Text = "Tổng số sản phẩm: 0";
            // 
            // tblMainLayout
            // 
            tblMainLayout.ColumnCount = 2;
            tblMainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            tblMainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            tblMainLayout.Controls.Add(pnlInput, 0, 0);
            tblMainLayout.Controls.Add(dgvProducts, 1, 0);
            tblMainLayout.Dock = DockStyle.Fill;
            tblMainLayout.Location = new Point(0, 28);
            tblMainLayout.Name = "tblMainLayout";
            tblMainLayout.RowCount = 1;
            tblMainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblMainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tblMainLayout.Size = new Size(982, 499);
            tblMainLayout.TabIndex = 2;
            // 
            // pnlInput
            // 
            pnlInput.Controls.Add(txtSearch);
            pnlInput.Controls.Add(btnDelete);
            pnlInput.Controls.Add(btnUpdate);
            pnlInput.Controls.Add(btnAdd);
            pnlInput.Controls.Add(btnChooseImage);
            pnlInput.Controls.Add(label1);
            pnlInput.Controls.Add(picAvatar);
            pnlInput.Controls.Add(txtQuantity);
            pnlInput.Controls.Add(txtUnitPrice);
            pnlInput.Controls.Add(cboCategory);
            pnlInput.Controls.Add(txtProductName);
            pnlInput.Controls.Add(txtProductId);
            pnlInput.Dock = DockStyle.Fill;
            pnlInput.Location = new Point(3, 3);
            pnlInput.Name = "pnlInput";
            pnlInput.Size = new Size(337, 493);
            pnlInput.TabIndex = 0;
            // 
            // txtProductId
            // 
            txtProductId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtProductId.Location = new Point(0, 3);
            txtProductId.Name = "txtProductId";
            txtProductId.Size = new Size(125, 27);
            txtProductId.TabIndex = 0;
            txtProductId.Text = "Mã sản phẩm";
            // 
            // txtProductName
            // 
            txtProductName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtProductName.Location = new Point(-3, 36);
            txtProductName.Name = "txtProductName";
            txtProductName.Size = new Size(125, 27);
            txtProductName.TabIndex = 1;
            txtProductName.Text = "Tên sản phẩm";
            // 
            // cboCategory
            // 
            cboCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(-3, 69);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(296, 28);
            cboCategory.TabIndex = 2;
            cboCategory.Text = "Danh mục (Điện thoại, Laptop, Phụ kiện)";
            // 
            // txtUnitPrice
            // 
            txtUnitPrice.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtUnitPrice.Location = new Point(0, 103);
            txtUnitPrice.Name = "txtUnitPrice";
            txtUnitPrice.Size = new Size(125, 27);
            txtUnitPrice.TabIndex = 3;
            txtUnitPrice.Text = "Đơn giá";
            // 
            // txtQuantity
            // 
            txtQuantity.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtQuantity.Location = new Point(0, 136);
            txtQuantity.Name = "txtQuantity";
            txtQuantity.Size = new Size(125, 27);
            txtQuantity.TabIndex = 4;
            txtQuantity.Text = "Số lượng";
            // 
            // picAvatar
            // 
            picAvatar.BorderStyle = BorderStyle.FixedSingle;
            picAvatar.Location = new Point(0, 169);
            picAvatar.Name = "picAvatar";
            picAvatar.Size = new Size(179, 62);
            picAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            picAvatar.TabIndex = 5;
            picAvatar.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 184);
            label1.Name = "label1";
            label1.Size = new Size(161, 20);
            label1.TabIndex = 6;
            label1.Text = "Ảnh đại diện sản phẩm";
            // 
            // btnChooseImage
            // 
            btnChooseImage.Location = new Point(0, 237);
            btnChooseImage.Name = "btnChooseImage";
            btnChooseImage.Size = new Size(94, 29);
            btnChooseImage.TabIndex = 7;
            btnChooseImage.Text = "Chọn ảnh";
            btnChooseImage.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(0, 269);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(94, 29);
            btnAdd.TabIndex = 8;
            btnAdd.Text = "Thêm mới";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnUpdate
            // 
            btnUpdate.Location = new Point(0, 304);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(94, 29);
            btnUpdate.TabIndex = 9;
            btnUpdate.Text = "Cập nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            btnDelete.Location = new Point(0, 339);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(94, 29);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "Xoá";
            btnDelete.UseVisualStyleBackColor = true;
            
            // 
            // txtSearch
            // 
            txtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSearch.Location = new Point(3, 374);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(134, 27);
            txtSearch.TabIndex = 11;
            txtSearch.Text = "Tìm kiếm theo tên";
            // 
            // errorProvider
            // 
            errorProvider.ContainerControl = this;
            // 
            // dgvProducts
            // 
            dgvProducts.AllowUserToAddRows = false;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Dock = DockStyle.Fill;
            dgvProducts.Location = new Point(346, 3);
            dgvProducts.MultiSelect = false;
            dgvProducts.Name = "dgvProducts";
            dgvProducts.ReadOnly = true;
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.Size = new Size(633, 493);
            dgvProducts.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(982, 553);
            Controls.Add(tblMainLayout);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "TechMart Product Manager";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            tblMainLayout.ResumeLayout(false);
            pnlInput.ResumeLayout(false);
            pnlInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAvatar).EndInit();
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem menuExportCsv;
        private ToolStripMenuItem menuExit;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblStatusCount;
        private TableLayoutPanel tblMainLayout;
        private Panel pnlInput;
        private TextBox txtProductId;
        private PictureBox picAvatar;
        private TextBox txtQuantity;
        private TextBox txtUnitPrice;
        private ComboBox cboCategory;
        private TextBox txtProductName;
        private Button btnChooseImage;
        private Label label1;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private TextBox txtSearch;
        private DataGridView dgvProducts;
        private ErrorProvider errorProvider;
    }
}
