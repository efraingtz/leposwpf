namespace De.LarsHildebrandt.WpfControls.WpfSimpleChart
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Media;

    /// <summary>
    /// Represents an Row in the bar-chart
    /// </summary>
    public class ChartRow : INotifyPropertyChanged
    {
        #region Fields

        private string m_caption;
        private Brush m_chartBrush;
        private Guid m_id;
        private double m_value;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ChartRow class.
        /// </summary>
        public ChartRow()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ChartRow class.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="value1"></param>
        public ChartRow(string caption, double value)
        {
            m_caption = caption;
            m_value = value;
            m_id = Guid.NewGuid();
            m_chartBrush = new SolidColorBrush(ColorGenerator.Instance.GetNext());
        }

        /// <summary>
        /// Initializes a new instance of the ChartRow class.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="value"></param>
        /// <param name="id"></param>
        /// <param name="chartBrush"></param>
        public ChartRow(string caption, double value, Brush chartBrush)
        {
            m_caption = caption;
            m_chartBrush = chartBrush;
            m_id = Guid.NewGuid();
            m_value = value;
        }

        /// <summary>
        /// Initializes a new instance of the ChartRow class.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="caption"></param>
        /// <param name="value"></param>
        public ChartRow(string caption, double value, Color color)
        {
            m_caption = caption;
            m_value = value;
            m_id = Guid.NewGuid();
            m_chartBrush = new SolidColorBrush(color);
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        /// <value>The caption.</value>
        public string Caption
        {
            get { return m_caption; }
            set
            {
                m_caption = value;
                RaisePropertyChangeEvent("Caption");
            }
        }

        /// <summary>
        /// Gets or sets the chart brush.
        /// </summary>
        /// <value>The chart brush.</value>
        public Brush ChartBrush
        {
            get { return m_chartBrush; }
            set
            {
                m_chartBrush = value;
                RaisePropertyChangeEvent("ChartBrush");
            }
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <value>The id.</value>
        public Guid Id
        {
            get { return m_id; }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public double Value
        {
            get { return m_value; }
            set
            {
                m_value = value;
                RaisePropertyChangeEvent("Value");
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Raises the property change event.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void RaisePropertyChangeEvent(String propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Methods
    }
}