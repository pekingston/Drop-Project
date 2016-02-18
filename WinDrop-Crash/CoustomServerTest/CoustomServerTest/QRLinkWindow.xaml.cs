using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace CoustomServerTest
{
    /// <summary>
    /// Lógica de interacción para QRLinkWindow.xaml
    /// </summary>
    public partial class QRLinkWindow : Window
    {
        public QRLinkWindow(PedroFileDownload fileData)
        {

            InitializeComponent();
            //PayloadGenerator.Url url2 = new PayloadGenerator.Url("www.google.es");
            PayloadGenerator.Url url = new PayloadGenerator.Url(fileData.Url + @"/index.html");
            //PayloadGenerator.WiFi wifiPayload = new PayloadGenerator.WiFi("MyWiFi-SSID", "MyWiFi-Pass", PayloadGenerator.WiFi.Authentication.WPA);
            QRCodeGenerator.ECCLevel  eccLevel= QRCodeGenerator.ECCLevel.L;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            //QRCodeData qrCodeData = qrGenerator.CreateQrCode(@fileData.Url+@"\index.html", eccLevel);
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url.ToString(), eccLevel);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeBitmap = qrCode.GetGraphic(20, "#000000", "#ffffff");

            ImageSource qrImageSource =ConvertBitmapToBitmapImage(qrCodeBitmap);
            imgQrCode.Source = qrImageSource;
        }

        private BitmapImage ConvertBitmapToBitmapImage(Bitmap bmp)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bmp.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                return bitmapImage;
            }
        }
    }
}
