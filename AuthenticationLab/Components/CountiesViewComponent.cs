using System;
using System.Linq;
using AuthenticationLab.Models;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationLab.Components
{
    public class CountiesViewComponent : ViewComponent 
    {
        private ICrashRepository repo { get; set; }

        public CountiesViewComponent (ICrashRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCounty = RouteData?.Values["COUNTY_NAME"];

            var counties = repo.mytable
                .Select(x => x.COUNTY_NAME)
                .Distinct()
                .OrderBy(x => x);

            return View(counties);
        }
    }
}
