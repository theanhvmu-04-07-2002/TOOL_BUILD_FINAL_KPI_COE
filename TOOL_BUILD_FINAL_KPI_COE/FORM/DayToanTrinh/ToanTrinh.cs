using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
//using OfficeOpenXml;
//using System.IO;

namespace TOOL_BUILD_FINAL_KPI_COE.FORM.DayToanTrinh
{
    public partial class ToanTrinh : BaseForm
    {
        public ToanTrinh()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ToanTrinh_Load(object sender, EventArgs e)
        {

        }


        private void XuatExcelTinhToan(List<string[]> danhSachDong)
        {
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Sheets[1];

            int startRow = 1;
            int startCol = 2; // Bắt đầu từ cột B

            for (int i = 0; i < danhSachDong.Count; i++)
            {
                string[] dong = danhSachDong[i];
                for (int j = 0; j < dong.Length; j++)
                {
                    worksheet.Cells[startRow + i, startCol + j] = dong[j];
                }
            }

            // Xác định đường dẫn thư mục Downloads của máy
            string downloadPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            // Tạo chuỗi thời gian thực
            string thoiGianThuc = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            // Đường dẫn đầy đủ của file cần lưu
            string filePath = Path.Combine(downloadPath, $"ImportTinhToanMapNV_{thoiGianThuc}.xlsx");

            try
            {
                workbook.SaveAs(filePath);
                MessageBox.Show("Đã xuất file Excel tại: " + filePath, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Hiện Excel lên cho người dùng thấy
            excelApp.Visible = true;
        }
        private void MauImportMapViewChung(string viewDau, string viewCuoi)
        {
            string field_type_join_c1 = "LEFT JOIN";
            string field_type_join_c2 = "LEFT JOIN";

            string field_from_c1 = "NHANVIEN_ID";
            string field_to_c1 = "NHANVIEN_ID";
            string table_to_c1 = "VW_VNP010055_NHANVIEN_DONVI";

            string field_from_c2_1 = "MA_NV";
            string field_to_c2_1 = "STAFF_CODE";
            
            string table_to_c2 = "BI_OCDM_STAGE_BI_MAP_STAFF_HRM_VNPT_NEW";
            string field_from_c2_2 = "THANG";
            string field_to_c2_2 = "MO_KEY";

            string field_from_pv = "PHANVUNG_ID";
            string field_to_pv = "PHANVUNG_ID";

            // 13 cot 
            // dong 1
            string[] dong1 = new string[]
            {
                "PURPOSE_CODE",
                "SOURCE_DATA",
                "FIELD_SEQ",
                "FIELD_TYPE",
                "FIELD_ALIAS",
                "FIELD_DESC",
                "TABLE_FROM",
                "TABLE_TO",
                "FIELD_FROM",
                "FIELD_TO",
                "FIELD_FORMULA",
                "STATUS",
                "DESTINATION_VIEW"
            };
            string[] dong2 = new string[] 
            {
                (viewCuoi + "_C1").Substring(3) , //"PURPOSE_CODE",
                "DATAGOV" , //"SOURCE_DATA",
                "" , //"FIELD_SEQ",
                field_type_join_c1 , //"FIELD_TYPE",
                "", //"FIELD_ALIAS",
                "", //"FIELD_DESC",
                viewDau, //"TABLE_FROM",
                table_to_c1, //"TABLE_TO",
                field_from_c1 , //"FIELD_FROM",
                field_to_c1 , //"FIELD_TO",
                "" , //"FIELD_FORMULA",
                "" , //"STATUS",
                viewCuoi + "_C1"  //"DESTINATION_VIEW"
            };

            string[] dong3 = new string[]
            {
                (viewCuoi + "_C1").Substring(3) , //"PURPOSE_CODE",
                "DATAGOV" , //"SOURCE_DATA",
                "" , //"FIELD_SEQ",
                "GROUP BY" , //"FIELD_TYPE",
                "", //"FIELD_ALIAS",
                "", //"FIELD_DESC",
                viewDau, //"TABLE_FROM",
                table_to_c1, //"TABLE_TO",
                "" , //"FIELD_FROM",
                "" , //"FIELD_TO",
                "A.KPI_NAME, NVL(B1.NHANVIEN_ID,-1), NVL(B1.MA_NV,'KXD'), " +
                    "NVL(B1.TEN_NV,'KHÔNG XÁC ĐỊNH') ,  \r\nB1.LOAIDV_ID_C2, " +
                    "NVL(B1.DONVI_C2_ID,-1), NVL(B1.MA_DV_C2,'KXD'), " +
                    "NVL(B1.TEN_DV_C2,'KHÔNG XÁC ĐỊNH'), \r\nB1.LOAIDV_ID_C3, " +
                    "NVL(B1.DONVI_C3_ID,-1), NVL(B1.MA_DV_C3,'KXD'), " +
                    "NVL(B1.TEN_DV_C3,'KHÔNG XÁC ĐỊNH')\r\n, NVL(B1.DONVI_ID_C2,-1), " +
                    "NVL(B1.DONVI_ID_C3,-1), A.PHANVUNG_ID" , //"FIELD_FORMULA",
                "" , //"STATUS",
                viewCuoi + "_C1"  //"DESTINATION_VIEW"
            };

            string[] dong4 = new string[]
            {
                (viewCuoi + "_C1").Substring(3) , //"PURPOSE_CODE",
                "DATAGOV" , //"SOURCE_DATA",
                "" , //"FIELD_SEQ",
                "SUM" , //"FIELD_TYPE",
                "", //"FIELD_ALIAS",
                "", //"FIELD_DESC",
                viewDau, //"TABLE_FROM",
                table_to_c1, //"TABLE_TO",
                "" , //"FIELD_FROM",
                "" , //"FIELD_TO",
                "CAST(1 AS NUMBER(1)) SUDUNG,\r\nA.PHANVUNG_ID PHANVUNG_ID," +
                    "\\r\nCAST(NULL AS NUMBER(12)) KHUVUC_ID,\r\nCAST(NULL AS VARCHAR2(50)) MA_KV," +
                    "\r\nCAST(NULL AS VARCHAR2(100)) TEN_KV,\r\nCAST(11 AS NUMBER(12)) STT,\r\nA.KPI_NAME," +
                    "\r\n@@BRCD_PTM_VNP010055_VTHANG@@ THANG, \r\n@@BRCD_PTM_VNP010055_VSTART_DATE@@   NGAY, " +
                    "\r\nSYSDATE THOIGIAN_CHOT,\r\nSUM(A.DL_NGAY) DL_NGAY,\r\nSUM(A.DL_THANG) DL_THANG," +
                    "\r\nCAST(NULL AS NUMBER(12)) DL_NAM,\r\nCAST(NULL AS NUMBER(12)) THANG_PSC," +
                    "\r\nCAST(NULL AS NUMBER(12)) THANG_THU,\r\nCAST(NULL AS VARCHAR2(50)) GHICHU," +
                    "\r\nCAST(NULL AS NUMBER(12)) KHDN,\r\nCAST(NULL AS NUMBER(12)) DL_NAM_TRUOC," +
                    "\r\nNVL(B1.NHANVIEN_ID,-1) NHANVIEN_ID,\r\nNVL(B1.MA_NV,'KXD') MA_NV," +
                    "\r\nNVL(B1.TEN_NV,'KHÔNG XÁC ĐỊNH') TEN_NV,\r\nB1.LOAIDV_ID_C2,\r\nNVL(B1.DONVI_C2_ID,-1) DONVI_C2_ID," +
                    "\r\nNVL(B1.MA_DV_C2,'KXD') MA_DV_C2,\r\nNVL(B1.TEN_DV_C2,'KHÔNG XÁC ĐỊNH') TEN_DV_C2," +
                    "\r\nB1.LOAIDV_ID_C3,\r\nNVL(B1.DONVI_C3_ID,-1) DONVI_C3_ID,\r\nNVL(B1.MA_DV_C3,'KXD') MA_DV_C3," +
                    "\r\nNVL(B1.TEN_DV_C3,'KHÔNG XÁC ĐỊNH') TEN_DV_C3,\r\nCAST(1 AS NUMBER(1)) LOAI_DL\r\n, " +
                    "NVL(B1.DONVI_ID_C2,-1) DONVI_ID_C2,\r\nNVL(B1.DONVI_ID_C3,-1) DONVI_ID_C3 ,NULL THANG_KTDC" , //"FIELD_FORMULA",
                "" , //"STATUS",
                viewCuoi + "_C1"  //"DESTINATION_VIEW"
            };


            //---------------------------- het view C1 ---------------------- //
            string[] dong5 = new string[]
            {
                (viewCuoi + "_C1").Substring(3) , //"PURPOSE_CODE",
                "DATAGOV" , //"SOURCE_DATA",
                "" , //"FIELD_SEQ",
                field_type_join_c2 , //"FIELD_TYPE",
                "", //"FIELD_ALIAS",
                "", //"FIELD_DESC",
                viewCuoi + "_C1", //"TABLE_FROM",
                table_to_c2 , //"TABLE_TO",
                field_from_c2_1 , //"FIELD_FROM",
                field_from_c2_2 , //"FIELD_TO",
                "" , //"FIELD_FORMULA",
                "" , //"STATUS",
                viewCuoi + "_C2"  //"DESTINATION_VIEW"
            };

            string[] dong6 = new string[]
           {
                (viewCuoi + "_C1").Substring(3) , //"PURPOSE_CODE",
                "DATAGOV" , //"SOURCE_DATA",
                "" , //"FIELD_SEQ",
                field_type_join_c2 , //"FIELD_TYPE",
                "", //"FIELD_ALIAS",
                "", //"FIELD_DESC",
                viewCuoi + "_C1", //"TABLE_FROM",
                table_to_c2 , //"TABLE_TO",
                field_from_c2_2 , //"FIELD_FROM",
                field_from_c2_2 , //"FIELD_TO",
                "" , //"FIELD_FORMULA",
                "" , //"STATUS",
                viewCuoi + "_C2"  //"DESTINATION_VIEW"
           };

            string[] dong7 = new string[]
           {
                (viewCuoi + "_C1").Substring(3) , //"PURPOSE_CODE",
                "DATAGOV" , //"SOURCE_DATA",
                "" , //"FIELD_SEQ",
                "SUM" , //"FIELD_TYPE",
                "", //"FIELD_ALIAS",
                "", //"FIELD_DESC",
                viewCuoi + "_C1", //"TABLE_FROM",
                table_to_c2 , //"TABLE_TO",
                "" , //"FIELD_FROM",
                "" , //"FIELD_TO",
                "A.*, B.LOAI_NV\r\n" , //"FIELD_FORMULA",
                "" , //"STATUS",
                viewCuoi + "_C2"  //"DESTINATION_VIEW"
           };

            /// ---------------------------- het view C2 ------------------ ///

            string[] dong8 = new string[]
           {
                (viewCuoi + "_C2").Substring(3) , //"PURPOSE_CODE",
                "DATAGOV" , //"SOURCE_DATA",
                "" , //"FIELD_SEQ",
                "LEFT JOIN" , //"FIELD_TYPE",
                "", //"FIELD_ALIAS",
                "", //"FIELD_DESC",
                viewCuoi + "_C1", //"TABLE_FROM",
                "ONEBSS_CSS_TINH" , //"TABLE_TO",
                "PHANVUNG_ID" , //"FIELD_FROM",
                "PHANVUNG_ID" , //"FIELD_TO",
                "" , //"FIELD_FORMULA",
                "" , //"STATUS",
                viewCuoi  //"DESTINATION_VIEW"
           };

            string[] dong9 = new string[]
          {
                (viewCuoi + "_C2").Substring(3) , //"PURPOSE_CODE",
                "DATAGOV" , //"SOURCE_DATA",
                "" , //"FIELD_SEQ",
                "SUM" , //"FIELD_TYPE",
                "", //"FIELD_ALIAS",
                "", //"FIELD_DESC",
                viewCuoi + "_C1", //"TABLE_FROM",
                "ONEBSS_CSS_TINH" , //"TABLE_TO",
                "PHANVUNG_ID" , //"FIELD_FROM",
                "PHANVUNG_ID" , //"FIELD_TO",
                "A.*, B.TENTAT AS MATINH" , //"FIELD_FORMULA",
                "" , //"STATUS",
                viewCuoi  //"DESTINATION_VIEW"
          };

            List<string[]> danhSachDong = new List<string[]>();
            danhSachDong.Add(dong1);
            danhSachDong.Add(dong2);
            danhSachDong.Add(dong3);
            danhSachDong.Add(dong4);
            danhSachDong.Add(dong5);
            danhSachDong.Add(dong6);
            danhSachDong.Add(dong7);
            danhSachDong.Add(dong8);
            danhSachDong.Add(dong9);

            XuatExcelTinhToan(danhSachDong);



        }

        private void MauImpotPoitMap(List<string[]> listKPI,bool dlNgay,bool dlThang,bool dlNam)
        {
            // tan suat du lieu
            

            string kpi_name = "";
            string mo_ta = "";

            string dau_view = "";
            string campain = "";

            string[] dong1 = new string[]
            {
                "SEQUENCE",
                "OBJECT",
                "DATA_GROUP",
                "DESCRIPTION_SHORT",
                "DESCRIPTION_FULL",
                "CAMPAIGN",
                "HIGH_LEVEL",
                "FILTER_OK",
                "SQL_FULL",
                "SEQUENCE_MAPPING",
                "NOTES",
                "DUPLICATE",
                "CONTAC",
                "PHAN_TA",
                "SOURCE_SYSTEM",
                "SOURCE_SCHEMA",
                "TABLE_CODE",
                "NEWEST_ROW_KEY",
                "FIELD_CODE",
                "PRIMARY_KEY",
                "PARTITION_KEY",
                "BI_OR_LAKE",
                "LOV_TABLE",
                "LOV_PURPOSE_CODE",
                "LOV_CONDITION_SQL",
                "REMARK",
                "PATH_CODE",
                "FLAG_RAW_FIELD",
                "SQL_CONDITION",
                "SQL_JOIN",
                "SQL_CALCULATION",
                "MAPPING_STATUS",
                "DESTINATION_TABLE",
                "DESTINATION_FIELD",
                "DESTINATION_MESSAG",
                "DESTINATION_PATH",
                "FREQUENCY",
                "MV",
                "KPI_DATA_QUALITY",
                "FIELD_COD",
                "TABLE_COD",
                "PUBLISH",
                "PRIORIT",
                "TARGET_TABLE"
            };

            // dl ngay
            string[] dl_ngay = new string[]
            {
                "",// "SEQUENCE",
                "",// "OBJECT",
                "",// "DATA_GROUP",
                kpi_name,// "DESCRIPTION_SHORT",
                dau_view + kpi_name + " | DL_NGAY | " + mo_ta,// "DESCRIPTION_FULL",
                campain ,// "CAMPAIGN",
                "1",// "HIGH_LEVEL",
                "",// "FILTER_OK",
                "",// "SQL_FULL",
                "",// "SEQUENCE_MAPPING",
                "",// "NOTES",
                "",// "DUPLICATE",
                "",// "CONTAC",
                "",// "PHAN_TA",
                "",// "SOURCE_SYSTEM",
                "",// "SOURCE_SCHEMA",
                "",// "TABLE_CODE",
                "",// "NEWEST_ROW_KEY",
                "",// "FIELD_CODE",
                "",// "PRIMARY_KEY",
                "",// "PARTITION_KEY",
                "",// "BI_OR_LAKE",
                "",// "LOV_TABLE",
                "",// "LOV_PURPOSE_CODE",
                "",// "LOV_CONDITION_SQL",
                "",// "REMARK",
                "",// "PATH_CODE",
                "",// "FLAG_RAW_FIELD",
                "",// "SQL_CONDITION",
                "",// "SQL_JOIN",
                "",// "SQL_CALCULATION",
                "",// "MAPPING_STATUS",
                "",// "DESTINATION_TABLE",
                "",// "DESTINATION_FIELD",
                "",// "DESTINATION_MESSAG",
                "",// "DESTINATION_PATH",
                "",// "FREQUENCY",
                "",// "MV",
                "",// "KPI_DATA_QUALITY",
                "",// "FIELD_COD",
                "",// "TABLE_COD",
                "",// "PUBLISH",
                "",// "PRIORIT",
                "",// "TARGET_TABLE"
            };

            string[] dl_thang = new string[]
            {
                "",// "SEQUENCE",
                "",// "OBJECT",
                "",// "DATA_GROUP",
                kpi_name,// "DESCRIPTION_SHORT",
                dau_view + kpi_name + " | DL_THANG | " + mo_ta,// "DESCRIPTION_FULL",
                campain ,// "CAMPAIGN",
                "1",// "HIGH_LEVEL",
                "",// "FILTER_OK",
                "",// "SQL_FULL",
                "",// "SEQUENCE_MAPPING",
                "",// "NOTES",
                "",// "DUPLICATE",
                "",// "CONTAC",
                "",// "PHAN_TA",
                "",// "SOURCE_SYSTEM",
                "",// "SOURCE_SCHEMA",
                "",// "TABLE_CODE",
                "",// "NEWEST_ROW_KEY",
                "",// "FIELD_CODE",
                "",// "PRIMARY_KEY",
                "",// "PARTITION_KEY",
                "",// "BI_OR_LAKE",
                "",// "LOV_TABLE",
                "",// "LOV_PURPOSE_CODE",
                "",// "LOV_CONDITION_SQL",
                "",// "REMARK",
                "",// "PATH_CODE",
                "",// "FLAG_RAW_FIELD",
                "",// "SQL_CONDITION",
                "",// "SQL_JOIN",
                "",// "SQL_CALCULATION",
                "",// "MAPPING_STATUS",
                "",// "DESTINATION_TABLE",
                "",// "DESTINATION_FIELD",
                "",// "DESTINATION_MESSAG",
                "",// "DESTINATION_PATH",
                "",// "FREQUENCY",
                "",// "MV",
                "",// "KPI_DATA_QUALITY",
                "",// "FIELD_COD",
                "",// "TABLE_COD",
                "",// "PUBLISH",
                "",// "PRIORIT",
                "",// "TARGET_TABLE"
            };

            string[] dl_nam = new string[]
            {
                "",// "SEQUENCE",
                "",// "OBJECT",
                "",// "DATA_GROUP",
                kpi_name,// "DESCRIPTION_SHORT",
                dau_view + kpi_name + " | DL_NAM | " + mo_ta,// "DESCRIPTION_FULL",
                campain ,// "CAMPAIGN",
                "1",// "HIGH_LEVEL",
                "",// "FILTER_OK",
                "",// "SQL_FULL",
                "",// "SEQUENCE_MAPPING",
                "",// "NOTES",
                "",// "DUPLICATE",
                "",// "CONTAC",
                "",// "PHAN_TA",
                "",// "SOURCE_SYSTEM",
                "",// "SOURCE_SCHEMA",
                "",// "TABLE_CODE",
                "",// "NEWEST_ROW_KEY",
                "",// "FIELD_CODE",
                "",// "PRIMARY_KEY",
                "",// "PARTITION_KEY",
                "",// "BI_OR_LAKE",
                "",// "LOV_TABLE",
                "",// "LOV_PURPOSE_CODE",
                "",// "LOV_CONDITION_SQL",
                "",// "REMARK",
                "",// "PATH_CODE",
                "",// "FLAG_RAW_FIELD",
                "",// "SQL_CONDITION",
                "",// "SQL_JOIN",
                "",// "SQL_CALCULATION",
                "",// "MAPPING_STATUS",
                "",// "DESTINATION_TABLE",
                "",// "DESTINATION_FIELD",
                "",// "DESTINATION_MESSAG",
                "",// "DESTINATION_PATH",
                "",// "FREQUENCY",
                "",// "MV",
                "",// "KPI_DATA_QUALITY",
                "",// "FIELD_COD",
                "",// "TABLE_COD",
                "",// "PUBLISH",
                "",// "PRIORIT",
                "",// "TARGET_TABLE"
            };


        }

        // button start
        private void button1_Click(object sender, EventArgs e)
        {
            string viewDau = txt_viewDau.Text;
            string viewCuoi = txt_viewCuoi.Text;
            MauImportMapViewChung(viewDau, viewCuoi);
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            txt_viewDau.Clear();
            txt_viewCuoi.Clear();
        }

        private void btn_chonFIle_Click(object sender, EventArgs e)
        {

        }

        

        private void btn_chonFile_Click_1(object sender, EventArgs e)
        {
            //OfficeOpenXml.ExcelPackage.License = new OfficeOpenXml.EPPlusLicenseContext(OfficeOpenXml.LicenseType.NonCommercial);
            //OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Chọn file Excel chứa KPI";
            openFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                List<string[]> danhSachKPI = new List<string[]>();

                try
                {
                    using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(filePath)))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                        int rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++)
                        {
                            string tenKPI = worksheet.Cells[row, 1].Text;
                            string moTa = worksheet.Cells[row, 2].Text;

                            if (!string.IsNullOrWhiteSpace(tenKPI))
                            {
                                danhSachKPI.Add(new string[] { tenKPI, moTa });
                            }
                        }
                    }

                    MessageBox.Show($"Đã đọc {danhSachKPI.Count} KPI từ file.");
                }
                catch (Exception ex)
                {
                    string logPath = Path.Combine(Application.StartupPath, "log_loi.txt");
                    File.WriteAllText(logPath, ex.ToString());
                    MessageBox.Show($"Lỗi khi đọc file.\nXem chi tiết tại:\n{logPath}");
                }
            }
        }
    }
}


