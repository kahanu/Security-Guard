using System.Collections.Generic;

namespace SecurityGuard.ViewModels
{
    public class JsonResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string CssClass { get; set; }
        public bool Approved { get; set; }
        public string ApprovedStatus { get; set; }
        public bool Locked { get; set; }
        public string LockedStatus { get; set; }
        public List<ResponseItem> Messages { get; set; }
        public bool Exists { get; set; }
    }
}