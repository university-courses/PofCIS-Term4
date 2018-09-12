using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task1CS.Interfaces
{
    public interface IFileManager
    {
        void WriteFile(string fileName);
        void ReadFile(string fileName);
    }
}
