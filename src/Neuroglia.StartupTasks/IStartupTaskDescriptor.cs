using System;

namespace Neuroglia.StartupTasks
{

    /// <summary>
    /// Defines the fundamentals of an object used to describe an <see cref="IStartupTask"/>
    /// </summary>
    public interface IStartupTaskDescriptor
    {

        /// <summary>
        /// Gets the described <see cref="IStartupTask"/>
        /// </summary>
        IStartupTask Task { get; }

        /// <summary>
        /// Gets the <see cref="IStartupTask"/>'s name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the UTC date and time at which the <see cref="IStartupTask"/> has started
        /// </summary>
        DateTime StartedAt { get; }

        /// <summary>
        /// Gets the UTC date and time at which the <see cref="IStartupTask"/> has ended
        /// </summary>
        DateTime? EndedAt { get; }

        /// <summary>
        /// Gets the duration during which the <see cref="IStartupTask"/> has run
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// Marks the <see cref="IStartupTask"/> as completed
        /// </summary>
        void MarkAsCompleted();

    }

}
