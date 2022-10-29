namespace NewLife.BusinessLogic.Item
{
	public partial interface IConfigChecker
	{
		bool IsItemTest(int itemId);
		bool IsItemProfession(int itemId);

	}
}