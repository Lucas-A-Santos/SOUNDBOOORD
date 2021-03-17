using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NAudio.Wave;
using System.IO;
using System.Threading;
using System.Windows.Input;

namespace WindowsFormsApp1
{
    public partial class SOUNDBOOOOOOORD : Form
    {
        
        private WaveOutEvent outputDevice;
        private WaveOutEvent outputDevice2;
        private AudioFileReader audioFile;
        private AudioFileReader audioFile2;
        string folderpath = "";

        


        public SOUNDBOOOOOOORD()
        {
            var savefolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LuckEs");
            Directory.CreateDirectory(savefolder);
           

            if (File.Exists(savefolder + @"\folderpath.txt") == false)
            {
                var fbd = new FolderBrowserDialog();
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    folderpath = fbd.SelectedPath;
                }
                else
                {
                
                    folderpath = @"C:\SoundBoard";
                    Directory.CreateDirectory(folderpath);
                }
                //            Console.WriteLine("FOLDER PATH BELOW");
                //            Console.WriteLine(folderpath);


                string[] lines = { folderpath };
                using (StreamWriter outputFile = new StreamWriter(Path.Combine(savefolder, "folderpath.txt")))
                {
                    foreach (string line in lines)
                        outputFile.WriteLine(line);
                }
                Console.WriteLine("Heres the folderpath before the fact:" + folderpath);
            }
            else
            {
                folderpath = System.IO.File.ReadAllText(savefolder + @"\folderpath.txt");
                folderpath = folderpath.Replace(System.Environment.NewLine, "");
                folderpath = folderpath.Trim();
                Console.WriteLine("SHOW THE DIFFERENCE");
                Console.WriteLine(@"C:\Users\User\Desktop\NAudio".Length);
                Console.WriteLine(folderpath.Length);
            }


            Console.WriteLine(folderpath);
            string[] files = Directory.GetFiles(folderpath, "*.wav", SearchOption.AllDirectories);
            
            foreach (string file in files)//tells me the name of all files in the folder
            {
                Console.WriteLine(Path.GetFileName(file));
            }

            int count = -2;
            InitializeComponent();
            for (int n = -1; n < WaveOut.DeviceCount; n++)
            {
                var caps = WaveOut.GetCapabilities(n);
                var caps2 = WaveIn.GetCapabilities(n);
                if (n != -1) comboBox1.Items.Add($"{n}: {caps.ProductName}");
                if (n != -1) comboBox2.Items.Add($"{n}: {caps.ProductName}");
                count++;
            }
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = count;

            int fCount = Directory.GetFiles(folderpath, "*.wav", SearchOption.AllDirectories).Length;
            List<Button> buttons = new List<Button>();
            var side = 0;
            for (int i = 0; i < fCount; i++)
            {
                Button newButton = new Button();
                buttons.Add(newButton);
                Controls.Add(newButton);
                if (i % 10 == 0 && i != 0) side++;
                //newButton.Location = new Point(side*100, 100 +(25* (i-(side*10))));
                flowLayoutPanel2.Controls.Add(newButton);
                string[] words = ("" + files[i]).Split('\\');
                var strL = words.Length;
                newButton.Margin = new Padding(0,0,0,0);
                newButton.Text = words[strL-1];
                newButton.Width = 100;
                newButton.Name = ""+i;
                newButton.Click += (s, a) =>
                {
                    float track = trackBar1.Value;
                    float vol = track / 100;
                    var L= Int32.Parse(newButton.Name);
                    string[] filename= new string[0];
                    var filelen = 0;
                    if (audioFile != null) {
                        filename = ("" + audioFile.FileName).Split('\\');
                        filelen = filename.Length;
                    }
                    
                    
                    if (outputDevice != null && audioFile.FileName!=null && newButton.Text==filename[filelen-1]) //&& audioFile.FileName==newButton.Name)
                    {
                        
                        
                        

                        audioFile2.Position = 0;
                        audioFile.Position = 0;


                    }
                    
                    else
                    {
                        //if (outputDevice!=null)outputDevice.Dispose();
                        outputDevice = null;
                        //if (outputDevice2 != null) outputDevice2.Dispose();
                        outputDevice2 = null;
                        if (audioFile != null) audioFile.Flush();
                        audioFile = null; 
                        if (audioFile2 != null) audioFile2.Flush();
                        audioFile2 = null;
                        //Thread.Sleep(10);
                        if (outputDevice == null)
                        {

                            outputDevice = new WaveOutEvent() { DeviceNumber = comboBox1.SelectedIndex };
                            outputDevice.Volume = vol;
                           
                            outputDevice.PlaybackStopped += OnPlaybackStopped;
                        }
                        if (outputDevice2 == null)
                        {

                            outputDevice2 = new WaveOutEvent() { DeviceNumber = comboBox2.SelectedIndex };
                            

                            outputDevice2.Volume = vol;
                            outputDevice2.PlaybackStopped += OnPlaybackStopped;
                        }

                        if (audioFile == null)
                        {

                            audioFile = new AudioFileReader("" + files[L]);
                            if (outputDevice != null) outputDevice.Init(audioFile);

                        }
                        if (audioFile2 == null)
                        {

                            audioFile2 = new AudioFileReader("" + files[L]);
                            if(outputDevice2 != null) outputDevice2.Init(audioFile2);

                        }
                        if (outputDevice != null) outputDevice.Play();
                        if (outputDevice2 != null) outputDevice2.Play();
                    }
                };
            }


        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            
            Console.WriteLine("END");
            if (outputDevice != null)
            {
                //outputDevice.Dispose();
                outputDevice = null;
            }
            if (outputDevice2 != null)
            {
                //outputDevice2.Dispose();
                outputDevice2 = null;
            }
            if (audioFile != null)
            {
                //audioFile.Dispose();
                audioFile = null;
            }
            if (audioFile2 != null)
            {
                //audioFile2.Dispose();
                audioFile2 = null;
            }
            
        }

        private void buttonPlay_Click(object sender, EventArgs e)
        {
            Application.Restart();
            outputDevice?.Stop();
            outputDevice2?.Stop();
            outputDevice?.Dispose();
            outputDevice2?.Dispose();
            audioFile?.Dispose();
            audioFile2?.Dispose();
            Environment.Exit(0);

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            outputDevice?.Stop();
            outputDevice2?.Stop();
            outputDevice?.Dispose();
            outputDevice2?.Dispose();
            audioFile?.Dispose();
            audioFile2?.Dispose();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl1.Text = ""+comboBox1.SelectedIndex;
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl2.Text = "" + comboBox2.SelectedIndex;
        }

        private void button2_Click(object sender, EventArgs e)//record
        {
            //var outputFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "NAudio");
            //Directory.CreateDirectory(outputFolder);

            

            var waveIn = new WaveInEvent() { DeviceNumber = 0 };
            WaveFileWriter writer = null;
            bool closing = false;
            var f = new Form();
            var buttonRecord = new Button() { Text = "Record" };
            var buttonStop = new Button() { Text = "Stop", Left = buttonRecord.Right, Enabled = false };
            var combobox = new ComboBox()
            {
                Text="Empty"
            };
            var lbl1 = new Label() { Text = "TEXT" };
            f.Width = 300;
            f.Height = 200;
            lbl1.Parent = f;
            lbl1.Location = new Point(160, 20);
            combobox.Parent = f;
            combobox.Location = new Point(160,0);
            buttonRecord.Parent = f;
            buttonRecord.Location = new Point(0, 0);
            buttonStop.Parent = f;
            var outputFilePath = Path.Combine(folderpath, "recorded.wav");
            int fCount = Directory.GetFiles(folderpath, "*.wav", SearchOption.AllDirectories).Length;

            for (int n = -1; n < WaveIn.DeviceCount; n++)
            {
                var caps = WaveIn.GetCapabilities(n);
                Console.WriteLine($"{n}: {caps.ProductName}");
                if (n != -1) combobox.Items.Add($"{n}: {caps.ProductName}");
            }
            combobox.SelectedIndex = 0;
            lbl1.Text = "" + combobox.SelectedIndex +" "+fCount;


            buttonRecord.Click += (s, a) =>
            {
                fCount = Directory.GetFiles(folderpath, "*.wav", SearchOption.AllDirectories).Length;
                outputFilePath = Path.Combine(folderpath, "recorded" +(fCount)+".wav");
                writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
                waveIn.StartRecording();
                buttonRecord.Enabled = false;
                buttonStop.Enabled = true;
            };

            buttonStop.Click += (s, a) => waveIn.StopRecording();

            waveIn.DataAvailable += (s, a) =>
            {
                writer.Write(a.Buffer, 0, a.BytesRecorded);
                if (writer.Position > waveIn.WaveFormat.AverageBytesPerSecond * 30)
                {
                    waveIn.StopRecording();
                }
            };

            waveIn.RecordingStopped += (s, a) =>
            {
                writer?.Dispose();
                writer = null;
                buttonRecord.Enabled = true;
                buttonStop.Enabled = false;
                if (closing)
                {
                    waveIn.Dispose();
                }
            };
            combobox.SelectedIndexChanged += (s, a) =>
            {
                waveIn.DeviceNumber = combobox.SelectedIndex;
                lbl1.Text = "" +combobox.SelectedIndex;
            };

            f.FormClosing += (s, a) => { closing = true; waveIn.StopRecording(); };
            f.ShowDialog();
        }

        
        bool isrunning = true;
        private void Form1_Load(object sender, EventArgs e)
        {
            Thread TH = new Thread(Keyboardd);
            TH.SetApartmentState(ApartmentState.STA);
            CheckForIllegalCrossThreadCalls = false;
            TH.Start();
        }
        void Keyboardd()
        {
            while (isrunning)
            {
                Thread.Sleep(40);
                if((Keyboard.GetKeyStates(Key.LeftCtrl)& KeyStates.Down)>0)
                {
                    //Lalt pressed
                    int fCount = Directory.GetFiles(folderpath, "*.wav", SearchOption.AllDirectories).Length;
                    string[] files = Directory.GetFiles(folderpath, "*.wav", SearchOption.AllDirectories);
                    
                    if (fCount > 1 &&(Keyboard.GetKeyStates(Key.D1) & KeyStates.Down) > 0)
                    {


                        var L = 0;
                        string[] filename = new string[0];
                        var filelen = 0;
                        float track = trackBar1.Value;
                        float vol = track / 100;
                        if (audioFile != null)
                        {
                            filename = ("" + audioFile.FileName).Split('\\');

                            filelen = filename.Length;
                            Console.WriteLine("Compared to=" + filename[filelen - 1]);
                        }


                        if (outputDevice != null && audioFile.FileName != null && audioFile.FileName == files[L]) //&& audioFile.FileName==newButton.Name)
                        {

                            
                            

                            audioFile2.Position = 0;
                            audioFile.Position = 0;


                        }

                        else
                        {
                            //if (outputDevice!=null)outputDevice.Dispose();
                            outputDevice = null;
                            //if (outputDevice2 != null) outputDevice2.Dispose();
                            outputDevice2 = null;
                            if (audioFile != null) audioFile.Flush();
                            audioFile = null;
                            if (audioFile2 != null) audioFile2.Flush();
                            audioFile2 = null;
                            //Thread.Sleep(10);
                            if (outputDevice == null)
                            {

                                outputDevice = new WaveOutEvent() { DeviceNumber = comboBox1.SelectedIndex };
                                outputDevice.Volume = vol;
                                outputDevice.PlaybackStopped += OnPlaybackStopped;
                            }
                            if (outputDevice2 == null)
                            {

                                outputDevice2 = new WaveOutEvent() { DeviceNumber = comboBox2.SelectedIndex };
                                outputDevice2.Volume = vol;
                                outputDevice2.PlaybackStopped += OnPlaybackStopped;
                            }

                            if (audioFile == null)
                            {

                                audioFile = new AudioFileReader("" + files[L]);
                                if (outputDevice != null) outputDevice.Init(audioFile);

                            }
                            if (audioFile2 == null)
                            {

                                audioFile2 = new AudioFileReader("" + files[L]);
                                if (outputDevice2 != null) outputDevice2.Init(audioFile2);

                            }
                            if (outputDevice != null) outputDevice.Play();
                            if (outputDevice2 != null) outputDevice2.Play();
                        }

                    }
                    if (fCount > 2 && (Keyboard.GetKeyStates(Key.D2) & KeyStates.Down) > 0)
                    {


                        var L = 1;
                        string[] filename = new string[0];
                        var filelen = 0;
                        float track = trackBar1.Value;
                        float vol = track / 100;
                        if (audioFile != null)
                        {
                            filename = ("" + audioFile.FileName).Split('\\');

                            filelen = filename.Length;
                            Console.WriteLine("Compared to=" + filename[filelen - 1]);
                        }


                        if (outputDevice != null && audioFile.FileName != null && audioFile.FileName == files[L]) //&& audioFile.FileName==newButton.Name)
                        {

                           
                            

                            audioFile2.Position = 0;
                            audioFile.Position = 0;


                        }

                        else
                        {
                            //if (outputDevice!=null)outputDevice.Dispose();
                            outputDevice = null;
                            //if (outputDevice2 != null) outputDevice2.Dispose();
                            outputDevice2 = null;
                            if (audioFile != null) audioFile.Flush();
                            audioFile = null;
                            if (audioFile2 != null) audioFile2.Flush();
                            audioFile2 = null;
                            //Thread.Sleep(10);
                            if (outputDevice == null)
                            {

                                outputDevice = new WaveOutEvent() { DeviceNumber = comboBox1.SelectedIndex };
                                outputDevice.Volume = vol;
                                outputDevice.PlaybackStopped += OnPlaybackStopped;
                            }
                            if (outputDevice2 == null)
                            {

                                outputDevice2 = new WaveOutEvent() { DeviceNumber = comboBox2.SelectedIndex };
                                outputDevice2.Volume = vol;
                                outputDevice2.PlaybackStopped += OnPlaybackStopped;
                            }

                            if (audioFile == null)
                            {

                                audioFile = new AudioFileReader("" + files[L]);
                                if (outputDevice != null) outputDevice.Init(audioFile);

                            }
                            if (audioFile2 == null)
                            {

                                audioFile2 = new AudioFileReader("" + files[L]);
                                if (outputDevice2 != null) outputDevice2.Init(audioFile2);

                            }
                            if (outputDevice != null) outputDevice.Play();
                            if (outputDevice2 != null) outputDevice2.Play();
                        }

                    }
                    if (fCount > 3 && (Keyboard.GetKeyStates(Key.D3) & KeyStates.Down) > 0)
                    {


                        var L = 2;
                        string[] filename = new string[0];
                        var filelen = 0;
                        float track = trackBar1.Value;
                        float vol = track / 100;
                        if (audioFile != null)
                        {
                            filename = ("" + audioFile.FileName).Split('\\');

                            filelen = filename.Length;
                            Console.WriteLine("Compared to=" + filename[filelen - 1]);
                        }


                        if (outputDevice != null && audioFile.FileName != null && audioFile.FileName == files[L]) //&& audioFile.FileName==newButton.Name)
                        {

                            
                            

                            audioFile2.Position = 0;
                            audioFile.Position = 0;


                        }

                        else
                        {
                            //if (outputDevice!=null)outputDevice.Dispose();
                            outputDevice = null;
                            //if (outputDevice2 != null) outputDevice2.Dispose();
                            outputDevice2 = null;
                            if (audioFile != null) audioFile.Flush();
                            audioFile = null;
                            if (audioFile2 != null) audioFile2.Flush();
                            audioFile2 = null;
                            //Thread.Sleep(10);
                            if (outputDevice == null)
                            {

                                outputDevice = new WaveOutEvent() { DeviceNumber = comboBox1.SelectedIndex };
                                outputDevice.Volume = vol;
                                outputDevice.PlaybackStopped += OnPlaybackStopped;
                            }
                            if (outputDevice2 == null)
                            {

                                outputDevice2 = new WaveOutEvent() { DeviceNumber = comboBox2.SelectedIndex };
                                outputDevice2.Volume = vol;
                                outputDevice2.PlaybackStopped += OnPlaybackStopped;
                            }

                            if (audioFile == null)
                            {

                                audioFile = new AudioFileReader("" + files[L]);
                                if (outputDevice != null) outputDevice.Init(audioFile);

                            }
                            if (audioFile2 == null)
                            {

                                audioFile2 = new AudioFileReader("" + files[L]);
                                if (outputDevice2 != null) outputDevice2.Init(audioFile2);

                            }
                            if (outputDevice != null) outputDevice.Play();
                            if (outputDevice2 != null) outputDevice2.Play();
                        }

                    }
                    if (fCount > 4 && (Keyboard.GetKeyStates(Key.D4) & KeyStates.Down) > 0)
                    {


                        var L = 3;
                        string[] filename = new string[0];
                        var filelen = 0;
                        float track = trackBar1.Value;
                        float vol = track / 100;
                        if (audioFile != null)
                        {
                            filename = ("" + audioFile.FileName).Split('\\');

                            filelen = filename.Length;
                            Console.WriteLine("Compared to=" + filename[filelen - 1]);
                        }


                        if (outputDevice != null && audioFile.FileName != null && audioFile.FileName == files[L]) //&& audioFile.FileName==newButton.Name)
                        {

                            
                            

                            audioFile2.Position = 0;
                            audioFile.Position = 0;


                        }

                        else
                        {
                            //if (outputDevice!=null)outputDevice.Dispose();
                            outputDevice = null;
                            //if (outputDevice2 != null) outputDevice2.Dispose();
                            outputDevice2 = null;
                            if (audioFile != null) audioFile.Flush();
                            audioFile = null;
                            if (audioFile2 != null) audioFile2.Flush();
                            audioFile2 = null;
                            //Thread.Sleep(10);
                            if (outputDevice == null)
                            {

                                outputDevice = new WaveOutEvent() { DeviceNumber = comboBox1.SelectedIndex };
                                outputDevice.Volume = vol;
                                outputDevice.PlaybackStopped += OnPlaybackStopped;
                            }
                            if (outputDevice2 == null)
                            {

                                outputDevice2 = new WaveOutEvent() { DeviceNumber = comboBox2.SelectedIndex };
                                outputDevice2.Volume = vol;
                                outputDevice2.PlaybackStopped += OnPlaybackStopped;
                            }

                            if (audioFile == null)
                            {

                                audioFile = new AudioFileReader("" + files[L]);
                                if (outputDevice != null) outputDevice.Init(audioFile);

                            }
                            if (audioFile2 == null)
                            {

                                audioFile2 = new AudioFileReader("" + files[L]);
                                if (outputDevice2 != null) outputDevice2.Init(audioFile2);

                            }
                            if (outputDevice != null) outputDevice.Play();
                            if (outputDevice2 != null) outputDevice2.Play();
                        }

                    }
                    if (fCount > 5 && (Keyboard.GetKeyStates(Key.D5) & KeyStates.Down) > 0)
                    {


                        var L = 4;
                        string[] filename = new string[0];
                        var filelen = 0;
                        float track = trackBar1.Value;
                        float vol = track / 100;
                        if (audioFile != null)
                        {
                            filename = ("" + audioFile.FileName).Split('\\');

                            filelen = filename.Length;
                            Console.WriteLine("Compared to=" + filename[filelen - 1]);
                        }


                        if (outputDevice != null && audioFile.FileName != null && audioFile.FileName == files[L]) //&& audioFile.FileName==newButton.Name)
                        {

                           
                            

                            audioFile2.Position = 0;
                            audioFile.Position = 0;


                        }

                        else
                        {
                            //if (outputDevice!=null)outputDevice.Dispose();
                            outputDevice = null;
                            //if (outputDevice2 != null) outputDevice2.Dispose();
                            outputDevice2 = null;
                            if (audioFile != null) audioFile.Flush();
                            audioFile = null;
                            if (audioFile2 != null) audioFile2.Flush();
                            audioFile2 = null;
                            //Thread.Sleep(10);
                            if (outputDevice == null)
                            {

                                outputDevice = new WaveOutEvent() { DeviceNumber = comboBox1.SelectedIndex };
                                outputDevice.Volume = vol;
                                outputDevice.PlaybackStopped += OnPlaybackStopped;
                            }
                            if (outputDevice2 == null)
                            {

                                outputDevice2 = new WaveOutEvent() { DeviceNumber = comboBox2.SelectedIndex };
                                outputDevice2.Volume = vol;
                                outputDevice2.PlaybackStopped += OnPlaybackStopped;
                            }

                            if (audioFile == null)
                            {

                                audioFile = new AudioFileReader("" + files[L]);
                                if (outputDevice != null) outputDevice.Init(audioFile);

                            }
                            if (audioFile2 == null)
                            {

                                audioFile2 = new AudioFileReader("" + files[L]);
                                if (outputDevice2 != null) outputDevice2.Init(audioFile2);

                            }
                            if (outputDevice!=null)outputDevice.Play();
                            if(outputDevice2 != null)outputDevice2.Play();
                        }

                    }
                    if (fCount > 6 && (Keyboard.GetKeyStates(Key.D6) & KeyStates.Down) > 0)
                    {


                        var L = 5;
                        string[] filename = new string[0];
                        var filelen = 0;
                        float track = trackBar1.Value;
                        float vol = track / 100;
                        if (audioFile != null)
                        {
                            filename = ("" + audioFile.FileName).Split('\\');

                            filelen = filename.Length;
                            Console.WriteLine("Compared to=" + filename[filelen - 1]);
                        }


                        if (outputDevice != null && audioFile.FileName != null && audioFile.FileName == files[L]) //&& audioFile.FileName==newButton.Name)
                        {

                           
                            

                            audioFile2.Position = 0;
                            audioFile.Position = 0;


                        }

                        else
                        {
                            //if (outputDevice!=null)outputDevice.Dispose();
                            outputDevice = null;
                            //if (outputDevice2 != null) outputDevice2.Dispose();
                            outputDevice2 = null;
                            if (audioFile != null) audioFile.Flush();
                            audioFile = null;
                            if (audioFile2 != null) audioFile2.Flush();
                            audioFile2 = null;
                            //Thread.Sleep(10);
                            if (outputDevice == null)
                            {

                                outputDevice = new WaveOutEvent() { DeviceNumber = comboBox1.SelectedIndex };
                                outputDevice.Volume = vol;
                                outputDevice.PlaybackStopped += OnPlaybackStopped;
                            }
                            if (outputDevice2 == null)
                            {

                                outputDevice2 = new WaveOutEvent() { DeviceNumber = comboBox2.SelectedIndex };
                                outputDevice2.Volume = vol;
                                outputDevice2.PlaybackStopped += OnPlaybackStopped;
                            }

                            if (audioFile == null)
                            {

                                audioFile = new AudioFileReader("" + files[L]);
                                if (outputDevice != null) outputDevice.Init(audioFile);

                            }
                            if (audioFile2 == null)
                            {

                                audioFile2 = new AudioFileReader("" + files[L]);
                                if (outputDevice2 != null) outputDevice2.Init(audioFile2);

                            }
                            if (outputDevice != null) outputDevice.Play();
                            if (outputDevice2 != null) outputDevice2.Play();
                        }

                    }
                    if (fCount > 7 && (Keyboard.GetKeyStates(Key.D7) & KeyStates.Down) > 0)
                    {


                        var L = 6;
                        string[] filename = new string[0];
                        var filelen = 0;
                        float track = trackBar1.Value;
                        float vol = track / 100;
                        if (audioFile != null)
                        {
                            filename = ("" + audioFile.FileName).Split('\\');

                            filelen = filename.Length;
                            Console.WriteLine("Compared to=" + filename[filelen - 1]);
                        }


                        if (outputDevice != null && audioFile.FileName != null && audioFile.FileName == files[L]) //&& audioFile.FileName==newButton.Name)
                        {

                            
                           

                            audioFile2.Position = 0;
                            audioFile.Position = 0;


                        }

                        else
                        {
                            //if (outputDevice!=null)outputDevice.Dispose();
                            outputDevice = null;
                            //if (outputDevice2 != null) outputDevice2.Dispose();
                            outputDevice2 = null;
                            if (audioFile != null) audioFile.Flush();
                            audioFile = null;
                            if (audioFile2 != null) audioFile2.Flush();
                            audioFile2 = null;
                            //Thread.Sleep(10);
                            if (outputDevice == null)
                            {

                                outputDevice = new WaveOutEvent() { DeviceNumber = comboBox1.SelectedIndex };
                                outputDevice.Volume = vol;
                                outputDevice.PlaybackStopped += OnPlaybackStopped;
                            }
                            if (outputDevice2 == null)
                            {

                                outputDevice2 = new WaveOutEvent() { DeviceNumber = comboBox2.SelectedIndex };
                                outputDevice2.Volume = vol;
                                outputDevice2.PlaybackStopped += OnPlaybackStopped;
                            }

                            if (audioFile == null)
                            {

                                audioFile = new AudioFileReader("" + files[L]);
                                if (outputDevice != null) outputDevice.Init(audioFile);

                            }
                            if (audioFile2 == null)
                            {

                                audioFile2 = new AudioFileReader("" + files[L]);
                                if (outputDevice2 != null) outputDevice2.Init(audioFile2);

                            }
                            if (outputDevice != null) outputDevice.Play();
                            if (outputDevice2 != null) outputDevice2.Play();
                        }

                    }
                    if (fCount > 8 && (Keyboard.GetKeyStates(Key.D8) & KeyStates.Down) > 0)
                    {


                        var L = 7;
                        string[] filename = new string[0];
                        var filelen = 0;
                        float track = trackBar1.Value;
                        float vol = track / 100;
                        if (audioFile != null)
                        {
                            filename = ("" + audioFile.FileName).Split('\\');

                            filelen = filename.Length;
                            Console.WriteLine("Compared to=" + filename[filelen - 1]);
                        }


                        if (outputDevice != null && audioFile.FileName != null && audioFile.FileName == files[L]) //&& audioFile.FileName==newButton.Name)
                        {

                            
                            

                            audioFile2.Position = 0;
                            audioFile.Position = 0;


                        }

                        else
                        {
                            //if (outputDevice!=null)outputDevice.Dispose();
                            outputDevice = null;
                            //if (outputDevice2 != null) outputDevice2.Dispose();
                            outputDevice2 = null;
                            if (audioFile != null) audioFile.Flush();
                            audioFile = null;
                            if (audioFile2 != null) audioFile2.Flush();
                            audioFile2 = null;
                            //Thread.Sleep(10);
                            if (outputDevice == null)
                            {

                                outputDevice = new WaveOutEvent() { DeviceNumber = comboBox1.SelectedIndex };
                                outputDevice.Volume = vol;
                                outputDevice.PlaybackStopped += OnPlaybackStopped;
                            }
                            if (outputDevice2 == null)
                            {

                                outputDevice2 = new WaveOutEvent() { DeviceNumber = comboBox2.SelectedIndex };
                                outputDevice2.Volume = vol;
                                outputDevice2.PlaybackStopped += OnPlaybackStopped;
                            }

                            if (audioFile == null)
                            {

                                audioFile = new AudioFileReader("" + files[L]);
                                if (outputDevice != null) outputDevice.Init(audioFile);

                            }
                            if (audioFile2 == null)
                            {

                                audioFile2 = new AudioFileReader("" + files[L]);
                                if (outputDevice2 != null) outputDevice2.Init(audioFile2);

                            }
                            if (outputDevice != null) outputDevice.Play();
                            if (outputDevice2 != null) outputDevice2.Play();
                        }

                    }
                    if (fCount > 9 && (Keyboard.GetKeyStates(Key.D9) & KeyStates.Down) > 0)
                    {


                        var L = 8;
                        string[] filename = new string[0];
                        var filelen = 0;
                        float track = trackBar1.Value;
                        float vol = track / 100;
                        if (audioFile != null)
                        {
                            filename = ("" + audioFile.FileName).Split('\\');

                            filelen = filename.Length;
                            Console.WriteLine("Compared to=" + filename[filelen - 1]);
                        }


                        if (outputDevice != null && audioFile.FileName != null && audioFile.FileName == files[L]) //&& audioFile.FileName==newButton.Name)
                        {

                           
                            

                            audioFile2.Position = 0;
                            audioFile.Position = 0;


                        }

                        else
                        {
                            //if (outputDevice!=null)outputDevice.Dispose();
                            outputDevice = null;
                            //if (outputDevice2 != null) outputDevice2.Dispose();
                            outputDevice2 = null;
                            if (audioFile != null) audioFile.Flush();
                            audioFile = null;
                            if (audioFile2 != null) audioFile2.Flush();
                            audioFile2 = null;
                            //Thread.Sleep(10);
                            if (outputDevice == null)
                            {

                                outputDevice = new WaveOutEvent() { DeviceNumber = comboBox1.SelectedIndex };
                                outputDevice.Volume = vol;
                                outputDevice.PlaybackStopped += OnPlaybackStopped;
                            }
                            if (outputDevice2 == null)
                            {

                                outputDevice2 = new WaveOutEvent() { DeviceNumber = comboBox2.SelectedIndex };
                                outputDevice2.Volume = vol;
                                outputDevice2.PlaybackStopped += OnPlaybackStopped;
                            }

                            if (audioFile == null)
                            {

                                audioFile = new AudioFileReader("" + files[L]);
                                if (outputDevice != null) outputDevice.Init(audioFile);

                            }
                            if (audioFile2 == null)
                            {

                                audioFile2 = new AudioFileReader("" + files[L]);

                                if (outputDevice2 != null) outputDevice2.Init(audioFile2);

                            }
                            if (outputDevice != null) outputDevice.Play();
                            if (outputDevice2 != null) outputDevice2.Play();
                        }

                    }
                    if (fCount > 0 && (Keyboard.GetKeyStates(Key.D0) & KeyStates.Down) > 0)
                    {


                        var L = 9;
                        string[] filename = new string[0];
                        var filelen = 0;
                        float track = trackBar1.Value;
                        float vol = track / 100;
                        if (audioFile != null)
                        {
                            filename = ("" + audioFile.FileName).Split('\\');

                            filelen = filename.Length;
                            Console.WriteLine("Compared to=" + filename[filelen - 1]);
                        }


                        if (outputDevice != null && audioFile.FileName != null && audioFile.FileName == files[L]) //&& audioFile.FileName==newButton.Name)
                        {

                           
                            

                            audioFile2.Position = 0;
                            audioFile.Position = 0;


                        }

                        else
                        {
                            //if (outputDevice!=null)outputDevice.Dispose();
                            outputDevice = null;
                            //if (outputDevice2 != null) outputDevice2.Dispose();
                            outputDevice2 = null;
                            if (audioFile != null) audioFile.Flush();
                            audioFile = null;
                            if (audioFile2 != null) audioFile2.Flush();
                            audioFile2 = null;
                            //Thread.Sleep(10);
                            if (outputDevice == null)
                            {

                                outputDevice = new WaveOutEvent() { DeviceNumber = comboBox1.SelectedIndex };
                                outputDevice.Volume = vol;
                                outputDevice.PlaybackStopped += OnPlaybackStopped;
                            }
                            if (outputDevice2 == null)
                            {

                                outputDevice2 = new WaveOutEvent() { DeviceNumber = comboBox2.SelectedIndex };
                                outputDevice2.Volume = vol;
                                outputDevice2.PlaybackStopped += OnPlaybackStopped;
                            }

                            if (audioFile == null)
                            {

                                audioFile = new AudioFileReader("" + files[L]);
                                if (outputDevice != null) outputDevice.Init(audioFile);

                            }
                            if (audioFile2 == null)
                            {

                                audioFile2 = new AudioFileReader("" + files[L]);
                                if (outputDevice2 != null) outputDevice2.Init(audioFile2);

                            }
                            if (outputDevice != null) outputDevice.Play();
                            if (outputDevice2 != null) outputDevice2.Play();
                        }

                    }


                }
                else
                {
                    
                   
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            isrunning = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            var savefolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "LuckEs");

            var fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                folderpath = fbd.SelectedPath;
            }
            else
            {
                folderpath = @"C:\SoundBoard";
                Directory.CreateDirectory(folderpath);
            }
            //            Console.WriteLine("FOLDER PATH BELOW");
            //            Console.WriteLine(folderpath);


            string[] lines = { folderpath };
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(savefolder, "folderpath.txt")))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
            Console.WriteLine("Heres the folderpath before the fact:" + folderpath);
            Application.Restart();
            outputDevice?.Stop();
            outputDevice2?.Stop();
            outputDevice?.Dispose();
            outputDevice2?.Dispose();
            audioFile?.Dispose();
            audioFile2?.Dispose();
            Environment.Exit(0);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            float track = trackBar1.Value;
            float vol = track/100;
            if (outputDevice != null) outputDevice.Volume = vol;
            if (outputDevice2 != null) outputDevice2.Volume = vol;
            label1.Text = "Volume:"+vol*100+"%";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SOUNDBOOOOOOORD_SizeChanged(object sender, EventArgs e)
        {
            flowLayoutPanel2.Width = this.Width;
            flowLayoutPanel2.Height = this.Height - 96;
        }
    }
}
