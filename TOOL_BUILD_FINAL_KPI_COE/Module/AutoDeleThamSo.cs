using OfficeOpenXml;
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
    public class AutoDeleThamSo
    {
        private readonly string logFolder = @"D:\datalake_data_point\tool\Tool_COE_KPI\TOOL_BUILD_FINAL_KPI_COE\TOOL_BUILD_FINAL_KPI_COE\bin\Release\log_error";
        private bool daKhoiTaoMenuTrangThai = false;
        private TextBox txtLog;

        public AutoDeleThamSo(TextBox logTextBox)
        {
            this.txtLog = logTextBox;
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
            bool inputXoaHangLoad, 
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
                        log($"-------------> STT: {stt} <--------");
                        XuLyDuLieuTrenWeb(driver, dong, inputXoaHangLoad, LoaiTheMenu);
                        Thread.Sleep(1000);
                        stt ++;
                    }
                    catch (Exception ex)
                    {
                        stt = 0;
                        log($"Lỗi nghiêm trọng khi xử lý dòng {dong}: {ex.Message}");
                        throw; // Dừng chương trình ngay lập tức
                    }
                }

                log("Đã hoàn thành toàn bộ.");
                stt = 0;
            }
            catch (Exception ex)
            {
                log($"Lỗi tổng ngoài cùng: {ex.Message}\n{ex.StackTrace}");
                throw;
            }
        }

        private void XuLyDuLieuTrenWeb(IWebDriver driver, 
            string giaTriTimKiem, 
            bool XoaHangLoat,
            string LoaiTheMenu)
        {
            try
            {
                int thoigian_cho = 500;
                int timeOutTimKiem = 1 * 60 * 1000; // 1 phút cho tìm kiếm
                int timeOutLuu = 3 * 60 * 1000;     // 3 phút cho lưu dữ liệu 

                
                log($"➡️ Start {DateTime.Now.ToString("HH:mm:ss - dd.MM.yyyy")}");
                log($"Bắt đầu xóa PURPOSE CODE: {giaTriTimKiem}");

                // Chỉ mở menu cho KPI đầu tiên thôi 
                if (!daKhoiTaoMenuTrangThai)
                {
                    // B1. Click button menu
                    Thread.Sleep(2000);
                    driver.FindElement(By.Id("btnSidebarToggle")).Click();
                    Thread.Sleep(thoigian_cho);
                    Thread.Sleep(500);

                    // B2. Tham so dong
                    if(LoaiTheMenu == "div")
                    {
                        driver.FindElement(By.CssSelector("div.list-group-item[href='#divTreeMenu-item-13']")).Click();
                    }
                    driver.FindElement(By.CssSelector("a.list-group-item[href='#divTreeMenu-item-13']")).Click();
                    Thread.Sleep(thoigian_cho);

                    // B3. Quy tac tinh toan
                    if(LoaiTheMenu == "div")
                    {
                        driver.FindElement(By.CssSelector("div.list-group-item[href='#divTreeMenu-item-18']")).Click();
                    }
                    driver.FindElement(By.CssSelector("a.list-group-item[href='#divTreeMenu-item-18']")).Click();
                    Thread.Sleep(thoigian_cho);

                    daKhoiTaoMenuTrangThai = true;
                }

                // Mở rộng bảng dữ liệu
                Thread.Sleep(thoigian_cho);
                try
                {
                    var pageSizeSelect = driver.FindElement(By.CssSelector("select.tabulator-page-size"));
                    var selectElement = new OpenQA.Selenium.Support.UI.SelectElement(pageSizeSelect);
                    selectElement.SelectByValue("100");
                    log("✅ Đã chọn hiển thị 100 dòng trên trang.");
                    Thread.Sleep(thoigian_cho);
                }
                catch (Exception ex)
                {
                    log($"⚠️ Cảnh báo: Không thể thay đổi số dòng hiển thị: {ex.Message}");
                    // Tiếp tục chạy vì đây không phải lỗi nghiêm trọng
                }

                // B4. Nhập nội dung tìm kiếm
                var inputs = driver.FindElements(By.CssSelector(".tabulator-header-filter input[type='search']"));
                var inputSearch = inputs.Count >= 3 ? inputs[2] : null;
                if (inputSearch == null) throw new Exception("Không tìm thấy ô nhập liệu tìm kiếm");

                inputSearch.Clear();

                foreach (char c in giaTriTimKiem)
                {
                    inputSearch.SendKeys(c.ToString());
                    Thread.Sleep(10);
                }
                Thread.Sleep(300);

                // B5. Tìm kiếm
                var btnSearch = driver.FindElement(By.Id("btnTableSearch"));
                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].click();", btnSearch);

                var batDauTimKiem = DateTime.Now;

                // B6. Chờ tìm kiếm xong
                while (true)
                {
                    if ((DateTime.Now - batDauTimKiem).TotalMilliseconds > timeOutTimKiem)
                    {
                        throw new TimeoutException($"Quá thời gian chờ tìm kiếm {timeOutTimKiem / 1000} giây");
                    }

                    try
                    {
                        if (driver.FindElement(By.Id("uploadProgressPopup")).GetAttribute("style").Contains("display: none"))
                        {
                            log($"Tìm kiếm xong sau {(DateTime.Now - batDauTimKiem).TotalSeconds:F2} giây");
                            break;
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        throw new Exception("Không tìm thấy popup tiến trình tìm kiếm");
                    }

                    Thread.Sleep(100);
                }

                Thread.Sleep(thoigian_cho);

                // B7. Tích chọn dòng
                bool timThay = false;
                int soDongDaTich = 0;
                DateTime batDauTimDong = DateTime.Now;

                // sau 2s mà ko tích được -> ko còn cấu hình view -> bỏ qua 
                while ((DateTime.Now - batDauTimDong).TotalMilliseconds < 2000)
                {
                    var rows = driver.FindElements(By.CssSelector(".tabulator-row"));
                    soDongDaTich = 0;

                    foreach (var row in rows)
                    {
                        try
                        {
                            var oNoiDung = row.FindElement(By.CssSelector("div[tabulator-field='3']"));
                            string noiDung = oNoiDung.Text.Trim();

                            if (XoaHangLoat)
                            {
                                if (noiDung.Contains(giaTriTimKiem))
                                {
                                    var checkbox = row.FindElement(By.CssSelector("input[type='checkbox']"));
                                    if (!checkbox.Selected)
                                    {
                                        checkbox.Click();
                                        soDongDaTich++;
                                    }
                                    timThay = true;
                                }
                            }
                            else
                            {
                                if (noiDung == giaTriTimKiem)
                                {
                                    var checkbox = row.FindElement(By.CssSelector("input[type='checkbox']"));
                                    if (!checkbox.Selected)
                                    {
                                        checkbox.Click();
                                        soDongDaTich++;
                                    }
                                    timThay = true;
                                    break;
                                }
                            }
                        }
                        catch { }
                    }

                    if (timThay && (!XoaHangLoat || soDongDaTich > 0)) break;
                    Thread.Sleep(200); // sau mỗi 0.2s thì kiểm tra lại xem có ra dòng nào ko 
                }

                if (!timThay)
                {
                    log($"❌ Không tìm thấy PURPOSE CODE: {giaTriTimKiem} - Theo nghiệp vụ, đây không phải lỗi");
                    log($"➡️ End {DateTime.Now.ToString("HH:mm:ss - dd.MM.yyyy")}");
                    return; // Thoát hàm bình thường, không coi là lỗi
                }

                log($"✅ Đã tìm và chọn {soDongDaTich} dòng phù hợp");

                Thread.Sleep(thoigian_cho);

                // B8. Xóa dữ liệu
                driver.FindElement(By.Id("btnTableDel")).Click();
                Thread.Sleep(thoigian_cho);

                // B9. Xác nhận và lưu
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
                    log($"✅ Xóa PURPOSE CODE hoàn tất trong {(DateTime.Now - batDauLuu).TotalSeconds:F2} giây.");
                }
                catch (WebDriverTimeoutException)
                {
                    throw new TimeoutException($"Timeout khi xóa dữ liệu sau {timeOutLuu / 1000} giây");
                }

                log($"✅ Đã xóa xong PURPOSE CODE: {giaTriTimKiem}");
                log($"➡️ End {DateTime.Now.ToString("HH:mm:ss - dd.MM.yyyy")}");
            }
            catch (Exception ex)
            {
                log($"❌ Lỗi nghiêm trọng khi xử lý PURPOSE CODE {giaTriTimKiem}: {ex.Message}");
                throw; // Ném lỗi lên trên
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