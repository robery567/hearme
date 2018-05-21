using HearMe.Properties;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
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
        private AudioFileReader audioFile;

        [DllImport("winmm.dll")]
        private static extern uint mciSendString(
            string command,
            StringBuilder returnValue,
            int returnLength,
            IntPtr winHandle);

        public double GetSoundLength()
        {
            return audioFile.TotalTime.TotalSeconds;
        }

        private void OnPlaybackStopped(string fileName, PictureBox avatar, Thread thread)
        {
            outputDevice.Dispose();
            outputDevice = null;
            audioFile.Dispose();
            audioFile = null;
            File.Delete(fileName);
            avatar.Image = (Image)Resources.ResourceManager.GetObject("avatar0");
            thread.Abort();
        }

        private void AvatarModifiy(PictureBox avatar)
        {
            double endOfSongDividedBy18 = GetSoundLength() / 18.0;
            MessageBox.Show(endOfSongDividedBy18 + " " + GetSoundLength());
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
                string localFilename = DateTime.Now.ToString("MMddyyyyhmm") + ".mp3";
                string fileName = Path.Combine(documentsPath, localFilename);
                File.WriteAllBytes(fileName, data.Result);
                
                Thread t = new Thread(() => AvatarModifiy(avatar));

                if (outputDevice == null)
                {
                    outputDevice = new WaveOutEvent();
                    outputDevice.PlaybackStopped += (sender, e) => OnPlaybackStopped(fileName, avatar, t);
                }

                if (audioFile == null)
                {
                    audioFile = new AudioFileReader(fileName);
                    outputDevice.Init(audioFile);
                }

                outputDevice.Play();
                
                t.Start();
            });
        }

        public void StopSound() => outputDevice?.Stop();
    }
}
