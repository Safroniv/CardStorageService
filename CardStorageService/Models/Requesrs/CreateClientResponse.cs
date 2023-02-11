namespace CardStorageService.Models.Requesrs
{
    public class CreateClientResponse : IOperationResult
    {
        public int? ClienId { get; set; }
        public int ErrorCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
