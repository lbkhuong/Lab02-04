using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab02_04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void ClearTextBox()
        {
            txtSoTK.Text = "";
            txtTenKH.Text = "";
            txtDiaChi.Text = "";
            txtSoTien.Text = "";
        }

        private void btnThemSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSoTK.Text == "" || txtTenKH.Text == "" || txtSoTien.Text == "" || txtDiaChi.Text == "")
                {
                    throw new Exception("Bạn Chưa Nhập Đủ Thông Tin !!");
                }
                int selectedRow = GetSelectedRow(txtSoTK.Text);

                if (selectedRow == -1)
                {
                    AddItem();
                    MessageBox.Show("Thêm Khách Hàng Mới Thành Công!!", "Thông Báo", MessageBoxButtons.OK);

                }
                else
                {
                    UpdateItem(selectedRow);
                    MessageBox.Show("Cập Nhật Thông Tin Khách Hàng Thành Công!!", "Thông Báo", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private int GetSelectedRow(string soTK)
        {
            for (int i = 0; i < listView.Items.Count; i++)
            {
                if (listView.Items[i].SubItems[1].Text != null)
                {
                    if (listView.Items[i].SubItems[1].Text == soTK)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
        private void AddItem()
        {
            int i = listView.Items.Count;
            listView.Items.Add((i + 1).ToString());
            listView.Items[i].SubItems.Add(txtSoTK.Text);
            listView.Items[i].SubItems.Add(txtTenKH.Text);
            listView.Items[i].SubItems.Add(txtDiaChi.Text);
            listView.Items[i].SubItems.Add(txtSoTien.Text);
            ClearTextBox();
            long tongTien = long.Parse(txtTongTien.Text);
            tongTien += long.Parse(listView.Items[i].SubItems[4].Text);
            txtTongTien.Text = tongTien.ToString();
        }
        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            try
            {
                int selectedRow = GetSelectedRow(txtSoTK.Text);
                if (selectedRow == -1)
                {
                    throw new Exception("Không Tìm Thấy Thông Tin Khách Hàng Cần Xóa!");
                }
                else
                {
                    DialogResult dialog = MessageBox.Show("Bạn Có Muốn Xóa ?", "Yes/No", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        long updateTongTien = long.Parse(txtTongTien.Text);
                        updateTongTien -= long.Parse(listView.Items[selectedRow].SubItems[4].Text);
                        txtTongTien.Text = updateTongTien.ToString();
                        listView.Items.RemoveAt(selectedRow);
                        MessageBox.Show("Xóa Khách Hàng Thành Công!", "Thông Báo", MessageBoxButtons.OK);
                    }
                }
                ClearTextBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateItem(int selectedRow)
        {
            long updateTongTien = long.Parse(txtTongTien.Text);
            listView.Items.Insert(selectedRow, (selectedRow + 1).ToString());
            listView.Items[selectedRow].SubItems.Add(txtSoTK.Text);
            listView.Items[selectedRow].SubItems.Add(txtTenKH.Text);
            listView.Items[selectedRow].SubItems.Add(txtDiaChi.Text);
            listView.Items[selectedRow].SubItems.Add(txtSoTien.Text);
            updateTongTien += long.Parse(listView.Items[selectedRow].SubItems[4].Text);
            updateTongTien -= long.Parse(listView.Items[selectedRow + 1].SubItems[4].Text);
            txtTongTien.Text = updateTongTien.ToString();
            listView.Items.RemoveAt(selectedRow + 1);
            ClearTextBox();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listView.GridLines = true;
            listView.FullRowSelect = true;
        }
        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
                txtSoTK.Text = e.Item.SubItems[1].Text;
                txtTenKH.Text = e.Item.SubItems[2].Text;
                txtDiaChi.Text = e.Item.SubItems[3].Text;
                txtSoTien.Text = e.Item.SubItems[4].Text;
        }
    }
}
