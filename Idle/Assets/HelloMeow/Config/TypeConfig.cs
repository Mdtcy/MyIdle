using UnityEngine;

namespace HM.ConfigTool
{
	public class TypeConfig : ScriptableObject
	{
		public int MajorType;
		public int MinorType;
		public string ConfigName;
		public string InheritFrom;
		public string Note;

		TypeConfig()
		{
			ConfigName = string.Empty;
			InheritFrom = "BaseConfig";
			Note = string.Empty;
		}
	}
}