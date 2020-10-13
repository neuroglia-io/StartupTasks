using System.Collections.Generic;
using System.Linq;

namespace Neuroglia.StartupTasks
{

    public class StartupTaskManager
        : IStartupTaskManager
    {

        private object _Lock = new object();

        private List<IStartupTaskDescriptor> _PendingTasks = new List<IStartupTaskDescriptor>();
        public IReadOnlyCollection<IStartupTaskDescriptor> PendingTasks
        {
            get
            {
                return this._PendingTasks.AsReadOnly();
            }
        }

        private List<IStartupTaskDescriptor> _CompletedTasks = new List<IStartupTaskDescriptor>();
        public IReadOnlyCollection<IStartupTaskDescriptor> CompletedTasks
        {
            get
            {
                return this._CompletedTasks.AsReadOnly();
            }
        }

        public bool TasksRanToCompletion
        {
            get
            {
                return !this._PendingTasks.Any();
            }
        }

        public void Register(IStartupTask task)
        {
            lock (this._Lock)
            {
                this._PendingTasks.Add(new StartupTaskDescriptor(task));
            }
        }

        public void MarkAsCompleted(IStartupTask task)
        {
            lock (this._Lock)
            {
                IStartupTaskDescriptor descriptor = this.PendingTasks.FirstOrDefault(t => t.Task == task);
                this._PendingTasks.Remove(descriptor);
                this._CompletedTasks.Add(descriptor);
            }
        }

    }

}
