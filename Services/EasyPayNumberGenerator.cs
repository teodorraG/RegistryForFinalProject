using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegistryForFinalProject.Services
{
    public class EasyPayNumberGenerator
    {
        public string GenerateEasyPayNumber()
        {
            string easyPayNumber = string.Empty;
            for (int i = 0; i < 10; i++)
            {
                Random random = new Random();
                easyPayNumber += random.Next(0, 9).ToString();
            }
            return easyPayNumber;
        }
    }
}
