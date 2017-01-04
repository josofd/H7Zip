using System;

namespace H7Zip
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            FormDialog dialog = new FormDialog();
            dialog.ShowDialog();

            var options = dialog.Options;

            if (string.IsNullOrWhiteSpace(options.Input) || string.IsNullOrWhiteSpace(options.OutputDiretory))
                return;

            new SevenZipGenerator().Execute(options);
        }
    }
}
