using System.Diagnostics;

namespace HM
{
    public class HMTimeWatch
    {
        #region FIELDS
        private Stopwatch sw;
        #endregion

        #region PROPERTIES
        #endregion

        #region PUBLIC METHODS
        public HMTimeWatch()
        {
            sw = new Stopwatch();
        }
        public void BeginWatch()
        {
            sw.Start();
        }

        public void EndWatch()
        {
            sw.Stop();
        }

        public long ElapsedMilliseconds()
        {
            return sw.ElapsedMilliseconds;
        }
        #endregion

        #region PROTECTED METHODS
        #endregion

        #region PRIVATE METHODS
        #endregion

        #region STATIC METHODS
        #endregion
    }
}