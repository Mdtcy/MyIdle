using HM.GameBase;

namespace NewLife.BusinessLogic.Item
{
	public partial class ConfigChecker : IConfigChecker
	{
		private const int Major101 = 101;
		private const int Minor101001 = 1;
		public bool IsItemApp(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major101) && ConfigCheckerBase.IsMinorType(itemId, Minor101001);}
		private const int Minor101002 = 2;
		public bool IsItemDesktop(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major101) && ConfigCheckerBase.IsMinorType(itemId, Minor101002);}
		private const int Minor101003 = 3;
		public bool IsItemAppIcon(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major101) && ConfigCheckerBase.IsMinorType(itemId, Minor101003);}
		private const int Major102 = 102;
		private const int Minor102001 = 1;
		public bool IsItemContact(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major102) && ConfigCheckerBase.IsMinorType(itemId, Minor102001);}
		private const int Minor102002 = 2;
		public bool IsItemContactProperty(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major102) && ConfigCheckerBase.IsMinorType(itemId, Minor102002);}
		private const int Minor102003 = 3;
		public bool IsItemPlayer(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major102) && ConfigCheckerBase.IsMinorType(itemId, Minor102003);}
		private const int Minor102004 = 4;
		public bool IsItemDetective(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major102) && ConfigCheckerBase.IsMinorType(itemId, Minor102004);}
		private const int Major103 = 103;
		private const int Minor103001 = 1;
		public bool IsItemLead(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major103) && ConfigCheckerBase.IsMinorType(itemId, Minor103001);}
		private const int Major104 = 104;
		private const int Minor104001 = 1;
		public bool IsItemTimeline(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major104) && ConfigCheckerBase.IsMinorType(itemId, Minor104001);}
		private const int Minor104002 = 2;
		public bool IsItemReplyChoice(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major104) && ConfigCheckerBase.IsMinorType(itemId, Minor104002);}
		private const int Minor104003 = 3;
		public bool IsItemReplyText(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major104) && ConfigCheckerBase.IsMinorType(itemId, Minor104003);}
		private const int Minor104004 = 4;
		public bool IsItemReplyImage(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major104) && ConfigCheckerBase.IsMinorType(itemId, Minor104004);}
		private const int Minor104005 = 5;
		public bool IsItemLocation(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major104) && ConfigCheckerBase.IsMinorType(itemId, Minor104005);}
		private const int Minor104006 = 6;
		public bool IsItemReplyForest(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major104) && ConfigCheckerBase.IsMinorType(itemId, Minor104006);}
		private const int Minor104007 = 7;
		public bool IsItemReplyPack(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major104) && ConfigCheckerBase.IsMinorType(itemId, Minor104007);}
		private const int Minor104008 = 8;
		public bool IsItemTimelineChannelSet(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major104) && ConfigCheckerBase.IsMinorType(itemId, Minor104008);}
		private const int Major106 = 106;
		private const int Minor106002 = 2;
		public bool IsItemNewbieGuide(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major106) && ConfigCheckerBase.IsMinorType(itemId, Minor106002);}
		private const int Major107 = 107;
		private const int Minor107001 = 1;
		public bool IsItemChat(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major107) && ConfigCheckerBase.IsMinorType(itemId, Minor107001);}
		private const int Minor107002 = 2;
		public bool IsItemSpecialChat(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major107) && ConfigCheckerBase.IsMinorType(itemId, Minor107002);}
		private const int Major108 = 108;
		private const int Minor108001 = 1;
		public bool IsItemCurrency(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major108) && ConfigCheckerBase.IsMinorType(itemId, Minor108001);}
		private const int Minor108002 = 2;
		public bool IsItemHp(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major108) && ConfigCheckerBase.IsMinorType(itemId, Minor108002);}
		private const int Major109 = 109;
		private const int Minor109001 = 1;
		public bool IsItemSchedule(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major109) && ConfigCheckerBase.IsMinorType(itemId, Minor109001);}
		private const int Major113 = 113;
		private const int Minor113001 = 1;
		public bool IsItemFileItem(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major113) && ConfigCheckerBase.IsMinorType(itemId, Minor113001);}
		private const int Minor113002 = 2;
		public bool IsItemFileImage(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major113) && ConfigCheckerBase.IsMinorType(itemId, Minor113002);}
		private const int Major114 = 114;
		private const int Minor114001 = 1;
		public bool IsItemMemory(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major114) && ConfigCheckerBase.IsMinorType(itemId, Minor114001);}
		private const int Major115 = 115;
		private const int Minor115001 = 1;
		public bool IsItemMusic(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major115) && ConfigCheckerBase.IsMinorType(itemId, Minor115001);}
		private const int Minor115002 = 2;
		public bool IsItemEffect(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major115) && ConfigCheckerBase.IsMinorType(itemId, Minor115002);}
		private const int Major116 = 116;
		private const int Minor116001 = 1;
		public bool IsItemShopCommodity(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major116) && ConfigCheckerBase.IsMinorType(itemId, Minor116001);}
		private const int Minor116002 = 2;
		public bool IsItemInfiniteHpCommodity(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major116) && ConfigCheckerBase.IsMinorType(itemId, Minor116002);}
		private const int Minor116003 = 3;
		public bool IsItemRecoverConstantHPCommodity(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major116) && ConfigCheckerBase.IsMinorType(itemId, Minor116003);}
		private const int Minor116004 = 4;
		public bool IsItemGiftCommodity(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major116) && ConfigCheckerBase.IsMinorType(itemId, Minor116004);}
		private const int Minor116005 = 5;
		public bool IsItemAddCurrencyCommodity(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major116) && ConfigCheckerBase.IsMinorType(itemId, Minor116005);}
		private const int Minor116006 = 6;
		public bool IsItemExtendedCommodity(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major116) && ConfigCheckerBase.IsMinorType(itemId, Minor116006);}
		private const int Minor116007 = 7;
		public bool IsItemUnlockItemCommodity(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major116) && ConfigCheckerBase.IsMinorType(itemId, Minor116007);}
		private const int Major117 = 117;
		private const int Minor117001 = 1;
		public bool IsItemJumpToShoppingLink(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major117) && ConfigCheckerBase.IsMinorType(itemId, Minor117001);}
		private const int Major118 = 118;
		private const int Minor118001 = 1;
		public bool IsItemBranch(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major118) && ConfigCheckerBase.IsMinorType(itemId, Minor118001);}
		private const int Major119 = 119;
		private const int Minor119001 = 1;
		public bool IsItemWeather(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major119) && ConfigCheckerBase.IsMinorType(itemId, Minor119001);}
		private const int Major120 = 120;
		private const int Minor120001 = 1;
		public bool IsItemAds(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major120) && ConfigCheckerBase.IsMinorType(itemId, Minor120001);}
		private const int Major121 = 121;
		private const int Minor121001 = 1;
		public bool IsItemIllustration(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major121) && ConfigCheckerBase.IsMinorType(itemId, Minor121001);}
		private const int Major122 = 122;
		private const int Minor122001 = 1;
		public bool IsItemStory(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major122) && ConfigCheckerBase.IsMinorType(itemId, Minor122001);}
		private const int Major123 = 123;
		private const int Minor123001 = 1;
		public bool IsItemFlower(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major123) && ConfigCheckerBase.IsMinorType(itemId, Minor123001);}
		private const int Major125 = 125;
		private const int Minor125001 = 1;
		public bool IsItemDressUp(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major125) && ConfigCheckerBase.IsMinorType(itemId, Minor125001);}
		private const int Major129 = 129;
		private const int Minor129001 = 1;
		public bool IsItemDressUpItem(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major129) && ConfigCheckerBase.IsMinorType(itemId, Minor129001);}
		private const int Minor129002 = 2;
		public bool IsItemLine(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major129) && ConfigCheckerBase.IsMinorType(itemId, Minor129002);}
		private const int Minor129003 = 3;
		public bool IsItemExpression(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major129) && ConfigCheckerBase.IsMinorType(itemId, Minor129003);}
		private const int Minor129004 = 4;
		public bool IsItemContactBackground(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major129) && ConfigCheckerBase.IsMinorType(itemId, Minor129004);}
		private const int Minor129005 = 5;
		public bool IsItemCloth(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major129) && ConfigCheckerBase.IsMinorType(itemId, Minor129005);}
		private const int Major130 = 130;
		private const int Minor130001 = 1;
		public bool IsItemRecordedUtterance(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major130) && ConfigCheckerBase.IsMinorType(itemId, Minor130001);}
		private const int Major131 = 131;
		private const int Minor131001 = 1;
		public bool IsItemProp(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major131) && ConfigCheckerBase.IsMinorType(itemId, Minor131001);}
		private const int Minor131002 = 2;
		public bool IsItemVentProp(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major131) && ConfigCheckerBase.IsMinorType(itemId, Minor131002);}
		private const int Major132 = 132;
		private const int Minor132001 = 1;
		public bool IsItemUnblockUser(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major132) && ConfigCheckerBase.IsMinorType(itemId, Minor132001);}
		private const int Major133 = 133;
		private const int Minor133001 = 1;
		public bool IsItemSecret(int itemId){return ConfigCheckerBase.IsMajorType(itemId, Major133) && ConfigCheckerBase.IsMinorType(itemId, Minor133001);}

	}
}