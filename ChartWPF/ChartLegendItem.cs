namespace De.LarsHildebrandt.WpfControls.WpfSimpleChart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Media;

    public class ChartLegendItem
    {
        #region Fields

        private string m_caption;
        private Brush m_itemBrush;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the StackedChartLegendItem class.
        /// </summary>
        /// <param name="caption"></param>
        /// /// <param name="color"></param>
        public ChartLegendItem(string caption, Color color)
        {
            m_caption = caption;
            m_itemBrush = new SolidColorBrush(color);
        }

        /// <summary>
        /// Initializes a new instance of the StackedChartLegendItem class.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="itemBrush"></param>
        public ChartLegendItem(string caption, Brush itemBrush)
        {
            m_caption = caption;
            m_itemBrush = itemBrush;
        }

        #endregion Constructors

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
            }
        }

        /// <summary>
        /// Gets or sets the item brush.
        /// </summary>
        /// <value>The item brush.</value>
        public Brush ItemBrush
        {
            get { return m_itemBrush; }
            set { m_itemBrush = value; }
        }

        #endregion Properties
    }
}