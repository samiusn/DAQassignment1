using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DAQassignment1
{
    public partial class Form1 : Form
    {
        Sensor sensor = new Sensor(01);
        //bool isSampling = false;
        TimeSpan ts = new TimeSpan(0, 0, 0, 5, 400);

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Get sensor value
            //if (isSampling) isSampling = false;
            //else isSampling = true;
            if(DateTime.Now.Subtract(sensor.lastSampleTime) > ts)
            textBox3.Text = sensor.getValue().ToString("n2");
        }

    }

    /// <summary>
    /// Purpose: Manage Sensors value and embbed with random generate value
    /// </summary>
    public class Sensor
    {
        double dVal;
        int sensorID;
        Random random;
        public DateTime lastSampleTime;

        public Sensor(int id)
        {
            sensorID = id;
            random = new Random(id);
            dVal = 0.0F;
        }

        public double getValue()
        {
            lastSampleTime = DateTime.Now;
            dVal = random.Next(0,4) + random.NextDouble();
            return dVal;
        }
    }
}
