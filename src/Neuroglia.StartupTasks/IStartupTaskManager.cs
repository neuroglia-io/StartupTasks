using System.Collections.Generic;

namespace Neuroglia.StartupTasks
{

    public interface IStartupTaskManager
    {

        IReadOnlyCollection<IStartupTaskDescriptor> PendingTasks { get; }

        IReadOnlyCollection<IStartupTaskDescriptor> CompletedTasks { get; }

        bool TasksRanToCompletion { get; }

        void Register(IStartupTask task);

        void MarkAsCompleted(IStartupTask task);

    }

}
