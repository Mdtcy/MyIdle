/**
 * @author BoLuo
 * @email [ tktetb@163.com ]
 * @create date  2022年10月18日
 * @modify date 2022年10月18日
 * @desc [任务组UI]
 */

#pragma warning disable 0649
using _Game.Scripts.UI.MainMenu;
using DefaultNamespace.Test;
using HM;
using HM.GameBase;
using HM.Interface;
using NewLife.Config;
using NewLife.Config.Helper;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.UI
{
    public class Test : MonoBehaviour
    {
        #region FIELDS

        // [Inject]
        // private IConfigGetter configGetter;

        [Inject]
        private ConfigCollectionFactory configCollectionFactory;

        [Inject]
        private IArchive archive;

        [Inject]
        private IItemUpdater itemUpdater;

        [Inject]
        private MainMenuController mainMenuController;

        #endregion

        #region PROPERTIES

        #endregion

        #region PUBLIC METHODS

        [Button]
        public void ShowMenu()
        {
            mainMenuController.Show();
        }

        [Button]
        public void 打印test()
        {
            using (var col = configCollectionFactory.CreateConfigCollection<TestConfig>())
            {
                foreach (var test in col)
                {
                    HMLog.LogDebug($"var {test} ");
                }
            }
        }

        [Button]
        public void 打印Itemtest()
        {
            var test = itemUpdater.GetOrAddItem<ItemTest>(te.Id);
            HMLog.LogDebug($"var {test} {test.score}");
        }

        [SerializeField]
        private TestConfig te;


        [Button]
        public void 改Item(int value)
        {
            var test = itemUpdater.GetOrAddItem<ItemTest>(te.Id);
            test.score = value;
        }


        [Button]
        public void 存档()
        {
            archive.Save();
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