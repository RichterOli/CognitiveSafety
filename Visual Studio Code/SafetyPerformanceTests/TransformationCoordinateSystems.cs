using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Emgu.CV;

namespace CognitiveRobotik.ModuleA.Models.Calculate
{
    /// <summary>
    /// Transforms the coordinate systems into Base, World and Local
    /// </summary>
    public class TransformationCoordinateSystems
    {
        public Point __pntOrigin { get; set; }

        public void SetOriginCoordinateSystem(Point pts)
        {
            __pntOrigin = pts;
        }

        /// <summary>
        /// transform base coordinate system to world coordinate system
        /// </summary>
        /// <param name="World_Koordinate_x"></param>
        /// <param name="World_Koordinate_y"></param>
        /// <param name="World_Koordinate_z"></param>
        /// <returns></returns>
        public Matrix<Double> TransformBaseToWorldKoordinate(double World_Koordinate_x, double World_Koordinate_y, double World_Koordinate_z)
        {

            Matrix<Double> clTranslationsMatrix = new Matrix<Double>(4, 4);
            Matrix<Double> clWorldKoordinateMatrix = new Matrix<Double>(4, 4);
            Matrix<Double> clPointWorldKoordinate = new Matrix<Double>(4, 4);

            //Defines the origin in the world coordinate system for the robot (borders of the robot)
            //-78.2,+253.6,+126.8,-92.0,+89.9
            double __dblCurrentPositionX = -78.2;
            double __dblCurrentPositionY = 253.6;
            double __dblCurrentPositionZ = 90;



            //Set the elements

            #region Translationsmatrix
            clTranslationsMatrix.Data[0, 0] = 1;
            clTranslationsMatrix.Data[0, 1] = 0;
            clTranslationsMatrix.Data[0, 2] = 0;
            clTranslationsMatrix.Data[0, 3] = 0;

            clTranslationsMatrix.Data[1, 0] = 0;
            clTranslationsMatrix.Data[1, 1] = 1;
            clTranslationsMatrix.Data[1, 2] = 0;
            clTranslationsMatrix.Data[1, 3] = 0;

            clTranslationsMatrix.Data[2, 0] = 0;
            clTranslationsMatrix.Data[2, 1] = 0;
            clTranslationsMatrix.Data[2, 2] = 1;
            clTranslationsMatrix.Data[2, 3] = 0;

            clTranslationsMatrix.Data[3, 0] = __dblCurrentPositionX;
            clTranslationsMatrix.Data[3, 1] = __dblCurrentPositionY;
            clTranslationsMatrix.Data[3, 2] = __dblCurrentPositionZ;
            clTranslationsMatrix.Data[3, 3] = 1;
            #endregion

            #region World_Koordinaten_System
            clWorldKoordinateMatrix.Data[0, 0] = World_Koordinate_x;
            clWorldKoordinateMatrix.Data[0, 1] = World_Koordinate_y;
            clWorldKoordinateMatrix.Data[0, 2] = World_Koordinate_z;
            clWorldKoordinateMatrix.Data[0, 3] = 1;

            clWorldKoordinateMatrix.Data[1, 0] = 0;
            clWorldKoordinateMatrix.Data[1, 1] = 0;
            clWorldKoordinateMatrix.Data[1, 2] = 0;
            clWorldKoordinateMatrix.Data[1, 3] = 0;

            clWorldKoordinateMatrix.Data[2, 0] = 0;
            clWorldKoordinateMatrix.Data[2, 1] = 0;
            clWorldKoordinateMatrix.Data[2, 2] = 0;
            clWorldKoordinateMatrix.Data[2, 3] = 0;

            clWorldKoordinateMatrix.Data[3, 0] = 0;
            clWorldKoordinateMatrix.Data[3, 1] = 0;
            clWorldKoordinateMatrix.Data[3, 2] = 0;
            clWorldKoordinateMatrix.Data[3, 3] = 0;

            #endregion

            #region Matrix Multiplicate 4x4
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    for (int k = 0; k <= 3; k++)
                    {
                        clPointWorldKoordinate.Data[i, j] += clWorldKoordinateMatrix.Data[i, k] * clTranslationsMatrix.Data[k, j];
                    }
                }
            }
            #endregion

            return clPointWorldKoordinate;
        }

        /// <summary>
        /// transform world coordinate system to base coordinate system
        /// </summary>
        /// <param name="World_Koordinate_x"></param>
        /// <param name="World_Koordinate_y"></param>
        /// <returns></returns>
        public Matrix<Double> TransformWorldToBaseKoordinate(double World_Koordinate_x, double World_Koordinate_y)
        {

            Matrix<Double> clTranslationsMatrix = new Matrix<Double>(4, 4);
            Matrix<Double> clWorldKoordinateMatrix = new Matrix<Double>(4, 4);
            Matrix<Double> clPointWorldKoordinate = new Matrix<Double>(4, 4);

            //Uhrspung aufgenommenes Bild rechts oben

            double __dblCurrentPositionX = -78.2;
            double __dblCurrentPositionY = 253.6;
            double __dblCurrentPositionZ = 90;



            //Set the elements
            #region Translationsmatrix
            clTranslationsMatrix.Data[0, 0] = 1;
            clTranslationsMatrix.Data[0, 1] = 0;
            clTranslationsMatrix.Data[0, 2] = 0;
            clTranslationsMatrix.Data[0, 3] = 0;

            clTranslationsMatrix.Data[1, 0] = 0;
            clTranslationsMatrix.Data[1, 1] = 1;
            clTranslationsMatrix.Data[1, 2] = 0;
            clTranslationsMatrix.Data[1, 3] = 0;

            clTranslationsMatrix.Data[2, 0] = 0;
            clTranslationsMatrix.Data[2, 1] = 0;
            clTranslationsMatrix.Data[2, 2] = 1;
            clTranslationsMatrix.Data[2, 3] = 0;

            clTranslationsMatrix.Data[3, 0] = __dblCurrentPositionX;
            clTranslationsMatrix.Data[3, 1] = __dblCurrentPositionY;
            clTranslationsMatrix.Data[3, 2] = __dblCurrentPositionZ;
            clTranslationsMatrix.Data[3, 3] = 1;
            #endregion

            #region World_Koordinaten_System
            clWorldKoordinateMatrix.Data[0, 0] = World_Koordinate_x;
            clWorldKoordinateMatrix.Data[0, 1] = World_Koordinate_y;
            clWorldKoordinateMatrix.Data[0, 2] = 0;
            clWorldKoordinateMatrix.Data[0, 3] = 1;

            clWorldKoordinateMatrix.Data[1, 0] = 0;
            clWorldKoordinateMatrix.Data[1, 1] = 0;
            clWorldKoordinateMatrix.Data[1, 2] = 0;
            clWorldKoordinateMatrix.Data[1, 3] = 0;

            clWorldKoordinateMatrix.Data[2, 0] = 0;
            clWorldKoordinateMatrix.Data[2, 1] = 0;
            clWorldKoordinateMatrix.Data[2, 2] = 0;
            clWorldKoordinateMatrix.Data[2, 3] = 0;

            clWorldKoordinateMatrix.Data[3, 0] = 0;
            clWorldKoordinateMatrix.Data[3, 1] = 0;
            clWorldKoordinateMatrix.Data[3, 2] = 0;
            clWorldKoordinateMatrix.Data[3, 3] = 0;

            #endregion

            #region Matrix Multiplicate 4x4
            for (int i = 0; i <= 3; i++)
            {
                for (int j = 0; j <= 3; j++)
                {
                    for (int k = 0; k <= 3; k++)
                    {
                        clPointWorldKoordinate.Data[i, j] += clWorldKoordinateMatrix.Data[i, k] * clTranslationsMatrix.Data[k, j];
                    }
                }
            }
            #endregion

            return clPointWorldKoordinate;
        }

        /// <summary>
        /// transform local coordinate system to world coordinate system
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        public double[] TransformLocalToWorldKoordinate(double X, double Y, double DPI)
        {
            // Wir rechnen hier bewusst nicht mit Point da der rundungsfehler inakzeptabel ist
            double[] __dblXYWorldCoordinate = new double[2];

            __dblXYWorldCoordinate[0] = __pntOrigin.X - X;
            __dblXYWorldCoordinate[1] = Y - __pntOrigin.Y;

            __dblXYWorldCoordinate[0] = ((__dblXYWorldCoordinate[0] * 2.54) / DPI);
            __dblXYWorldCoordinate[1] = ((__dblXYWorldCoordinate[1] * 2.54) / DPI);

            // Hier werden die Koordinaten mit 10 multipliziert da das Weltkoordinatensystem in mm definiert ist
            __dblXYWorldCoordinate[0] = __dblXYWorldCoordinate[0] * 10;
            __dblXYWorldCoordinate[1] = __dblXYWorldCoordinate[1] * 10;


            return __dblXYWorldCoordinate;
        }
    }
}
