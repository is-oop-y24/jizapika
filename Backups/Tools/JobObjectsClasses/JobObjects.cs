using System.Collections.Generic;
using System.Collections.Immutable;

namespace Backups.Tools.JobObjectsClasses
{
    public class JobObjects
    {
        private List<JobObject> _jobObjects;
        public JobObjects()
        {
            _jobObjects = new List<JobObject>();
        }

        public ImmutableList<JobObject> JobObjectsImmutableList => _jobObjects.ToImmutableList();

        public JobObject AddJobObject(string jobObjectWay)
        {
            var jobObject = new JobObject(jobObjectWay);
            _jobObjects.Add(jobObject);
            return jobObject;
        }

        public bool DeleteJobObject(JobObject unusedJobObject)
        {
            return _jobObjects.Remove(unusedJobObject);
        }
    }
}