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
    public class AutoSaveTrangThaiTinhToan
    {
        private readonly string logFolder = @"D:\datalake_data_point\tool\Tool_COE_KPI\TOOL_BUILD_FINAL_KPI_COE\TOOL_BUILD_FINAL_KPI_COE\bin\Release\log_error";
        private bool daKhoiTaoMenuTrangThai = false;
        private TextBox txtLog;
        //private DangNhap dangNhap; 

        public AutoSaveTrangThaiTinhToan(TextBox logTextBox)
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
            IWebDriver driver, string LoaiTheMenu)
        {
            try
            {
                int stt = 1;
                foreach (var dong in danhSachDuLieu)
                {
                    try
                    {
                        log("");
                        log($"----------> STT : {stt} <------------");
                        XuLyDuLieuTrenWeb(driver, dong, LoaiTheMenu);
                        Thread.Sleep(1000);
                        stt ++;
                    }
                    catch (Exception ex)
                    {
                        stt = 0;
                        log($"Lỗi khi xử lý dòng {dong}: {ex.Message}");
                        // Bỏ throw để tiếp tục với dòng tiếp theo
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

        private void XuLyDuLieuTrenWeb(IWebDriver driver, string giaTriTimKiem, string LoaiTheMenu)
        {
            try
            {
                int thoigian_cho = 500;
                int timeOutTimKiem = 1 * 60 * 1000; // 1 phút cho tìm kiếm
                int timeOutLuu = 10 * 60 * 1000;     // 10 phút cho lưu dữ liệu

                
                log($"➡️ Start {DateTime.Now.ToString("HH:mm:ss - dd.MM.yyyy")}");
                log($"Bắt đầu save trạng thái  view: {giaTriTimKiem}");

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

                    // B3. Trang thai tinh toan
                    if (LoaiTheMenu == "div")
                    {
                        driver.FindElement(By.CssSelector("div.list-group-item[href='#divTreeMenu-item-20']")).Click();
                    }
                    driver.FindElement(By.CssSelector("a.list-group-item[href='#divTreeMenu-item-20']")).Click();
                    Thread.Sleep(thoigian_cho);

                    daKhoiTaoMenuTrangThai = true;
                }

                // B4. Nhập nội dung tìm kiếm
                var inputs = driver.FindElements(By.CssSelector(".tabulator-header-filter input[type='search']"));
                var inputSearch = inputs.Count >= 4 ? inputs[3] : null;
                if (inputSearch == null)
                {
                    log("Không tìm thấy ô tìm kiếm");
                    throw new Exception($"Không tìm thấy ô tìm kiếm cho giá trị: {giaTriTimKiem}");
                }

                inputSearch.Clear();
                foreach (char c in giaTriTimKiem)
                {
                    inputSearch.SendKeys(c.ToString());
                    Thread.Sleep(10);
                }
                Thread.Sleep(500);

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
                        throw new Exception($"Quá thời gian chờ tìm kiếm {timeOutTimKiem / 1000} giây");
                    }

                    try
                    {
                        if (driver.FindElement(By.Id("uploadProgressPopup")).GetAttribute("style").Contains("display: none"))
                        {
                            log($"Tìm kiếm xong sau {(DateTime.Now - batDauTimKiem).TotalSeconds:F2} giây");
                            break;
                        }
                    }
                    catch
                    {
                        // Nếu không tìm thấy popup, coi như đã xong
                        break;
                    }

                    Thread.Sleep(100);
                }

                Thread.Sleep(thoigian_cho);

                // B7. Tích chọn dòng
                bool timThay = false;
                DateTime batDauTimDong = DateTime.Now;

                while ((DateTime.Now - batDauTimDong).TotalMilliseconds < timeOutTimKiem)
                {
                    var rows = driver.FindElements(By.CssSelector(".tabulator-row"));
                    foreach (var row in rows)
                    {
                        try
                        {
                            var oNoiDung = row.FindElement(By.CssSelector("div[tabulator-field='4']"));
                            if (oNoiDung.Text.Trim() == giaTriTimKiem)
                            {
                                var trangThaiThamSo = row.FindElement(By.CssSelector("div[tabulator-field='3']"));
                                if(trangThaiThamSo.Text.Trim() == "Y")
                                {
                                    row.FindElement(By.CssSelector("input[type='checkbox']")).Click();
                                    timThay = true;
                                    break;
                                }
                                else
                                {
                                    throw new Exception($"❌❌❌ view : {giaTriTimKiem} bị cấu hình tính toán sai");
                                }
                                
                            }
                        }
                        catch { }
                    }

                    if (timThay) break;
                    Thread.Sleep(200);
                }

                if (!timThay)
                {
                    log($"❌❌❌Không tìm thấy: {giaTriTimKiem} ❌❌❌");
                    throw new Exception($"Không tìm thấy dòng có nội dung: {giaTriTimKiem}");
                }

                Thread.Sleep(thoigian_cho);

                // B8. Ấn lưu
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
                    log($"save trạng thái view hoàn tất trong {(DateTime.Now - batDauLuu).TotalSeconds:F2} giây.");
                }
                catch (WebDriverTimeoutException)
                {
                    log($"Cảnh báo: Timeout save trạng thái view sau {timeOutLuu / 1000} giây");
                    throw new Exception($"time out");
                }

                // B9. Kiểm tra trạng thái sau khi lưu
                try
                {
                    log("Bắt đầu kiểm tra trạng thái save trạng thái...");
                    var batDauKiemTra = DateTime.Now;

                    Thread.Sleep(2000);

                    bool timThay2 = false;
                    var rows2 = driver.FindElements(By.CssSelector(".tabulator-row"));

                    foreach (var row in rows2)
                    {
                        try
                        {
                            var oNoiDung = row.FindElement(By.CssSelector("div[tabulator-field='4']"));
                            if (oNoiDung.Text.Trim() == giaTriTimKiem)
                            {
                                var oTrangThai = row.FindElement(By.CssSelector("div[tabulator-field='5']"));
                                string trangThaiText = oTrangThai.Text.Trim();

                                if (trangThaiText == "")
                                {
                                    log($"✅✅✅ Đã save trạng thái view - Dòng '{giaTriTimKiem}' có trạng thái SUCCESS");
                                    timThay2 = true;
                                    break;
                                }
                                else
                                {
                                    log($"❌❌❌save trạng thái view Thất bại - ERROR: {trangThaiText}");
                                    throw new Exception($"save trạng thái thất bại - Trạng thái: {trangThaiText}");
                                }
                            }
                        }
                        catch (NoSuchElementException)
                        {
                            continue;
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
                    log($"🔥 Lỗi nghiêm trọng khi save trạng thái: {ex.Message}");
                    throw;
                }

                log($"save trạng thái view: {giaTriTimKiem}");
            }
            catch (Exception ex)
            {
                log($"Lỗi khi xử lý save trạng thái {giaTriTimKiem}: {ex.Message}");
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