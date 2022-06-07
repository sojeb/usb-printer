using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PdfiumViewer;
using Syncfusion.Windows.Forms.Barcode;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.ServiceProcess;
using System.Threading;

namespace PrintingSerivce
{
    public partial class Service1 : ServiceBase
    {
       // public static String file = "";
        public static String a4PrinterName="", tharmalPrinterName="";
      //  string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

        // static String file = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/printer" + "/document.pdf";
        // C:\Users\Public\Documents\printer
        static String folderDirectory = "C:\\Users\\Public\\Documents" + "\\printer";
        static String fileA4size = "C:\\Users\\Public\\Documents" + "\\printer" + "\\document_a4size.pdf";
        static String fileThermal= "C:\\Users\\Public\\Documents" + "\\printer" + "\\document_thermal.pdf";

        static String url = "";

        string a4PrinterData = "[{\"id\":\"402\",\"cn_number\":\"1000040000402\",\"sender_contact_no\":\"01990352988\",\"sender_reference\":\"test\",\"sender_name\":\"sojeb\",\"sender_address\":\"Noakhali-HUB\",\"receiver_contact_no\":\"01990352988\",\"receiver_name\":\"popular\",\"receiver_address\":\"Barishal\",\"created_at\":\"2022-05-2916:35:23.000\",\"print_status\":\"0\",\"total_lot_qty\":\"4\",\"condition_amount\":\".00\",\"condition_mms_charge\":\".00\",\"is_home_delivery\":\"1\",\"net_amount\":\"250.00\",\"user_name\":\"NasrullahAlJadid\",\"sender_branch_name\":\"DHAKA-HUB\",\"receiver_branch_name\":\"Barishal\",\"department_name\":\"Courier\",\"service_name\":\"Document\",\"item_qty\":\"1\",\"items\":\"AMBBattery(Ctn)\"},{\"id\":\"402\",\"cn_number\":\"1000040000402\",\"sender_contact_no\":\"01990352988\",\"sender_reference\":\"test\",\"sender_name\":\"ACI\",\"sender_address\":\"DHAKA-HUB\",\"receiver_contact_no\":\"01990352988\",\"receiver_name\":\"popular\",\"receiver_address\":\"Barishal\",\"created_at\":\"2022-05-2916:35:23.000\",\"print_status\":\"0\",\"total_lot_qty\":\"4\",\"condition_amount\":\".00\",\"condition_mms_charge\":\".00\",\"is_home_delivery\":\"1\",\"net_amount\":\"250.00\",\"user_name\":\"NasrullahAlJadid\",\"sender_branch_name\":\"DHAKA-HUB\",\"receiver_branch_name\":\"Barishal\",\"department_name\":\"Courier\",\"service_name\":\"Document\",\"item_qty\":\"1\",\"items\":\"AMBBattery(Ctn)\"}]";
        string thermalPrinterData = "[{\"id\":\"402\",\"cn_number\":\"1000040000402\",\"branch_id\":\"12\",\"receiver_branch_id\":\"2\",\"sender_contact_no\":\"01990352988\",\"sender_reference\":\"test\",\"sender_name\":\"ACI\",\"receiver_contact_no\":\"01990352988\",\"receiver_name\":\"popular\",\"receiver_address\":\"Barishal\",\"created_at\":\"2022-05-2916:35:23.000\",\"print_status\":\"0\",\"total_lot_qty\":\"4\",\"item_qty\":\"1\",\"lot_qty\":\"4\",\"lot_number\":\"1000040000402000\",\"is_home_delivery\":\"1\",\"user_name\":\"NasrullahAlJadid\",\"sender_branch_name\":\"DHAKA-HUB\",\"receiver_branch_name\":\"Barishal\",\"receiver_branch_name_bd\":\"বরিশাল\",\"department_name\":\"Courier\",\"service_name\":\"Document\",\"item_name\":\"AMBBattery\",\"unit_name\":\"Ctn\"},{\"id\":\"402\",\"cn_number\":\"1000040000402\",\"branch_id\":\"12\",\"receiver_branch_id\":\"2\",\"sender_contact_no\":\"01990352988\",\"sender_reference\":\"test\",\"sender_name\":\"ACI\",\"receiver_contact_no\":\"01990352988\",\"receiver_name\":\"popular\",\"receiver_address\":\"Barishal\",\"created_at\":\"2022-05-2916:35:23.000\",\"print_status\":\"0\",\"total_lot_qty\":\"4\",\"item_qty\":\"1\",\"lot_qty\":\"4\",\"lot_number\":\"1000040000402001\",\"is_home_delivery\":\"1\",\"user_name\":\"NasrullahAlJadid\",\"sender_branch_name\":\"DHAKA-HUB\",\"receiver_branch_name\":\"Barishal\",\"receiver_branch_name_bd\":\"বরিশাল\",\"department_name\":\"Courier\",\"service_name\":\"Document\",\"item_name\":\"AMBBattery\",\"unit_name\":\"Ctn\"},{\"id\":\"402\",\"cn_number\":\"1000040000402\",\"branch_id\":\"12\",\"receiver_branch_id\":\"2\",\"sender_contact_no\":\"01990352988\",\"sender_reference\":\"test\",\"sender_name\":\"ACI\",\"receiver_contact_no\":\"01990352988\",\"receiver_name\":\"popular\",\"receiver_address\":\"Barishal\",\"created_at\":\"2022-05-2916:35:23.000\",\"print_status\":\"0\",\"total_lot_qty\":\"4\",\"item_qty\":\"1\",\"lot_qty\":\"4\",\"lot_number\":\"1000040000402002\",\"is_home_delivery\":\"1\",\"user_name\":\"NasrullahAlJadid\",\"sender_branch_name\":\"DHAKA-HUB\",\"receiver_branch_name\":\"Barishal\",\"receiver_branch_name_bd\":\"বরিশাল\",\"department_name\":\"Courier\",\"service_name\":\"Document\",\"item_name\":\"AMBBattery\",\"unit_name\":\"Ctn\"},{\"id\":\"402\",\"cn_number\":\"1000040000402\",\"branch_id\":\"12\",\"receiver_branch_id\":\"2\",\"sender_contact_no\":\"01990352988\",\"sender_reference\":\"test\",\"sender_name\":\"ACI\",\"receiver_contact_no\":\"01990352988\",\"receiver_name\":\"popular\",\"receiver_address\":\"Barishal\",\"created_at\":\"2022-05-2916:35:23.000\",\"print_status\":\"0\",\"total_lot_qty\":\"4\",\"item_qty\":\"1\",\"lot_qty\":\"4\",\"lot_number\":\"1000040000402003\",\"is_home_delivery\":\"1\",\"user_name\":\"NasrullahAlJadid\",\"sender_branch_name\":\"DHAKA-HUB\",\"receiver_branch_name\":\"Barishal\",\"receiver_branch_name_bd\":\"বরিশাল\",\"department_name\":\"Courier\",\"service_name\":\"Document\",\"item_name\":\"AMBBattery\",\"unit_name\":\"Ctn\"}]";
        public static List<A4Printer> a4Printers;
        public static List<TharmalPrinter> thermalPrinters;
        Code128ASetting code128ASetting = new Code128ASetting();
        static string directoryPath;
        
        System.Drawing.Image usbimg;

        int a4PrinterIndex = 0;
        int thermalIndex = 0;

        int imageWidth = 200;  // barcode image width
        int imageHeight = 60; //barcode image height
        Color foreColor = Color.Black; // Color to print barcode
        Color backColor = Color.White; //background color



        Thread thread;
        public Service1()
        {
          //ss  WriteToFile(projectDirectory, " printer");
            InitializeComponent();
           

        } 

     
        protected override void OnStart(string[] args)
        {
           
            if (!Directory.Exists(folderDirectory))
            {
                Directory.CreateDirectory(folderDirectory);
            }
            //  string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            directoryPath = Path.GetDirectoryName(location);

            WriteToFile("project Directory::" + directoryPath, " printer");

            usbimg = System.Drawing.Image.FromFile(directoryPath + "\\Resource" + "\\usb-logo.png");
            WriteToFile(folderDirectory, " printer");
            WriteToFile(""+usbimg.Height, " printer");

            setPrinterName();
            WriteToFile(a4PrinterName, "printer1");
            WriteToFile(tharmalPrinterName, "printer2");
            thread = new Thread(StartListening);
            thread.Start();
            System.IO.File.Create(AppDomain.CurrentDomain.BaseDirectory + "onstart.text");
        }

        protected override void OnStop()
        {
           
        

        }

        protected void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e) // unhandled exception handler
        {
           
            Thread.Sleep(5000);
            OnStop(); 

            var serviceMgr = new ServiceController();
            serviceMgr.Start(); 

            WriteToFile(e.ExceptionObject.ToString(), "printing error");
        }


        private  void StartListening()
        {
            HttpListener listener = new HttpListener();

            SetPrefixes(listener);

            if (listener.Prefixes.Count > 0)
            {
                listener.Start();

                while (true)
                {
                    HttpListenerContext context = listener.GetContext();
                    HttpListenerRequest request = context.Request;

                    String url = request.RawUrl;
                    WriteToFile(url, "");
                    String[] queryStringArray = url.Split('/');

                    long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    WriteToFile("now time: " + milliseconds," time");
        
                    JObject o = JObject.Parse(GetPostedText(request));
                    String a4Text = o.GetValue("a4size_printer").ToString();
                    String thermal = o.GetValue("thermal_printer").ToString();

                    convertToJsonObject(a4Text, thermal);
                    WriteToFile("from server", " ok");      
                    milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    WriteToFile("now time: " + milliseconds, " time");

                    try
                    {
                        setPrinterName();
                        printData();
                    }
                    catch (Exception e)
                    {
                        WriteToFile(e.Message, "printing error");
                    }

                  


                 
                }
            }
        }

     public static void  writePdfFile(String fileName, String textValue) {

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            try
            {
                byte[] bytes = Convert.FromBase64String(textValue);
                System.IO.FileStream stream =
                new FileStream(fileName, FileMode.CreateNew);
                System.IO.BinaryWriter writer =
                    new BinaryWriter(stream);
                writer.Write(bytes, 0, bytes.Length);
                writer.Close();
            }
            catch (Exception e)
            {
                WriteToFile(e.Message, " errror");
            }
        }

        private static void SetPrefixes(HttpListener listener)
        {
            String[] prefixes = new String[] { url };
          
            
            WriteToFile(prefixes+" "+url, " errror");

            int i = 0;

            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
                i++;
            }
        }

        private static string GetPostedText(HttpListenerRequest request)
        {
            string text = "";

            using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                text = reader.ReadToEnd();
            }

            return text;
        }
        public  void a4Printer()
        {
            WriteToFile("a4 printer writng start", "error in printer 1");
            try
                        {
                a4PrintDocument.PrinterSettings.PrinterName = a4PrinterName;
                a4PrintDocument.DocumentName = "file.pdf";
                a4PrintDocument.PrinterSettings.PrintFileName = "file.pdf";
                a4PrintDocument.PrinterSettings.Duplex = Duplex.Simplex;
                a4PrintDocument.PrintController = new StandardPrintController();
                a4PrintDocument.Print();
                        }
                        catch (Exception e)
                        {
                            WriteToFile(e.Message, "error in printer 1");
                        }


        }

              
        public  void thermalPrinter()
        {

            WriteToFile("thermal printer", "error in printer 1");
            try
                        {
                            thermalPrintDocument.PrinterSettings.PrinterName = tharmalPrinterName;
                            thermalPrintDocument.DocumentName = "file.pdf";
                            thermalPrintDocument.PrinterSettings.PrintFileName = "file.pdf";
                            thermalPrintDocument.PrintController = new StandardPrintController();
                            thermalPrintDocument.Print();
                        }
                        catch (Exception e)
                        {
                            WriteToFile(e.Message, "error in printer 2");

                        }
        }
               

        public  void printData()
        {
             a4PrinterIndex = 0;
             thermalIndex = 0;

            try
            {
                ThreadStart threadStart = new ThreadStart(a4Printer);
                Thread thread1 = new Thread(threadStart);
                thread1.Start();
            }
            catch (Exception e)
            {
                WriteToFile("a4printer error", "error");

            }

            try
            {
                ThreadStart threadStart2 = new ThreadStart(thermalPrinter);
                Thread thread2 = new Thread(threadStart2);
                thread2.Start();

            }
            catch (Exception e)
            {
                WriteToFile("thermal error: ", "error");

            }



           



        }

        public static void setPrinterName() {

 
            String FileName = folderDirectory + "\\printerdata.txt";
            try
            {
                if (File.Exists(FileName))
                {
                    // Read a text file line by line.  
                    string[] lines = File.ReadAllLines(FileName);
                    if (lines.Length == 2)
                    {
                        a4PrinterName = lines[0];
                        tharmalPrinterName = lines[1];
                    }
                    else
                    {
                        WriteToFile("printer name was not setup", "error");
                    }



                }
                String urlFle = folderDirectory + "\\url.txt";
                if (File.Exists(urlFle))
                {
                    string[] urls = File.ReadAllLines(urlFle);
                    if (urls.Length == 1)
                    {
                        url = urls[0];
                    }
                    else
                    {
                        WriteToFile("url was not setup", "error");
                    }
                }
            }
            catch (Exception e) { 
            }

        }
        public static void WriteToFile(string text, String tag)
        {
            string path = directoryPath.ToString()+"\\ServiceLog.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.WriteLine(string.Format(text, tag + ": " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                    writer.Close();
                }
            }
            catch (Exception e) { 
            }
        }



        private void a4PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {

            try { 
            var dateTime = DateTime.Now;

            String convertedDate = dateTime.ToLongDateString();
              

                BarcodeLib.Barcode barcodLib = new BarcodeLib.Barcode();
                barcodLib.StandardizeLabel = true;
                barcodLib.IncludeLabel = true;

                Image barCodeimage = barcodLib.Encode(BarcodeLib.TYPE.CODE128, a4Printers[a4PrinterIndex].cn_number, foreColor, backColor, imageWidth, imageHeight);
                string formatdDate = a4Printers[a4PrinterIndex].created_at;
                try
                {
                    formatdDate = Convert.ToDateTime(a4Printers[a4PrinterIndex].created_at).ToString();
                } catch (Exception error) {
                    WriteToFile(error.Message, "error");
                }


                int topDisplacement = 0;
            int topAdjustMent = 60;
            for (int j = 0; j < 3; j++)
            {
                string fontName = "Arial";
                int fontSize = 9;
                FontStyle fontStyle = FontStyle.Bold;

                int topPanelleft1 = 200;
                int TopPaneltop1 = 36 + topDisplacement;
                int topPanellineGape = 18;

                 
                    // Uppur Information 

                    e.Graphics.DrawString(convertedDate, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(topPanelleft1 - 100, TopPaneltop1));
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].sender_reference, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(topPanelleft1 - 100, TopPaneltop1 + topPanellineGape));
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].sender_branch_name, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(topPanelleft1 - 40, TopPaneltop1 + topPanellineGape * 2));
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].receiver_branch_name, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(topPanelleft1, TopPaneltop1 + topPanellineGape * 3 + 1));

                int topPanellineGape2 = 26;
                topPanelleft1 = 420;
                int adjustTop1 = 2;
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].service_name, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(topPanelleft1-5, TopPaneltop1 - adjustTop1-1 ));
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].is_home_delivery == "1" ? "H/D" : "O/D", new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(topPanelleft1-5, TopPaneltop1 - adjustTop1 + topPanellineGape2+2));
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].condition_amount==".00"? "00": a4Printers[a4PrinterIndex].condition_amount, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(topPanelleft1-5, TopPaneltop1 - adjustTop1 + topPanellineGape2 * 2+2));


                // BarCode 

                e.Graphics.DrawImage(barCodeimage, new Rectangle(topPanelleft1 + 140, TopPaneltop1 + 77, 200, 60));

                int senderleft1 = 153;
                int sendertop1 = topDisplacement + 205 - topAdjustMent;
                int lineGape = 16;

                // sender information 

                e.Graphics.DrawString(a4Printers[a4PrinterIndex].sender_name, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(senderleft1-12, sendertop1-3));
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].sender_contact_no, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(senderleft1-10, sendertop1 + lineGape));
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].sender_address, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(senderleft1-12, sendertop1 + lineGape * 2));

                // reciver information 
                senderleft1 = 395;
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].receiver_name, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(senderleft1-9, sendertop1 - 8));
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].receiver_contact_no, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(senderleft1-7, sendertop1 + lineGape - 5));
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].receiver_address, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(senderleft1-9, sendertop1 + lineGape * 2 - 3));

                // Product Description 
                int productTop1 = topDisplacement + 300 - topAdjustMent;
                int productLeft = 150;
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].items, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(productLeft, productTop1));
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].item_qty, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(productLeft + 360, productTop1));
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].condition_mms_charge, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(productLeft + 430, productTop1));
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].net_amount, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(productLeft + 500, productTop1));

                // Booking Officer Information 

                int moneyTop1 = topDisplacement + 342 - topAdjustMent;
                e.Graphics.DrawString(MYPRINTER.AmountToText.amountToWord(a4Printers[a4PrinterIndex].net_amount), new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(productLeft + 70, moneyTop1-2));
                e.Graphics.DrawString(a4Printers[a4PrinterIndex].user_name, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(productLeft + 80, moneyTop1 + 22));
                e.Graphics.DrawString(DateTime.Now.ToString("MM/dd/yyyy h:mm tt") +"( "+a4Printers[a4PrinterIndex].user_name+")", new Font(fontName, fontSize-3, fontStyle), Brushes.Black, new Point(productLeft + 245, moneyTop1 + 22));

                    e.Graphics.DrawString(formatdDate, new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(productLeft + 460, moneyTop1 + 20));

                   
                topDisplacement += 370 - j * 6;
            }

            a4PrinterIndex = a4PrinterIndex + 1;

        }
            catch (Exception error) {
                WriteToFile(error.Message, "printing error");
            }

            if (a4PrinterIndex < a4Printers.Count)
            {
                e.HasMorePages = true;
            }

        }

        private void thermalPrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            WriteToFile("thermal document preparation", "print");
            try
            {
                var dateTime = DateTime.Now;

                string formatedDate = thermalPrinters[thermalIndex].created_at;
                try
                {

                    string[] dateArr = thermalPrinters[thermalIndex].created_at.Split(' ');
                    string[] oDate = dateArr[0].Split('-');
                    formatedDate = oDate[1] + "-" + oDate[2] + "-" + oDate[0];
                }
                catch (Exception error)
                {
                    WriteToFile("exception in parsing date " + error, "error");
                }

               // WriteToFile(formatedDate +" ", "error");

                String convertedDate = dateTime.ToLongDateString();



                BarcodeLib.Barcode barcodLib = new BarcodeLib.Barcode();
                barcodLib.StandardizeLabel = true;
                barcodLib.IncludeLabel = true;

                Image barCodeimage = barcodLib.Encode(BarcodeLib.TYPE.CODE128, thermalPrinters[thermalIndex].lot_number, foreColor, backColor, imageWidth, imageHeight);

             

                string fontName = "Roboto";
                int fontSize = 8;
                FontStyle fontStyle = FontStyle.Bold;
                int refsTop1 = 6;
                int refsLeft1 = 5;


                e.Graphics.DrawString("Ref: ", new Font(fontName, 10), Brushes.Black, new Point(refsLeft1, refsTop1));
                e.Graphics.DrawString(thermalPrinters[thermalIndex].sender_reference, new Font(fontName, fontSize+1, fontStyle), Brushes.Black, new Point(refsLeft1 + 28, refsTop1 + 2));

                e.Graphics.DrawString(thermalPrinters[thermalIndex].department_name, new Font(fontName, 10, fontStyle), Brushes.Black, new Point(refsLeft1 + 65, refsTop1 + 3));
                e.Graphics.DrawString("(" + thermalPrinters[thermalIndex].service_name + ")", new Font(fontName, fontSize, fontStyle), Brushes.Black, new Point(refsLeft1 + 125, refsTop1 + 3));

                // usbimg.RotateFlip(RotateFlipType.Rotate180FlipX);

                // Image 
                e.Graphics.DrawImage(usbimg, new Rectangle(refsLeft1 + 220, 4, 35, 30));

                //e.Graphics.DrawImage(usbimg, new Rectangle(refsLeft1 + 215, 200, 55, 40));


                // Dhaka Hub 
                int receiverHabNameTop = 30;
                e.Graphics.DrawString(thermalPrinters[thermalIndex].receiver_branch_name_bd, new Font(fontName, 23, FontStyle.Bold), Brushes.Black, new Point(refsLeft1 + 80, receiverHabNameTop));

                int senderToReceiverTop = 60;
                int fontSizeSenderToReceiver = 11;

                SizeF senderBranchNameSize = e.Graphics.MeasureString(thermalPrinters[thermalIndex].sender_branch_name, new Font(fontName, fontSizeSenderToReceiver, fontStyle, GraphicsUnit.Point));

                // Dhaka Hub TO Barishal Hub 

                e.Graphics.DrawString(thermalPrinters[thermalIndex].sender_branch_name, new Font(fontName, fontSizeSenderToReceiver, fontStyle), Brushes.Black, new Point(refsLeft1 + 20, senderToReceiverTop));
                e.Graphics.DrawString("to", new Font(fontName, fontSizeSenderToReceiver, fontStyle), Brushes.Black, new Point(refsLeft1 + 30 + (int)senderBranchNameSize.Width, senderToReceiverTop));
                e.Graphics.DrawString(thermalPrinters[thermalIndex].receiver_branch_name, new Font(fontName, fontSizeSenderToReceiver, fontStyle), Brushes.Black, new Point(refsLeft1 + 40 + (int)senderBranchNameSize.Width + 15, senderToReceiverTop));

                // BarCode 
                int barcodeHight = 85;
                // barCodeimage.RotateFlip(RotateFlipType.Rotate180FlipX);

                e.Graphics.DrawString(thermalPrinters[thermalIndex].cn_number + " / " + thermalPrinters[thermalIndex].total_lot_qty, new Font(fontName, fontSizeSenderToReceiver, FontStyle.Bold), Brushes.Black, new Point(refsLeft1 + 60, barcodeHight));


                e.Graphics.DrawImage(barCodeimage, new Rectangle(refsLeft1 + 50, barcodeHight + 20, 200, 60));


                int dayHeight = 180;
                e.Graphics.DrawString(convertedDate, new Font(fontName, 10, fontStyle), Brushes.Black, new Point(refsLeft1, dayHeight));
                e.Graphics.DrawString(thermalPrinters[thermalIndex].is_home_delivery=="1"? "H/D":"O/D", new Font(fontName, 12, fontStyle | FontStyle.Bold), Brushes.Black, new Point(refsLeft1 + 220, dayHeight));

                int itemDescriptionHeight = 210;
                int lineGapeItemDescription = 25;
                e.Graphics.DrawString("Item Description", new Font(fontName, fontSizeSenderToReceiver, FontStyle.Underline | FontStyle.Bold), Brushes.Black, new Point(refsLeft1, itemDescriptionHeight));

                e.Graphics.DrawString(thermalPrinters[thermalIndex].item_name + ":" + thermalPrinters[thermalIndex].item_qty, new Font(fontName, fontSizeSenderToReceiver, fontStyle), Brushes.Black, new Point(refsLeft1, itemDescriptionHeight + lineGapeItemDescription));
                e.Graphics.DrawString("Receiver: " + thermalPrinters[thermalIndex].receiver_name, new Font(fontName, fontSizeSenderToReceiver, fontStyle), Brushes.Black, new Point(refsLeft1, itemDescriptionHeight + lineGapeItemDescription * 2));
                e.Graphics.DrawString("Contact: " + thermalPrinters[thermalIndex].receiver_contact_no, new Font(fontName, fontSizeSenderToReceiver, fontStyle), Brushes.Black, new Point(refsLeft1, itemDescriptionHeight + lineGapeItemDescription * 3));

                // Reciver Address 
                int receverAdressHeight = 315;
                e.Graphics.DrawString("Receiver Address:", new Font(fontName, fontSizeSenderToReceiver, FontStyle.Underline | FontStyle.Bold), Brushes.Black, new Point(refsLeft1, receverAdressHeight));
                e.Graphics.DrawString(thermalPrinters[thermalIndex].receiver_address, new Font(fontName, fontSizeSenderToReceiver-2, fontStyle), Brushes.Black, new Point(refsLeft1, receverAdressHeight + 20));

                int dateAndUserHeight = 350;
                e.Graphics.DrawString(formatedDate, new Font(fontName, 8, fontStyle), Brushes.Black, new Point(refsLeft1 + 90, dateAndUserHeight));
                e.Graphics.DrawString("( " + thermalPrinters[thermalIndex].user_name + ")", new Font(fontName, 8, fontStyle), Brushes.Black, new Point(refsLeft1 + 75, dateAndUserHeight + 10));

                thermalIndex = thermalIndex + 1;



                if (thermalIndex < thermalPrinters.Count)
                {
                    e.HasMorePages = true;
                }
            }
            catch (Exception error) {
                WriteToFile(error.Message, "printing error");
            }

        }


        public class A4Printer
        {
            public string id { get; set; }
            public string cn_number { get; set; }
            public string sender_contact_no { get; set; }
            public string sender_reference { get; set; }
            public string sender_name { get; set; }
            public string sender_address { get; set; }
            public string receiver_contact_no { get; set; }
            public string receiver_name { get; set; }
            public string receiver_address { get; set; }
            public string created_at { get; set; }
            public string print_statu { get; set; }
            public string total_lot_qty { get; set; }
            public string condition_amount { get; set; }
            public string condition_mms_charge { get; set; }
            public string is_home_delivery { get; set; }
            public string net_amount { get; set; }
            public string user_name { get; set; }
            public string sender_branch_name { get; set; }
            public string receiver_branch_name { get; set; }
            public string department_name { get; set; }
            public string service_name { get; set; }
            public string item_qty { get; set; }
            public string items { get; set; }
        }

        public class TharmalPrinter
        {
            public string id { get; set; }
            public string cn_number { get; set; }
            public string branch_id { get; set; }
            public string receiver_branch_id { get; set; }
            public string sender_contact_no { get; set; }
            public string sender_reference { get; set; }
            public string sender_name { get; set; }
            public string sender_address { get; set; }
            public string receiver_contact_no { get; set; }
            public string receiver_name { get; set; }
            public string receiver_address { get; set; }
            public string created_at { get; set; }
            public string print_status { get; set; }
            public string total_lot_qty { get; set; }
            public string lot_number { get; set; }
            public string is_home_delivery { get; set; }
            public string user_name { get; set; }
            public string sender_branch_name { get; set; }
            public string receiver_branch_name { get; set; }
            public string receiver_branch_name_bd { get; set; }
            public string department_name { get; set; }
            public string service_name { get; set; }
            public string item_qty { get; set; }
            public string item_name { get; set; }
            public string unit_name { get; set; }
        }
        public void convertToJsonObject(string aa4PrinterData, string thermalPrinterData)
        {

            a4Printers = JsonConvert.DeserializeObject<List<A4Printer>>(aa4PrinterData);
            thermalPrinters = JsonConvert.DeserializeObject<List<TharmalPrinter>>(thermalPrinterData);

            WriteToFile(a4Printers.ToString(), " ok");
            WriteToFile(thermalPrinters.ToString(), " ok");
        }
        public static Image RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

    }
}
