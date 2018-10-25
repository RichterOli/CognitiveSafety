using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Drawing;

namespace SafetyPerformanceTests
{
    class Filter
    {
        public Bitmap gaussFilter(Bitmap bmpMyBitmap)
        {
            using (Image<Gray, Byte> img = new Image<Gray, Byte>(bmpMyBitmap))
            {
                // GrauwertBild
                Image<Gray, byte> gray = new Image<Gray, byte>(bmpMyBitmap);
                //use image pyr to remove noise
                UMat pyrDown = new UMat();
                CvInvoke.PyrDown(gray, pyrDown);
                CvInvoke.PyrUp(pyrDown, gray);

                // Gauß Filter um störungen zu filtern
                Mat src1 = new Mat();
                src1 = img.Mat;
                Mat dstGaussBlur = new Mat();
                CvInvoke.Canny(gray, src1, 90, 50);
                CvInvoke.GaussianBlur(src1, dstGaussBlur, new System.Drawing.Size(25, 25), 0);// detalierte ergebnisse kernel size runternehmen (15,15), umso höher umso mehr störungen werden gefiltern--->umso höher umso weniger binarisieren
                                                                                              //Binarisieren um den Block zu filtern
                Mat dstBinary = new Mat();
                CvInvoke.Threshold(dstGaussBlur, dstBinary, 10, 255, ThresholdType.Binary);

                return dstBinary.Bitmap;
            }
        }
    }
}
