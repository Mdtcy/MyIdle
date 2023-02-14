using UnityEngine;

namespace IdleGame.Buff
{
    [CreateAssetMenu(fileName = "BuffConfig", menuName = "BuffConfig", order = 0)]
    public class BuffConfig : ScriptableObject
    {
        ///<summary>
        ///buff的名称
        ///</summary>
        public string Name;

        ///<summary>
        ///buff堆叠的规则中需要的层数，在这个游戏里只要id和caster相同的buffObj就可以堆叠
        ///激战2里就不同，尽管图标显示堆叠，其实只是统计了有多少个相同id的buffObj作为层数显示了
        ///</summary>
        public int MaxStack;

        ///<summary>
        ///buff的tag
        ///</summary>
        public string[] Tags;
    }
}