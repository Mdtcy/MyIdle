using System;

namespace HM.Interface
{
    public interface ICommand : IDisposable
    {
        /// <summary>
        /// Gets or sets the priority. Greater value means lower priority.
        /// Default value is 1, priority must greater than zero.
        /// </summary>
        /// <value>The priority.</value>
        int Priority { get; set; }

        /// <summary>
        /// Called after command is executed.
        /// </summary>
        /// <value>The on done.</value>
        event Action<ICommand> OnDone;

        /// <summary>
        /// Command id.
        /// </summary>
        /// <value>The identifier.</value>
        int Id { get; }

        /// <summary>
        /// Whether the command is executing
        /// </summary>
        /// <value><c>true</c> if is executing; otherwise, <c>false</c>.</value>
        bool IsExecuting { get; }

        /// <summary>
        /// Execute command.
        /// </summary>
        void Execute();

        /// <summary>
        /// command nickname
        /// </summary>
        string Name { get; }

        string Para { get; }
    }
}
