using System;
using System.Collections.Generic;
using System.IO;

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
            MethodType = frame.GetMethod().DeclaringType.ToString();

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

            // build software info
            report.SoftwareData = new Dictionary<string, string>();

            report.SoftwareData.Add("OS", Environment.OSVersion.ToString() + " " + (Inkore.Coreworks.Windows.Helpers.SystemHelper.Is64BitOperatingSystem() ? "x64" : "x86"));
            report.SoftwareData.Add(".NET Version", Environment.Version.ToString());

            return report;
        }


        public static readonly uint MagicHeader = 0xDEADF00D;
        public static readonly uint VersionHeader = 1;

        public List<ExceptionData> Data { get; set; }

        public Dictionary<string, string> SoftwareData { get; set; }
        public Dictionary<string, string> OpenGLData { get; set; }

        public Dictionary<string, string> HardwareData { get; set; }

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

            return writer.ToString();
        }
    }
}