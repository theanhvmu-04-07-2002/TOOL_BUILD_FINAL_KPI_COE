namespace TOOL_BUILD_FINAL_KPI_COE.FORM.AutoSaveTrangThai
{
    partial class AutoSaveStatus
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
            this.btnChonFile = new System.Windows.Forms.Button();
            this.btnBatDau = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnXoaQuyTac = new System.Windows.Forms.Button();
            this.rdbXoaGanDung = new System.Windows.Forms.RadioButton();
            this.rdbXoaChinhXac = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnInsertDL = new System.Windows.Forms.Button();
            this.btnDele_SaveStatus_InsertDL = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdbElementMenuA = new System.Windows.Forms.RadioButton();
            this.rdbElementMenuDiv = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPasswordLogin = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUsernameLogin = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ckbBoQuaXoaThamSo = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(7, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(504, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "TOOL DELETE QUY TẮC + SAVE TRẠNG THÁI + INSERT DL";
            // 
            // btnChonFile
            // 
            this.btnChonFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnChonFile.Location = new System.Drawing.Point(26, 173);
            this.btnChonFile.Name = "btnChonFile";
            this.btnChonFile.Size = new System.Drawing.Size(172, 33);
            this.btnChonFile.TabIndex = 1;
            this.btnChonFile.Text = "Chọn file excel";
            this.btnChonFile.UseVisualStyleBackColor = true;
            this.btnChonFile.Click += new System.EventHandler(this.btnChonFile_Click);
            // 
            // btnBatDau
            // 
            this.btnBatDau.ForeColor = System.Drawing.Color.Green;
            this.btnBatDau.Location = new System.Drawing.Point(26, 324);
            this.btnBatDau.Name = "btnBatDau";
            this.btnBatDau.Size = new System.Drawing.Size(172, 33);
            this.btnBatDau.TabIndex = 2;
            this.btnBatDau.Text = "Bắt đầu lưu trạng thái";
            this.btnBatDau.UseVisualStyleBackColor = true;
            this.btnBatDau.Click += new System.EventHandler(this.btnBatDau_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(221, 59);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(334, 403);
            this.txtLog.TabIndex = 3;
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            // 
            // btnXoaQuyTac
            // 
            this.btnXoaQuyTac.Location = new System.Drawing.Point(5, 66);
            this.btnXoaQuyTac.Name = "btnXoaQuyTac";
            this.btnXoaQuyTac.Size = new System.Drawing.Size(158, 33);
            this.btnXoaQuyTac.TabIndex = 4;
            this.btnXoaQuyTac.Text = "Bắt đầu xóa quy tắc";
            this.btnXoaQuyTac.UseVisualStyleBackColor = true;
            this.btnXoaQuyTac.Click += new System.EventHandler(this.btnXoaQuyTac_Click);
            // 
            // rdbXoaGanDung
            // 
            this.rdbXoaGanDung.AutoSize = true;
            this.rdbXoaGanDung.Location = new System.Drawing.Point(5, 19);
            this.rdbXoaGanDung.Name = "rdbXoaGanDung";
            this.rdbXoaGanDung.Size = new System.Drawing.Size(140, 17);
            this.rdbXoaGanDung.TabIndex = 5;
            this.rdbXoaGanDung.TabStop = true;
            this.rdbXoaGanDung.Text = "Xóa hàng loạt gần đúng";
            this.rdbXoaGanDung.UseVisualStyleBackColor = true;
            // 
            // rdbXoaChinhXac
            // 
            this.rdbXoaChinhXac.AutoSize = true;
            this.rdbXoaChinhXac.Location = new System.Drawing.Point(5, 42);
            this.rdbXoaChinhXac.Name = "rdbXoaChinhXac";
            this.rdbXoaChinhXac.Size = new System.Drawing.Size(139, 17);
            this.rdbXoaChinhXac.TabIndex = 6;
            this.rdbXoaChinhXac.TabStop = true;
            this.rdbXoaChinhXac.Text = "Xóa chính xác duy nhất";
            this.rdbXoaChinhXac.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnXoaQuyTac);
            this.groupBox1.Controls.Add(this.rdbXoaGanDung);
            this.groupBox1.Controls.Add(this.rdbXoaChinhXac);
            this.groupBox1.ForeColor = System.Drawing.Color.Purple;
            this.groupBox1.Location = new System.Drawing.Point(26, 212);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(171, 106);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cấu hình kiểu xóa";
            // 
            // btnInsertDL
            // 
            this.btnInsertDL.ForeColor = System.Drawing.Color.Teal;
            this.btnInsertDL.Location = new System.Drawing.Point(26, 364);
            this.btnInsertDL.Name = "btnInsertDL";
            this.btnInsertDL.Size = new System.Drawing.Size(172, 33);
            this.btnInsertDL.TabIndex = 9;
            this.btnInsertDL.Text = "Bắt đầu Insert DL";
            this.btnInsertDL.UseVisualStyleBackColor = true;
            this.btnInsertDL.Click += new System.EventHandler(this.btnInsertDL_Click);
            // 
            // btnDele_SaveStatus_InsertDL
            // 
            this.btnDele_SaveStatus_InsertDL.ForeColor = System.Drawing.Color.Navy;
            this.btnDele_SaveStatus_InsertDL.Location = new System.Drawing.Point(26, 424);
            this.btnDele_SaveStatus_InsertDL.Name = "btnDele_SaveStatus_InsertDL";
            this.btnDele_SaveStatus_InsertDL.Size = new System.Drawing.Size(172, 33);
            this.btnDele_SaveStatus_InsertDL.TabIndex = 10;
            this.btnDele_SaveStatus_InsertDL.Text = "Xóa + Lưu + Insert";
            this.btnDele_SaveStatus_InsertDL.UseVisualStyleBackColor = true;
            this.btnDele_SaveStatus_InsertDL.Click += new System.EventHandler(this.btnDele_SaveStatus_InsertDL_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdbElementMenuA);
            this.groupBox2.Controls.Add(this.rdbElementMenuDiv);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtPasswordLogin);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtUsernameLogin);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(4, 59);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(194, 99);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cấu hình login";
            // 
            // rdbElementMenuA
            // 
            this.rdbElementMenuA.AutoSize = true;
            this.rdbElementMenuA.Location = new System.Drawing.Point(116, 74);
            this.rdbElementMenuA.Margin = new System.Windows.Forms.Padding(2);
            this.rdbElementMenuA.Name = "rdbElementMenuA";
            this.rdbElementMenuA.Size = new System.Drawing.Size(31, 17);
            this.rdbElementMenuA.TabIndex = 6;
            this.rdbElementMenuA.TabStop = true;
            this.rdbElementMenuA.Text = "a";
            this.rdbElementMenuA.UseVisualStyleBackColor = true;
            // 
            // rdbElementMenuDiv
            // 
            this.rdbElementMenuDiv.AutoSize = true;
            this.rdbElementMenuDiv.Location = new System.Drawing.Point(65, 74);
            this.rdbElementMenuDiv.Margin = new System.Windows.Forms.Padding(2);
            this.rdbElementMenuDiv.Name = "rdbElementMenuDiv";
            this.rdbElementMenuDiv.Size = new System.Drawing.Size(39, 17);
            this.rdbElementMenuDiv.TabIndex = 5;
            this.rdbElementMenuDiv.TabStop = true;
            this.rdbElementMenuDiv.Text = "div";
            this.rdbElementMenuDiv.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 76);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(28, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "thẻ :";
            // 
            // txtPasswordLogin
            // 
            this.txtPasswordLogin.Location = new System.Drawing.Point(65, 46);
            this.txtPasswordLogin.Margin = new System.Windows.Forms.Padding(2);
            this.txtPasswordLogin.Name = "txtPasswordLogin";
            this.txtPasswordLogin.Size = new System.Drawing.Size(116, 20);
            this.txtPasswordLogin.TabIndex = 3;
            this.txtPasswordLogin.TextChanged += new System.EventHandler(this.txtPasswordLogin_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 51);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "password :";
            // 
            // txtUsernameLogin
            // 
            this.txtUsernameLogin.Location = new System.Drawing.Point(65, 23);
            this.txtUsernameLogin.Margin = new System.Windows.Forms.Padding(2);
            this.txtUsernameLogin.Name = "txtUsernameLogin";
            this.txtUsernameLogin.Size = new System.Drawing.Size(116, 20);
            this.txtUsernameLogin.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 28);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "username :";
            // 
            // ckbBoQuaXoaThamSo
            // 
            this.ckbBoQuaXoaThamSo.AutoSize = true;
            this.ckbBoQuaXoaThamSo.Location = new System.Drawing.Point(26, 465);
            this.ckbBoQuaXoaThamSo.Margin = new System.Windows.Forms.Padding(2);
            this.ckbBoQuaXoaThamSo.Name = "ckbBoQuaXoaThamSo";
            this.ckbBoQuaXoaThamSo.Size = new System.Drawing.Size(120, 17);
            this.ckbBoQuaXoaThamSo.TabIndex = 12;
            this.ckbBoQuaXoaThamSo.Text = "Bỏ qua xóa tham số";
            this.ckbBoQuaXoaThamSo.UseVisualStyleBackColor = true;
            this.ckbBoQuaXoaThamSo.CheckedChanged += new System.EventHandler(this.ckbBoQuaXoaThamSo_CheckedChanged);
            // 
            // AutoSaveStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 499);
            this.Controls.Add(this.ckbBoQuaXoaThamSo);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnDele_SaveStatus_InsertDL);
            this.Controls.Add(this.btnInsertDL);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnChonFile);
            this.Controls.Add(this.btnBatDau);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.label1);
            this.Name = "AutoSaveStatus";
            this.Text = "TAB CHA";
            this.Load += new System.EventHandler(this.AutoSaveStatus_Load_2);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnChonFile;
        private System.Windows.Forms.Button btnBatDau;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnXoaQuyTac;
        private System.Windows.Forms.RadioButton rdbXoaGanDung;
        private System.Windows.Forms.RadioButton rdbXoaChinhXac;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnInsertDL;
        private System.Windows.Forms.Button btnDele_SaveStatus_InsertDL;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPasswordLogin;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUsernameLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rdbElementMenuDiv;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton rdbElementMenuA;
        private System.Windows.Forms.CheckBox ckbBoQuaXoaThamSo;
    }
}