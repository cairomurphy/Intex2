using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationLab.Models
{
    public interface ICrashRepository
    {
        IQueryable<Crash> mytable { get; }
       

        public void SaveCrash(Crash c);
        public void CreateCrash(Crash c);
        public void DeleteCrash(Crash c);
        public void UpdateCrash(Crash c);

        
    }
}
