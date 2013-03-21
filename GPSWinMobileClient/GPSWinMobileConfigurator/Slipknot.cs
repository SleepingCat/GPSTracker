using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading;

namespace GPSWinMobileConfigurator
{
    // взято тут http://www.pinvoke.net/default.aspx/coredll.SystemIdleTimerReset
    static class Slipknot
    {
        /// <summary>
        /// This function resets a system timer that controls whether or not the
        /// device will automatically go into a suspended state.
        /// </summary>
        [DllImport("CoreDll.dll")]
        public static extern void SystemIdleTimerReset();

        private static int nDisableSleepCalls = 0;
        private static System.Threading.Timer preventSleepTimer = null;
        public static void DisableDeviceSleep()
        {
            nDisableSleepCalls++;
            if (nDisableSleepCalls == 1)
            {
                //Debug.Assert(preventSleepTimer == null);
                // start a 30-second periodic timer
                preventSleepTimer = new System.Threading.Timer(new TimerCallback(PokeDeviceToKeepAwake), null, 0, 30 * 1000);
            }
        }

        public static void EnableDeviceSleep()
        {
            nDisableSleepCalls--;
            if (nDisableSleepCalls == 0)
            {
                //Debug.Assert(preventSleepTimer != null);
                if (preventSleepTimer != null)
                {
                    preventSleepTimer.Dispose();
                    preventSleepTimer = null;
                }
            }
        }

        private static void PokeDeviceToKeepAwake(object extra)
        {
            try
            {
                SystemIdleTimerReset();
            }
            catch (Exception e)
            {
                Allert.ShowMessage(e.Message);
            }
        }
    }
}
