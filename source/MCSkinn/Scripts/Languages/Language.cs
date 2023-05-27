//
//    MCSkinn, a 3d skin management studio for Minecraft
//    Copyright (C) 2013 Altered Softworks & MCSkinn Team
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MCSkinn.Scripts.Languages
{
    public class Language
    {
        public Language()
        {
            StringTable = new Dictionary<string, string>();
        }

        public string Product { get; private set; }
        public string FileName { get; private set; }
        public string Name { get; private set; }
        public string LangVersion { get; private set; }
        public string AppVersion { get; private set; }
        public string Author { get; private set; }
        public Version SupportedVersion { get; private set; }
        public Dictionary<string, string> StringTable { get; private set; }
        public ToolStripMenuItem Item { get; set; }
        public CultureInfo Culture { get; set; }

        public static Language Parse(StreamReader sr, string fileName)
        {
            var lang = new Language();
            bool headerFound = false;
            lang.FileName = fileName;
            while (!sr.EndOfStream)
            {
                try
                {
                    string line = sr.ReadLine();

                    if (line.StartsWith("//") || string.IsNullOrEmpty(line))
                        continue;

                    //if (line == "MCSkinn Language File")
                    //{
                    //    headerFound = true;
                    //    continue;
                    //}

                    //if (!headerFound)
                    //    throw new Exception("No header");

                    //if (!line.Contains('='))
                    //    throw new Exception("Parse error");

                    string left = line.Substring(0, line.IndexOf('=')).Trim();
                    string right = line.Substring(line.IndexOf('=') + 1).Trim(' ', '\t', '\"', '\'').Replace("\\r", "\r").Replace(
                        "\\n", "\n");
                    lang.StringTable.Add(left, right);

                    if (left[0] == '#')
                    {
                        if (left == "#Product")
                            lang.Product = right;
                        if (left == "#Name")
                            lang.Name = right;
                        else if (left == "#LangVersion")
                            lang.LangVersion = right;
                        else if (left == "#AppVersion")
                        {
                            lang.SupportedVersion = new Version(right);
                            lang.AppVersion = right;
                        }
                        else if (left == "#Culture")
                            lang.Culture = CultureInfo.GetCultureInfo(right);
                        else if (left == "#Author")
                            lang.Author = right;
                    }

                }
                catch { }
            }

            return lang;
        }
    }
}