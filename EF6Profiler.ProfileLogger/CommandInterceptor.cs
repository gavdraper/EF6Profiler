using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;

namespace EF6Profiler.ProfileLogger
{
    public class CommandInterceptor : DbCommandInterceptor
    {
        private IProfileLogger logger;

        public CommandInterceptor(IProfileLogger logger)
        {
            this.logger = logger;
        }

        private delegate void ExecutingMethod<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext);

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            CommandExecuting(base.NonQueryExecuting, command, interceptionContext,CommandType.NonQuery);
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            CommandExecuting(base.ReaderExecuting, command, interceptionContext,CommandType.Reader);
        }

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            CommandExecuting(base.ScalarExecuting, command, interceptionContext, CommandType.Scalar);
        }

        private void CommandExecuting<T>(ExecutingMethod<T> executingMethod, DbCommand command, DbCommandInterceptionContext<T> interceptionContext,CommandType commandType)
        {
            var sw = Stopwatch.StartNew();
            executingMethod.Invoke(command, interceptionContext);
            sw.Stop();
            var endTime = DateTime.Now;

            var cp = new CommandProfile
            {
                Async = interceptionContext.IsAsync,
                CommandText = command.CommandText,
                CommandType = commandType,
                Elapsed = sw.Elapsed,
                End = endTime,
                Start = endTime.Subtract(sw.Elapsed),
                Exception = interceptionContext.Exception != null ? interceptionContext.Exception.Message : "",
                Failed = interceptionContext.Exception != null
            };
            if (interceptionContext.Exception != null)
            {
                logger.Log(cp);
            }
            else
            {
                logger.Log(cp);
            }
        }
    }
}
