namespace De.LarsHildebrandt.WpfControls.WpfSimpleChart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Media;

    public class ColorGenerator
    {
        #region Fields

        private static ColorGenerator m_instance = null;
        private static bool m_isInitialized = false;
        private static object m_syncLock = new object();

        private List<Color> m_colors = new List<Color>();
        private Color m_nextColor;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ColorGenerator class.
        /// </summary>
        public ColorGenerator()
        {
            m_colors.Add(Colors.Red);
            m_colors.Add(Colors.Green);
            m_colors.Add(Colors.Blue);
            m_colors.Add(Colors.Yellow);
            m_colors.Add(Colors.BlueViolet);
            m_colors.Add(Colors.Brown);
            m_colors.Add(Colors.BurlyWood);
            m_colors.Add(Colors.DarkCyan);
            m_colors.Add(Colors.Orange);
            m_colors.Add(Colors.SteelBlue);
            m_colors.Add(Colors.YellowGreen);

            m_nextColor = m_colors[0];
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the instance of the colorgenerator
        /// </summary>
        /// <value>The instance.</value>
        public static ColorGenerator Instance
        {
            get
            {
                if (!m_isInitialized)
                {
                    lock (m_syncLock)
                    {
                        if (!m_isInitialized)
                        {
                            m_instance = new ColorGenerator();
                            m_instance.Initialize();
                            m_isInitialized = true;
                        }
                    }
                }

                return m_instance;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets the next color.
        /// </summary>
        /// <returns></returns>
        public Color GetNext()
        {
            Color result = m_nextColor;
            int index = m_colors.IndexOf(result);
            if (index == m_colors.Count - 1)
            {
                m_nextColor = m_colors[0];
            }
            else
            {
                m_nextColor = m_colors[index + 1];
            }

            return result;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
        }

        #endregion Methods
    }
}