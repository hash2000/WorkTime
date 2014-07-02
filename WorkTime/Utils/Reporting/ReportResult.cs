using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;


namespace WorkTime.Utils.Reporting
{
    public enum ReportFormat {
        Excel,
        Word,
        Pdf
    }

    public class ReportResult : ActionResult
    {
        private string ReportName;
        private string ReportType;
        private string DeviceInfo;
        private string MimiType;
        private string Encoding;
        private string FileExt;
        private string[] Streams;
        private Warning[] Warnings;
        private byte[] RenderBytes;

        public ReportResult(ReportFormat format, string outReportName, string reportPath, ReportDataSource [] ds, SubreportProcessingEventHandler[] subReportProcessing = null)
        {
            var local = new LocalReport();
            local.ReportPath = reportPath;

            if (ds != null)
            {
                for(int i = 0 ; i < ds.Count() ; i++ )
                    local.DataSources.Add(ds[i]);
            }
            // подключение обработчиков вложенных отчетов
            if (subReportProcessing != null)
            {
                for (int i = 0; i < subReportProcessing.Count(); i++ )
                    local.SubreportProcessing += subReportProcessing[i];
            }
            
            ReportType = format.ToString();
            DeviceInfo = String.Empty;
            ReportName = outReportName;

            RenderBytes = local.Render(ReportType, DeviceInfo
                , out this.MimiType
                , out this.Encoding
                , out this.FileExt
                , out this.Streams
                , out this.Warnings
                );
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.Clear();
            response.ContentType = MimiType;
            response.AddHeader("content-disposition", String.Format("attachment;filename={0}.{1}", ReportName, FileExt));

            byte[] buf = new byte[32768];
            using (var spreadsheet = new System.IO.MemoryStream(RenderBytes))
            {
                while (true)
                {
                    int read = spreadsheet.Read(buf, 0, buf.Length);
                    if (read <= 0)
                        break;
                    response.OutputStream.Write(buf, 0, read);
                }
            }

            response.Flush();
        }

    }
}