using AppQuanLyKhachSan;

namespace TrangChuQLKS
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //Application.Run(new TrangChu());
            //Application.Run(new TrangChu_receptionist());
            Application.Run(new LapPTP());
        }
    }
}