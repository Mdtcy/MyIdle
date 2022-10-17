using System;

namespace HM.GameBase
{
    public class CommandCallback2<T1, T2> : CommandBase
    {
        #region FIELDS
        #endregion

        #region PROPERTIES

        /// <summary>
        /// Passed-in action being called indicates the command has been really executed.
        /// </summary>
        /// <value>The callback.</value>
        public Action<T1, T2, Action> Callback { get; set; }

        public T1 param1 { get; set; }
        public T2 param2 { get; set; }
        #endregion

        #region PUBLIC METHODS
        public override void Execute()
        {
            base.Execute();
            if (Callback != null)
            {
                Callback(param1, param2, Done);
            }
        }

        public override string ToString()
        {
            return string.Format("[CommandCallback1: Id={0} Priority={1} Name={2}]", Id, Priority, Name);
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