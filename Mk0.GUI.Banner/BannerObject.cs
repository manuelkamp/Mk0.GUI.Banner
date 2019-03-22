using System.Drawing;

namespace Mk0.GUI.Banner
{
    internal class BannerObject
    {
        internal string Label1Text { get; }
        internal string Label2Text { get; }
        internal string Pb1ImageLocation { get; }
        internal Image Pb1Image { get; }
        internal Image Pb2Image { get; }

        internal BannerObject(string label1Text, string label2Text, string pb1ImageLocation, Image pb2Image)
        {
            Label1Text = label1Text;
            Label2Text = label2Text;
            Pb1ImageLocation = pb1ImageLocation;
            Pb2Image = pb2Image;
        }

        internal BannerObject(string label1Text, string label2Text, Image pb1Image, Image pb2Image)
        {
            Label1Text = label1Text;
            Label2Text = label2Text;
            Pb1Image = pb1Image;
            Pb2Image = pb2Image;
        }
    }
}
