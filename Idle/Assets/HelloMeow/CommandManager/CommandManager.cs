using HM.Interface;
using Priority_Queue;

namespace HM.GameBase
{
    public class CommandManager : CommonBase, IHMPooledObject
    {
        #region FIELDS
        private readonly IPriorityQueue<ICommand, int> _cmdQueue;

        private string _name = "";

        private bool _pause = false;
        #endregion

        #region PROPERTIES

        public bool Pause
        {
            get => _pause;
            set
            {
                _pause = value;
                if (!_pause)
                {
                    // continue to execute next command.
                    ExecuteFirstCommand();
                }
            }
        }

        public bool Empty => _cmdQueue.Count <= 0;
        public int Count => _cmdQueue.Count;

        #endregion

        #region PUBLIC METHODS
        public void Clear()
        {
            foreach (var obj in _cmdQueue)
            {
                obj.Dispose();
            }
            _cmdQueue.Clear();
        }

        public CommandManager()
        {
            _cmdQueue = new SimplePriorityQueue<ICommand, int>();
        }
        
        public CommandManager(string name)
        {
            if (!string.IsNullOrEmpty(name)) _name = name;
            _cmdQueue = new SimplePriorityQueue<ICommand, int>();
        }

        public void Add(ICommand cmd)
        {
            // HMLog.LogDebug("[CommandManager({0})]::Add command: {1}", _name, cmd);
            _cmdQueue.Enqueue(cmd, cmd.Priority);
            if (!Pause)
            {
                Begin();
            }
        }

        public void Begin()
        {
            ExecuteFirstCommand();
        }
        #endregion

        #region PROTECTED METHODS
        protected void ExecuteFirstCommand()
        {
            if (_cmdQueue.Count <= 0)
            {
                HMLog.LogDebug("[CommandManager({0})]::全部指令执行完毕，空闲…", _name);
                return;
            }

            if (Pause)
            {
                HMLog.LogInfo($"[CommandManager({_name}) is pausing, wait for resume...");
                return;
            }

            ICommand cmd = _cmdQueue.First;

            if (cmd.IsExecuting)
            {
                // HMLog.LogDebug("[CommandManager({1})]::Busy! {0} cmds are waiting in line", _cmdQueue.Count, _name);
                return;
            }

            // HMLog.LogDebug("[CommandManager({1})]::Execute command: {0}", cmd, _name);

            cmd.OnDone += OnCommandExecuted;
            cmd.Execute();
        }

        protected void OnCommandExecuted(ICommand cmd)
        {
            // HMLog.LogDebug("[CommandManager({1})]::Done & delete command: {0}", cmd, _name);
            cmd.OnDone -= OnCommandExecuted;
            _cmdQueue.Dequeue().Dispose();
            ExecuteFirstCommand();
        }
        #endregion

        #region PRIVATE METHODS
        #endregion

        #region STATIC METHODS
        #endregion

        /// <inheritdoc />
        public void OnEnterPool()
        {
            foreach (var obj in _cmdQueue)
            {
                obj.Dispose();
            }
            _cmdQueue.Clear();
        }
    }
}
