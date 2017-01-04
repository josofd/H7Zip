namespace H7Zip
{
    public enum ExitCodeType
    {
        Success = 0,
        Warning = 1,
        FatalError = 2,
        CommandLineError = 7,
        NotEnoughMemoryError = 8,
        UserStoppedProcessingError = 255
    }

    public enum ZipType
    {
        Zip = 1,
        Unzip = 2
    }
}
