/**
 * @author [jie.wen]
 * @email [example@mail.com]
 * @create date 2019-04-12 14:19:38
 * @modify date 2019-04-12 14:19:38
 * @desc [用户信息]
 */

namespace HM.GameBase
{
    [System.Serializable]
    public class User : CommonBase
    {
        #region FIELDS
        [UnityEngine.SerializeField]
        private string _uid;
        #endregion

        #region PROPERTIES
        public string Id => _uid;
        public bool IsDirty{ get; set; }

        #endregion

        #region PUBLIC METHODS

        public override string ToString()
        {
            return $"[User uid={_uid}";
        }

        public User(string uid)
        {
            _uid = uid;
        }

        public User()
        {
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