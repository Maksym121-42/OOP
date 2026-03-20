using System;

namespace Lab2FactoryMethod
{
    enum ExportFormat
    {
        Pdf,
        Excel,
        Html,
        Json
    }

    
    interface IReportExporter
    {
        void Export(string reportData);
    }

    // Конкретні експортери
    class PdfExporter : IReportExporter
    {
        public void Export(string reportData)
        {
            Console.WriteLine($"[PDF] Звіт збережено: {reportData}");
        }
    }

    class ExcelExporter : IReportExporter
    {
        public void Export(string reportData)
        {
            Console.WriteLine($"[Excel] Звіт збережено: {reportData}");
        }
    }

    class HtmlExporter : IReportExporter
    {
        public void Export(string reportData)
        {
            Console.WriteLine($"[HTML] Звіт збережено: {reportData}");
        }
    }

    class JsonExporter : IReportExporter
    {
        public void Export(string reportData)
        {
            Console.WriteLine($"[JSON] Звіт збережено: {reportData}");
        }
    }

    // Фабрика (створює об'єкт залежно від налаштування)
    static class ReportExporterFactory
    {
        public static IReportExporter Create(ExportFormat format)
        {
            switch (format)
            {
                case ExportFormat.Pdf: return new PdfExporter();
                case ExportFormat.Excel: return new ExcelExporter();
                case ExportFormat.Html: return new HtmlExporter();
                case ExportFormat.Json: return new JsonExporter();
                default: throw new ArgumentException("Невідомий формат");
            }
        }
    }
    
    class ReportService
    {
        private readonly ExportFormat _format;

        public ReportService(ExportFormat format)
        {
            _format = format;
        }

        public void SaveReport(string data)
        {
            IReportExporter exporter = ReportExporterFactory.Create(_format);
            exporter.Export(data);
        }
    }

    class Pr_Lab2_5
    {
        static void Main()
        {
            // Налаштування (можна змінити на Pdf/Excel/Html/Json)
            ExportFormat formatFromSettings = ExportFormat.Json;

            ReportService service = new ReportService(formatFromSettings);
            service.SaveReport("Звіт");

            Console.ReadKey();
        }
    }
}
