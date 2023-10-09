using LAB04.Model;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB04
{
    public partial class fReport : Form
    {
        public fReport()
        {
            InitializeComponent();
        }

        private void fReport_Load(object sender, EventArgs e)
        {
            StudentDBContext
            context = new StudentDBContext();
            List<Student> listStudent = context.Students.ToList();
            List<StudentReport> listReport = new List<StudentReport>();
            foreach (Student i in listStudent)
            {
                StudentReport temp = new StudentReport();
                temp.StudentID = i.StudentID;
                temp.FullName = i.FullName;
                temp.AverageScore = i.AverageScore;
                temp.FacultyName = i.Faculty.FacultyName;
                listReport.Add(temp);
            }
            this.reportViewer1.LocalReport.ReportPath = "rptStudentReport.rdlc";
            var reportDataSource = new ReportDataSource("StudentDataSet", listReport);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            this.reportViewer1.RefreshReport();

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}

