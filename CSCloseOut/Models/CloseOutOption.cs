using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace CSCloseOut.Models
{
    [Serializable]
    public class CloseOutOption
    {

        public int? ID { get; set; }
        public int? ParentID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Color Color
        {
            get
            {
                return ColorTranslator.FromHtml(this.ColorHex);
            }
            set
            {
                if (value != null)
                {
                    this.ColorHex = "#" + value.R.ToString("X2") + value.G.ToString("X2") + value.B.ToString("X2");
                }
            }
        }
        public string ColorHex { get; set; }

        public List<CloseOutOption> Children { get; set; }

        public CloseOutOption()
        {
            this.Children = new List<CloseOutOption>();
        }

        public void AddChildOption(CloseOutOption option)
        {
            if (option == null)
                return;

            if (this.Children.FirstOrDefault(o => o.ID == option.ID) == null)
            {
                option.ParentID = this.ID;
                if (string.IsNullOrEmpty(option.ColorHex))
                {
                    option.ColorHex = this.ColorHex;
                }

                this.Children.Add(option);
            }
        }
    }

    public static class ColorChanger
    {
        public static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        public static Color ColorFromAhsb(int a, float h, float s, float b)
        {

            if (0 > a || 255 < a)
            {
                throw new ArgumentOutOfRangeException("a", a, "Invalid Alpha");
            }
            if (0f > h || 360f < h)
            {
                throw new ArgumentOutOfRangeException("h", h,"InvalidHue");
            }
            if (0f > s || 1f < s)
            {
                throw new ArgumentOutOfRangeException("s", s,"InvalidSaturation");
            }
            if (0f > b || 1f < b)
            {
                throw new ArgumentOutOfRangeException("b", b,"InvalidBrightness");
            }

            if (0 == s)
            {
                return Color.FromArgb(a, Convert.ToInt32(b * 255),
                  Convert.ToInt32(b * 255), Convert.ToInt32(b * 255));
            }

            float fMax, fMid, fMin;
            int iSextant, iMax, iMid, iMin;

            if (0.5 < b)
            {
                fMax = b - (b * s) + s;
                fMin = b + (b * s) - s;
            }
            else
            {
                fMax = b + (b * s);
                fMin = b - (b * s);
            }

            iSextant = (int)Math.Floor(h / 60f);
            if (300f <= h)
            {
                h -= 360f;
            }
            h /= 60f;
            h -= 2f * (float)Math.Floor(((iSextant + 1f) % 6f) / 2f);
            if (0 == iSextant % 2)
            {
                fMid = h * (fMax - fMin) + fMin;
            }
            else
            {
                fMid = fMin - h * (fMax - fMin);
            }

            iMax = Convert.ToInt32(fMax * 255);
            iMid = Convert.ToInt32(fMid * 255);
            iMin = Convert.ToInt32(fMin * 255);

            switch (iSextant)
            {
                case 1:
                    return Color.FromArgb(a, iMid, iMax, iMin);
                case 2:
                    return Color.FromArgb(a, iMin, iMax, iMid);
                case 3:
                    return Color.FromArgb(a, iMin, iMid, iMax);
                case 4:
                    return Color.FromArgb(a, iMid, iMin, iMax);
                case 5:
                    return Color.FromArgb(a, iMax, iMin, iMid);
                default:
                    return Color.FromArgb(a, iMax, iMid, iMin);
            }
        }
    }

}