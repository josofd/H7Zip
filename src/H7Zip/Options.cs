namespace H7Zip
{
    public class Options
    {
        public string Input { get; set; }
        public string OutputDiretory { get; set; }
        public ZipType Type { get; set; }
        public int SplitInSize { get; set; }
        public string Password { get; set; }
    }
}
