using System;
using System.Collections.Generic;
using System.Text;
using PETimer;

namespace Server
{
    /// <summary>
    /// 定时服务
    /// </summary>
    public class TimerSvc : Singleton<TimerSvc>
    {
        TickTimer timer = new TickTimer(0, false);
        public override void Init()
        {
            base.Init();

            timer.LogFunc = this.Log;
            timer.WarnFunc = this.Warn;
            timer.ErrorFunc = this.Error;

            this.Log("TimerSvc Init Done");
        }

        public override void Update()
        {
            base.Update();

            timer.UpdateTask();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="delay">延时</param>
        /// <param name="taskCB">执行事务</param>
        /// <param name="cancleCB">取消回调</param>
        /// <param name="count">循环多少次</param>
        /// <returns></returns>
        public int AddTask(uint delay, Action<int> taskCB, Action<int> cancleCB = null,int count = 1)
        {
            return timer.AddTask(delay, taskCB, cancleCB, count);
        }

        /// <summary>
        /// 删除事务
        /// </summary>
        /// <param name="tid"></param>
        /// <returns></returns>
        public bool DeleteTask(int tid)
        {
            return timer.DeleteTask(tid);
        }
    }
}
