using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

using CognitiveSafety.Communicate;

using uEye;
using Emgu.CV;
using Emgu.CV.Structure;

namespace SafetyPerformanceTests
{
    public partial class Form1 : Form
    {
        // UEye Cam Variabeln
        private uEye.Camera __uEyeCamera;
        Int32 __intS32LastMemId;
        Int32 __intS32Width;
        Int32 __int__intS32Height;

        private bool filtering = false;
        private Filter filterClass = new Filter();
        private RobotsCommands __robotCommands;

        private string __selectedDevice = "UI164xLE - C_4002747307";

        public Form1()
        {
            InitializeComponent();
        }

        private void __initCamera()
        {
            __uEyeCamera = new uEye.Camera();

            uEye.Defines.Status statusRet = 0;

            // Open __ueyeCamera
            statusRet = __uEyeCamera.Init();
            if (statusRet != uEye.Defines.Status.Success)
            {
                label1.Text = "__ueyeCamera initializing failed";
                Environment.Exit(-1);
            }

            // Set Color Format
            uEye.Types.SensorInfo SensorInfo;
            statusRet = __uEyeCamera.Information.GetSensorInfo(out SensorInfo);

            if (SensorInfo.SensorColorMode == uEye.Defines.SensorColorMode.Bayer)
            {
                statusRet = __uEyeCamera.PixelFormat.Set(uEye.Defines.ColorMode.BGR8Packed);
            }
            else
            {
                statusRet = __uEyeCamera.PixelFormat.Set(uEye.Defines.ColorMode.Mono8);
            }


            // Allocate Memory
            statusRet = __uEyeCamera.Memory.Allocate();
            if (statusRet != uEye.Defines.Status.Success)
            {
                label1.Text = "Allocate Memory failed";
                Environment.Exit(-1);
            }

            // Start Live Video
            statusRet = __uEyeCamera.Acquisition.Capture();
            if (statusRet != uEye.Defines.Status.Success)
            {
                label1.Text = "Start Live Video failed";
            }


            // Get last image memory
            __uEyeCamera.AutoFeatures.Software.WhiteBalance.SetEnable(true);
            __uEyeCamera.AutoFeatures.Software.Shutter.SetEnable(true);
            __uEyeCamera.AutoFeatures.Software.Gain.SetEnable(true);
            Thread.Sleep(3000); // Wait if Gain is finished
            __uEyeCamera.Focus.Auto.SetEnable(true);

            // Connect Event
            __uEyeCamera.EventFrame += onFrameEvent;
        }

        private void ExecuteStartLiveVideoCommand(object sender, EventArgs e)
        {
            if (__selectedDevice.Contains("UI164xLE - C") == true)
            {
                if (__uEyeCamera == null)
                {
                    __initCamera();
                }

                // Open Camera and Start Live Video
                if (__uEyeCamera.Acquisition.Capture() == uEye.Defines.Status.Success)
                {
                }
            }
        }

        private void onFrameEvent(object sender, EventArgs e)
        {
            uEye.Defines.Status statusRet = 0;
            Bitmap __bmpMyBitmap = null;

            // Get last image memory

            statusRet = __uEyeCamera.Memory.GetLast(out __intS32LastMemId);
            statusRet = __uEyeCamera.Memory.Lock(__intS32LastMemId);
            statusRet = __uEyeCamera.Memory.GetSize(__intS32LastMemId, out __intS32Width, out __int__intS32Height);


            statusRet = __uEyeCamera.Memory.ToBitmap(__intS32LastMemId, out __bmpMyBitmap); 

            // unlock image buffer
            statusRet = __uEyeCamera.Memory.Unlock(__intS32LastMemId);

            if(filtering)
            {
                livePicture.Image = filterClass.gaussFilter(__bmpMyBitmap); //display it in pictureBox
            }
            else
                livePicture.Image = __bmpMyBitmap;
        }

        private void filter_Click(object sender, EventArgs e)
        {
            if (filtering)
            {
                filtering = false;
                label1.Text = "Live Video";
            }
            else
            {
                label1.Text = "Filtering...";
                filtering = true;
            }
        }
    }
}
