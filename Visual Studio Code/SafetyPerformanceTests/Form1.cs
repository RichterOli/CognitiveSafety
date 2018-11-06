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
using System.Windows.Threading;
using System.Diagnostics;

using CognitiveSafety.Communicate;
using uEye;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Cuda;

namespace SafetyPerformanceTests
{
    public partial class Form1 : Form
    {
        // Camera
        private uEye.Camera __uEyeCamera = null;

        // Frame
        private int __intS32LastMemId;
        private int __intS32Width;
        private int __intS32Height;
        Image<Bgr, byte> __imgCurrentFrame = null;

        // Filter
        private Filter __filterClass = new Filter();
        private bool __filtering = false;

        // Robo
        private RobotsCommands __robotCommands;

        // Textboxes
        List<TextBox> textBoxes = new List<TextBox>();

        public Form1()
        {
            InitializeComponent();
        }

        private void filter_Click(object sender, EventArgs e)
        {
            if (__filtering)
            {
                __filtering = false;
                label1.Text = "Live Video";
            }
            else
            {
                // make Position TextBox visible
                tbPosition.Visible = true;

                // make histogram boxes invisible
                histogramBox1.Visible = false;
                histogramBox2.Visible = false;
                histogramBox3.Visible = false;

                // show color textboxes
                gbColorFilterVal.Visible = true;

                label1.Text = "Filtering...";
                __filtering = true;
            }
        }

        private void onFormLoad(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

            // Store a reference to every textbox in the form
            textBoxes.Add(tbBlueMin);
            textBoxes.Add(tbGreenMin);
            textBoxes.Add(tbRedMin);
            textBoxes.Add(tbBlueMax);
            textBoxes.Add(tbGreenMax);
            textBoxes.Add(tbRedMax);

            // Create camera object
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

            this.WindowState = FormWindowState.Maximized;
        }

        /// <summary>
        /// Checks if the value of a textbox is a integer which is in range from 0 to 255.
        /// </summary>
        /// <param name="textBox">Textbox element that should be checked</param>
        /// <param name="colorValue">Value inside the textbox: 0 - 255
        /// If the textbox is empty, not an integer or not a part of the given range: 0</param>
        /// <returns></returns>
        private void checkTextBox(TextBox textBox, out int colorValue)
        {
            colorValue = (int)SafetyPerformanceTests.Color.MaxValue;

            // if the user has not assigned a value set it to MaxValue (0)
            if (textBox.Text == "")
                return;

            // check if the Text in the textbox is a integer
            if (!int.TryParse(textBox.Text, out int i))
                return;

            colorValue = Convert.ToInt32(textBox.Text);

            // check if the value is inside the range from 0 to 255, if not set it to 0
            if (colorValue < 0 || colorValue > 255)
                colorValue = (int)SafetyPerformanceTests.Color.MaxValue;

            return;
        }

        /// <summary>
        /// Function for the image processing after every frame update of the camera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onFrameEvent(object sender, EventArgs e)
        {
            uEye.Defines.Status statusRet = 0;
            Bitmap bmpMyBitmap = null;

            // Get last image memory

            statusRet = __uEyeCamera.Memory.GetLast(out __intS32LastMemId);
            statusRet = __uEyeCamera.Memory.Lock(__intS32LastMemId);
            statusRet = __uEyeCamera.Memory.GetSize(__intS32LastMemId, out __intS32Width, out __intS32Height);


            statusRet = __uEyeCamera.Memory.ToBitmap(__intS32LastMemId, out bmpMyBitmap);

            // clone bitmap
            Rectangle cloneRect = new Rectangle(0, 0, __intS32Width, __intS32Height);
            System.Drawing.Imaging.PixelFormat format = System.Drawing.Imaging.PixelFormat.Format32bppArgb;

            __imgCurrentFrame = new Image<Bgr, byte>(bmpMyBitmap.Clone(cloneRect, format));

            // catch cross threading exception, when form is closed
            try
            {
                // show the original image in the form
                imgbOriginal.Image = __imgCurrentFrame;
            }
            catch
            {
                return;
            }



            if (__filtering)
            {
                int i = 0;
                int[] colorVals = new int[6];

                foreach (TextBox textBox in textBoxes)
                {
                    int colorValue;

                    checkTextBox(textBox, out colorValue);

                    colorVals[i++] = colorValue;

                }

                // catch cross threading exception, when form is closed
                try
                {
                    // show the processed image in the form
                    imgbProcessed.Image = __filterClass.filterColor(bmpMyBitmap, ref colorVals); //display it in pictureBox
                }
                catch(Exception exception)
                {
                    return;
                }
             }

            // unlock image buffer
            statusRet = __uEyeCamera.Memory.Unlock(__intS32LastMemId);

            // Prevent RAM Out of Memory Exception
            GC.Collect();
        }

        private void buttonTakePicture_Click(object sender, EventArgs e)
        {
            __imgCurrentFrame.Save(@"C:\Users\olive\Pictures\hand.jpg");
        }

        private void buttonHistogramm_Click(object sender, EventArgs e)
        {
            // make Position Textbox invisible
            tbPosition.Visible = false;

            // make histogram boxes invisible
            histogramBox1.Visible = true;
            histogramBox2.Visible = true;
            histogramBox3.Visible = true;

            // make color textboxes invisible
            gbColorFilterVal.Visible = false;

            Image<Gray, byte> blueChannel = __imgCurrentFrame[0];
            Image<Gray, byte> greenChannel = __imgCurrentFrame[1];
            Image<Gray, byte> redChannel = __imgCurrentFrame[2];

            DenseHistogram histRed = new DenseHistogram(256, new RangeF(0.0f, 255.0f));
            histRed.Calculate(new Image<Gray, byte>[] { redChannel }, false, null);
            DenseHistogram histBlue = new DenseHistogram(256, new RangeF(0.0f, 255.0f));
            histBlue.Calculate(new Image<Gray, byte>[] { blueChannel }, false, null);
            DenseHistogram histGreen = new DenseHistogram(256, new RangeF(0.0f, 255.0f));
            histGreen.Calculate(new Image<Gray, byte>[] { greenChannel }, false, null);

            Mat matRed = new Mat();
            histRed.CopyTo(matRed);
            Mat matBlue = new Mat();
            histBlue.CopyTo(matBlue);
            Mat matGreen = new Mat();
            histGreen.CopyTo(matGreen);

            histogramBox1.ClearHistogram();
            histogramBox2.ClearHistogram();
            histogramBox3.ClearHistogram();

            histogramBox1.AddHistogram("Rot-Kanal Histogramm mit Hand", System.Drawing.Color.Red, matRed, 256, new float[] { 0f, 255f });
            histogramBox2.AddHistogram("Blau-Kanal Histogramm mit Hand", System.Drawing.Color.Blue, matBlue, 256, new float[] { 0f, 255f });
            histogramBox3.AddHistogram("Grün-Kanal Histogramm mit Hand", System.Drawing.Color.Green, matGreen, 256, new float[] { 0f, 255f });

            histogramBox1.Refresh();
            histogramBox2.Refresh();
            histogramBox3.Refresh();
        }
    }
}
