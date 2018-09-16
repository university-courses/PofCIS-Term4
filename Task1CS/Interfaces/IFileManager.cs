using System.IO;

namespace Task1CS.Interfaces
{
	public interface IFileManager
	{
		void WriteToStream(ref StreamWriter streamWriter);
		bool ReadFromStream(ref StreamReader streamReader);
	}
}
