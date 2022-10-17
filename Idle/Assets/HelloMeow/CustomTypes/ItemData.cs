using System;

namespace HM.GameBase
{
    [Serializable]
    public struct ItemData
    {
        public int ItemId;
        public int Num;
        public ItemState State;

        public ItemData(int itemId, int num, ItemState state)
        {
            ItemId = itemId;
            Num = num;
            State = state;
        }

        public override string ToString()
        {
            return $"[ItemData: ItemId={ItemId}, Num={Num}, State={State}]";
        }
    }
}
