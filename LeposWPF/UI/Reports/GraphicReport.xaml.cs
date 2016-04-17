namespace LeposWPF.UI.Reports
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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

    using De.LarsHildebrandt.WpfControls.WpfSimpleChart;
   
    using MahApps.Metro.Controls;
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class GraphicReport : MetroWindow
    {
        #region Fields

        private ObservableCollection<ChartLegendItem> m_barLegend = new ObservableCollection<ChartLegendItem>();
        private ObservableCollection<ChartRow> m_barRows = new ObservableCollection<ChartRow>();
        private ObservableCollection<ChartLegendItem> m_pieLegend = new ObservableCollection<ChartLegendItem>();
        private ObservableCollection<ChartRow> m_pieRows = new ObservableCollection<ChartRow>();
        private ObservableCollection<ChartLegendItem> m_stackedLegend = new ObservableCollection<ChartLegendItem>();
        private ObservableCollection<StackedChartRow> m_stackedRows = new ObservableCollection<StackedChartRow>();
        private List<dynamic> valores;
        #endregion Fields

        #region Constructors

        public GraphicReport(List<dynamic> valores, String titulo, string chartTitle)
        {
            this.valores = valores;
            PrepareBarChart();
            PreparePieChart();
            InitializeComponent();
            this.Title = titulo;
            this.chartTest.Caption = this.pieChart.Caption = chartTitle;
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<ChartLegendItem> BarLegend
        {
            get { return m_barLegend; }
            set { m_barLegend = value; }
        }

        public ObservableCollection<ChartRow> BarRows
        {
            get { return m_barRows; }
            set { m_barRows = value; }
        }

        public ObservableCollection<StackedChartRow> StackedRows
        {
            get { return m_stackedRows; }
            set { m_stackedRows = value; }
        }

        public ObservableCollection<ChartLegendItem> PieLegend
        {
            get { return m_pieLegend; }
            set { m_pieLegend = value; }
        }

        public ObservableCollection<ChartRow> PieRows
        {
            get { return m_pieRows; }
            set { m_pieRows = value; }
        }

        public ObservableCollection<ChartLegendItem> StackedLegend
        {
            get { return m_stackedLegend; }
            set { m_stackedLegend = value; }
        }

        #endregion Properties

        #region Methods


        private void PrepareBarChart()
        {
            Random gen = new Random(DateTime.Now.Millisecond);
            m_barLegend.Clear();
            System.Windows.Media.Color color;
            Random random = new Random();
            foreach(var v in valores)
            {
                byte r = Convert.ToByte(random.Next(255));
                byte g = Convert.ToByte(random.Next(255));
                byte b = Convert.ToByte(random.Next(255));
                color =System.Windows.Media.Color.FromRgb(r,g,b);
                m_barRows.Add(new ChartRow(v.ID, v.Value, color));
                m_barLegend.Add(new ChartLegendItem(v.ID, color));
            }
        }

        private void PreparePieChart()
        {
            Random gen = new Random(DateTime.Now.Millisecond);
            System.Windows.Media.Color color;
            Random random = new Random();
            m_pieLegend.Clear();
            foreach (var v in valores)
            {
                byte r = Convert.ToByte(random.Next(255));
                byte g = Convert.ToByte(random.Next(255));
                byte b = Convert.ToByte(random.Next(255));
                color = System.Windows.Media.Color.FromRgb(r, g, b);
                m_pieRows.Add(new ChartRow(v.ID, v.Value, color));
                m_pieLegend.Add(new ChartLegendItem(v.ID, color));
            }

        }

        #endregion Methods

        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}