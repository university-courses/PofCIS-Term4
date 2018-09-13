using System.IO;

namespace Task1CS.Interfaces
{
	public interface IFileManager
	{
		void WriteToFile(ref StreamWriter streamWriter);
		void ReadFromFile(ref StreamReader streamReader);
	}
}
