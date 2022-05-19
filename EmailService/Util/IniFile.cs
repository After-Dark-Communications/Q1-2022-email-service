using System.Runtime.InteropServices;
using System.Text;
using IniParser;
using IniParser.Model;

namespace EmailService.Util
{
    public static class IniFile
    {
        private static FileIniDataParser _parser;
        private static string _Path;
        private static string EXE = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        //[DllImport("kernel32", CharSet = CharSet.Unicode)]
        //static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);
        //
        //[DllImport("kernel32", CharSet = CharSet.Unicode)]
        //static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        static IniFile()
        {
            _parser = new FileIniDataParser();
            _Path = new FileInfo(EXE + ".ini").FullName;
            if (!File.Exists(_Path))
            {
                File.Create(_Path);
            }
        }

        /// <summary>
        /// Return the value from the given key in section, if it exists.
        /// </summary>
        /// <param name="Key">key to look for</param>
        /// <param name="Section">section to look in</param>
        /// <returns>value associated with the given key in section. if it doesn't exist, returns an empty string</returns>
        public static string Read(string Key, string? Section = null)
        {
            IniData data = _parser.ReadFile(_Path);
            string RetVal;
            if (string.IsNullOrEmpty(Section))
            {
                RetVal = data.Global[Key];
                if (RetVal == null)
                {
                    //get the first key if there are none in the sectionless part
                    foreach (SectionData sections in data.Sections)
                    {
                        RetVal = data[sections.SectionName][Key];
                        if (RetVal != null)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                RetVal = data[Section][Key];
            }

            //StringBuilder RetVal = new StringBuilder(255);
            //GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, _path);
            return RetVal;
        }

        /// <summary>
        /// Writes value to key in section
        /// </summary>
        /// <param name="Key">key to write to</param>
        /// <param name="Value">value to write</param>
        /// <param name="Section">section to write in</param>
        public static void Write(string Key, string Value, string? Section = null)
        {
            IniData data = _parser.ReadFile(_Path);
            if (string.IsNullOrEmpty(Section))
            {
                data.Global[Key] = Value;
            }
            else
            {
                data[Section][Key] = Value;
            }
            _parser.WriteFile(_Path, data);
        }

        /*/// <summary>
        /// Return the value from the given key in section, if it exists.
        /// </summary>
        /// <param name="Key">key to look for</param>
        /// <param name="Section">section to look in</param>
        /// <returns>value associated with the given key in section. if it doesn't exist, returns an empty string</returns>
        public static string Read(string Key, string Section = null)
        {
            StringBuilder RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, _path);
            return RetVal.ToString();
        }

        /// <summary>
        /// Writes value to key in section
        /// </summary>
        /// <param name="Key">key to write to</param>
        /// <param name="Value">value to write</param>
        /// <param name="Section">section to write in</param>
        public static void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, _path);
        }*/

        /// <summary>
        /// deletes key in section
        /// </summary>
        /// <param name="Key">Key to delete</param>
        /// <param name="Section">section to delete the key in</param>
        public static void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        /// <summary>
        /// Deletes an entire section
        /// </summary>
        /// <param name="Section">Section to delete</param>
        public static void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        /// <summary>
        /// Returns if a key exists
        /// </summary>
        /// <param name="Key">key to check for</param>
        /// <param name="Section">section to check in</param>
        /// <returns></returns>
        public static bool KeyExists(string Key, string Section = null)
        {
            string Retval = Read(Key, Section);
            if (Retval != null)
            {
                return Retval.Length > 0;
            }
            return false;
        }

        /// <summary>
        /// Gets the value as a boolean
        /// </summary>
        /// <param name="Key">The key to look for</param>
        /// <param name="Section">The section to look in</param>
        /// <returns>true on "1","yes","true" and "on". returns false on everything else</returns>
        public static bool ReadBool(string Key, string Section = null)
        {
            string val = Read(Key, Section);
            switch (val.ToLower())
            {
                case "1":
                case "yes":
                case "true":
                case "on":
                    return true;
                default:
                    return false;
            }
        }
        /// <summary>
        /// Writes the boolean value as "on"/"off" on key in section
        /// </summary>
        /// <param name="Key">key to write to</param>
        /// <param name="value">boolean value</param>
        /// <param name="Section">section to write to</param>
        public static void WriteBool(string Key, bool value, string Section = null)
        {
            Write(Key, value ? "on" : "off", Section);
        }
    }
}
