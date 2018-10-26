using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CognitiveSafety.Communicate
{
    /// <summary>
    /// Provides communication with the robot via socket
    /// </summary>
    public class SocketData
    {
        private Socket __sockSocket;
        private string __strRecievedMessage { get; set; }
        public SocketData(string __strAdress, int __intPort)
        {
            try
            {
                IPHostEntry hostInfo = Dns.GetHostByName(__strAdress);
                System.Net.IPEndPoint ep = new System.Net.IPEndPoint(hostInfo.AddressList[0], __intPort);
                __sockSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                __sockSocket.Connect(ep);

            }
            catch (Exception ex)
            {
                throw new Exception("Fehler beim Herstellen der Verbindung zum Server", ex);
            }
        }

        /// <summary>
        /// Sends a message to the socket and checks the response. This function is runtime optimized. It is not possible to evaluate the message.
        /// </summary>
        /// <param name="data">Order Robot</param>
        public void SendDataSocket(string data)
        {
            try
            {
                byte[] __bytData = System.Text.Encoding.ASCII.GetBytes(data);
                __sockSocket.Send(__bytData, SocketFlags.None);
                __bytData = null;
                __strRecievedMessage = RecieveDataSocket();

                //Check Error Code
                byte[] __bytDataFault = System.Text.Encoding.ASCII.GetBytes("ER");
                __sockSocket.Send(__bytDataFault, SocketFlags.None);
                __bytDataFault = null;

                __strRecievedMessage = RecieveDataSocket();

                if (__strRecievedMessage == "1")
                {
                    /*System.Windows.Application.Current.MainWindow.Dispatcher.Invoke((Action)async delegate
                    {
                        var metroWindow = (System.Windows.Application.Current.MainWindow as MetroWindow);
                        metroWindow.FontSize = 10;
                        await metroWindow.ShowMessageAsync("Error", "Fatal error please restart robot and roboETH");
                    });*/
                }
                else if (__strRecievedMessage == "2")
                {
                    SendDataSocket("RS");
                }
                else if (__strRecievedMessage == "")
                {
                    //TODO
                }
            }
            catch
            {

            }

        }

        /// <summary>
        /// Sends a command to the socket and then evaluates the message
        /// </summary>
        /// <param name="data">Order Robot</param>
        /// <returns></returns>
        public string SendDataSocketSingleStep(string data)
        {
            byte[] __bytData = System.Text.Encoding.ASCII.GetBytes(data);
            __sockSocket.Send(__bytData, SocketFlags.None);
            __bytData = null;

            return RecieveDataSocket();
        }

        /// <summary>
        /// Close the Socket
        /// </summary>
        public void SocketClose()
        {
            __sockSocket.Close();

        }

        /// <summary>
        /// Processes the response from the socket
        /// </summary>
        /// <returns></returns>
        public string RecieveDataSocket()
        {
            try
            {
                //Antwort vom Server
                byte[] __bytBuffer = new byte[1024];
                int __intIrx = __sockSocket.Receive(__bytBuffer);
                char[] __chCars = new char[__intIrx];

                Decoder __decD = Encoding.UTF8.GetDecoder();
                int __intCharLen = __decD.GetChars(__bytBuffer, 0, __intIrx, __chCars, 0);
                String __strRecv = new String(__chCars);

                return SplitXamlMessage(__strRecv, "<MESSAGE>", "</MESSAGE>");
            }
            catch
            {
                return "";
            }
        }


        /// <summary>
        /// Filters the response from the server
        /// </summary>
        /// <param name="Input"> Message from the Server</param>
        /// <param name="FirstString">Beginning of the message</param>
        /// <param name="LastString">Ending of the message</param>
        /// <returns></returns>
        public string SplitXamlMessage(string Input, string FirstString, string LastString)
        {

            int __intStartMessage = Input.IndexOf(FirstString) + FirstString.Length;

            int __intEndMessage = Input.IndexOf(LastString);

            return Input.Substring(__intStartMessage, __intEndMessage - __intStartMessage);
        }

    }
}
