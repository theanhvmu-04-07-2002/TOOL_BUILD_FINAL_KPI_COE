
using OfficeOpenXml;
// Chrome & Selenium
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TOOL_BUILD_FINAL_KPI_COE.MODULE;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TOOL_BUILD_FINAL_KPI_COE.FORM.AutoSaveTrangThai
{
    public partial class AutoSaveStatus : BaseForm
    {
        public AutoSaveStatus()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private readonly string logFolder = @"D:\datalake_data_point\tool\Tool_COE_KPI\TOOL_BUILD_FINAL_KPI_COE\TOOL_BUILD_FINAL_KPI_COE\bin\Release\log_error";
        
        private AutoSaveTrangThaiTinhToan autoSaveTrangThaiTinhToan;
        private AutoSaveInsertDl moduleInsertDL;
        private DangNhap moduleDangNhap;



        private void log(string message)
        {
            // Phần ghi log vào file 
            //try
            //{
            //    if (!Directory.Exists(logFolder))
            //        Directory.CreateDirectory(logFolder);

            //    string logPath = Path.Combine(logFolder, "log.txt");
            //    File.AppendAllText(logPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}");
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine($"Lỗi khi ghi log ra file: {ex.Message}");
            //}

            // Phần ghi log vào TextBox 
            try
            {
                if (txtLog == null || txtLog.IsDisposed) return;

                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke((MethodInvoker)(() =>
                    {
                        txtLog.AppendText(message + Environment.NewLine);
                        txtLog.SelectionStart = txtLog.Text.Length;
                        txtLog.ScrollToCaret();
                    }));
                }
                else
                {
                    txtLog.AppendText(message + Environment.NewLine);
                    txtLog.SelectionStart = txtLog.Text.Length;
                    txtLog.ScrollToCaret();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi khi ghi log ra TextBox: {ex.Message}");
            }
        }

        private void AutoSaveStatus_Load(object sender, EventArgs e)
        {
            MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory, "Thư mục thực thi app");
        }








        private List<string> ListViewAutoSaveTrangThai = new List<string>();
        private List<string> ListViewAutoDeleThamSo = new List<string>();
        private List<string> ListViewAutoInsertDL = new List<string>();

        private void btnChonFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Excel files|*.xlsx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ListViewAutoSaveTrangThai.Clear();
                    ListViewAutoDeleThamSo.Clear();
                    ListViewAutoInsertDL.Clear();

                    log("--------------------- -------- OKELA ----------- ------------------------");

                    using (var package = new ExcelPackage(new FileInfo(dlg.FileName)))
                    {
                        // Đọc sheet ViewTrangThai
                        var wsTrangThai = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "ViewTrangThai");
                        if (wsTrangThai != null)
                        {
                            int rowCount = wsTrangThai.Dimension.End.Row;
                            for (int row = 2; row <= rowCount; row++) // tru dong tieu de
                            {
                                string giaTri = wsTrangThai.Cells[row, 1].Text.Trim();
                                if (!string.IsNullOrEmpty(giaTri))
                                {
                                    ListViewAutoSaveTrangThai.Add(giaTri);
                                }
                            }
                            log($"✅ Đã đọc {ListViewAutoSaveTrangThai.Count} dòng từ sheet ViewTrangThai.");
                        }
                        else
                        {
                            log("⚠️ Không tìm thấy sheet ViewTrangThai.");
                        }

                        // Đọc sheet ViewThamSo
                        var wsThamSo = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "ViewThamSo");
                        if (wsThamSo != null)
                        {

                            int rowCount = wsThamSo.Dimension.End.Row;
                            for (int row = 2; row <= rowCount; row++) // tru dong tieu de
                            {
                                string giaTri = wsThamSo.Cells[row, 1].Text.Trim();
                                if (!string.IsNullOrEmpty(giaTri))
                                {
                                    ListViewAutoDeleThamSo.Add(giaTri);
                                }
                            }
                            log($"✅ Đã đọc {ListViewAutoDeleThamSo.Count} dòng từ sheet ViewThamSo.");
                        }
                        else
                        {
                            log("⚠️ Không tìm thấy sheet ViewThamSo.");
                        }

                        // Đọc sheet ViewInsert -- insert DL
                        var wsInsertDL = package.Workbook.Worksheets.FirstOrDefault(x => x.Name == "ViewInsertDL");
                        if (wsInsertDL != null)
                        {
                            int rowCount = wsInsertDL.Dimension.End.Row;
                            for (int row = 2; row <= rowCount; row++) // tru dong tieu de
                            {
                                string giaTri = wsInsertDL.Cells[row, 1].Text.Trim();
                                if (!string.IsNullOrEmpty(giaTri))
                                {
                                    ListViewAutoInsertDL.Add(giaTri);
                                }
                            }
                            log($"✅ Đã đọc {ListViewAutoInsertDL.Count} dòng từ sheet ViewInsertDL.");
                        }
                        else
                        {
                            log("⚠️ Không tìm thấy sheet ViewInsertDL.");
                        }
                    }

                    MessageBox.Show("Đã đọc dữ liệu hoàn tất.");
                    log("");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi đọc file: {ex.Message}");
                }
            }
        }


        private bool daKhoiTaoMenuTrangThai = false;


        private string LoaiTheMenu; 


        // bắt đầu lưu trạng thái : 
        private async void btnBatDau_Click(object sender, EventArgs e)
        {
            try
            {
                // kiểm tra login
                if (txtUsernameLogin.Text.Trim() == ""
                || txtPasswordLogin.Text.Trim() == "")
                {
                    MessageBox.Show("Chưa điền thông tin user / pass đăng nhập !!");
                    return;
                }
               
                // kiểm tra excel
                if (ListViewAutoSaveTrangThai == null || ListViewAutoSaveTrangThai.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn file Excel trước.");
                    return;
                }

                // kiểm tra loại thẻ
                if(!rdbElementMenuA.Checked && !rdbElementMenuDiv.Checked)
                {
                    MessageBox.Show("Chưa chọn loại thẻ của menu");
                }

                if(rdbElementMenuA.Checked)
                {
                    LoaiTheMenu = "a";
                }
                else
                {
                    LoaiTheMenu = "div";
                }


                // lấy thông tin login 
                string username = txtUsernameLogin.Text.Trim();
                string password = txtPasswordLogin.Text.Trim();

                btnBatDau.Enabled = false;


                // khởi tạo đăng nhập + lấy driver 
                moduleDangNhap = new DangNhap(txtLog);
                moduleDangNhap.KhoiTaoVaDangNhap(username, password);
                var driver = moduleDangNhap.Driver;

                //
                autoSaveTrangThaiTinhToan = new AutoSaveTrangThaiTinhToan(txtLog);
                autoSaveTrangThaiTinhToan.ThucHienXuLy(ListViewAutoSaveTrangThai, driver, LoaiTheMenu);

                log("Đã hoàn thành toàn bộ.");
            }
            catch (Exception ex)
            {
                log($"Lỗi tổng ngoài cùng: {ex.Message}\n{ex.StackTrace}");
                log("Đã dừng chương trình");
            }
            finally
            {
                btnBatDau.Enabled = true;
            }
        }

        private void AutoSaveStatus_Load_1(object sender, EventArgs e)
        {

        }

        private void AutoSaveStatus_Load_2(object sender, EventArgs e)
        {
            txtLog.Multiline = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.WordWrap = true;

            rdbElementMenuA.Checked = true;
        }

        
        

        private void btnXoaQuyTac_Click(object sender, EventArgs e)
        {
            if (txtUsernameLogin.Text.Trim() == ""
                || txtPasswordLogin.Text.Trim() == "")
            {
                MessageBox.Show("Chưa điền thông tin user / pass đăng nhập !!");
                return;
            }

            // kiểm tra loại thẻ
            if (!rdbElementMenuA.Checked && !rdbElementMenuDiv.Checked)
            {
                MessageBox.Show("Chưa chọn loại thẻ của menu");
            }

            if (rdbElementMenuA.Checked)
            {
                LoaiTheMenu = "a";
            }
            else
            {
                LoaiTheMenu = "div";
            }

            string username = txtUsernameLogin.Text.Trim();
            string password = txtPasswordLogin.Text.Trim();

            bool XoaHangLoat = true;
            if (rdbXoaChinhXac.Checked)
            {
                XoaHangLoat = false;
            }

            if (!rdbXoaChinhXac.Checked && !rdbXoaGanDung.Checked)
            {
                MessageBox.Show("Chưa chọn kiểu xóa tham số !!");
                return;
            }

            if (ListViewAutoDeleThamSo == null || ListViewAutoDeleThamSo.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn file Excel trước.");
                return;
            }

            btnXoaQuyTac.Enabled = false;

            // khoi tao dang nhap 
            moduleDangNhap = new DangNhap(txtLog);
            moduleDangNhap.KhoiTaoVaDangNhap(username, password);
            var driver = moduleDangNhap.Driver;

            try
            {
                AutoDeleThamSo deleThamSo = new AutoDeleThamSo(txtLog);
                deleThamSo.ThucHienXuLy(ListViewAutoDeleThamSo, XoaHangLoat, driver, LoaiTheMenu);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
                log("Đã dừng chương trình");
            }
            finally
            {
                btnXoaQuyTac.Enabled = true;
            }
        }

        
        private void btnInsertDL_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsernameLogin.Text.Trim() == ""
                || txtPasswordLogin.Text.Trim() == "")
                {
                    MessageBox.Show("Chưa điền thông tin user / pass đăng nhập !!");
                    return;
                }

                // kiểm tra loại thẻ
                if (!rdbElementMenuA.Checked && !rdbElementMenuDiv.Checked)
                {
                    MessageBox.Show("Chưa chọn loại thẻ của menu");
                }

                if (rdbElementMenuA.Checked)
                {
                    LoaiTheMenu = "a";
                }
                else
                {
                    LoaiTheMenu = "div";
                }

                string username = txtUsernameLogin.Text.Trim();
                string password = txtPasswordLogin.Text.Trim();


                if (ListViewAutoInsertDL == null || ListViewAutoInsertDL.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn file Excel trước.");
                    return;
                }

                btnInsertDL.Enabled = false;

                // khoi tao dang nhap
                moduleDangNhap = new DangNhap(txtLog);
                moduleDangNhap.KhoiTaoVaDangNhap(username, password);
                var driver = moduleDangNhap.Driver;

                try
                {
                    moduleInsertDL = new AutoSaveInsertDl(txtLog);
                    moduleInsertDL.ThucHienXuLy(ListViewAutoInsertDL, driver, LoaiTheMenu);
                }
                catch
                {
                    return;
                }

                log("Đã hoàn thành toàn bộ.");
            }
            catch (Exception ex)
            {
                log($"Lỗi tổng ngoài cùng: {ex.Message}\n{ex.StackTrace}");
                log("Đã dừng chương trình");
            }
            finally
            {
                btnInsertDL.Enabled = true;
            }
        }

        private void ckbBoQuaXoaThamSo_CheckedChanged(object sender, EventArgs e)
        {
            log("xác nhận bỏ qua bước xóa tham số ");
        }

        private void btnDele_SaveStatus_InsertDL_Click(object sender, EventArgs e)
        {
            try
            {
                log($"Trạng thái checkbox TRƯỚC khởi tạo: {ckbBoQuaXoaThamSo.Checked}");
                //bool boQuaThamSo = ckbBoQuaXoaThamSo.Checked;

                if (txtUsernameLogin.Text.Trim() == ""
                || txtPasswordLogin.Text.Trim() == "")
                {
                    MessageBox.Show("Chưa điền thông tin user / pass đăng nhập !!");
                    return;
                }

                if (ListViewAutoInsertDL == null || ListViewAutoInsertDL.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn file Excel trước.");
                    return;
                }

                // kiểm tra loại thẻ
                if (!rdbElementMenuA.Checked && !rdbElementMenuDiv.Checked)
                {
                    MessageBox.Show("Chưa chọn loại thẻ của menu");
                }

                if (rdbElementMenuA.Checked)
                {
                    LoaiTheMenu = "a";
                }
                else
                {
                    LoaiTheMenu = "div";
                }

                string username = txtUsernameLogin.Text.Trim();
                string password = txtPasswordLogin.Text.Trim();

                btnDele_SaveStatus_InsertDL.Enabled = false;


                // khoi tao dang nhap
                moduleDangNhap = new DangNhap(txtLog);
                moduleDangNhap.KhoiTaoVaDangNhap(username, password);
                var driver = moduleDangNhap.Driver;


                // xoa tham so tinh toan
                bool XacNhanXoa;
                if (ckbBoQuaXoaThamSo.InvokeRequired)
                {
                    XacNhanXoa = (bool)ckbBoQuaXoaThamSo.Invoke(new Func<bool>(() => ckbBoQuaXoaThamSo.Checked));
                }
                else
                {
                    XacNhanXoa = ckbBoQuaXoaThamSo.Checked;
                }
                log($"Bỏ qua xóa tham số: {XacNhanXoa}");

                
                if (!XacNhanXoa) 
                {
                    log(" ---------------------------------- ");
                    log("Bắt đầu quá trình xóa tham số...");
                    if(rdbXoaChinhXac.Checked == false
                        && rdbXoaGanDung.Checked == false)
                    {
                        MessageBox.Show("Chưa chọn kiểu xóa view ");
                        return;
                    }
                    bool XoaHangLoat = !rdbXoaChinhXac.Checked;

                    // 
                    AutoDeleThamSo deleThamSo = new AutoDeleThamSo(txtLog);
                    deleThamSo.ThucHienXuLy(ListViewAutoDeleThamSo, XoaHangLoat, driver, LoaiTheMenu);

                    if (MessageBox.Show("Đã xóa tham số cũ, upload tham số mới xong nhấn OK để tiếp tục",
                              "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information) != DialogResult.OK)
                    {
                        return;
                    }

                    // 
                }
                else
                {
                    log("Bỏ qua bước xóa tham số do người dùng chọn");
                }

                Thread.Sleep(200);
                driver.Navigate().Refresh();
                Thread.Sleep(500);
                
                try
                {
                    // 3. save trang thai
                    log(" ---------------------------------- ");
                    log("Chuyển sang Save trạng thái");
                    var saveModule = new AutoSaveTrangThaiTinhToan(txtLog);
                    log($"Kiểu module thực tế: {saveModule.GetType().Name}"); 
                    saveModule.ThucHienXuLy(ListViewAutoSaveTrangThai, driver, LoaiTheMenu);

                    Thread.Sleep(200);
                    driver.Navigate().Refresh();
                    Thread.Sleep(500);

                    // 4. insert du lieu
                    log(" ---------------------------------- ");
                    log("Chuyển sang insert DL");
                    new AutoSaveInsertDl(txtLog).ThucHienXuLy(ListViewAutoInsertDL, driver, LoaiTheMenu);

                    log("Đã hoàn thành toàn bộ.");
                }
                catch (Exception ex)
                {
                    log($"Lỗi khi lưu trạng thái hoặc insert dữ liệu: {ex.Message}");
                    return;
                }
            }
            catch (Exception ex)
            {
                log($"Lỗi tổng: {ex.Message}\n{ex.StackTrace}");
            }
            finally
            {
                btnDele_SaveStatus_InsertDL.Enabled = true;
            }
        }

        private void btnBoQuaXoaThamSo_CheckedChanged(object sender, EventArgs e)
        {
            log("Xác nhận bỏ qua Dele tham số ");
        }

        private void txtPasswordLogin_TextChanged(object sender, EventArgs e)
        {
            txtPasswordLogin.PasswordChar = '*';
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
