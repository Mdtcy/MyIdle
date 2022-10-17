using System;
using HM.Interface;
namespace HM.GameBase
{
    public class CommandBase : ICommand, IHMPooledObject
    {
        #region FIELDS
        private static int _idx;
        #endregion

        #region PROPERTIES
        public int Id { get; private set; }
        public string Name { get; set; }
        public string Para { get; private set; }
        public int Priority { get; set; }
        public event Action<ICommand> OnDone;
        public bool IsExecuting { get; protected set; }
        #endregion

        #region PUBLIC METHODS
        public virtual void Execute()
        {
            IsExecuting = true;
        }

        public CommandBase()
        {
            Id = ++_idx;
            Priority = 1; // default value
            OnDone += (obj) => { };
            IsExecuting = false;
        }
        #endregion

        #region PROTECTED METHODS
        protected void Done()
        {
            IsExecuting = false;
            OnDone?.Invoke(this);
        }
        #endregion

        #region PRIVATE METHODS
        #endregion

        #region STATIC METHODS
        static CommandBase()
        {
            _idx = 10000;
        }
        #endregion

        #region IDisposable

        /// <inheritdoc />
        public virtual void Dispose()
        {
            var inst = this;
            ObjectPool<CommandBase>.Release(ref inst);
        }

        #endregion

        #region IHMPooledObject

        /// <inheritdoc />
        public void OnEnterPool()
        {
            
        }

        #endregion
    }
}
