using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Mk0.GUI.Banner
{
    public class Banner
    {
        private readonly Settings settings;
        private readonly Queue queue;
        private readonly Timer timerEinblenden;
        private readonly Timer timerAnzeigen;
        private readonly Timer timerAusblenden;
        private readonly Panel bannerPanel;
        private bool messagesRunning = false;
        private PictureBox pb1;
        private PictureBox pb2;
        private Label l1;
        private Label l2;

        public Banner(IContainer container, Panel bannerPanel, PictureBox pb1, PictureBox pb2, Label l1, Label l2)
        {
            settings = new Settings();
            queue = new Queue();
            this.bannerPanel = bannerPanel;
            this.pb1 = pb1;
            this.pb2 = pb2;
            this.l1 = l1;
            this.l2 = l2;

            timerEinblenden = new Timer(container)
            {
                Interval = settings.TimerEinblendenIntervall
            };
            timerEinblenden.Tick += new EventHandler(TimerEinblenden_Tick);
            timerAnzeigen = new Timer(container)
            {
                Interval = settings.TimerAnzeigenIntervall
            };
            timerAnzeigen.Tick += new EventHandler(TimerAnzeigen_Tick);
            timerAusblenden = new Timer(container)
            {
                Interval = settings.TimerAusblendenIntervall
            };
            timerAusblenden.Tick += new EventHandler(TimerAusblenden_Tick);
        }

        private void TimerEinblenden_Tick(object sender, EventArgs e)
        {
            //einblenden
            if (bannerPanel.Location != new Point(0, 0))
            {
                bannerPanel.Location = new Point(bannerPanel.Location.X, bannerPanel.Location.Y + 1);
            }
            else
            {
                timerEinblenden.Stop();
                if (queue.bol.Count < 2)
                {
                    timerAnzeigen.Interval = 1500;
                }
                else if (queue.bol.Count < 5)
                {
                    timerAnzeigen.Interval = 1000;
                }
                else if (queue.bol.Count < 10)
                {
                    timerAnzeigen.Interval = 750;
                }
                else if (queue.bol.Count < 15)
                {
                    timerAnzeigen.Interval = 500;
                }
                else
                {
                    timerAnzeigen.Interval = 250;
                }
                timerAnzeigen.Start();
            }
        }

        private void TimerAnzeigen_Tick(object sender, EventArgs e)
        {
            //anzeigen
            timerAnzeigen.Stop();
            timerAusblenden.Start();
        }

        private void TimerAusblenden_Tick(object sender, EventArgs e)
        {
            //ausblenden
            if (bannerPanel.Location != new Point(0, -(bannerPanel.Height + 2)))
            {
                bannerPanel.Location = new Point(bannerPanel.Location.X, bannerPanel.Location.Y - 1);
            }
            else
            {
                timerAusblenden.Stop();
                if (queue.bol.Count > 0)
                {
                    ShowMessages(true);
                }
                else
                {
                    messagesRunning = false;
                }
            }
        }

        public void Enqueue(string label1Text, string label2Text, string pb1ImageLocation, Image pb2Image)
        {
            BannerObject bo = new BannerObject(label1Text, label2Text, pb1ImageLocation, pb2Image);
            queue.bol.Enqueue(bo);
        }

        public void Enqueue(string label1Text, string label2Text, Image pb1Image, Image pb2Image)
        {
            BannerObject bo = new BannerObject(label1Text, label2Text, pb1Image, pb2Image);
            queue.bol.Enqueue(bo);
        }

        public void SetTimerEinblendenIntervall(int intervall)
        {
            settings.TimerEinblendenIntervall = intervall;
        }

        public void SetTimerAnzeigenIntervall(int intervall)
        {
            settings.TimerAnzeigenIntervall = intervall;
        }

        public void SetTimerAusblendenIntervall(int intervall)
        {
            settings.TimerAusblendenIntervall = intervall;
        }

        public void ShowMessages(bool doit = false)
        {
            if (!messagesRunning || doit)
            {
                messagesRunning = true;

                if (queue.bol.Count > 0)
                {
                    BannerObject bo = queue.bol.Dequeue();
                    if (bo.Pb1ImageLocation != null)
                    {
                        pb1.ImageLocation = bo.Pb1ImageLocation;
                    }
                    else
                    {
                        pb1.Image = bo.Pb1Image;
                    }
                    pb2.Image = bo.Pb2Image;
                    l1.Text = bo.Label1Text;
                    l2.Text = bo.Label2Text;
                    bannerPanel.Location = new Point(bannerPanel.Location.X, bannerPanel.Location.Y - (bannerPanel.Height + 2));
                    timerEinblenden.Start();
                }
            }
        }
    }
}
