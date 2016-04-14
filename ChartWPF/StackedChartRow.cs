namespace De.LarsHildebrandt.WpfControls.WpfSimpleChart
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Windows.Media;

    public class StackedChartRow : INotifyPropertyChanged
    {
        #region Fields

        private string m_caption;
        private Brush m_chartBrush;
        private Guid m_id;
        private List<ChartRow> m_values = new List<ChartRow>();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ChartRow class.
        /// </summary>
        public StackedChartRow()
        {
        }

        /// <summary>
        /// Initializes a new instance of the ChartRow class.
        /// </summary>
        /// <param name="caption"></param>
        public StackedChartRow(string caption)
        {
            m_caption = caption;
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
        public StackedChartRow(string caption, Brush chartBrush)
        {
            m_caption = caption;
            m_chartBrush = chartBrush;
            m_id = Guid.NewGuid();
        }

        /// <summary>
        /// Initializes a new instance of the ChartRow class.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="caption"></param>
        /// <param name="value"></param>
        public StackedChartRow(string caption, Color color)
        {
            m_caption = caption;
            m_id = Guid.NewGuid();
            m_chartBrush = new SolidColorBrush(color);
        }

        #endregion Constructors

        #region Events

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

        public Brush ChartBrush
        {
            get { return m_chartBrush; }
            set
            {
                m_chartBrush = value;
                RaisePropertyChangeEvent("ChartBrush");
            }
        }

        public Guid Id
        {
            get { return m_id; }
        }

        public List<ChartRow> Values
        {
            get
            {
                return m_values;
            }
        }

        #endregion Properties

        #region Methods

        private void RaisePropertyChangeEvent(String propertyName)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion Methods
    }
}