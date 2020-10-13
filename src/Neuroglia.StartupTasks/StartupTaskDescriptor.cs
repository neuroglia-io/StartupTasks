using System;

namespace Neuroglia.StartupTasks
{
    public class StartupTaskDescriptor
        : IStartupTaskDescriptor
    {

        public StartupTaskDescriptor(IStartupTask task)
        {
            this.Task = task;
            this.Name = task.GetType().Name.Replace("StartupTask", "").Replace("Task", "");
            this.StartedAt = DateTime.UtcNow;
        }

        public IStartupTask Task { get; }

        public string Name { get; }

        public DateTime StartedAt { get; }

        public DateTime? EndedAt { get; protected set; }

        public TimeSpan Duration
        {
            get
            {
                if (this.EndedAt.HasValue)
                    return this.EndedAt.Value - this.StartedAt;
                return DateTime.UtcNow - this.StartedAt;
            }
        }

        public void MarkAsCompleted()
        {
            this.EndedAt = DateTime.UtcNow;
        }

    }

}
