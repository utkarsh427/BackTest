using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BAckTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public int numofcandles = 2;
        public int SL = 18;
        public int tempLow = 0;
        public int temphigh = 0;
        public int TGT = 10;
        public string bet = "";
        public bool bola = false;

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string[] line = File.ReadAllLines(@"C:\Users\Utkarsh Saxena\source\repos\BackTest\crudeback\CrudeHalfHour.txt");
            for (int i = 0; i < line.Length; i++)
            {
                //date open high low close
                //                line[i] = line[i].Replace("\"", "");
                string[] columns = line[i].Split(',');
                string vals = "";
                for (int j = 0; j < columns.Length; j++)
                {
                    if (j == 0)
                    {
                        var dateStr = columns[0];
                        var dateTime = DateTime.Parse(dateStr);
                        vals = dateTime.ToString("yyyy/MM/dd HH:mm") + " |O| ";
                    }
                    else if (j == 5 || j == 6)
                    {
                    }
                    else if (j == 1)
                    {
                        vals = vals + columns[j] + " |H| ";
                    }
                    else if (j == 2)
                    {
                        vals = vals + columns[j] + " |L| ";
                    }
                    else if (j == 3)
                    {
                        vals = vals + columns[j] + " |C| ";
                    }
                    else if (j == 4)
                    {
                        vals = vals + columns[j] + " || ";
                    }
                }
                listBox1.Items.Add(vals);
                vals = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var reader = new StreamReader(@"C:\Users\Utkarsh Saxena\source\repos\BackTest\crudeback\c.csv"))
            {
                var line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    line = line.Replace("\"", "");
                    var words = line.ToString().Split(',');
                    listBox2.Items.Add(
                        words[0] + " || " +
                        words[1] + "  " +
                        words[2] + "  " +
                        words[3] + " |TV| " +
                        words[4] + " |BSL| " +
                        words[5] + " |SSL| " +
                        words[6] + "  " +
                        words[7] + "  " +
                        words[8]
                        );
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox4.Items.Clear();
            listBox3.Items.Clear();
        
            using (var reader = new StreamReader(@"C:\Users\Utkarsh Saxena\source\repos\BackTest\crudeback\c.csv"))
            using (var reader2 = new StreamReader(@"C:\Users\Utkarsh Saxena\source\repos\BackTest\crudeback\Resis.txt"))
            {
                string value;
                var line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line2 = reader2.ReadLine();
                    int open = (int)Double.Parse(line2.ToString().Split(',')[1]);
                    int high = (int)Double.Parse(line2.ToString().Split(',')[2]);
                    int low = (int)Double.Parse(line2.ToString().Split(',')[3]);
                    int close = (int)Double.Parse(line2.ToString().Split(',')[4]);

                    int pp = (open + high + low) / 3;
                    int s1 = pp - (int)((high - low) * 0.382);
                    int s2 = pp - (int)((high - low) * 0.618);
                    int s3 = pp - (int)((high - low) * 1);
                    int r1 = pp + (int)((high - low) * 0.382);
                    int r2 = pp + (int)((high - low) * 0.6118);
                    int r3 = pp + (int)((high - low) * 1);
                    string str = "";
                    line = reader.ReadLine();
                    line = line.Replace("\"", "");
                    var wordsfromvcv = line.ToString().Split(',');
                    if (r3 < double.Parse(wordsfromvcv[4]))
                    {
                        str = " AboveR3";
                    }
                    else if (r3 >= double.Parse(wordsfromvcv[4]) && double.Parse(wordsfromvcv[4]) > r2)
                    {
                        str = " R3&R2";
                    }
                    else if (r2 >= double.Parse(wordsfromvcv[4]) && double.Parse(wordsfromvcv[4]) > r1)
                    {
                        str = " R2&R1";
                    }
                    else if (r1 >= double.Parse(wordsfromvcv[4]) && double.Parse(wordsfromvcv[4]) > pp)
                    {
                        str = " R1&PP";
                    }
                    else if (pp >= double.Parse(wordsfromvcv[4]) && double.Parse(wordsfromvcv[4]) > s1)
                    {
                        str = " PP&S1";
                    }
                    else if (s1 >= double.Parse(wordsfromvcv[4]) && double.Parse(wordsfromvcv[4]) > s2)
                    {
                        str = " S1&S2";
                    }
                    else if (s2 >= double.Parse(wordsfromvcv[4]) && double.Parse(wordsfromvcv[4]) > s3)
                    {
                        str = " S2&S3";
                    }
                    else if (s3 >= double.Parse(wordsfromvcv[4]))
                    {
                        str = " belowS3";
                    }
                    string[] lines = File.ReadAllLines(@"C:\Users\Utkarsh Saxena\source\repos\BackTest\crudeback\CrudeHalfHour.txt");
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] wordfromtxt = lines[i].Split(',');
                        var dateStr = wordfromtxt[0];
                        var dateTime = DateTime.Parse(dateStr);
                        value = dateTime.ToString("yyyy/MM/dd HH:mm");
                        if (wordsfromvcv[0].Equals(value.Split(' ')[0]))
                        {
                            if (!(value.Split(' ')[1].Equals("09:00")) &&
                               !(value.Split(' ')[1].Equals("09:30")) &&
                               !(value.Split(' ')[1].Equals("10:00"))
                               //                               && value.Split(' ')[0].Equals("2020-02-14")

                               )
                            {
                                string trade = wordsfromvcv[1] + wordsfromvcv[2] + wordsfromvcv[3];
//                                if (checkif(trade))
                                    if (
                                            trade.Equals("000") ||
                                            trade.Equals("001") ||
                                            trade.Equals("010") ||
                                            trade.Equals("011") ||
                                            trade.Equals("101") ||
                                            trade.Equals("110") 
                                            )
                                    {
                                        //sell
                                        Console.WriteLine(" Value " + value);
                                    Console.WriteLine(" 2 " + double.Parse(wordfromtxt[2]));
                                    Console.WriteLine(" 3 " + double.Parse(wordfromtxt[3]));
                                    Console.WriteLine(" 4 " + double.Parse(wordsfromvcv[4]));
                                    Console.WriteLine(" 6 " + double.Parse(wordfromtxt[6]));
                                    Console.WriteLine(" TextBox " + double.Parse(textBox1.Text.ToString()));
                                    Console.WriteLine(" Profit " + (double.Parse(wordsfromvcv[4]) - double.Parse(textBox1.Text.ToString())));
                                    //                           Console.WriteLine(" ProfitBool " + (double.Parse(wordsfromvcv[4]) - double.Parse(textBox1.Text.ToString())));

                                    if (double.Parse(wordfromtxt[2]) >= double.Parse(wordsfromvcv[6]))
                                    {

                                        listBox4.Items.Add(str + wordsfromvcv[1] +
                                            wordsfromvcv[2] +
                                            wordsfromvcv[3] +
                                            " || " + "Sell LOSS C1"
                                            + value
                                            );
                                        listBox3.Items.Add("-" + Math.Abs(double.Parse(wordsfromvcv[6])
                                            - double.Parse(wordsfromvcv[4])));
                                        break;
                                    }
                                    else if ((double.Parse(wordfromtxt[3]) < (double.Parse(wordsfromvcv[4]) - double.Parse(textBox1.Text))))
                                    {
                                        listBox4.Items.Add(str + wordsfromvcv[1] +
                                            wordsfromvcv[2] +
                                            wordsfromvcv[3] +
                                            " || " + "Sell PROFIT C2"
                                            + value
                                            );
                                        listBox3.Items.Add(Math.Abs(double.Parse(textBox1.Text.ToString())));
                                        break;
                                    }
                                }
                                //                                if (trade.Equals("111"))
//                                if (checkelse(trade))
                                else
                                {
                                    //buy
                                    //first loss
                                    if (double.Parse(wordfromtxt[3]) <= double.Parse(wordsfromvcv[5]))
                                    {
                                        listBox4.Items.Add(str + wordsfromvcv[1] +
                                            wordsfromvcv[2] +
                                            wordsfromvcv[3] +
                                            " || " + "Buy LOSS C5"
                                            + value
                                            );
                                        listBox3.Items.Add("-" + Math.Abs(double.Parse(wordsfromvcv[5])
                                            - double.Parse(wordsfromvcv[4])));
                                        break;
                                    }
                                    else if (double.Parse(wordfromtxt[2]) >
                                        (double.Parse(wordsfromvcv[4]) + double.Parse(textBox1.Text)))
                                    {
                                        listBox4.Items.Add(str + wordsfromvcv[1] +
                                            wordsfromvcv[2] +
                                            wordsfromvcv[3] +
                                            " || " + "Buy PROFIT C6"
                                            + value
                                            );
                                        listBox3.Items.Add(Math.Abs(double.Parse(textBox1.Text.ToString())));
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                Console.WriteLine(listBox3.Items.Count);
                int lossearned = 0, lossdays = 0, profitdays = 0,profitearned=0;
                string [] Astring = new string[200];
                int count = 0;
                for(int i = 0; i < listBox4.Items.Count; i++)
                {
                    if (!listBox4.Items[i].ToString().Contains("PROFIT"))
                    {
                        Astring[count]= listBox4.Items[i].ToString();
                        count++;
                    }
                }
                listBox4.Items.Clear();
                for (int i = 0; i < count; i++)
                {
                    listBox4.Items.Add(Astring[i]);
                }
                for (int i = 0; i < listBox3.Items.Count; i++)
                {
                    //                    if (listBox3.Items[i].ToString().Length<4)
                    {
                        if (Int32.Parse(listBox3.Items[i].ToString()) < 0)
                        {
                            lossearned = lossearned + Int32.Parse(listBox3.Items[i].ToString());
                            lossdays++;
                        }
                        else if (Double.Parse(listBox3.Items[i].ToString()) > 0)
                        {
                            profitearned = profitearned + Int32.Parse(listBox3.Items[i].ToString());
                            profitdays++;
                        }
                    }
                    //               Console.WriteLine(listBox3.Items[i]);
                }
                label1.Text = "PEarned " + profitearned + "LEarned " + lossearned + " Loss Days " + lossdays + " Profit Days " + profitdays;
                //                Clipboard.SetText(string.Join(Environment.NewLine, listBox4.Items.OfType<string>()));

                /*                str = " AboveR3";
                                str = " R3&R2";
                                str = " R2&R1";
                                str = " R1&PP";
                                str = " PP&S1";
                                str = " S1&S2";
                                str = " S2&S3";
                                str = " belowS3";
                */


            }

        }

        bool invert = false;
        string str = "000";
        private bool checkelse(string trade)
        {
            if (!invert)
            {
                return false;
            }
            else
            {
                if (trade.Equals(str))
                    return true;
                return false;
            }

        }

        private bool checkif(string trade)
        {
            if (!invert)
            {
                if (trade.Equals(str))
                    return true;
                return false;
            }else
            {
                return false;
            }
        }
    }

}
