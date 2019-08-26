using NBench;
using Project_Manager_Unit_Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Manager_NBench
{
    public class NBench_Class
    {
        private Counter counter;
        private IBenchmarkTrace trace;
        UserUnitTest ut = new UserUnitTest();
        ProjectUnitTest pt = new ProjectUnitTest();
        TaskUnitTest tt = new TaskUnitTest();
        ParentTaskUnitTest put = new ParentTaskUnitTest();
        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            counter = context.GetCounter("MyCounter");
            trace = context.Trace;

        }
        [PerfBenchmark(NumberOfIterations = 500, RunMode = RunMode.Throughput, TestMode = TestMode.Test, SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 600000)]
        [CounterMeasurement("MyCounter")]
        [MemoryMeasurement(MemoryMetric.TotalBytesAllocated)]
        [TimingMeasurement()]
        public void BenchMark_Method(BenchmarkContext context)
        {
            ut.GetAllUsers();
            ut.GetUser_ShouldReturnItemWithSameID();
            ut.PostUser_ShouldReturnSameItem();
            ut.PutUser_ShouldFail_WhenDifferentID();
            ut.DeleteUser_ShouldReturnOK();
            pt.DeleteProject_ShouldReturnOK();
            pt.GetProject_ShouldReturnItemWithSameID();
            pt.PostProject_ShouldReturnSameItem();
            pt.PutProject_ShouldFail_WhenDifferentID();
            pt.GetAllProjects();
            tt.GetAllTasks();
            tt.GetTask_ShouldReturnItemWithSameID();
            tt.PostTask_ShouldReturnSameItem();
            tt.PutTask_ShouldFail_WhenDifferentID();
            tt.DeleteTask_ShouldReturnOK();
            put.DeleteParentTask_ShouldReturnOK();
            put.GetAllParentTasks();
            put.GetParentTask_ShouldReturnItemWithSameID();
            put.PostParentTask_ShouldReturnSameItem();
            put.PutParentTask_ShouldFail_WhenDifferentID();
            counter.Increment();
        }
    }
}
