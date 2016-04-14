namespace De.LarsHildebrandt.WpfControls.WpfSimpleChart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;

    /// <summary>
    /// Represents an Instance of the bar-chart
    /// </summary>
    public class BarChart : ChartBase
    {
        #region Fields

        public static readonly DependencyProperty ChartCornerRadiusProperty =
            DependencyProperty.Register("ChartCornerRadius",
            typeof(CornerRadius),
            typeof(BarChart),
            new PropertyMetadata(new CornerRadius(3, 3, 0, 0)));
        public static readonly DependencyProperty ChartItemWidthProperty =
           DependencyProperty.Register("ChartItemWidth",
           typeof(double),
           typeof(BarChart),
           new PropertyMetadata(30.0));
        public static readonly DependencyProperty ChartScrollBarVisibilityProperty =
           DependencyProperty.Register("ShowScrollbars",
           typeof(ScrollBarVisibility),
           typeof(BarChart),
           new PropertyMetadata(ScrollBarVisibility.Disabled));
        public static readonly DependencyProperty ChartCaptionVisibilityProperty =
            DependencyProperty.Register("ChartCaptionVisibility",
            typeof(Visibility),
            typeof(BarChart),
            new PropertyMetadata(Visibility.Visible));
        public static readonly DependencyProperty ChartValuesVisibilityProperty =
            DependencyProperty.Register("ChartValuesVisibility",
            typeof(Visibility),
            typeof(BarChart),
            new PropertyMetadata(Visibility.Hidden));

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes the <see cref="BarChart"/> class.
        /// </summary>
        static BarChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BarChart),
                new FrameworkPropertyMetadata(typeof(BarChart)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BarChart"/> class.
        /// </summary>
        public BarChart()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the chart corner radius.
        /// </summary>
        /// <value>The chart corner radius.</value>
        public CornerRadius ChartCornerRadius
        {
            get
            {
                return (CornerRadius)GetValue(ChartCornerRadiusProperty);
            }
            set
            {
                SetValue(ChartCornerRadiusProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the chart item.
        /// </summary>
        /// <value>The width of the chart item.</value>
        public double ChartItemWidth
        {
            get { return (double)GetValue(ChartItemWidthProperty); }
            set { SetValue(ChartItemWidthProperty, value); }
        }

        /// <summary>
        /// Gets or sets the chart scroll bar visibility.
        /// </summary>
        /// <value>The chart scroll bar visibility.</value>
        public ScrollBarVisibility ChartScrollBarVisibility
        {
            get
            {
                return (ScrollBarVisibility)GetValue(ChartScrollBarVisibilityProperty);
            }
            set
            {
                SetValue(ChartScrollBarVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the visibility of chart caption.
        /// </summary>
        /// <value>The chart show caption.</value>
        public Visibility ChartCaptionVisibility
        {
            get
            {
                return (Visibility)GetValue(ChartCaptionVisibilityProperty);
            }
            set
            {
                SetValue(ChartCaptionVisibilityProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the visibility of chart values.
        /// </summary>
        /// <value>The chart show values.</value>
        public Visibility ChartValuesVisibility
        {
            get
            {
                return (Visibility)GetValue(ChartValuesVisibilityProperty);
            }
            set
            {
                SetValue(ChartValuesVisibilityProperty, value);
            }
        }

        #endregion Properties
    }
}