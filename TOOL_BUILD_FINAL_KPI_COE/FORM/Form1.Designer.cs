namespace TOOL_BUILD_FINAL_KPI_COE
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbn_toan_trinh = new System.Windows.Forms.Button();
            this.btn_updateApp = new System.Windows.Forms.Button();
            this.btb_autoSaveStatus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(315, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "TOOL BUILD FINAL COE";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tbn_toan_trinh
            // 
            this.tbn_toan_trinh.Location = new System.Drawing.Point(29, 122);
            this.tbn_toan_trinh.Name = "tbn_toan_trinh";
            this.tbn_toan_trinh.Size = new System.Drawing.Size(135, 39);
            this.tbn_toan_trinh.TabIndex = 2;
            this.tbn_toan_trinh.Text = "Toàn trình";
            this.tbn_toan_trinh.UseVisualStyleBackColor = true;
            this.tbn_toan_trinh.Click += new System.EventHandler(this.tbn_toan_trinh_Click);
            // 
            // btn_updateApp
            // 
            this.btn_updateApp.Location = new System.Drawing.Point(12, 12);
            this.btn_updateApp.Name = "btn_updateApp";
            this.btn_updateApp.Size = new System.Drawing.Size(111, 23);
            this.btn_updateApp.TabIndex = 3;
            this.btn_updateApp.Text = "Cập nhật app";
            this.btn_updateApp.UseVisualStyleBackColor = true;
            this.btn_updateApp.Click += new System.EventHandler(this.btn_updateApp_Click);
            // 
            // btb_autoSaveStatus
            // 
            this.btb_autoSaveStatus.Location = new System.Drawing.Point(29, 183);
            this.btb_autoSaveStatus.Name = "btb_autoSaveStatus";
            this.btb_autoSaveStatus.Size = new System.Drawing.Size(135, 56);
            this.btb_autoSaveStatus.TabIndex = 4;
            this.btb_autoSaveStatus.Text = "-- ĐẨY LẺ -- \r\nUP LẠI VIEW\r\n";
            this.btb_autoSaveStatus.UseVisualStyleBackColor = true;
            this.btb_autoSaveStatus.Click += new System.EventHandler(this.btb_autoSaveStatus_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 642);
            this.Controls.Add(this.btb_autoSaveStatus);
            this.Controls.Add(this.btn_updateApp);
            this.Controls.Add(this.tbn_toan_trinh);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "VanCuLaOK";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button tbn_toan_trinh;
        private System.Windows.Forms.Button btn_updateApp;
        private System.Windows.Forms.Button btb_autoSaveStatus;
    }
}

