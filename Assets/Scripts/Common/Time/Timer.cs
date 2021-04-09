using UnityEngine;

namespace Common
{
    public class Timer
    {
        private int updateTime = 0;
        private int interval = 0;

        public void Update() 
        {
            updateTime = (int)Time.realtimeSinceStartup;
        }
        
        public void StartUp(int secs) 
        {
            interval = secs;
            this.Update();
        }

        public void Clear()
        {
            interval = 0;
            updateTime = 0;
        }

        public bool IsActive()
        {
            return updateTime > 0;
        }

        public int GetRemain()
        {
            return updateTime > 0 ? Mathf.Min(Mathf.Max(interval - ((int)Time.realtimeSinceStartup  - updateTime), 0), interval) : 0;
        }
    }
}