using UnityEngine;

namespace UGF.Util
{
    public static class NumberExtensions
    {
        /// <summary>
        /// Returns the representation of this float in percentage relative to the max value given.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int AsPercent(this float value, float max)
        {
            return (int)((value / max) * 100);
        }

        /// <summary>
        /// Returns the representation of this float in relative percentage relative to the max value given (between 0 and 1)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float AsRelativeTo(this float value, float max)
        {
            return (value / max);
        }

        /// <summary>
        /// Returns value * deltaTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float AsTimeAdjusted(this float value)
        {
            return value * Time.deltaTime;
        }
    }
}
