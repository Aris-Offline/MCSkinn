using System.Collections.Generic;

namespace MCSkinn.Swatches
{
	public interface ISwatch : IList<NamedColor>
	{
		bool Dirty { get; }
		string Name { get; set; }
		string FilePath { get; set; }
		string Format { get; }

		void Save();
		void Load();
	}
}