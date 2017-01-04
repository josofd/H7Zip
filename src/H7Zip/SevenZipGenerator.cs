using System;
using System.IO;
using System.Reflection;

namespace H7Zip
{
    public class SevenZipGenerator
    {
        public const string Unzip_Command = @"x -y {1} -o{2} -aoa -mmt";
        public const string Zip_Command = @"a -v{1}m {2} {3} -mx9 -mmt";

        public string ExeFileName { get; private set; }

        public SevenZipGenerator()
        {
            ExeFileName = Path.Combine(Path.GetTempPath(), Path.GetFileNameWithoutExtension(Path.GetTempFileName()) + ".exe");
        }

        public ExitCodeType Execute(Options options)
        {
            try
            {
                if (options.Type == ZipType.Zip)
                    RemoveOldFiles(options.Input);

                var zipCommand = Zip_Command;
                var unzipCommand = Unzip_Command;
                if (!string.IsNullOrWhiteSpace(options.Password))
                {
                    zipCommand = Zip_Command + " -p" + options.Password;
                    unzipCommand = Unzip_Command + " -p" + options.Password;
                }

                var lineParams = string.Format(zipCommand, ExeFileName, options.SplitInSize, options.Input, options.OutputDiretory);
                if (options.Type == ZipType.Unzip)
                    lineParams = string.Format(unzipCommand, ExeFileName, options.Input, options.OutputDiretory);

                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "H7Zip.7za.exe";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                        return ExitCodeType.CommandLineError;

                    using (FileStream fileStream = System.IO.File.Create(ExeFileName, (int)stream.Length))
                    {
                        byte[] bytesInStream = new byte[stream.Length];
                        stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                        fileStream.Write(bytesInStream, 0, bytesInStream.Length);
                    }
                }

                return (ExitCodeType)ExternalApplication.RunCommandLine(ExeFileName, lineParams);
            }
            catch (Exception)
            {
                return ExitCodeType.CommandLineError;
            }
            finally
            {
                if (File.Exists(ExeFileName))
                    File.Delete(ExeFileName);
            }
        }

        private void RemoveOldFiles(string inputFileName)
        {
            foreach (var file in Directory.EnumerateFiles(new FileInfo(inputFileName).Directory.FullName, "*.7z.*", SearchOption.TopDirectoryOnly))
            {
                File.Delete(file);
            }
        }
    }
}
