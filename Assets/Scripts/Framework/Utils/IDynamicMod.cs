using System;

namespace Modpack.Utils
{
	internal interface IDynamicMod : IMod
	{
		bool shouldUpdateUI { get; set; }

		bool shouldRefreshUI { get; set; }
	}
}
