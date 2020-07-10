using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Majako.Plugin.Common.Extensions;
using Nop.Core.Domain.Tasks;
using Nop.Services.Tasks;

namespace Majako.Plugin.Common.Infrastructure.ScheduleTasks
{
    public static class ScheduleTasksRegistrator
    {
        public static void RegisterScheduleTasks(this IScheduleTaskService source, Assembly assembly)
        {
            source.NotNull(nameof(source));
            assembly.NotNull(nameof(assembly));

            foreach (var task in GetScheduleTasks(assembly))
            {
                var scheduledTask = new ScheduleTask()
                {
                    Enabled = task.Item2.IsEnabled,
                    Name = task.Item2.Name,
                    Seconds = (int)task.Item2.Interval.TotalSeconds,
                    StopOnError = task.Item2.StopOnError,
                    Type = task.Item1.FullName
                };

                source.InsertTask(scheduledTask);
            }
        }

        public static void DeregisterScheduleTasks(this IScheduleTaskService source, Assembly assembly)
        {
            source.NotNull(nameof(source));
            assembly.NotNull(nameof(assembly));

            foreach(var task in GetScheduleTasks(assembly))
            {
                var foundTask = source.GetTaskByType(task.Item1.FullName);
                if (foundTask == null)
                    continue;
                source.DeleteTask(foundTask);
            }
        }

        private static IEnumerable<ValueTuple<Type, RegistredScheduleTaskAttribute>> GetScheduleTasks(Assembly assembly)
        {
            foreach(var type in assembly.GetTypes().Where(c => c.IsAssignableFrom(typeof(IScheduleTask))))
            {
                var attribute = type.GetCustomAttribute<RegistredScheduleTaskAttribute>();
                if (attribute == null)
                    continue;
                yield return new ValueTuple<Type, RegistredScheduleTaskAttribute>(type, attribute);
            }
        }
    }
}
