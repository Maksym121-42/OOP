using System;

namespace AnalyticsBridgeDemo
{
 
    public interface IDisplayMethod
    {
        void Display(string dataType, string data);
    }

    public class WebDashboardDisplay : IDisplayMethod
    {
        public void Display(string dataType, string data)
        {
            Console.WriteLine($"[Web Dashboard] {dataType}: {data}");
        }
    }

    public class MobileAppDisplay : IDisplayMethod
    {
        public void Display(string dataType, string data)
        {
            Console.WriteLine($"[Mobile App] {dataType}: {data}");
        }
    }

    public class PdfExportDisplay : IDisplayMethod
    {
        public void Display(string dataType, string data)
        {
            Console.WriteLine($"[PDF Export] {dataType}: {data}");
        }
    }
    
    public abstract class AnalyticsData
    {
        protected IDisplayMethod displayMethod;

        protected AnalyticsData(IDisplayMethod displayMethod)
        {
            this.displayMethod = displayMethod;
        }

        public abstract string DataType { get; }
        public abstract string GetData();

        public void Show()
        {
            displayMethod.Display(DataType, GetData());
        }
    }

    public class FinancialData : AnalyticsData
    {
        public FinancialData(IDisplayMethod displayMethod) : base(displayMethod) { }

        public override string DataType => "Фінансові показники";
        public override string GetData() => "Дохід: 120000, Витрати: 85000";
    }

    public class UserMetricsData : AnalyticsData
    {
        public UserMetricsData(IDisplayMethod displayMethod) : base(displayMethod) { }

        public override string DataType => "Метрики користувачів";
        public override string GetData() => "DAU: 3400, Retention: 41%";
    }

    public class LogsData : AnalyticsData
    {
        public LogsData(IDisplayMethod displayMethod) : base(displayMethod) { }

        public override string DataType => "Логи";
        public override string GetData() => "ERROR: 12, WARN: 48";
    }

    class Pr_Lab3_6
    {
        static void Main()
        {
         
            AnalyticsData financialWeb = new FinancialData(new WebDashboardDisplay());
            AnalyticsData userMobile = new UserMetricsData(new MobileAppDisplay());
            AnalyticsData logsPdf = new LogsData(new PdfExportDisplay());

            financialWeb.Show();
            userMobile.Show();
            logsPdf.Show();

            Console.ReadLine();
        }
    }
}
