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

                # region find triangles, rectangles and polygons
                // find a sequence of contours using the simple approximation method
                CvInvoke.FindContours(cannyEdges, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple);

                /* declare lists for triangles, rectangles and polygons
                List<Triangle2DF> listTriangles = new List<Triangle2DF>();
                List<RotatedRect> listRectangles = new List<RotatedRect>();
                VectorOfVectorOfPoint listPolygons = new VectorOfVectorOfPoint();

                // iterate through the contour vector
                for (int i = 0; i < contours.Size; i++)
                {
                    using (VectorOfPoint contour = new VectorOfPoint())
                    {
                        // approximate one or more curves and return the approximation results
                        CvInvoke.ApproxPolyDP(contours[i], contour, CvInvoke.ArcLength(contours[i], true) * 0.05, true);

                        // ensure that the area of the contour is greater than 250.0
                        if(CvInvoke.ContourArea(contour, false) > 250.0)
                        {
                            // get the number of array elements of the contour
                            int arrayElements = contour.Size;

                            // check if contour is a triangle
                            if (3 == arrayElements)
                            {
                                // get contour points
                                Point[] points = contour.ToArray();

                                // add to triangle list
                                listTriangles.Add(new Triangle2DF(points[0], points[1], points[2]));
                            }
                            // check if contour is a rectangle or polygon
                            else if(arrayElements >= 4 && arrayElements <= 6)
                            {
                                // get contour points
                                Point[] points = contour.ToArray();

                                // suppose that contour is a rectangle
                                bool isRectangle = true;

                                // check if contour has 4 points meaning it could be an rectangle
                                if (4 == arrayElements)
                                {
                                    # region determine if all angles in the contour are within [80, 100] degree
                                    // get edges between points
                                    LineSegment2D[] edges = PointCollection.PolyLine(points, true);

                                    // step through edges
                                    for (int x = 0; x < edges.Length; x++)
                                    {
                                        double angle = Math.Abs(edges[(x + 1) % edges.Length].GetExteriorAngleDegree(edges[x]));

                                        // if angle between edges is not about 90 degrees
                                        if (angle < 80.0 || angle > 100.0)
                                        {
                                            // contour is not a rectangle
                                            isRectangle = false;
                                            break;
                                        }
                                    }
                                    # endregion
                                }
                                else
                                    // contour is not a rectangle
                                    isRectangle = false;

                                if (true == isRectangle)
                                    listRectangles.Add(CvInvoke.MinAreaRect(contour));
                                else
                                    listPolygons.Push(contour);
                            }
                        }
                    }
                }
                #endregion

                # region draw found contours
                foreach (Triangle2DF triangle in listTriangles)
                    imgTrisRecsPolys.Draw(triangle, new Bgr(System.Drawing.Color.Yellow), 2);

                foreach (RotatedRect rectangle in listRectangles)
                    imgTrisRecsPolys.Draw(rectangle, new Bgr(System.Drawing.Color.Blue), 2);

                
                CvInvoke.DrawContours(imgTrisRecsPolys, listPolygons, -1, new MCvScalar(0, 0, 255));*/
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
