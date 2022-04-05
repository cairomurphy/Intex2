using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationLab.Models
{
    public class EFCrashRepository : ICrashRepository
    {
        private CrashDbContext _context { get; set; }
        public EFCrashRepository(CrashDbContext temp)
        {
            _context = temp;
        }
        public IQueryable<Crash> mytable => _context.mytable;
        public IQueryable<Severity> Severities => _context.Severities;

        public void CreateCrash(Crash c)
        {
            _context.Add(c);
            _context.SaveChanges();
        }

        public void DeleteCrash(Crash c)
        {
            _context.Remove(c);
            _context.SaveChanges();
        }

        public void SaveCrash(Crash c)
        {
            _context.SaveChanges();
        }

        public void UpdateCrash(Crash c)
        {
            _context.Update(c);
            _context.SaveChanges();
        }

        public void SaveSeverity(Severity severity)
        {
            if (severity.CRASH_SEVERITY_ID == 0)
            {
                _context.Severities.Add(severity);
            }

            _context.SaveChanges();
        }
    }
}
