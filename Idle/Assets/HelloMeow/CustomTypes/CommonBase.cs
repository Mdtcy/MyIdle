using System;

namespace HM.GameBase
{
    [System.Serializable]
    public class CommonBase : IDisposable
    {
        #region FIELDS
        protected bool _isDisposed = false;
        #endregion

        #region PROPERTIES
        #endregion

        #region PUBLIC METHODS

        //场景设施被删除时没有立即调用析构函数（在GC时才会调用）
        //注册的监听事件没有及时的撤销
        //导致再次进入场景时，被删除的设施还会再次加载
        //必须有一个强制析构的方法供外部调用
        //临时使用一个CommonBase时，可以使用using(CommonBase obj = new CommonBase()){/*do something*/}以保证用完后及时回收

        public void Dispose()
        {
            _DisposeMe();
            //通知GC不用调用自己的析构函数了
            GC.SuppressFinalize(this);
        }

        #endregion

        #region PROTECTED METHODS

        protected virtual void DisposeAssets()
        {
        }

        #endregion

        #region PRIVATE METHODS
        ~CommonBase()
        {
            _DisposeMe();
        }

        private void _DisposeMe()
        {
            if (!_isDisposed)
            {
                DisposeAssets();
            }
            _isDisposed = true;
        }

        #endregion

        #region STATIC METHODS
        #endregion
    }
}
