using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BAckTest.Models;
using OfficeOpenXml;


namespace BAckTest
{
    public partial class DataMaker : Form
    {
        List<DataClass> dataClasses = new List<DataClass>();
        public DataMaker()
        {
            InitializeComponent();
        }

        private void DataMaker_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(@"C:\Users\Utkarsh Saxena\Downloads\LevelIndicator.xlsx")))
            {
                var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
                var totalRows = myWorksheet.Dimension.End.Row;
//                var totalColumns = myWorksheet.Dimension.End.Column;
                var totalColumns = 8;
                var sb = new StringBuilder(); //this is your data
                for (int rowNum = 3; rowNum <= totalRows; rowNum++) //select starting row here
                {
                    var row = myWorksheet.Cells[rowNum, 1, rowNum, totalColumns].Select(c => c.Value == null ? string.Empty : c.Value.ToString());
                    sb.AppendLine(string.Join(";", row));
                }
                sb = sb.Replace(System.Environment.NewLine, "|");
                int count = 0;

                foreach(char c in sb.ToString())
                {
                    if (c.Equals('|'))
                    {
                        count++;
                    }
                }

                String final = sb.ToString().Substring(0, sb.Length - 1);
                String[] modelstrings = final.Split('|');
                //model create from here
                for (int i = 0; i < count; i++)
                {
                    DataClass dataclass = new DataClass();
                    //  Date; Symbol; Expiry Date; Open; High; Low; Close; Pclose
                    dataclass.date = modelstrings[i].Split(';')[0];
                    dataclass.Symbol = modelstrings[i].Split(';')[1];
                    dataclass.Expiry = modelstrings[i].Split(';')[2];
                    dataclass.Open = modelstrings[i].Split(';')[3];
                    dataclass.High = modelstrings[i].Split(';')[4];
                    dataclass.Low = modelstrings[i].Split(';')[5];
                    dataclass.Close = modelstrings[i].Split(';')[6];
                    dataclass.Pclose = modelstrings[i].Split(';')[7];
                    dataClasses.Add(dataclass);
                }
                for (int i=1;i<dataClasses.Count;i++)
                {
                    dataClasses[i].PVT = npvt(dataClasses[i - 1], dataClasses[i]);
                    dataClasses[i].PVT = fpvt(dataClasses[i - 1], dataClasses[i]);
                    dataClasses[i].PVT = ns1(dataClasses[i - 1], dataClasses[i]);
                    dataClasses[i].PVT = ns2(dataClasses[i - 1], dataClasses[i]);
                    dataClasses[i].PVT = ns3(dataClasses[i - 1], dataClasses[i]);
                    dataClasses[i].PVT = nr1(dataClasses[i - 1], dataClasses[i]);
                    dataClasses[i].PVT = nr2(dataClasses[i - 1], dataClasses[i]);
                    dataClasses[i].PVT = nr3(dataClasses[i - 1], dataClasses[i]);
                    dataClasses[i].PVT = fs1(dataClasses[i - 1], dataClasses[i]);
                    dataClasses[i].PVT = fr1(dataClasses[i - 1], dataClasses[i]);
                    dataClasses[i].PVT = fs2(dataClasses[i - 1], dataClasses[i]);
                    dataClasses[i].PVT = fr2(dataClasses[i - 1], dataClasses[i]);
                    dataClasses[i].PVT = fs3(dataClasses[i - 1], dataClasses[i]);
                    dataClasses[i].PVT = fr3(dataClasses[i - 1], dataClasses[i]);
                }
                Console.WriteLine(dataClasses[0].date);
            }

        }

        private string fr3(DataClass dataClass1, DataClass dataClass2)
        {
            throw new NotImplementedException();
        }

        private string fs3(DataClass dataClass1, DataClass dataClass2)
        {
            throw new NotImplementedException();
        }

        private string fr2(DataClass dataClass1, DataClass dataClass2)
        {
            throw new NotImplementedException();
        }

        private string fs2(DataClass dataClass1, DataClass dataClass2)
        {
            throw new NotImplementedException();
        }

        private string fr1(DataClass dataClass1, DataClass dataClass2)
        {
            throw new NotImplementedException();
        }

        private string fs1(DataClass dataClass1, DataClass dataClass2)
        {
            throw new NotImplementedException();
        }

        private string nr3(DataClass dataClass1, DataClass dataClass2)
        {
            throw new NotImplementedException();
        }

        private string nr2(DataClass dataClass1, DataClass dataClass2)
        {
            throw new NotImplementedException();
        }

        private string nr1(DataClass dataClass1, DataClass dataClass2)
        {
            throw new NotImplementedException();
        }

        private string ns3(DataClass dataClass1, DataClass dataClass2)
        {
            throw new NotImplementedException();
        }

        private string ns2(DataClass dataClass1, DataClass dataClass2)
        {
            throw new NotImplementedException();
        }

        private string ns1(DataClass dataClass1, DataClass dataClass2)
        {
            throw new NotImplementedException();
        }

        private string fpvt(DataClass dataClass1, DataClass dataClass2)
        {
            throw new NotImplementedException();
        }

        private string npvt(DataClass dataClass1, DataClass dataClass2)
        {
            throw new NotImplementedException();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
