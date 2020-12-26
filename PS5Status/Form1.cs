using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using PS5Status.JsonClasses;

namespace PS5Status
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private bool CheckShop(CityData data)
        {
            if (data.digital_info != null && data.digital_info.available)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    Console.Beep(1000, 1000);
                    MessageBox.Show($"Digital: Available in {data.name}");
                }

                listBox1.Items.Add($"{DateTime.Now.TimeOfDay:hh\\:mm\\:ss}! Digital: Available in {data.name}");

                return true;
            }

            if (data.normal_info != null && data.normal_info.available)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    Console.Beep(1000, 1000);
                    MessageBox.Show($"Disc: Available in {data.name}");
                }

                listBox1.Items.Add($"{DateTime.Now.TimeOfDay:hh\\:mm\\:ss}! Disc: Available in {data.name}");

                return false;
            }

            return false;
        }

        List<Button> leftButtons = new List<Button>();
        List<Button> rightButtons = new List<Button>();

        private void timerMain_Tick(object sender, EventArgs e)
        {
            var json = new WebClient().DownloadString("https://ps5status.ru/api/data");
            var rootobject = JsonConvert.DeserializeObject<Rootobject>(json);

            Shops shops = rootobject.data.shops;

            foreach (var leftButton in leftButtons)
            {
                this.Controls.Remove(leftButton);
            }

            foreach (var rightButton in rightButtons)
            {
                this.Controls.Remove(rightButton);
            }

            int leftCount = 0;
            int rightCount = 0;

            bool anyAvailable = false;
            foreach (var cityData in shops.All())
            {
                bool available = CheckShop(cityData);

                // SIZE: 128; 23
                // Location1: 8; 352 left, 152; 352 right
                // Location2: 8; 376 left

                if (cityData.normal_info != null)
                {
                    Button btn = new Button();
                    btn.Location = new Point(8, 352 + leftCount++ * 24);
                    btn.Size = new Size(128, 23);
                    btn.Text = cityData.name + " Disc";
                    btn.Click += (o, args) => { Process.Start(cityData.normal_link); };
                    if (cityData.normal_info.available)
                    {
                        btn.Font = new Font(DefaultFont, FontStyle.Bold);
                    }

                    this.Controls.Add(btn);
                    leftButtons.Add(btn);
                }
                else
                {
                    leftCount++;
                }

                if (cityData.digital_info != null)
                {
                    Button btn = new Button();
                    btn.Location = new Point(152, 352 + rightCount++ * 24);
                    btn.Size = new Size(128, 23);
                    btn.Text = cityData.name + " Dig";
                    btn.Click += (o, args) => { Process.Start(cityData.digital_link); };
                    if (cityData.digital_info.available)
                    {
                        btn.Font = new Font(DefaultFont, FontStyle.Bold);
                    }

                    this.Controls.Add(btn);
                    rightButtons.Add(btn);
                }
                else
                {
                    rightCount++;
                }

                anyAvailable |= available;
            }

            if (!anyAvailable)
            {
                this.Text = $"None available at {DateTime.Now.TimeOfDay:hh\\:mm\\:ss}";
            }
            else
            {
                listBox1.Items.Add($"Was available at {DateTime.Now.TimeOfDay:hh\\:mm\\:ss}");
            }
        }
    }
}