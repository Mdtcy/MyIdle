using HM.GameBase;

namespace NewLife.BusinessLogic.Item
{
	public partial class ConfigChecker : IConfigChecker
	{
		private const int Major101 = 101;
		private const int Minor101101 = 101;
		public bool IsItemTest(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major101) && ConfigCheckerBase.IsMinorType(itemId, Minor101101);}
		private const int Minor101102 = 102;
		public bool IsItemProfession(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major101) && ConfigCheckerBase.IsMinorType(itemId, Minor101102);}

	}
}