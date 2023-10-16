using LAB04.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// trieu o day
// hoang da o day
namespace LAB04
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private List<Faculty> listFalcultys = new List<Faculty>();
        private void lblQLSV_Click(object sender, EventArgs e)
        {

        }

        private void txtStudentID_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                StudentDBContext context = new StudentDBContext();
                List<Faculty> listFalcultys = context.Faculties.ToList(); //lấy các khoa
                List<Student> listStudent = context.Students.ToList(); //lấy sinh viên
                FillFalcultyCombobox(listFalcultys);
                BindGrid(listStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void BindGrid(List<Student> listStudent)
        {
            dgvStudent.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dgvStudent.Rows.Add();
                dgvStudent.Rows[index].Cells[0].Value = item.StudentID;
                dgvStudent.Rows[index].Cells[1].Value = item.FullName;
                dgvStudent.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dgvStudent.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }
        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            if (rowIndex >= 0)
            {
                txtStudentID.Text = dgvStudent.Rows[rowIndex].Cells[0].Value.ToString();
                txtFullName.Text = dgvStudent.Rows[rowIndex].Cells[1].Value.ToString();
                txtAverageScore.Text = dgvStudent.Rows[rowIndex].Cells[3].Value.ToString();
                //  var khoa = listFalcultys.Single(c => c.Equals(dgvStudent.Rows[rowIndex].Cells[3].Value.ToString()));


                // cmbFaculty.SelectedItem = khoa;

                cmbFaculty.Text = dgvStudent.Rows[rowIndex].Cells[2].Value.ToString();
            }
        }
        private void FillFalcultyCombobox(List<Faculty> listFalcultys)
        {
            this.cmbFaculty.DataSource = listFalcultys;
            this.cmbFaculty.DisplayMember = "FacultyName";
            this.cmbFaculty.ValueMember = "FacultyID";
        }
        private void cmbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {   
            
            try
            { 
            if(txtStudentID.TextLength < 10 || txtStudentID.TextLength > 10)
                MessageBox.Show("MSSV phải đủ 10 ký tự !! ");
            double diemtb = double.Parse(txtAverageScore.Text);
            if (diemtb > 10)
                {
                    MessageBox.Show("Điểm phải trong khoảng từ 0 - > 10 nhé ~~~ ");
                    return;
                }
            StudentDBContext db = new StudentDBContext();
            var mssv = txtStudentID.Text;
            var ten = txtFullName.Text;
            var diem = txtAverageScore.Text;
            var khoa = (int)cmbFaculty.SelectedValue;

            Student s = new Student()
            {
                StudentID = mssv,
                FullName = ten,
                AverageScore = double.Parse(diem),
                FacultyID = khoa

            };
            db.Students.Add(s);
            db.SaveChanges();
            BindGrid(db.Students.ToList());
            MessageBox.Show("Thêm thành công ");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (txtStudentID.TextLength < 10 || txtStudentID.TextLength > 10)
                MessageBox.Show("MSSV phải đủ 10 ký tự !! ");
            double diemtb = double.Parse(txtAverageScore.Text);
            if (diemtb > 10)
            {
                MessageBox.Show("Điểm phải trong khoảng từ 0 - > 10 nhé ~~~ ");
                return;
            }
            if (txtStudentID.Text == "")
            {
                MessageBox.Show("MSSV không được để trống!");
                return;
            }
            StudentDBContext db = new StudentDBContext();
            var updateStudent = db.Students.SingleOrDefault(c => c.StudentID.Equals(txtStudentID.Text));
            if (updateStudent == null)
            {
                MessageBox.Show("Không tồn tại sinh viên có MSSV {0}", txtStudentID.Text);
                return;
            }
            updateStudent.FullName = txtFullName.Text;
            updateStudent.AverageScore = double.Parse(txtAverageScore.Text);
            updateStudent.FacultyID = (int)cmbFaculty.SelectedValue;

            db.SaveChanges();
            BindGrid(db.Students.ToList());
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtStudentID.Text == "")
            {
                MessageBox.Show("MSSV không được để trống!");
                return;
            }
            StudentDBContext db = new StudentDBContext();
            string ID = txtStudentID.Text;
            Student dbDelete = db.Students.FirstOrDefault(p => p.StudentID == ID);
            if (dbDelete != null)
            {
                db.Students.Remove(dbDelete);
                db.SaveChanges();
                BindGrid(db.Students.ToList());
            }
            else
            {
                MessageBox.Show("Không có sinh viên nào mang MSSV này!");
            }
        }

        private void txtAverageScore_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAverageScore_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtStudentID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
