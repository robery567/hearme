using HearMe.Properties;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HearMe
{
    class Sound
    {
        private WaveOutEvent outputDevice;
        private WaveFileReader waveFileReader;
        private WaveIn waveSource = null;
        public WaveIn WaveSource { get => waveSource; }
        private WaveFileWriter waveFile = null;
        public WaveFileWriter WaveFile { get => waveFile; }

        public double GetSoundLength()
        {
            while (waveFileReader == null);
            return waveFileReader.TotalTime.TotalSeconds;
        }

        private void OnPlaybackStopped(string fileName, PictureBox avatar, Thread thread)
        {
            outputDevice.Dispose();
            outputDevice = null;
            waveFileReader.Dispose();
            waveFileReader = null;
            File.Delete(fileName);
            avatar.Image = (Image)Resources.ResourceManager.GetObject("avatar0");
            thread.Abort();
        }

        private void AvatarModifiy(PictureBox avatar)
        {
            double endOfSongDividedBy18 = GetSoundLength() / 18.0;
            for (int i = 0; i <= 17; i++)
            {
                avatar.Image = (Image)Resources.ResourceManager.GetObject("avatar" + i);
                Thread.Sleep(Convert.ToInt32(endOfSongDividedBy18 * 1000));
            }
        }

        public void PlaySound(Uri url, PictureBox avatar)
        {
            StopSound();

            var httpClient = new HttpClient();
            httpClient.GetByteArrayAsync(url).ContinueWith(data => {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string localFilename = DateTime.Now.ToString("MMddyyyyhmmssfff") + ".wav";
                string fileName = Path.Combine(documentsPath, localFilename);
                File.WriteAllBytes(fileName, data.Result);
                
                Thread t = new Thread(() => AvatarModifiy(avatar));

                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                    outputDevice.PlaybackStopped += (sender, e) => OnPlaybackStopped(fileName, avatar, t);
                }

                if (waveFile == null)
                {
                    waveFileReader = new WaveFileReader(fileName);
                    outputDevice.Init(waveFileReader);
                }

                outputDevice.Play();
                
                t.Start();
            });
        }

        public void StopSound() => outputDevice?.Stop();

        public void StartRecording()
        {
            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat(44100, 1);

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            waveFile = new WaveFileWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), DateTime.Now.ToString("MMddyyyyhmmssfff") + ".wav"), waveSource.WaveFormat);
            
            waveSource.StartRecording();
        } 

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }
        }

        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }
    }
}
