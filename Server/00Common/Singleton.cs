using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    /// <summary>
    /// 单例类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : new()
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new T();
                }
                return instance;
            }
        }

        public virtual void Init() { }

        public virtual void Update() { }
    }
}
