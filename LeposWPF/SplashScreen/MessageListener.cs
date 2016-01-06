using System.Windows;

namespace LEPOSWPF.SplashScreen
{
    /// <summary>
    /// Message listener, singleton pattern.
    /// Inherit from DependencyObject to implement DataBinding.
    /// </summary>
    public class MessageListener : DependencyObject
    {
        #region Definition
        /// <summary>
        /// Instance of MessageListener
        /// </summary>
        private static MessageListener mInstance;
        /// <summary>
        /// Get MessageListener instance
        /// </summary>
        public static MessageListener Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new MessageListener();
                }

                return mInstance;
            }
        }
       
        /// <summary>
        /// Get or set received message
        /// </summary>
        public string Message
        {
            get
            {
                return (string)GetValue(MessageProperty);
            }

            set
            {
                SetValue(MessageProperty, value);
            }
        }

        /// <summary>
        /// Register message
        /// </summary>
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register("Message", typeof(string), typeof(MessageListener), new UIPropertyMetadata(null));
        #endregion
        #region Constructors
        /// <summary>
        /// Empty Constructor
        /// </summary>
        private MessageListener()
        {

        }
        #endregion
        #region Helpers
        /// <summary>
        /// Receive message among clases
        /// </summary>
        /// <param name="message">Message to display</param>
        public void ReceiveMessage (string message)
        {
            Message = message;
            DispatcherHelper.DoEvents();
        }
        #endregion
    }
}
