using System;

namespace Neuroglia.StartupTasks
{

    /// <summary>
    /// Represents the default implementation of the <see cref="IStartupTaskDescriptor"/> interface
    /// </summary>
    public class StartupTaskDescriptor
        : IStartupTaskDescriptor
    {

        /// <summary>
        /// Initializes a new <see cref="StartupTaskDescriptor"/>
        /// </summary>
        /// <param name="task">The <see cref="IStartupTask"/> to describe</param>
        public StartupTaskDescriptor(IStartupTask task)
        {
            this.Task = task;
            this.Name = task.GetType().Name.Replace("StartupTask", "").Replace("Task", "");
            this.StartedAt = DateTime.UtcNow;
        }

        /// <inheritdoc/>
        public IStartupTask Task { get; }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public DateTime StartedAt { get; }

        /// <inheritdoc/>
        public DateTime? EndedAt { get; protected set; }

        /// <inheritdoc/>
        public TimeSpan Duration
        {
            get
            {
                if (this.EndedAt.HasValue)
                    return this.EndedAt.Value - this.StartedAt;
                return DateTime.UtcNow - this.StartedAt;
            }
        }

        /// <inheritdoc/>
        public void MarkAsCompleted()
        {
            this.EndedAt = DateTime.UtcNow;
        }

    }

}
