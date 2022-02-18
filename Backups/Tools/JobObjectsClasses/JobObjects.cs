using System.Collections.Generic;
using System.Collections.Immutable;
using Newtonsoft.Json;

namespace Backups.Tools.JobObjectsClasses
{
    public class JobObjects
    {
        public JobObjects()
        {
            JobObjectList = new List<JobObject>();
        }

        [JsonConstructor]
        private JobObjects(List<JobObject> jobObjectList = null)
        {
            jobObjectList ??= new List<JobObject>();
            JobObjectList = jobObjectList;
        }

        [JsonIgnore]
        public ImmutableList<JobObject> JobObjectsImmutableList => JobObjectList.ToImmutableList();
        [JsonProperty]
        private List<JobObject> JobObjectList { get; }

        public JobObject AddJobObject(string jobObjectWay)
        {
            var jobObject = new JobObject(jobObjectWay);
            JobObjectList.Add(jobObject);
            return jobObject;
        }

        public bool DeleteJobObject(JobObject unusedJobObject)
        {
            return JobObjectList.Remove(unusedJobObject);
        }
    }
}