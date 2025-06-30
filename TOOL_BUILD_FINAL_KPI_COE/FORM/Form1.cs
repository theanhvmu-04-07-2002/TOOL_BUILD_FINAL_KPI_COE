using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TOOL_BUILD_FINAL_KPI_COE.FORM.AutoSaveTrangThai;
using TOOL_BUILD_FINAL_KPI_COE.FORM.DayToanTrinh;

namespace TOOL_BUILD_FINAL_KPI_COE
{
    public partial class Form1 : BaseForm
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tbn_toan_trinh_Click(object sender, EventArgs e)
        {
            ToanTrinh frm = new ToanTrinh();
            frm.Show();
        }

        private void btn_updateApp_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/c git pull";
                process.StartInfo.WorkingDirectory = Application.StartupPath;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                string logMessage = $"[THỜI GIAN]: {DateTime.Now}\nKẾT QUẢ:\n-----------------\n{output}\n\nLỖI:\n-----------------\n{error}";

                // Ghi log đầy đủ ra file
                try
                {
                    string logPath = Path.Combine(Application.StartupPath, "update_log.txt");
                    File.AppendAllText(logPath, logMessage + "\n\n");
                }
                catch (Exception exLog)
                {
                    MessageBox.Show("Không ghi được log:\n" + exLog.Message, "Lỗi ghi log", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                // Thông báo rõ ràng cho người dùng
                if (!string.IsNullOrWhiteSpace(error))
                {
                    MessageBox.Show("Đã xảy ra lỗi khi cập nhật:\n" + error, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Cập nhật thành công!\n" + output, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btb_autoSaveStatus_Click(object sender, EventArgs e)
        {
            AutoSaveStatus frm = new AutoSaveStatus();
            frm.Show();
        }
    }
}
