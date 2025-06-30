using OfficeOpenXml;
using OfficeOpenXml.Utils;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WebDriverManager.DriverConfigs.Impl;

namespace TOOL_BUILD_FINAL_KPI_COE.MODULE
{
    public class AutoSaveInsertDl
    {
        private readonly string logFolder = @"D:\datalake_data_point\tool\Tool_COE_KPI\TOOL_BUILD_FINAL_KPI_COE\TOOL_BUILD_FINAL_KPI_COE\bin\Release\log_error";
        private bool daKhoiTaoMenuTrangThai = false;
        private TextBox txtLog;
        //private DangNhap dangNhap;

        public AutoSaveInsertDl(TextBox logTextBox)
        {
            this.txtLog = logTextBox;
            //this.dangNhap = new DangNhap(logTextBox);
        }

        public List<string> DocDuLieuTuFileExcel(string filePath)
        {
            List<string> danhSachDuLieu = new List<string>();
            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets[1];
                    int rowCount = worksheet.Dimension.End.Row;

                    for (int row = 1; row <= rowCount; row++)
                    {
                        string giaTri = worksheet.Cells[row, 1].Text.Trim();
                        if (!string.IsNullOrEmpty(giaTri))
                        {
                            danhSachDuLieu.Add(giaTri);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log($"Lỗi đọc file: {ex.Message}");
                throw;
            }
            return danhSachDuLieu;
        }

        public void ThucHienXuLy(List<string> danhSachDuLieu, 
            IWebDriver driver,
            string LoaiTheMenu)
        {
            try
            {
                int stt = 1;
                foreach (var dong in danhSachDuLieu)
                {
                    try
                    {
                        log("");
                        log($"--------------> STT: {stt} <-----------------");

                        XuLyDuLieuTrenWeb(driver, dong, LoaiTheMenu);
                        Thread.Sleep(1000);
                        stt++;
                    }
                    catch (Exception ex)
                    {
                        stt = 0;
                        log($"Lỗi khi xử lý dòng {dong}: {ex.Message}");
                        throw;
                       
                    }
                }
                stt = 0;
                log("Đã hoàn thành toàn bộ.");
            }
            catch (Exception ex)
            {
                log($"Lỗi tổng ngoài cùng: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }



        private void XuLyDuLieuTrenWeb(IWebDriver driver, 
            string giaTriTimKiem,
            string LoaiTheMenu)
        {
            try
            {
                int thoigian_cho = 500;
                int timeOutTimKiem = 1 * 60 * 1000; // 1 phút cho tìm kiếm
                int timeOutLuu = 3 * 60 * 1000;     // 3 phút cho lưu dữ liệu

                
                log($"➡️ Start {DateTime.Now.ToString("HH:mm:ss - dd.MM.yyyy")}");
                log($"Bắt đầu INSERT DL view: {giaTriTimKiem}");

                // Chỉ mở menu cho KPI đầu tiên thôi 
                if (!daKhoiTaoMenuTrangThai)
                {

                    // B1. Click button menu - Không cần timeout
                    Thread.Sleep(2000);
                    driver.FindElement(By.Id("btnSidebarToggle")).Click();
                    Thread.Sleep(thoigian_cho);
                    Thread.Sleep(500);

                    // B2. Tham so dong
                    if (LoaiTheMenu == "div")
                    {
                        driver.FindElement(By.CssSelector("div.list-group-item[href='#divTreeMenu-item-13']")).Click();
                    }
                    driver.FindElement(By.CssSelector("a.list-group-item[href='#divTreeMenu-item-13']")).Click();
                    Thread.Sleep(thoigian_cho);

                    // B3. Insert du lieu 
                    if (LoaiTheMenu == "div")
                    {
                        driver.FindElement(By.CssSelector("div.list-group-item[href='#divTreeMenu-item-22']")).Click();
                    }
                    driver.FindElement(By.CssSelector("a.list-group-item[href='#divTreeMenu-item-22']")).Click();
                    Thread.Sleep(thoigian_cho);

                    daKhoiTaoMenuTrangThai = true;
                }

                // B4. Nhập nội dung tìm kiếm - Không cần timeout
                var inputs = driver.FindElements(By.CssSelector(".tabulator-header-filter input[type='search']"));
                var inputSearch = inputs.Count >= 4 ? inputs[3] : null;
                inputSearch.Clear();

                foreach (char c in giaTriTimKiem)
                {
                    inputSearch.SendKeys(c.ToString());
                    Thread.Sleep(10);
                }
                Thread.Sleep(500);

                // B5. Tìm kiếm - Không cần timeout
                var btnSearch = driver.FindElement(By.Id("btnTableSearch"));

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", btnSearch);

                var batDauTimKiem = DateTime.Now;

                // B6. Chờ tìm kiếm xong - Có timeout quan trọng
                while (true)
                {
                    // Kiểm tra timeout
                    if ((DateTime.Now - batDauTimKiem).TotalMilliseconds > timeOutTimKiem)
                    {
                        throw new TimeoutException($"Quá thời gian chờ tìm kiếm {timeOutTimKiem / 1000} giây");
                    }

                    // Kiểm tra popup đã ẩn chưa
                    try
                    {
                        var popup = driver.FindElement(By.Id("uploadProgressPopup"));
                        if (popup.GetAttribute("style").Contains("display: none"))
                        {
                            log($"Tìm kiếm xong sau {(DateTime.Now - batDauTimKiem).TotalSeconds:F2} giây");
                            break;
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        throw new Exception("Không tìm thấy popup tiến trình tìm kiếm");
                    }

                    Thread.Sleep(100); // Chờ 0.1s trước khi kiểm tra lại
                }

                Thread.Sleep(thoigian_cho);

                // B7. Tích chọn dòng - Không cần timeout
                bool timThay = false;
                DateTime batDauTimDong = DateTime.Now;

                while ((DateTime.Now - batDauTimDong).TotalMilliseconds < 5000) // Chờ tối đa 5 giây tìm dòng
                {
                    var rows = driver.FindElements(By.CssSelector(".tabulator-row"));
                    foreach (var row in rows)
                    {
                        try
                        {
                            var oNoiDung = row.FindElement(By.CssSelector("div[tabulator-field='4']"));
                            if (oNoiDung.Text.Trim() == giaTriTimKiem)
                            {
                                row.FindElement(By.CssSelector("input[type='checkbox']")).Click();
                                timThay = true;
                                break;
                            }
                        }
                        catch { } // Bỏ qua nếu dòng chưa render xong đầy đủ
                    }

                    if (timThay) break;
                    Thread.Sleep(200); // Chờ trước khi thử lại
                }

                if (!timThay)
                {
                    throw new Exception($"Không tìm thấy dòng có nội dung: {giaTriTimKiem}");
                }

                Thread.Sleep(thoigian_cho);

                // B8. Ấn lưu  - Có timeout quan trọng
                driver.FindElement(By.Id("btnTableSave")).Click();
                Thread.Sleep(thoigian_cho);
                driver.FindElement(By.CssSelector("#confirmSaveModal button[onclick='fnSave()']")).Click();
                var batDauLuu = DateTime.Now;

                var waitLuu = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeOutLuu));
                try
                {
                    waitLuu.Until(d =>
                    {
                        var element = d.FindElement(By.Id("uploadProgressPopup"));
                        return element.GetAttribute("style").Contains("display: none");
                    });
                    log($"INSERT DL view hoàn tất trong {(DateTime.Now - batDauLuu).TotalSeconds:F2} giây.");
                }
                catch (WebDriverTimeoutException)
                {
                    throw new TimeoutException($"Timeout khi xóa dữ liệu sau {timeOutLuu / 1000} giây");
                }



                // B9. check trạng thái lưu 
                // B9. Kiểm tra trạng thái sau khi lưu
                try
                {
                    log("Bắt đầu kiểm tra trạng thái lưu dữ liệu...");
                    var batDauKiemTra = DateTime.Now;

                    // Chờ bảng dữ liệu reload (nếu có)
                    Thread.Sleep(2000); // Chờ đủ thời gian load lại

                    // Tìm lại dòng dữ liệu đã lưu
                    bool timThay2 = false;
                    var rows2 = driver.FindElements(By.CssSelector(".tabulator-row"));

                    foreach (var row in rows2)
                    {
                        try
                        {
                            // Kiểm tra cột nội dung (field 4)
                            var oNoiDung = row.FindElement(By.CssSelector("div[tabulator-field='4']"));
                            if (oNoiDung.Text.Trim() == giaTriTimKiem)
                            {
                                // Kiểm tra cột trạng thái (field 5)
                                var oTrangThai = row.FindElement(By.CssSelector("div[tabulator-field='7']"));
                                string trangThaiText = oTrangThai.Text.Trim();


                                if (trangThaiText == "SUCCESS")
                                {
                                    log($"✅✅✅ Đã INSERT DL view - Dòng '{giaTriTimKiem}' có trạng thái SUCCESS");
                                    timThay2 = true;
                                    break; // tiếp tục xử lý các bước tiếp theo sau vòng lặp này
                                }
                                else
                                {
                                    log($"❌❌❌INSERT DL view Thất bại - ERROR: {trangThaiText}");
                                    return; // dừng hàm tại đây nhưng không đóng chương trình
                                }

                                
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            continue; // Bỏ qua nếu không tìm thấy cột
                        }
                    }

                    if (!timThay2)
                    {
                        throw new Exception($"Không tìm thấy dòng '{giaTriTimKiem}' sau khi lưu");
                    }

                    log($"Thời gian kiểm tra: {(DateTime.Now - batDauKiemTra).TotalSeconds:F2} giây");
                    log($"➡️ End {DateTime.Now.ToString("HH:mm:ss - dd.MM.yyyy")}");
                }
                catch (Exception ex)
                {
                    log($"🔥 Lỗi nghiêm trọng khi kiểm tra trạng thái: {ex.Message}");
                    throw;
                }


                log($"Đã INSERT DL view: {giaTriTimKiem}");
            }
            catch (Exception ex)
            {
                log($"Lỗi khi xử lý dữ liệu {giaTriTimKiem}: {ex.Message}");
                throw;
            }
        }


        private void log(string message)
        {
            // Phần ghi log vào file giữ nguyên
            try
            {
                if (!Directory.Exists(logFolder))
                    Directory.CreateDirectory(logFolder);

                string logPath = Path.Combine(logFolder, "log.txt");
                File.AppendAllText(logPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi khi ghi log ra file: {ex.Message}");
            }

            // Phần ghi log vào TextBox
            try
            {
                if (txtLog == null || txtLog.IsDisposed) return;

                if (txtLog.InvokeRequired)
                {
                    txtLog.Invoke((MethodInvoker)(() => txtLog.AppendText(message + Environment.NewLine)));
                }
                else
                {
                    txtLog.AppendText(message + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Lỗi khi ghi log ra TextBox: {ex.Message}");
            }
        }
    }
}