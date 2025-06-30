using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TOOL_BUILD_FINAL_KPI_COE
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());


        }
        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            GhiLogLoiHeThong("Lỗi ThreadException", e.Exception);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                GhiLogLoiHeThong("Lỗi UnhandledException", ex);
            }
        }

        private static void GhiLogLoiHeThong(string loaiLoi, Exception ex)
        {
            try
            {
                string logFolder = @"D:\Tool_AutoSaveTrangThai\Log"; // hoặc thay đường dẫn log theo cấu hình bạn đang dùng
                if (!Directory.Exists(logFolder))
                    Directory.CreateDirectory(logFolder);

                string logPath = Path.Combine(logFolder, "log_he_thong.txt");
                File.AppendAllText(logPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {loaiLoi}: {ex}\n");
            }
            catch
            {
                // Tránh lỗi phát sinh khi ghi log
            }
        }
    }
}
