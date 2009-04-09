using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ActiveObjects;
using ActiveObjects.SqlServer;
using System.Reflection;
using System.Timers;

namespace Mubble.Models
{

	public partial class ScheduledCommand
	{
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static bool running = false;

        public static Timer Timer = new Timer(30000);
        private IActiveObject appliesTo;
        public IActiveObject AppliesTo
        {
            get
            {
                if (this.appliesTo == null)
                {
                    this.appliesTo = this.FindAppliesTo();
                }
                return this.appliesTo;
            }
        }

        protected IActiveObject FindAppliesTo()
        {
            System.Type type = System.Type.GetType(this.Type);
            IActiveObject obj = Activator.CreateInstance(type) as IActiveObject;

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add(obj.DataManager.PrimaryKeyField, this.ActiveObjectID);

            IActiveCollection results = obj.DataManager.List(parameters);

            if (results.Count == 1)
            {
                return results[0] as IActiveObject;
            }
            return null;
        }

        #region Runner functions
        public static void StartTimer()
        {
            Timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            Timer.Start();
        }

        static MiscUtil.Threading.SyncLock locker = new MiscUtil.Threading.SyncLock("Worker timer lock");

        static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                using (locker.Lock())
                {
                    if (running) return;
                    running = true;
                }
                try
                {
                    Dictionary<string, object> parameters = new Dictionary<string, object>();
                    parameters.Add("StartRunAt", new DateTime(1970, 1, 1));
                    parameters.Add("EndRunAt", DateTime.Now);

                    ActiveCollection<ScheduledCommand> commands = Find(parameters);

                    if (commands.Count > 0)
                    {
                        log.InfoFormat("Found {0} commands to execute", commands.Count);
                    }

                    for (int i = 0; i < commands.Count; i++)
                    {
                        ScheduledCommand command = commands[i];

                        IActiveObject obj = command.AppliesTo;

                        MethodInfo mi = obj.GetType().GetMethod(command.Command, new Type[] { });

                        mi.Invoke(obj, new object[] { });

                        command.DataManager.Delete();
                    }
                }
                finally
                {
                    running = false;
                }
            }
            catch (Exception ex)
            {
                log.Error("An error occurred in the ScheduledCommand job", ex);
            }
        }
        #endregion

        #region Static helper functions
        public static ActiveCollection<ScheduledCommand> Find(IActiveObject obj)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Type", obj.DataManager.Settings.BaseActiveObjectType.FullName);
            parameters.Add("ActiveObjectID", obj.DataManager.PrimaryKeyValue);
            return Find(parameters);
        }

        public static ScheduledCommand Schedule(IActiveObject obj, string command, DateTime runAt)
        {
            ScheduledCommand task = ScheduledCommand.Create(obj);
            task.Command = command;
            task.RunAt = runAt;
            task.Save();
            return task;
        }

        public static ScheduledCommand Create(IActiveObject obj)
        {
            ScheduledCommand cmd = new ScheduledCommand();
            cmd.Type = obj.DataManager.Settings.BaseActiveObjectType.FullName;
            cmd.ActiveObjectID = (Guid)obj.DataManager.PrimaryKeyValue;
            return cmd;
        }

        public static void ClearPendingCommands(IActiveObject obj, string command)
        {
            ActiveCollection<ScheduledCommand> tasks = ScheduledCommand.Find(obj);
            for (int i = 0; i < tasks.Count; i++)
            {
                ScheduledCommand task = tasks[i];
                if (task.Command.Equals(command, StringComparison.CurrentCultureIgnoreCase))
                {
                    task.DataManager.Delete();
                }
            }
        }
        #endregion
    }
}
