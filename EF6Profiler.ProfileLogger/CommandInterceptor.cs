using System;
using System.Data.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;
using System.Linq;

namespace EF6Profiler.ProfileLogger
{
    public class CommandInterceptor : DbCommandInterceptor
    {
        private IProfileLogger logger;
        //TODO : Would guess this will fail for async queries.        
        private readonly Stopwatch stopwatch = new Stopwatch();

        public CommandInterceptor(IProfileLogger logger)
        {
            this.logger = logger;
        }

        private delegate void ExecutingMethod<T>(DbCommand command, DbCommandInterceptionContext<T> interceptionContext);

        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuted(command,interceptionContext);
            stopwatch.Restart();            
        }

        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            stopwatch.Stop();
            LogCommand(stopwatch.Elapsed, command, interceptionContext, CommandType.NonQuery);
            base.NonQueryExecuted(command, interceptionContext);
        }

        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuting(command, interceptionContext);
            stopwatch.Restart();
        }

        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            stopwatch.Stop();
            LogCommand(stopwatch.Elapsed, command, interceptionContext, CommandType.Scalar);
            base.ScalarExecuted(command, interceptionContext);
        }

        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command,interceptionContext);
            stopwatch.Restart();
        }

        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            stopwatch.Stop();
            LogCommand(stopwatch.Elapsed, command, interceptionContext, CommandType.Reader);
            base.ReaderExecuted(command, interceptionContext);
        }

        private void LogCommand<T>(TimeSpan ts, DbCommand command, DbCommandInterceptionContext<T> interceptionContext,CommandType commandType)
        {
            var endTime = DateTime.Now;
            var cp = new CommandProfile
            {
                Async = interceptionContext.IsAsync,
                CommandText = command.CommandText,
                CommandType = commandType,
                Elapsed = ts,
                End = endTime,
                Parameters = command.Parameters.Cast<DbParameter>().ToDictionary(dp => dp.ParameterName, dp => dp.Value.ToString()),
                Start = endTime.Subtract(ts),
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
