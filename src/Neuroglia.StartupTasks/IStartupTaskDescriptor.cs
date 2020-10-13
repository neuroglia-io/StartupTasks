using System;

namespace Neuroglia.StartupTasks
{
    public interface IStartupTaskDescriptor
    {

        IStartupTask Task { get; }

        string Name { get; }

        DateTime StartedAt { get; }

        DateTime? EndedAt { get; }

        TimeSpan Duration { get; }

        void MarkAsCompleted();

    }

}
