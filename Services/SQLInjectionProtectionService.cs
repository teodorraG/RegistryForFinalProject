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
            if (input == null)
            {
                return false;
            }
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

        public bool HasMaliciousCharacters(List<string> ListOfInputs)
        {
            if (ListOfInputs == null)
            {
                return false;
            }
            char[] maliciousCharacters = new char[]
               {
                '<','>','%','"',';'
               };
            string[] maliciousWords = { "--", "xp_cmdshell", "drop", "update" };

            foreach (var item in ListOfInputs)
            {
                if (item != null)
                {
                    foreach (char c in item)
                    {
                        if (maliciousCharacters.Contains(c))
                        {
                            return true;
                        }
                    }
                    foreach (var w in maliciousWords)
                    {
                        if (item.ToLower().Contains(w))
                        {
                            return true;
                        }
                    }
                }
                
            }
            return false;
        }
    }
}
