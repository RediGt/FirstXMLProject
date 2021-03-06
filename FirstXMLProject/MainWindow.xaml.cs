﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.IO;

namespace FirstXMLProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bttSimplePros_Click(object sender, RoutedEventArgs e)
        {
            SimpleReader();
        }

        private void SimpleReader()
        {
            StringBuilder sb = new StringBuilder(512); 
            XmlReader rdr;

            //Open an XmlReader
            rdr = XmlReader.Create(GetEmployeesFile());
           
            //Skip over <xml...> element
            rdr.Read();
            //Skip over CRLF (Carriage Return / Line Feed)
            rdr.Read();
            //Move to '<Employees>' element
            rdr.Read();

            if (rdr.LocalName.Equals("Employees"))
            {
                //Skip over CRLF
                rdr.Read();
                //Move to '<Employee>' element 
                rdr.Read();
                if (rdr.LocalName.Equals("Employee") && rdr.NamespaceURI.Equals(""))
                {                   
                    sb.Append(ReadEmployeeDetails(rdr));
                }
                else
                {
                    sb.Append("Can't find <Employee> Element");
                    sb.Append(Environment.NewLine);
                }
                //Read to the end - skip the rest of document
                while(rdr.Read());
            }
            else
            {
                sb.Append("Can't find <Employees> Element");
                sb.Append(Environment.NewLine);
            }
            rdr.Close();
            rdr.Dispose();

            tBoxResult.Text = sb.ToString();
        }

        public static string GetEmployeesFile()
        {
            string file;
            //file = Directory.GetCurrentDirectory();
            file = @"C:\Users\2nd_PC\source\repos\FirstXMLProject\FirstXMLProject"; //Directory .GetCurrentDirectory();
            file += @"\HML\Employees.xml";

            return file;
        }

        public static string GetEmployeesSchemaFile()
        {
            string file;
            file = Directory.GetCurrentDirectory();
            file = @"C:\Users\2nd_PC\source\repos\FirstXMLProject\FirstXMLProject"; //Directory.GetCurrentDirectory();
            file += @"\HML\Employees.xsd";

            return file;
        }

        private StringBuilder ReadEmployeeDetails(XmlReader rdr)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("One-by-one simple read");
            sb.Append(Environment.NewLine);

            //Skip over CRLF (Carriage Return / Line Feed)
            rdr.Read();
            //Move to '<id>' Element               
            rdr.Read();

            if (rdr.LocalName.Equals("id") && rdr.NamespaceURI.Equals(""))
            {
                //Move to the value of <id>
                rdr.Read();
                sb.AppendFormat("id={0}", rdr.Value);
                sb.Append(Environment.NewLine);
                //Move to '</id>' Element
                rdr.Read();
                //Skip over CRLF (Carriage Return / Line Feed)
                rdr.Read();
            }
            else
            {
                sb.Append("Can't find <id> Element");
                sb.Append(Environment.NewLine);
            }

            //Move to '<FirstName>' Element               
            rdr.Read();
            if (rdr.LocalName.Equals("FirstName") && rdr.NamespaceURI.Equals(""))
            {
                //Move to the value of <FirstName>
                rdr.Read();
                sb.AppendFormat("FirstName={0}", rdr.Value);
                sb.Append(Environment.NewLine);
                //Move to '</FirstName>' Element
                rdr.Read();
                //Skip over CRLF (Carriage Return / Line Feed)
                rdr.Read();
            }
            else
            {
                sb.Append("Can't find <FirstName> Element");
                sb.Append(Environment.NewLine);
            }
            return sb;
        }

        private void bttMoveToCont_Click(object sender, RoutedEventArgs e)
        {
            MoveToContent();
        }

        private void MoveToContent()
        {
            StringBuilder sb = new StringBuilder(512);
            XmlReader rdr;

            sb.Append("MoveToContent method");
            sb.Append(Environment.NewLine);
            //Open XmlReader
            rdr = XmlReader.Create(GetEmployeesFile());
            //Move to <Employees> element using XmlReader method
            rdr.MoveToContent();
            
            if (rdr.LocalName.Equals("Employees"))
            {
                //Move to the next line
                rdr.Read();
                //Move to <Employee> element using XmlReader method
                rdr.MoveToContent();

                if (rdr.LocalName.Equals("Employee"))
                {
                    //Move to the next line
                    rdr.Read();
                    //Move to <id> element using XmlReader method
                    rdr.MoveToContent();
                    if (rdr.LocalName.Equals("id"))
                    {
                        //Move to the value of <id>
                        rdr.Read();
                        sb.AppendFormat("id={0}", rdr.Value);
                        sb.Append(Environment.NewLine);
                        rdr.Read();
                    }
                    else
                    {
                        sb.Append("Can't find <id> Element");
                        sb.Append(Environment.NewLine);
                    }

                    rdr.Read();
                    //Move to '<FirstName>' Element               
                    rdr.MoveToContent();
                    if (rdr.LocalName.Equals("FirstName"))
                    {
                        //Move to the value of <FirstName>
                        rdr.Read();
                        sb.AppendFormat("FirstName={0}", rdr.Value);
                        sb.Append(Environment.NewLine);                     
                    }
                    else
                    {
                        sb.Append("Can't find <FirstName> Element");
                        sb.Append(Environment.NewLine);
                    }
                    //Read to the end - skip the rest of document
                    while (rdr.Read()) ;
                }
            }
            else
            {
                sb.Append("Can't find <Employees> Element");
                sb.Append(Environment.NewLine);
            }
            rdr.Close();
            rdr.Dispose();

            tBoxResult.Text = sb.ToString();
        }

        private void bttReadStartElement_Click(object sender, RoutedEventArgs e)
        {
            ReadStartElement();
        }

        private void ReadStartElement()
        {
            StringBuilder sb = new StringBuilder(512);
            XmlReader rdr;

            sb.Append("ReadStartElement method");
            sb.Append(Environment.NewLine);
            //Open XmlReader
            rdr = XmlReader.Create(GetEmployeesFile());
            //ReadStartElement checkes the current node and reads the next one
            rdr.ReadStartElement("Employees");
            rdr.ReadStartElement("Employee");
            rdr.ReadStartElement("id");
            sb.AppendFormat("id={0}", rdr.Value);
            sb.Append(Environment.NewLine);

            //Read past the Text node
            rdr.Read();
            //Move past the </id> element
            rdr.ReadEndElement();
            //Read <FirstName> element
            rdr.ReadStartElement("FirstName");
            sb.AppendFormat("FirstName={0}", rdr.Value);
            sb.Append(Environment.NewLine);

            //Reads to the end, skips the rest of document
            while (rdr.Read());
            rdr.Close();
            rdr.Dispose();

            tBoxResult.Text = sb.ToString();
        }

        private void bttReadElementStr_Click(object sender, RoutedEventArgs e)
        {
            ReadElementString();
        }

        private void ReadElementString()
        {
            StringBuilder sb = new StringBuilder();
            XmlReader rdr;

            sb.Append("ReadElementString method");
            sb.Append(Environment.NewLine);
            //Open XmlReader
            rdr = XmlReader.Create(GetEmployeesFile());
            rdr.ReadStartElement("Employees");
            rdr.ReadStartElement("Employee");
            //Checkes the node equals to "id" and reads next text element
            sb.Append("id=" + rdr.ReadElementString("id"));
            sb.Append(Environment.NewLine);
            sb.Append("FirstName=" + rdr.ReadElementString("FirstName"));
            sb.Append(Environment.NewLine);

            //Reads to the end, skips the rest of document
            while (rdr.Read()) ;
            rdr.Close();
            rdr.Dispose();

            tBoxResult.Text = sb.ToString();
        }
    }
}
