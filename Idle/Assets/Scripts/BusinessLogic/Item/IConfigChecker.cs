namespace NewLife.BusinessLogic.Item
{
	public partial interface IConfigChecker
	{
		bool IsItemApp(int itemId);
		bool IsItemDesktop(int itemId);
		bool IsItemAppIcon(int itemId);
		bool IsItemContact(int itemId);
		bool IsItemContactProperty(int itemId);
		bool IsItemPlayer(int itemId);
		bool IsItemDetective(int itemId);
		bool IsItemLead(int itemId);
		bool IsItemTimeline(int itemId);
		bool IsItemReplyChoice(int itemId);
		bool IsItemReplyText(int itemId);
		bool IsItemReplyImage(int itemId);
		bool IsItemLocation(int itemId);
		bool IsItemReplyForest(int itemId);
		bool IsItemReplyPack(int itemId);
		bool IsItemTimelineChannelSet(int itemId);
		bool IsItemNewbieGuide(int itemId);
		bool IsItemChat(int itemId);
		bool IsItemSpecialChat(int itemId);
		bool IsItemCurrency(int itemId);
		bool IsItemHp(int itemId);
		bool IsItemSchedule(int itemId);
		bool IsItemFileItem(int itemId);
		bool IsItemFileImage(int itemId);
		bool IsItemMemory(int itemId);
		bool IsItemMusic(int itemId);
		bool IsItemEffect(int itemId);
		bool IsItemShopCommodity(int itemId);
		bool IsItemInfiniteHpCommodity(int itemId);
		bool IsItemRecoverConstantHPCommodity(int itemId);
		bool IsItemGiftCommodity(int itemId);
		bool IsItemAddCurrencyCommodity(int itemId);
		bool IsItemExtendedCommodity(int itemId);
		bool IsItemUnlockItemCommodity(int itemId);
		bool IsItemJumpToShoppingLink(int itemId);
		bool IsItemBranch(int itemId);
		bool IsItemWeather(int itemId);
		bool IsItemAds(int itemId);
		bool IsItemIllustration(int itemId);
		bool IsItemStory(int itemId);
		bool IsItemFlower(int itemId);
		bool IsItemDressUp(int itemId);
		bool IsItemDressUpItem(int itemId);
		bool IsItemLine(int itemId);
		bool IsItemExpression(int itemId);
		bool IsItemContactBackground(int itemId);
		bool IsItemCloth(int itemId);
		bool IsItemRecordedUtterance(int itemId);
		bool IsItemProp(int itemId);
		bool IsItemVentProp(int itemId);
		bool IsItemUnblockUser(int itemId);
		bool IsItemSecret(int itemId);

	}
}