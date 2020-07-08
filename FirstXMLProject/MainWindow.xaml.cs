using System;
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
            file = @"C:\Users\2nd_PC\source\repos\FirstXMLProject\FirstXMLProject"; //Directory.GetCurrentDirectory();
            file += @"\HML\Employees.xml";

            return file;
        }

        public static string GetEmployeesSchemaFile()
        {
            string file;
            file = @"C:\Users\2nd_PC\source\repos\FirstXMLProject\FirstXMLProject"; //Directory.GetCurrentDirectory();
            file += @"\HML\Employees.xsd";

            return file;
        }

        private StringBuilder ReadEmployeeDetails(XmlReader rdr)
        {
            StringBuilder sb = new StringBuilder();
            
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
    }
}
