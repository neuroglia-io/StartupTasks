using System.Collections.Generic;

namespace Neuroglia.StartupTasks
{

    /// <summary>
    /// Defines the fundamentals of a service used to manage <see cref="IStartupTask"/>s
    /// </summary>
    public interface IStartupTaskManager
    {

        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing all pending <see cref="IStartupTask"/>s
        /// </summary>
        IReadOnlyCollection<IStartupTaskDescriptor> PendingTasks { get; }

        /// <summary>
        /// Gets an <see cref="IReadOnlyCollection{T}"/> containing all completed <see cref="IStartupTask"/>s
        /// </summary>
        IReadOnlyCollection<IStartupTaskDescriptor> CompletedTasks { get; }

        /// <summary>
        /// Gets a boolean indicating whether or not <see cref="IStartupTask"/>s have run to completion
        /// </summary>
        bool TasksRanToCompletion { get; }

        /// <summary>
        /// Registers the specified <see cref="IStartupTask"/>
        /// </summary>
        /// <param name="task">The <see cref="IStartupTask"/> to register</param>
        void Register(IStartupTask task);

        /// <summary>
        /// Marks the specified <see cref="IStartupTask"/> as completed
        /// </summary>
        /// <param name="task">The <see cref="IStartupTask"/> to mark as completed</param>
        void MarkAsCompleted(IStartupTask task);

    }

}
