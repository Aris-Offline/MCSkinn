using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Net.NetworkInformation;

namespace MCSkinn.Scripts
{
    public struct StackFrame
    {
        public string Method { get; set; }
        public string MethodType { get; set; }
        public string FileName { get; set; }
        public int FileLine { get; set; }
        public int FileColumn { get; set; }

        public StackFrame(System.Diagnostics.StackFrame frame) :
            this()
        {
            Method = frame.GetMethod().ToString();
            MethodType = frame.GetMethod()?.DeclaringType?.ToString();

            FileName = frame.GetFileName();
            FileLine = frame.GetFileLineNumber();
            FileColumn = frame.GetFileColumnNumber();
        }
    }

    public struct ExceptionData
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public List<StackFrame> Frames { get; private set; }

        public ExceptionData(Exception exception) :
            this()
        {
            Type = exception.GetType().FullName;
            Message = exception.Message;
            Frames = new List<StackFrame>();

            var trace = new System.Diagnostics.StackTrace(exception, true);

            if (trace.FrameCount != 0)
                foreach (var frame in trace.GetFrames())
                    Frames.Add(new StackFrame(frame));
        }
    }

    public class ErrorReport
    {
        public static ErrorReport Construct(Exception topException)
        {
            ErrorReport report = new ErrorReport();

            report.Data = new List<ExceptionData>();

            report.UserData = new Dictionary<string, string>();

            for (var ex = topException; ex != null; ex = ex.InnerException)
                report.Data.Add(new ExceptionData(ex));

            // build GL info
            report.OpenGLData = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(Editor.GLVendor))
                report.OpenGLData.Add("OpenGL", Editor.GLVendor + " " + Editor.GLVersion + " " + Editor.GLRenderer);
            else
                report.OpenGLData.Add("OpenGL", "Not Loaded");

            report.HardwareData = HardwareInformation.GetCurrent();

            // build software info
            report.SoftwareData = new Dictionary<string, string>
            {
                { "Operating System", Environment.OSVersion.ToString() + " " + (iNKORE.Coreworks.Windows.Helpers.SystemHelper.Is64BitOperatingSystem() ? "x64" : "x86") },
                { "App Version", Program.Version.ToString() },
                { "dotNET Version", Environment.Version.ToString() },
            };


            return report;
        }


        public static readonly uint MagicHeader = 0xDEADF00D;
        public static readonly uint VersionHeader = 1;

        public List<ExceptionData> Data { get; set; }

        public Dictionary<string, string> SoftwareData { get; set; }
        public Dictionary<string, string> OpenGLData { get; set; }

        public HInformaiton HardwareData { get; set; }

        public Dictionary<string, string> UserData { get; set; }

        public override string ToString()
        {
            StringWriter writer = new StringWriter();

            writer.WriteLine("OpenGL");
            writer.WriteLine("--------------------------------");

            foreach (var e in OpenGLData)
                writer.WriteLine(e.Key + ": " + e.Value);

            writer.WriteLine();
            writer.WriteLine("Software");
            writer.WriteLine("--------------------------------");

            foreach (var e in SoftwareData)
                writer.WriteLine(e.Key + ": " + e.Value);

            writer.WriteLine();
            writer.WriteLine("Hardware");
            writer.WriteLine("--------------------------------");

            writer.WriteLine("ManufactureDate: " + HardwareData.ManufactureDate);
            writer.WriteLine("ManufacturerName: " + HardwareData.ManufacturerName);
            writer.WriteLine("HarddiskSize: " + HardwareData.HarddiskSize);
            writer.WriteLine("MainboardName: " + HardwareData.MainboardName);
            writer.WriteLine("MemorySize: " + HardwareData.MemorySize);
            writer.WriteLine("NetworkcardName: " + HardwareData.NetworkcardName);
            writer.WriteLine("SoundcardName: " + HardwareData.SoundcardName);
            writer.WriteLine("VideocardName: " + HardwareData.VideocardName);



            writer.WriteLine();
            writer.WriteLine("Exception");
            writer.WriteLine("--------------------------------");

            foreach (var e in Data)
            {
                writer.WriteLine(e.Type);
                writer.WriteLine(e.Message);

                if (e.Frames.Count == 0)
                    writer.WriteLine("No Stack Trace Available");
                else
                {
                    foreach (var f in e.Frames)
                    {
                        writer.WriteLine(" " + f.MethodType + " :: " + f.Method);

                        if (!string.IsNullOrEmpty(f.FileName))
                            writer.WriteLine("  - " + f.FileName + " " + f.FileLine + ":" + f.FileColumn);
                    }
                }
            }

            string s = writer.ToString();
            writer.Dispose();
            return s;
        }

        public class OSInformation
        {
            public string OSName { get; set; }
            public string OSType { get; set; }
            public string OSSerialNumber { get; set; }
            public string ComputerName { get; set; }
            public string LogonUserName { get; set; }
        }

        public class HInformaiton
        {
            public string MainboardName { get; set; }
            public string MemorySize { get; set; }
            public string HarddiskSize { get; set; }
            public string VideocardName { get; set; }
            public string SoundcardName { get; set; }
            public string NetworkcardName { get; set; }
            public string ManufacturerName { get; set; }
            public string ManufactureDate { get; set; }
        }

        public class SInformation
        {
            public string VRV { get; set; }
            public string QAX { get; set; }
            public string WPS { get; set; }
        }

        public class WInformation
        {
            public string IP { get; set; }
            public string Subnetmask { get; set; }
            public string Gateway { get; set; }
        }

        public static class HardwareInformation
        {
            public static HInformaiton GetCurrent()
            {
                var HInfor = new HInformaiton { MainboardName = "未知", MemorySize = "未知", HarddiskSize = "未知", VideocardName = "未知", SoundcardName = "未知", NetworkcardName = "未知", ManufacturerName = "未知", ManufactureDate = "未知" };

                //主板信息
                try
                {
                    ManagementObjectSearcher myMainboard = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
                    foreach (ManagementObject board in myMainboard.Get())
                    {
                        HInfor.MainboardName = board["Product"].ToString();
                        //break;
                    }
                }
                catch
                {
                    HInfor.MainboardName = "未知";
                }

                //内存信息
                try
                {
                    ManagementObjectSearcher myMemory = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
                    //定义内存总大小变量
                    double MemorySize = 0;
                    foreach (ManagementObject obj in myMemory.Get())
                    {
                        //将内存大小换算成G单位
                        MemorySize += Convert.ToDouble(obj["Capacity"]) / 1024 / 1024 / 1024;
                    }
                    HInfor.MemorySize = MemorySize.ToString() + "G";
                }
                catch
                {
                    HInfor.MemorySize = "未知";
                }

                //硬盘信息
                try
                {
                    //获取本机所有硬盘信息  
                    ManagementObjectSearcher myHarddisk = new ManagementObjectSearcher("select * from Win32_DiskDrive");
                    foreach (ManagementObject drive in myHarddisk.Get())
                    {
                        //获取硬盘容量
                        var capacity = (Convert.ToDouble(drive["Size"]) / 1024 / 1024 / 1024).ToString("0.00") + "G";
                        //获取硬盘类型
                        var mediaType = drive["MediaType"];
                        HInfor.HarddiskSize = capacity.ToString() + "|" + mediaType.ToString();
                    }
                }
                catch
                {
                    HInfor.HarddiskSize = "未知";
                }

                //显卡信息
                try
                {
                    ManagementObjectSearcher myVideoObject = new ManagementObjectSearcher("select * from Win32_VideoController");
                    foreach (ManagementObject obj in myVideoObject.Get())
                    {
                        HInfor.VideocardName = obj["Name"].ToString();
                    }
                }
                catch
                {
                    //e.Message
                    HInfor.VideocardName = "未知";
                }

                //声卡信息
                try
                {
                    // 创建WMI搜索对象
                    ManagementObjectSearcher mySoundcard = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_SoundDevice");
                    // 遍历搜索结果
                    foreach (ManagementObject mo in mySoundcard.Get())
                    {
                        // 获取声卡名称
                        HInfor.SoundcardName = mo.GetPropertyValue("Name").ToString();
                    }
                }
                catch
                {
                    HInfor.SoundcardName = "未知";
                }

                //网卡信息（Mac地址）
                try
                {

                    // 获取本地网络接口信息 
                    NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                    foreach (NetworkInterface adapter in nics)
                    {
                        // 如果是RJ45网卡 
                        if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                        {
                            string S1 = (string)adapter.Description;
                            if (S1.Contains("PCI"))
                            {
                                HInfor.NetworkcardName = S1;
                            }
                        }
                    }
                }
                catch
                {
                    HInfor.NetworkcardName = "未知";
                }

                //获取生产商和生产日期
                try
                {
                    //获取生产商
                    ManagementObjectSearcher ManufacturerInfo = new ManagementObjectSearcher("select * from Win32_ComputerSystem");
                    foreach (ManagementObject obj in ManufacturerInfo.Get())
                    {
                        HInfor.ManufacturerName = obj["Manufacturer"].ToString();
                    }
                    //获取生产日期
                    ConnectionOptions options = new ConnectionOptions();
                    ManagementScope scope = new ManagementScope("\\\\.\\root\\cimv2", options);
                    ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_BIOS");
                    ManagementObjectSearcher BoisInfo = new ManagementObjectSearcher(scope, query);
                    foreach (ManagementObject mo in BoisInfo.Get())
                    {
                        string StrManufactureDate = mo["ReleaseDate"].ToString().Substring(0, 8);
                        DateTime DT = DateTime.ParseExact(StrManufactureDate, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                        HInfor.ManufactureDate = String.Format("{0:d}", DT);
                    }
                }
                catch
                {

                }

                return HInfor;
            }
        }
    }
}