﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogHandler
{
    public class LogWriter
    {
        private DirectoryInfo logDirectory = Directory.CreateDirectory(Directory.GetCurrentDirectory().Replace("Debug", "Logs"));
        private int logNumber = 1;
        private string logEntry { get; set; }
        private string dateTime { get; set; }
        private string logPathOutput { get; set; }
        private StreamWriter streamWriter;

        // directory of the logPath

        public static void Main(string[] args)
        {
            string test = "TEST HALLO FDSFR#$@FDA";

            byte[] testerByte = { 0x22, 0x2, 0x4 };
            int intTest = 321432143;
            testerByte = Encoding.UTF8.GetBytes(test);

            //LogWriter writer = new LogWriter();
            //DirectoryInfo info =  writer.CreateNewFolderInsideProject("Test");
            //Console.WriteLine(info.FullName);
            //Console.ReadLine();

            //writer.WriteBytesToFile(writer.logPathOutput, testerByte);
            //writer.WriteBytesToFile(writer.logPathOutput, testerByte);
            //writer.WriteBytesToFile(writer.logPathOutput, testerByte);
            //writer.WriteBytesToFile(writer.logPathOutput, testerByte);
            //writer.WriteBytesToFile(writer.logPathOutput, testerByte);
            //writer.WriteBytesToFile(writer.logPathOutput, testerByte);
            //writer.WriteBytesToFile(writer.logPathOutput, testerByte);
            //writer.WriteBytesToFile(writer.logPathOutput, testerByte);
            //writer.WriteTextToFile(writer.logPathOutput, "End message");
     
            Console.ReadLine();
        }

        public LogWriter()
        {
            logPathOutput = Path.Combine(Directory.GetCurrentDirectory(), "ServerLogHandler.log");

            this.logEntry = this.logNumber.ToString() + ": ";
            this.dateTime = DateTime.Now.ToString() + ":   ";
        }

        public DirectoryInfo CreateNewFolderInsideProject(string path) // creates a new directory inside the project and returns the directory path
        {
            DirectoryInfo dirPath = Directory.CreateDirectory(Directory.GetCurrentDirectory().Replace("Debug", path));
            return dirPath;
        }

        public DirectoryInfo CreateNewFolder(string path)
        {
            DirectoryInfo dirPath = Directory.CreateDirectory(path);
            return dirPath;
        }

        public void WriteTextToFile(string filePath, string logString)
        {
            this.streamWriter = new StreamWriter(filePath, true);

            this.logEntry = this.logNumber.ToString() + ": ";
            this.dateTime = DateTime.Now.ToString() + ":   ";

            streamWriter.WriteLine(this.logEntry + this.dateTime + logString);
            streamWriter.Flush();
            streamWriter.Close();
            this.logNumber++;

            this.streamWriter = new StreamWriter(filePath, true);
        }

        public void WriteBytesToFile(string filePath, byte[] logBytes)
        {
            this.streamWriter = new StreamWriter(filePath, true);

            this.logEntry = this.logNumber.ToString() + ": ";
            this.dateTime = DateTime.Now.ToString() + ":  ";

            string data = Encoding.UTF8.GetString(logBytes);

            streamWriter.WriteLine(this.logEntry + this.dateTime + data);
            streamWriter.Flush();
            streamWriter.Close();
            this.logNumber++;
        }
    }
}
