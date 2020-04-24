using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Services
{
    public class SQLInjectionProtectionService
    {
        public bool HasMaliciousCharacters(string input)
        {
            char[] maliciousCharacters = new char[]
            {
                '<','>','%','"',';'
            };
            string[] maliciousWords = { "--", "xp_cmdshell", "drop", "update" };
            foreach (char c in input)
            {
                if (maliciousCharacters.Contains(c))
                {
                    return true;
                }
            }
            foreach (var w in maliciousWords)
            {
                if (input.ToLower().Contains(w))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
