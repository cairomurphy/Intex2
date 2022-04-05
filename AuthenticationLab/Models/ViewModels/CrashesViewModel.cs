using System;
using System.Linq;

namespace AuthenticationLab.Models.ViewModels
{
    public class CrashesViewModel
    {
        public IQueryable<Crash> mytable { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
