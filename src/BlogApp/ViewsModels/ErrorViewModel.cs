namespace BlogApp.ViewsModels
{
    public class ErrorViewModel
    {   
        public string? Title { get; set; }

        public string? Message { get; set; }
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
