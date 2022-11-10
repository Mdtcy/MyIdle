/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月29日
 * @modify date 2022年10月29日
 * @desc [Game]
 */

#pragma warning disable 0649
using HM.GameBase;
using HM.Interface;
using Zenject;

namespace Game.Procedure
{
    public class GameProcedure : IInitializable
    {
        #region FIELDS

        [Inject]
        private IArchive archive;

        [Inject]
        private Inventory inventory;

        // todo
        [Inject]
        private PlayerAge.Age age;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        public void Initialize()
        {
            // todo  登录游戏
            string userName = "Editor1";

            // 注册需要存档的东西
            archive.Register(inventory);
            archive.Register(age);

            // 读取用户存档
            archive.Load(userName);
        }

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS

        #endregion

        #region STATIC METHODS

        #endregion
    }
}
#pragma warning restore 0649