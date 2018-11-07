using System;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
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

        public Image<Gray, byte> binarizeImage(Bitmap bmpMyBitmap)
        {
            // gray value image
            Image<Gray, byte> grayImg = new Image<Gray, byte>(bmpMyBitmap);

            // binarize
            Image<Gray, byte> binImg = new Image<Gray, byte>(grayImg.Width, grayImg.Height, new Gray(0));

            CvInvoke.Threshold(grayImg, binImg, 150, 255, ThresholdType.Binary);

            return binImg;
        }

        public Image<Bgr, byte> detectHand(Image<Bgr, byte> currentFrameImg, out double biggestArea)
        {
            Image<Gray, byte> grayImgWithHand;

            // filter the image in terms of color and improve countour boundaries
            grayImgWithHand = filterForHand(currentFrameImg);

            Image<Bgr, byte> imgWithHandAndHull;

            // find a contour which represents a hand and draw a hull
            imgWithHandAndHull = extractContourAndHull(grayImgWithHand, currentFrameImg, out biggestArea);

            return imgWithHandAndHull;
        }

        /// <summary>
        /// Filters the given image by color and improves the boundaries of the remaining contours.
        /// Color space conversion Rgb <-> YCrCb: https://docs.opencv.org/3.4.3/de/d25/imgproc_color_conversions.html
        /// image eroding: https://homepages.inf.ed.ac.uk/rbf/HIPR2/erode.htm (auch bei Jaehne und Bernd)
        /// </summary>
        /// <param name="img">The image which should be progressed.</param>
        /// <returns>Gray Image which can contain a hand.</returns>
        private Image<Gray, byte> filterForHand(Image<Bgr, byte> img)
        {
            // convert color space from BGR to YCrCb
            Image<Ycc, Byte> currentYCrCbFrame = img.Convert<Ycc, Byte>();

            // create gray image with the same size as image
            Image<Gray, byte> grayImg = new Image<Gray, byte>(img.Width, img.Height);

            // perform color filtering
            grayImg = currentYCrCbFrame.InRange(new Ycc(0, 131, 80), new Ycc(255, 185, 135));

            // create a kernel for the image erosion
            Mat rect_12 = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(12, 12), new Point(6, 6));
            CvInvoke.Erode(grayImg, grayImg, rect_12, new Point(-1, -1), 1, BorderType.Default, new MCvScalar(0, 255, 0));

            // create a kernel for the image dilation
            Mat rect_6 = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(6, 6), new Point(3, 3));
            CvInvoke.Dilate(grayImg, grayImg, rect_6, new Point(-1, -1), 2, BorderType.Default, new MCvScalar(0, 255, 0));

            return grayImg;
        }

        private Image<Bgr, byte> extractContourAndHull(Image<Gray, byte> grayImg, Image<Bgr, byte> currentFrameImg, out double biggestArea)
        {
            // create variable where found contours can be stored
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();

            // convert Gray-Image to UMat
            UMat grayMat = grayImg.ToUMat();

            // search for contours in the Gray-Image
            CvInvoke.FindContours(grayMat, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

            # region Find biggest contour
            VectorOfPoint biggestContour = null;

            biggestArea = 0;
            double contourArea = 0;
            
            for(int contourNumber = 0; contourNumber < contours.Size; contourNumber++)
            {
                contourArea = CvInvoke.ContourArea(contours[contourNumber], false);
                if (contourArea > biggestArea)
                {
                    biggestArea = contourArea;
                    biggestContour = contours[contourNumber];
                }
            }
            # endregion

            if (biggestContour != null)
            {
                VectorOfPoint currentContour = new VectorOfPoint();

                CvInvoke.ApproxPolyDP(biggestContour, currentContour, CvInvoke.ArcLength(biggestContour, true) * 0.0025, true);

                Point[] currentContourPoints = currentContour.ToArray();

                currentFrameImg.Draw(currentContourPoints, new Bgr(System.Drawing.Color.LimeGreen), 2);

                PointF[] hullPoints = CvInvoke.ConvexHull(Array.ConvertAll<Point, PointF>(currentContourPoints, new Converter<Point, PointF>(PointToPointF)), true);

                RotatedRect rotatedRect = CvInvoke.MinAreaRect(currentContour);

                PointF[] pointFs = rotatedRect.GetVertices();

                Point[] points = new Point[pointFs.Length];

                for (int i = 0; i < pointFs.Length; i++)
                    points[i] = new Point((int)pointFs[i].X, (int)pointFs[i].Y);

                currentFrameImg.DrawPolyline(Array.ConvertAll<PointF, Point>(hullPoints, Point.Round),
                    true, new Bgr(200, 125, 75), 2);

                currentFrameImg.Draw(new CircleF(new PointF(rotatedRect.Center.X, rotatedRect.Center.Y), 3),
                    new Bgr(200, 125, 75), 2);
            }

            return currentFrameImg;
        }

        /// <summary>
        /// Converts a Point structure to a PointF structure
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        public static PointF PointToPointF(Point pt)
        {
            return new PointF(((float)pt.X), ((float)pt.Y));
        }

        /// <summary>
        /// filter for hand detection by color
        /// </summary>
        /// <param name="bmpMyBitmap">Orignial image without any filtering</param>
        /// <param name="colorVals">Color values representing lower and upper border for the color filtering</param>
        /// <returns>Filtered Gray image with elements drawn around detected bodies</returns>
        public Image<Bgr, byte> filterColor(Bitmap bmpMyBitmap, ref int[] colorVals)
        {
            using (Image<Bgr, byte> imgOriginal = new Image<Bgr, Byte>(bmpMyBitmap))
            {
                Image<Bgr, byte> imgProcessed = imgOriginal;

                // perform image smoothing with Gaussian pyramid decomposition and Gaussian smooth
                imgProcessed = imgOriginal.PyrDown().PyrUp();
                imgProcessed.SmoothGaussian(3);

                // filter on color
                Image<Gray, byte> imgGrayColorFiltered = imgProcessed.InRange(new Bgr(colorVals[0], colorVals[1], colorVals[2]),
                    new Bgr(colorVals[3], colorVals[4], colorVals[5]));

                // improve contrast
                CvInvoke.EqualizeHist(imgGrayColorFiltered, imgGrayColorFiltered);

                // repeat smoothing
                imgGrayColorFiltered = imgGrayColorFiltered.PyrDown().PyrUp();
                imgGrayColorFiltered.SmoothGaussian(3);

                // Canny threshold
                double cannyThreshold = 160.0;
                double cannyThresholdLinking = 80.0;

                // Canny image used for line and polygon detection
                UMat cannyEdges = new UMat();
                CvInvoke.Canny(imgGrayColorFiltered, cannyEdges, cannyThreshold, cannyThresholdLinking);

                // create blank image, used for triangles, rectangles and polygons
                Image<Bgr, byte> imgTrisRecsPolys = imgOriginal.CopyBlank();

                // declare a vector for contour storing
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();

                # region find contures
                // find a sequence of contours using the simple approximation method
                CvInvoke.FindContours(cannyEdges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);
                # endregion

                Point[][] arrayOfArrayOfPts = contours.ToArrayOfArray();


                if (contours.Size != 0)
                {
                    for (int i = 0; i < contours.Size; i++)
                    {
                        PointF[] hullPoints = CvInvoke.ConvexHull(Array.ConvertAll<Point, PointF>(arrayOfArrayOfPts[i], new Converter<Point, PointF>(PointToPointF)));

                        CvInvoke.Polylines(imgTrisRecsPolys,
                                Array.ConvertAll<PointF, Point>(hullPoints, Point.Round),
                                true, new MCvScalar(255.0, 255.0, 255.0));
                    }
                }


                return imgTrisRecsPolys;
            }

        } // end of filterColor()
    }
}
