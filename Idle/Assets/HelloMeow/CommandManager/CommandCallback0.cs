using System;

namespace HM.GameBase
{
    public class CommandCallback0 : CommandBase
    {
        #region FIELDS
        #endregion

        #region PROPERTIES

        /// <summary>
        /// Passed-in action being called indicates the command has been really executed.
        /// </summary>
        /// <value>The callback.</value>
        public Action<Action> Callback { get; set; }
        #endregion

        #region PUBLIC METHODS
        public override void Execute()
        {
            base.Execute();
            if (Callback != null)
            {
                Callback(Done);
            }
        }

        public override string ToString()
        {
            return string.Format("[CommandCallback0: Id={0} Priority={1} Name={2}]", Id, Priority, Name);
        }

        /// <summary>
        /// 链式调用，设置回调
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public CommandCallback0 SetCallback(Action<Action> callback)
        {
            Callback = callback;
            return this;
        }

        #endregion

        #region PROTECTED METHODS
        #endregion

        #region PRIVATE METHODS
        #endregion

        #region STATIC METHODS
        #endregion

        #region IDisposable

        /// <inheritdoc />
        public override void Dispose()
        {
            var inst = this;
            ObjectPool<CommandCallback0>.Release(ref inst);
        }

        #endregion
    }
}
