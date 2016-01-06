using System;
using System.Security.Permissions;
using System.Windows.Threading;

namespace LEPOSWPF.SplashScreen
{
    /// <summary>
    /// Simulate Application DoEvents
    /// </summary>
    public static class DispatcherHelper
    {
        #region Helpers
        /// <summary>
        /// Simulate Application.DoEvents function of <see cref=" System.Windows.Forms.Application"/> class.
        /// </summary>
        [SecurityPermissionAttribute (SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,new DispatcherOperationCallback(ExitFrames), frame);

            try
            {
                Dispatcher.PushFrame(frame);
            }
            catch ( InvalidOperationException )
            {
            }
        }

        /// <summary>
        /// Exit current frame
        /// </summary>
        /// <param name="frame">Current frame</param>
        /// <returns>Null value</returns>
        private static object ExitFrames(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;
            return null;
        }
        #endregion
    }
}
