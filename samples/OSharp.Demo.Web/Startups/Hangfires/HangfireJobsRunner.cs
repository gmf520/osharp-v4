using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

using Hangfire;
using Hangfire.Common;

using OSharp.Core.Dependency;
using OSharp.Demo.Contracts;


namespace OSharp.Demo.Web.Startups.Hangfires
{
    public static class HangfireJobsRunner
    {
        public static void Start()
        {
            IJobCancellationToken token = new JobCancellationToken(true);
            BackgroundJob.Enqueue<TestJobRunner>(m => m.Run(token));
        }
    }


    public class TestJobRunner : IScopeDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public TestJobRunner(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Run(IJobCancellationToken cancelToken)
        {
            for (int i = 0; i < 50; i++)
            {
                cancelToken.ThrowIfCancellationRequested();
                IIdentityContract contract = _serviceProvider.GetService<IIdentityContract>();
                Debug.WriteLine($"HashCode:{contract.GetHashCode()}---{i}");
                await Task.Delay(1000, cancelToken.ShutdownToken);
            }
        }
    }
}