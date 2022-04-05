using System;
namespace AuthenticationLab.Models.ViewModels
{
    public class PageInfo
    {
        public int TotalNumCrashes { get; set; }
        public int CrashesPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int startPage { get; set; }
        public int endPage { get; set; }
        public int LinksPerPage { get; set; }
        public string UrlParams { get; set; }

        //Num pages needed
        public int TotalPages => (int)Math.Ceiling((double)TotalNumCrashes / CrashesPerPage);
    }
}
