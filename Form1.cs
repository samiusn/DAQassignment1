using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DAQassignment1
{
    public partial class Form1 : Form
    {
        Sensor[] sensors;
        Log log;
        TimeSpan tsSample;
        TimeSpan tsLog;

        public Form1()
        {
            InitializeComponent();
            sensors = new Sensor[]
            {
                new Sensor(01),
                new Sensor(02),
                new Sensor(03),
                new Sensor(04),
                new Sensor(05),
                new Sensor(06)
            };
            log = new Log(sensors);

            tsSample = new TimeSpan(0, 0, 0, 5, 400);
            tsLog = new TimeSpan(0, 0, 0, 19);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String data = "";

            if (DateTime.Now.Subtract(sensors[0].lastSampleTime) > tsSample)
            {
                data = DateTime.Now.ToString();
                foreach (var sensor in sensors)
                {
                    if(sensor.sensorID < 5)
                        data = data + " : " + sensor.getValue().ToString("n2");
                    else
                        data = data + " : " + sensor.getDigitalValue().ToString("n0");
                }
                textBox3.Text = data;
            }
            else
            {
                MessageBox.Show("Interval time not reached");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {   
            if (DateTime.Now.Subtract(log.lastLogTime) > tsLog)
            {
                log.toFile();
            }
            else
            {
                MessageBox.Show("Interval time not reached");
            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }

    /// <summary>
    /// Purpose: Manage Sensors value and embbed with random generate value
    ///          Lowpass Filter is also included in the sampling function.
    /// </summary>
    public class Sensor
    {
        public double dVal;
        public int sensorID;
        Random random;
        public DateTime lastSampleTime;

        //Constructor of class
        public Sensor(int id)
        {
            sensorID = id;
            random = new Random(id);
            dVal = 0.0F;
        }

        //Generate random value and do lowpass AM before return
        public double getValue()
        {
            lastSampleTime = DateTime.Now;
            for(int i=0; i<10; i++)
            {
                dVal += random.Next(0, 5) + random.NextDouble();
            }
            dVal /= 10;
            return dVal;
        }
        public double getDigitalValue()
        {
            lastSampleTime = DateTime.Now;
            dVal = random.Next(0, 2);
            return dVal;
        }
    }

    /// <summary>
    /// Purpose: Manage Logging datas and functions to write in a csv file format.
    /// </summary>
    public class Log
    {
        Sensor[] sensorsLog;
        String toLog;
        public DateTime lastLogTime;

        public Log (Sensor[] sensors)
        {
            sensorsLog = sensors;
        }

        public void toFile()
        {
            StreamWriter sw = new StreamWriter("daqData.txt",true);
            toLog = DateTime.Now.ToString();
            lastLogTime = DateTime.Now;

            foreach (var sensor in sensorsLog)
            {
                if (sensor.sensorID < 5)
                    toLog = toLog + "," + sensor.dVal.ToString("n2");
                else
                    toLog = toLog + "," + sensor.dVal.ToString("n0");
            }
            sw.WriteLine(toLog);
            sw.Close();
        }

    }
}
