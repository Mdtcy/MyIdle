using HM.GameBase;

namespace NewLife.BusinessLogic.Item
{
	public partial class ConfigChecker : IConfigChecker
	{
		private const int Major111 = 111;
		private const int Minor111211 = 211;
		public bool IsItemTest(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major111) && ConfigCheckerBase.IsMinorType(itemId, Minor111211);}

	}
}