using System;
using System.Collections.Generic;
using System.Text;
using Majako.Plugin.Common.Extensions;

namespace Majako.Plugin.Common.Infrastructure.ScheduleTasks
{
    public sealed class RegistredScheduleTaskAttribute : Attribute
    {
        public RegistredScheduleTaskAttribute(string name, bool isEnabled, bool stopOnError, double interval) //Timespan is not eligble because it needs to be instantiated at runtime
        {
            name.NotEmpty(nameof(name));
            this.Name = name;
            this.IsEnabled = isEnabled;
            this.StopOnError = stopOnError;
            this.Interval = TimeSpan.FromSeconds(interval);
        }

        public string Name { get; }
        public bool IsEnabled { get; }
        public bool StopOnError { get; }
        public TimeSpan Interval { get; set; }

    }
}
