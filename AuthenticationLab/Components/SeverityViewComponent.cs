using System;
using System.Linq;
using AuthenticationLab.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationLab.Components
{

    //SEVERITY COMPONENT TO BE DROPPED IN

    public class SeverityViewComponent : ViewComponent
    {
        private ICrashRepository repo { get; set; }

        public SeverityViewComponent(ICrashRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedSeverity = RouteData?.Values["CRASH_SEVERITY_ID"];

            var severity = repo.mytable
                .Select(x => x.CRASH_SEVERITY_ID)
                .Distinct()
                .OrderBy(x => x);

            return View(severity);
        }
    }
}
