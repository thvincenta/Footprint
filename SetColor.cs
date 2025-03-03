using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Footprint
{
    public class SetColor
    {
        private Button prevButton;
        private SolidColorBrush defaultColor = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0xFF, 0xFE, 0x7C));
        private SolidColorBrush activeColor = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0xD0, 0xA8, 0x8D));

        /// <summary>
        /// Установка цвета кнопкам
        /// </summary>
        /// <param name="button"></param>
        public  void SetButtonColor(Button button)
        {
            if (prevButton != null)
            {
                prevButton.Background = defaultColor;
            }

            button.Background = activeColor;
            prevButton = button;
        }
    }
}
