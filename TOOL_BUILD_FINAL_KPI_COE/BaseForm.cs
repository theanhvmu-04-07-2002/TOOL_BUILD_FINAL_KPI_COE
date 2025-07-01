using System;
using System.Drawing;
using System.Windows.Forms;

namespace TOOL_BUILD_FINAL_KPI_COE
{
    public class BaseForm : Form
    {
        public BaseForm()
        {
            this.Font = new Font("Microsoft Sans Serif", 8);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "BaseForm";
            this.Load += new System.EventHandler(this.BaseForm_Load);
            this.ResumeLayout(false);

        }

        private void BaseForm_Load(object sender, EventArgs e)
        {

        }
    }
}
