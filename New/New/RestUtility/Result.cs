namespace New.RestUtility
{
    public class Result<T>
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public bool IsZipData { get; set; }
        public T Data { get; set; }
    }
}
