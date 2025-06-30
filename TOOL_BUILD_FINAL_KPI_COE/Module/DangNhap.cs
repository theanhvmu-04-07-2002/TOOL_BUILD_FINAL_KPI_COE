using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TOOL_BUILD_FINAL_KPI_COE.MODULE
{
    public class DangNhap
    {
        private IWebDriver _driver;
        private readonly TextBox _txtLog;

        // Thông tin cấu hình (có thể đọc từ file config nếu cần)
        
        public IWebDriver Driver => _driver;

        public DangNhap(TextBox logTextBox)
        {
            _txtLog = logTextBox;
        }

        public void KhoiTaoVaDangNhap(string InputUsername, string InputPassword)
        {
            try
            {
                KhoiTaoChrome();
                ThucHienDangNhap(InputUsername, InputPassword);
                Log("Khởi tạo và đăng nhập thành công");
            }
            catch (Exception ex)
            {
                Log($"Lỗi trong quá trình khởi tạo và đăng nhập: {ex.Message}");
                throw;
            }
        }

        private void KhoiTaoChrome()
        {
            try
            {
                var driverPath = new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());

                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--start-maximized");
                options.AddArgument("--disable-infobars");
                options.AddArgument("--disable-notifications");

                var service = ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(driverPath));
                _driver = new ChromeDriver(service, options);

                Log("Khởi tạo ChromeDriver thành công");
            }
            catch (Exception ex)
            {
                Log($"Lỗi khi khởi tạo ChromeDriver: {ex.Message}");
                throw;
            }
        }

        private void ThucHienDangNhap(string InputUsername, string InputPassword)
        {
            try
            {
                string URL = "http://10.165.38.19:8080/";
                string USERNAME = InputUsername;
                string PASSWORD = InputPassword;
                string LOG_FOLDER = @"D:\datalake_data_point\tool\Tool_COE_KPI\TOOL_BUILD_FINAL_KPI_COE\TOOL_BUILD_FINAL_KPI_COE\bin\Release\log_error";


                _driver.Navigate().GoToUrl(URL);

                _driver.FindElement(By.Id("inpUser")).SendKeys(USERNAME);
                Thread.Sleep(500);
                _driver.FindElement(By.Id("inpPassword")).SendKeys(PASSWORD);
                Thread.Sleep(500);
                _driver.FindElement(By.Id("btnSubmit")).Click();

                // Chờ đăng nhập thành công (có thể thêm điều kiện kiểm tra)
                //Thread.Sleep(2000);
                Log($"Đã đăng nhập thành công vào {URL}");
            }
            catch (Exception ex)
            {
                Log($"Lỗi khi thực hiện đăng nhập: {ex.Message}");
                throw;
            }
        }

        public void DongTrinhDuyet()
        {
            try
            {
                if (_driver != null)
                {
                    _driver.Quit();
                    Log("Đã đóng trình duyệt thành công");
                }
            }
            catch (Exception ex)
            {
                Log($"Lỗi khi đóng trình duyệt: {ex.Message}");
            }
        }

        private string LOG_FOLDER = "Logs";
        private void Log(string message)
        {
            try
            {
                // Ghi log vào file
                if (!Directory.Exists(LOG_FOLDER))
                    Directory.CreateDirectory(LOG_FOLDER);

                string logPath = Path.Combine(LOG_FOLDER, "log_dangnhap.txt");
                File.AppendAllText(logPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}");

                // Ghi log vào TextBox
                if (_txtLog != null && !_txtLog.IsDisposed)
                {
                    if (_txtLog.InvokeRequired)
                    {
                        _txtLog.Invoke((MethodInvoker)(() => _txtLog.AppendText(message + Environment.NewLine)));
                    }
                    else
                    {
                        _txtLog.AppendText(message + Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi ghi log: {ex.Message}");
            }
        }
    }
}