using CognitiveRobotik.ModuleA.Models.Calculate;
//using CognitiveRobotik.ModuleA.Models.Data;
//using CognitiveRobotik.ModuleA.ViewModels;
using Emgu.CV;
//using Prism.Commands;
//using Prism.Events;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CognitiveSafety.Communicate;


namespace CognitiveSafety.Communicate
{
    /// <summary>
    /// Contains the interaction logic and the standard commands of the robot
    /// </summary>
    public class RobotsCommands
    {
        private SocketData __socketData;
        private TransformationCoordinateSystems __transformationCoordinateSystems;

        private string __strPosition { get; set; }
        public RobotsCommands( ref TransformationCoordinateSystems transformationCoordinateSystems)
        {
            //ThresholdValues
 
            __transformationCoordinateSystems = transformationCoordinateSystems;

            __socketData = new SocketData("127.0.0.1", 32572);
            //HostCommands.ShutdownCommand.RegisterCommand(new DelegateCommand(this.ExitApplication, this.CanExitApplication));

            
        }

        /// <summary>
        /// Check if Socket Can Close
        /// </summary>
        /// <returns></returns>
        public bool CanExitApplication()
        {

            return true;
        }

        /// <summary>
        /// Closes the socket
        /// </summary>
        public void ExitApplication()
        {
            __socketData.SocketClose();
        }


        /// <summary>
        /// Drives the robot out of the camera's  vision field
        /// </summary>
        public void MoveOutOfView()
        {

            string __strPosition = "-178.9,+2.7,+408.1,-79.9,+89.9";
            string __strRecieve = "";
            __socketData.SendDataSocket("TL 77");

            __strRecieve = __socketData.SendDataSocketSingleStep("WH");


            if (__strRecieve != __strPosition)
            {
                #region Move Robot out of the Vision View

                string __strPoint = "PD 30,-178.9,+2.7,+408.1,-79.9,+89.9";
                string __strMove = "MO 30";


                // send Data
                __socketData.SendDataSocket(__strPoint);
                __socketData.SendDataSocket(__strMove);


                __strPoint = null;
                __strMove = null;

                #endregion  

            }
        }

        /// <summary>
        /// Draws the TicTacToe board
        /// </summary>
        public void DrawPlayingField()
        {
            int __intSpielfeld = 150;        //Spielfeldgröße in mm
            int __intX = -((__intSpielfeld / 2) + 3);
            int __intY = 400;
            //string __strZZeichnen = __thresholdValues.ValuePinHeight.ToString().Replace(",", ".");      //Stift auf Papier  --aufs papier
            string __strZNeuPos = "140";        //Höhe zum neuen Punkt anfahren

            //Variabeln für Spielfeldpositionen
            int __intX0 = __intX;
            int __intX1 = __intX + (__intSpielfeld / 3);
            int __intX2 = __intX + (__intSpielfeld * 2 / 3);
            int __intX3 = __intSpielfeld + __intX;
            int __intY0 = __intY;
            int __intY1 = __intY - (__intSpielfeld / 3);
            int __intY2 = __intY - (__intSpielfeld * 2 / 3);
            int __intY3 = __intY - __intSpielfeld;

            string __strX0 = __intX0.ToString();
            string __strX1 = __intX1.ToString();
            string __strX2 = __intX2.ToString();
            string __strX3 = __intX3.ToString();
            string __strY0 = __intY0.ToString();
            string __strY1 = __intY1.ToString();
            string __strY2 = __intY2.ToString();
            string __strY3 = __intY3.ToString();


            __socketData.SendDataSocket("TL 77");


            /*


            // Definieren der Startposition
            __socketData.SendDataSocket("PD 20," + __strX0 + ", " + __strY0 + ", " + __strZNeuPos + ", -90, 90");

            // Definieren der erster Punkt mit Stift Stabilo
            __socketData.SendDataSocket("PD 21," + __strX0 + ", " + __strY0 + ", " + __strZZeichnen + ", -90, 90");
            // Erste Linie __intSpielfeld
            __socketData.SendDataSocket("PD 22," + __strX3 + ", " + __strY0 + ", " + __strZZeichnen + ", -90, 90");
            // Zweite Linie __intSpielfeld
            __socketData.SendDataSocket("PD 23," + __strX3 + ", " + __strY3 + ", " + __strZZeichnen + ", -90, 90");
            // Dritte Linie __intSpielfeld
            __socketData.SendDataSocket("PD 24," + __strX0 + ", " + __strY3 + ", " + __strZZeichnen + ", -90, 90");
            // Vierte Linie __intSpielfeld
            __socketData.SendDataSocket("PD 25," + __strX0 + ", " + __strY0 + ", " + __strZZeichnen + ", -90, 90");


            // Stift Anheben um neue position anzufahren
            __socketData.SendDataSocket("PD 26," + __strX0 + ", " + __strY0 + ", " + __strZNeuPos + ", -90, 90");
            // Position für Linie 5 anfahren
            __socketData.SendDataSocket("PD 27," + __strX0 + ", " + __strY1 + ", " + __strZNeuPos + ", -90, 90");
            // Position auf Linie 5 anfahren (abwärts)
            __socketData.SendDataSocket("PD 28," + __strX0 + ", " + __strY1 + ", " + __strZZeichnen + ", -90, 90");
            // Linie 5 Zeichnen
            __socketData.SendDataSocket("PD 29," + __strX3 + ", " + __strY1 + ", " + __strZZeichnen + ", -90, 90");

            // Stift Anheben um neue position anzufahren
            __socketData.SendDataSocket("PD 30," + __strX3 + ", " + __strY1 + ", " + __strZNeuPos + ", -90, 90");
            // Position für Linie 6 anfahren
            __socketData.SendDataSocket("PD 31," + __strX3 + ", " + __strY2 + ", " + __strZNeuPos + ", -90, 90");
            // Position auf Linie 6 anfahren (abwärts)
            __socketData.SendDataSocket("PD 32," + __strX3 + ", " + __strY2 + ", " + __strZZeichnen + ", -90, 90");
            // Linie 6 Zeichnen
            __socketData.SendDataSocket("PD 33," + __strX0 + ", " + __strY2 + ", " + __strZZeichnen + ", -90, 90");

            // Stift Anheben um neue position anzufahren
            __socketData.SendDataSocket("PD 34," + __strX0 + ", " + __strY2 + ", " + __strZNeuPos + ", -90, 90");
            // Position für Linie 7 anfahren
            __socketData.SendDataSocket("PD 35," + __strX1 + ", " + __strY3 + ", " + __strZNeuPos + ", -90, 90");
            // Position auf Linie 7 anfahren (abwärts)
            __socketData.SendDataSocket("PD 36," + __strX1 + ", " + __strY3 + ", " + __strZZeichnen + ", -90, 90");
            // Linie 7 Zeichnen
            __socketData.SendDataSocket("PD 37," + __strX1 + ", " + __strY0 + ", " + __strZZeichnen + ", -90, 90");

            // Stift Anheben um neue position anzufahren
            __socketData.SendDataSocket("PD 38," + __strX1 + ", " + __strY0 + ", " + __strZNeuPos + ", -90, 90");
            // Position für Linie 8 anfahren
            __socketData.SendDataSocket("PD 39," + __strX2 + ", " + __strY0 + ", " + __strZNeuPos + ", -90, 90");
            // Position auf Linie 8 anfahren (abwärts)
            __socketData.SendDataSocket("PD 40," + __strX2 + ", " + __strY0 + ", " + __strZZeichnen + ", -90, 90");
            // Linie 7 Zeichnen
            __socketData.SendDataSocket("PD 41," + __strX2 + ", " + __strY3 + ", " + __strZZeichnen + ", -90, 90");


            // Stift Anheben um neue position anzufahren
            __socketData.SendDataSocket("PD 42," + __strX2 + ", " + __strY3 + ", " + __strZNeuPos + ", -90, 90");

            */

            __socketData.SendDataSocket("MO 20");
            __socketData.SendDataSocket("MO 21");


            //Zeichnen 1 Linie
            __socketData.SendDataSocket("MS 22,6");
            __socketData.SendDataSocket("TI 5");


            //Zeichnen 2 Linie
            __socketData.SendDataSocket("MS 23,6");
            __socketData.SendDataSocket("TI 5");


            //Zeichnen 3 Linie
            __socketData.SendDataSocket("MS 24,6");
            __socketData.SendDataSocket("TI 5");


            //Zeichnen 4 Linie
            __socketData.SendDataSocket("MS 25,6");
            __socketData.SendDataSocket("TI 5");

            //Zeichnen 5 Linie
            __socketData.SendDataSocket("MS 26,6");
            __socketData.SendDataSocket("TI 5");
            __socketData.SendDataSocket("MS 27,6");
            __socketData.SendDataSocket("TI 5");
            __socketData.SendDataSocket("MS 28,6");
            __socketData.SendDataSocket("TI 5");
            __socketData.SendDataSocket("MS 29,6");
            __socketData.SendDataSocket("TI 5");

            //Zeichnen 6 Linie
            __socketData.SendDataSocket("MS 30,6");
            __socketData.SendDataSocket("TI 5");
            __socketData.SendDataSocket("MS 31,6");
            __socketData.SendDataSocket("TI 5");
            __socketData.SendDataSocket("MS 32,6");
            __socketData.SendDataSocket("TI 5");
            __socketData.SendDataSocket("MS 33,6");
            __socketData.SendDataSocket("TI 5");


            //Zeichnen 7 Linie
            __socketData.SendDataSocket("MS 34,6");
            __socketData.SendDataSocket("TI 5");
            __socketData.SendDataSocket("MS 35,6");
            __socketData.SendDataSocket("TI 5");
            __socketData.SendDataSocket("MS 36,6");
            __socketData.SendDataSocket("TI 5");
            __socketData.SendDataSocket("MS 37,6");
            __socketData.SendDataSocket("TI 5");


            //Zeichnen 8 Linie
            __socketData.SendDataSocket("MS 38,6");
            __socketData.SendDataSocket("TI 5");
            __socketData.SendDataSocket("MS 39,6");
            __socketData.SendDataSocket("TI 5");
            __socketData.SendDataSocket("MS 40,6");
            __socketData.SendDataSocket("TI 5");
            __socketData.SendDataSocket("MS 41,6");
            __socketData.SendDataSocket("TI 5");


            //Safety_move anfahren
            __socketData.SendDataSocket("MS 42,6");
            __socketData.SendDataSocket("TI 5");
            __socketData.SendDataSocket("MO 13");

            MoveOutOfView();

        }

        /// <summary>
        /// Sends the command to the Drive Unit to switch the light on
        /// </summary>
        public void LightOn()
        {
            __socketData.SendDataSocket("OB +15");
        }

        /// <summary>
        /// Sends the command to the Drive Unit to switch the light off
        /// </summary>
        public void LightOff()
        {
            __socketData.SendDataSocket("OB -15");
        }

        /// <summary>
        /// Sends the command to close the roboETH Socket
        /// </summary>
        public void SocketClose()
        {
            __socketData.SocketClose() ;
        }

        /// <summary>
        /// Command to retrieve the pen from the holder
        /// </summary>
        public void GrapPen()
        {

            __socketData.SendDataSocket("TL 77");

            __socketData.SendDataSocket("GO");


            //Erste Abholposition Stift
            __socketData.SendDataSocket("PD 10, -126.3,+347.6,+115.2,-30.0,+90.0");
            //Stift abholen
            __socketData.SendDataSocket("PD 11, -178.9,+323.6,+115.2,-30.0,+90.0");
            //Stift zurueckziehen
            __socketData.SendDataSocket("PD 12, -171.9,+305.6,+126.2,-30.0,+90.0");
            //Stift aus Kollisionsbereich fahren
            __socketData.SendDataSocket("PD 13, -230.0,+350 ,400,0,+90");

            __socketData.SendDataSocket("MO 13");
            __socketData.SendDataSocket("MO 10");
            __socketData.SendDataSocket("MO 11");
            __socketData.SendDataSocket("GC");
            __socketData.SendDataSocket("MO 12");
            __socketData.SendDataSocket("MO 13");

            MoveOutOfView();

        }

        /// <summary>
        /// Command to return the pen to the holder
        /// </summary>
        public void DiscardPen()
        {
            __socketData.SendDataSocket("TL 77");

            //Erste Abholposition Stift
            __socketData.SendDataSocket("PD 10, -126.3,+347.6,+115.2,-30.0,+90.0");
            //Stift abholen
            __socketData.SendDataSocket("PD 11, -178.9,+323.6,+115.2,-30.0,+90.0");
            //Stift zurueckziehen
            __socketData.SendDataSocket("PD 12, -171.9,+305.6,+126.2,-30.0,+90.0");
            //Stift aus Kollisionsbereich fahren
            __socketData.SendDataSocket("PD 13, -230.0,+350 ,400,0,+90");

            __socketData.SendDataSocket("MO 13");
            __socketData.SendDataSocket("MO 12");
            __socketData.SendDataSocket("MO 11");
            __socketData.SendDataSocket("GO");
            __socketData.SendDataSocket("MO 10");
            __socketData.SendDataSocket("MO 13");

            MoveOutOfView();


        }

        /// <summary>
        /// Draws the move of the computer into the playing field
        /// </summary>
        /// <param name="Form"> The symbol which the computer represent</param>
        /// <param name="XCoordinate">X Coordinate where the symbol is to be drawn</param>
        /// <param name="YCoordinate">Y Coordinate where the symbol is to be drawn</param>
        /// <param name="DPI"> DPI Value</param>
        /// <param name="drawHeight">Size of the object to be drawn</param>
        public void RobotDrawObject(string Form, double XCoordinate, double YCoordinate, double DPI, double drawHeight)
        {
            

            double[] temp_x_y_P1 = new double[2];
            double[] temp_x_y_P2 = new double[2];
            double[] temp_x_y_P3 = new double[2];
            double[] temp_x_y_P4 = new double[2];

            Matrix<Double> tempMatrix1 = new Matrix<double>(1, 4);
            Matrix<Double> tempMatrix2 = new Matrix<double>(1, 4);
            Matrix<Double> tempMatrix3 = new Matrix<double>(1, 4);
            Matrix<Double> tempMatrix4 = new Matrix<double>(1, 4);

            double distance = 0;

            int hohe_bewegen = (int)drawHeight + 20;

            //Kreuze zeichnen
            if (Form == "Cross")
            {
                distance = (1 * DPI) / 2.54;

                double P1X = (XCoordinate - distance);
                double P1Y = (YCoordinate - distance);

                double P2X = (XCoordinate + distance);
                double P2Y = (YCoordinate - distance);

                double P3X = (XCoordinate - distance);
                double P3Y = (YCoordinate + distance);

                double P4X = (XCoordinate + distance);
                double P4Y = (YCoordinate + distance);

                //Kamera in Welt
/*
                temp_x_y_P1 = __transformationCoordinateSystems.TransformLocalToWorldKoordinate(P1X, P1Y,__dPI._DPI);
                temp_x_y_P2 = __transformationCoordinateSystems.TransformLocalToWorldKoordinate(P2X, P2Y, __dPI._DPI);
                temp_x_y_P3 = __transformationCoordinateSystems.TransformLocalToWorldKoordinate(P3X, P3Y, __dPI._DPI);
                temp_x_y_P4 = __transformationCoordinateSystems.TransformLocalToWorldKoordinate(P4X, P4Y, __dPI._DPI);

                tempMatrix1 = __transformationCoordinateSystems.TransformWorldToBaseKoordinate(temp_x_y_P1[0], temp_x_y_P1[1]);
                tempMatrix2 = __transformationCoordinateSystems.TransformWorldToBaseKoordinate(temp_x_y_P2[0], temp_x_y_P2[1]);
                tempMatrix3 = __transformationCoordinateSystems.TransformWorldToBaseKoordinate(temp_x_y_P3[0], temp_x_y_P3[1]);
                tempMatrix4 = __transformationCoordinateSystems.TransformWorldToBaseKoordinate(temp_x_y_P4[0], temp_x_y_P4[1]);

    */


                //Punkte Kreuz auf Feld
                

                __socketData.SendDataSocket("PD 1," + Math.Round(tempMatrix1[0, 0], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + Math.Round(tempMatrix1[0, 1], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + drawHeight.ToString().Replace(",", ".") + "," + " -90, 90");
                __socketData.SendDataSocket("PD 2," + Math.Round(tempMatrix2[0, 0], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + Math.Round(tempMatrix2[0, 1], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + drawHeight.ToString().Replace(",", ".") + "," + " -90, 90");
                __socketData.SendDataSocket("PD 3," + Math.Round(tempMatrix3[0, 0], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + Math.Round(tempMatrix3[0, 1], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + drawHeight.ToString().Replace(",", ".") + "," + " -90, 90");
                __socketData.SendDataSocket("PD 4," + Math.Round(tempMatrix4[0, 0], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + Math.Round(tempMatrix4[0, 1], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + drawHeight.ToString().Replace(",", ".") + "," + " -90, 90");
                //Punkte Kreuz über Feld                                                                                            
                __socketData.SendDataSocket("PD 5," + Math.Round(tempMatrix1[0, 0], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + Math.Round(tempMatrix1[0, 1], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + hohe_bewegen.ToString() + "," + " -90, 90");
                __socketData.SendDataSocket("PD 6," + Math.Round(tempMatrix2[0, 0], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + Math.Round(tempMatrix2[0, 1], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + hohe_bewegen.ToString() + "," + " -90, 90");
                __socketData.SendDataSocket("PD 7," + Math.Round(tempMatrix3[0, 0], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + Math.Round(tempMatrix3[0, 1], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + hohe_bewegen.ToString() + "," + " -90, 90");
                __socketData.SendDataSocket("PD 8," + Math.Round(tempMatrix4[0, 0], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + Math.Round(tempMatrix4[0, 1], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + hohe_bewegen.ToString() + "," + " -90, 90");
                //Punkte anfahren
                __socketData.SendDataSocket("MO 5");
                __socketData.SendDataSocket("TI 5");
                __socketData.SendDataSocket("MS 1,6");
                __socketData.SendDataSocket("TI 5");
                __socketData.SendDataSocket("MS 4,6");
                __socketData.SendDataSocket("TI 5");
                __socketData.SendDataSocket("MS 8,6");
                __socketData.SendDataSocket("TI 5");
                __socketData.SendDataSocket("MS 6,6");
                __socketData.SendDataSocket("TI 5");
                __socketData.SendDataSocket("MS 2,6");
                __socketData.SendDataSocket("TI 5");
                __socketData.SendDataSocket("MS 3,6");
                __socketData.SendDataSocket("TI 5");
                __socketData.SendDataSocket("MS 7,6");

                MoveOutOfView();
            }

            //Kreise zeichnen
            else if (Form == "Circle")
            {
                distance = ((1.5 * DPI) / 2.54);

                //Stützpunkte berechnen für Kreis



                double angle = (2 * Math.PI) / 90;
                double radian = (1.5 * DPI) / 2.54;

                /*
                //Punkt über Kreis definieren
                var LocalWorld1 = __transformationCoordinateSystems.TransformLocalToWorldKoordinate(((Math.Cos(1 * angle) * radian) + XCoordinate), (Math.Sin(1 * angle) * radian) + YCoordinate, __dPI._DPI);
                var temp1 = __transformationCoordinateSystems.TransformWorldToBaseKoordinate(LocalWorld1[0], LocalWorld1[1]);
                __socketData.SendDataSocket("PD " + "98" + "," + Math.Round(temp1[0, 0], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + Math.Round(temp1[0, 1], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + hohe_bewegen.ToString() + "," + " -90, 90");
                string pos = null;
                for (int i = 1; i < 92; i++)
                {

                    var LocalWorld = __transformationCoordinateSystems.TransformLocalToWorldKoordinate(((Math.Cos(i * angle) * radian) + XCoordinate), (Math.Sin(i * angle) * radian) + YCoordinate, __dPI._DPI);
                    var temp = __transformationCoordinateSystems.TransformWorldToBaseKoordinate(LocalWorld[0], LocalWorld[1]);
                    if (i == 15)
                    {
                        pos = "PD " + i.ToString() + "," + Math.Round(temp[0, 0], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + Math.Round(temp[0, 1] - 5, 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + hohe_bewegen.ToString() + "," + " -90, 90";
                    }
                    //Punkte Definieren beim Y-Wert 
                    __socketData.SendDataSocket("PD " + i.ToString() + "," + Math.Round(temp[0, 0], 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + Math.Round(temp[0, 1] , 1, MidpointRounding.AwayFromZero).ToString().Replace(",", ".") + "," + drawHeight.ToString().Replace(",", ".") + "," + " -90, 90");

                }
                __socketData.SendDataSocket("MO 98");
                __socketData.SendDataSocket("MC 1, 90");
                __socketData.SendDataSocket("MC 1, 15");
                //Stift anheben
                __socketData.SendDataSocket(pos);
                __socketData.SendDataSocket("MO 15");
                MoveOutOfView();

            */

            }



        }

        /// <summary>
        /// Moves the robot to the world coordinates origin
        /// </summary>
        public void Orgin()
        {
            if (__socketData.SendDataSocketSingleStep("WH") != __strPosition)
            {
                MoveOutOfView();
            }

            //Ursprung anfahren
            /*
            __socketData.SendDataSocket("PD 10, -78.2,+253.6," + __thresholdValues.ValuePinHeight.ToString().Replace(",", ".") + ",-92.0,+89.9");
            __socketData.SendDataSocket("MO 10");
            __strPosition = "-78.2,+253.6," + "+" + __thresholdValues.ValuePinHeight.ToString().Replace(",", ".") + ",-92.0,+89.9";

    */
        }
    }
}
