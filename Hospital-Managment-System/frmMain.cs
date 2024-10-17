using HMS_BusinessLayer;
using HMS_DataBusinessLayer;
using Hospital_Managment_System.Appointment;
using Hospital_Managment_System.Empolyee;
using Hospital_Managment_System.Empolyee.Doctor;
using Hospital_Managment_System.Empolyee.Users;
using Hospital_Managment_System.Global;
using Hospital_Managment_System.Settings;
using System;
using System.Windows.Forms;

namespace Hospital_Managment_System
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        struct stChildForms
        {
            // this forms for can be navigated without having to reload the page.
            public frmListUsers frmListUsers { get; set; }
            public frmAllEmployeesList frmAllEmployeesList { get; set; }
            public frmDoctorsList frmDoctorsList { get; set; }
            public frmAppointmentList frmAppointmentList { get; set; }
            public frmSettings frmSettings { get; set; }
        }
        private stChildForms Forms = new stChildForms();

        private Form activeForm;
        private Form activeSubForm;

        public void OpenChailedForm(Form ChildForm)
        {
            try
            {
                if (activeForm != null)
                {
                    activeForm.Close();

                    if (activeSubForm != null)
                        activeSubForm.Close();
                }

                //ChangeThameColor(ThameColor);
                activeForm = ChildForm;
                ChildForm.TopLevel = false;
                ChildForm.FormBorderStyle = FormBorderStyle.None;
                ChildForm.Dock = DockStyle.Fill;
                this.panelForms.Controls.Add(ChildForm);
                this.panelForms.Tag = ChildForm;
                ChildForm.BringToFront();
                ChildForm.Show();
                //lblTitle.Text = ChildForm.Text;
                //ImageTitle(imageTitle);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void OpenChildSubForm(Form ChildSubForm)
        {
            try
            {
                if (activeSubForm != null)
                {
                    //activeForm.Close();
                    activeSubForm.Hide();
                }

                //ChangeThameColor(ThameColor);
                activeSubForm = ChildSubForm;
                ChildSubForm.TopLevel = false;
                ChildSubForm.FormBorderStyle = FormBorderStyle.None;
                ChildSubForm.Dock = DockStyle.Fill;
                this.panelForms.Controls.Add(ChildSubForm);
                this.panelForms.Tag = ChildSubForm;
                ChildSubForm.BringToFront();
                ChildSubForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string HospitalID = null;

           if (!clsUtil.GetStoredHospitalID(ref HospitalID))
           {
               frmAddUpdateHospital updateHospital = new frmAddUpdateHospital();
               updateHospital.ShowDialog();
           }
           else
           {
                int Ho = int.Parse(HospitalID.Trim());
                clsGlobal.CurrentHospital = clsHospitalInfo.FindByID(Ho);

           }
        }

        private void btnSignOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            Forms.frmAllEmployeesList = new frmAllEmployeesList(this);

            OpenChailedForm(Forms.frmAllEmployeesList);
        }

        private void btnDoctors_Click(object sender, EventArgs e)
        {
            Forms.frmDoctorsList = new frmDoctorsList(this);

            OpenChailedForm(Forms.frmDoctorsList);
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            Forms.frmListUsers = new frmListUsers(this);

            OpenChailedForm(Forms.frmListUsers);
        }

        private void btnAppointments_Click(object sender, EventArgs e)
        {
            Forms.frmAppointmentList = new frmAppointmentList(this);

            OpenChailedForm(Forms.frmAppointmentList);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            Forms.frmSettings = new frmSettings(this);

            OpenChailedForm(Forms.frmSettings);
        }
    }

}