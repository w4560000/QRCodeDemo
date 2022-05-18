using Ionic.Zip;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace QRCodeDemo
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode("https://www.google.com.tw/", QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            string outputFileName = "D:\\Test\\Code.png";
            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(outputFileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    qrCodeImage.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

            using (ZipFile zip = new ZipFile())
            {
                // 1. 欲壓縮的資料夾
                // 2. 解壓縮後生成的資料夾
                zip.AddDirectory("D:\\Test");
                //zip.AddDirectory("D:\\Test", "aaa");

                Console.WriteLine("將 Test 資料夾壓縮成ZIP 檔案");

                zip.Comment = "This is test zip";

                zip.Save("D:\\ZipDemo\\ZipDemo.zip");
            }

            Console.ReadKey();
        }
    }
}