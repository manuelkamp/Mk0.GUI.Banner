namespace Mk0.GUI.Banner
{
    public class Settings
    {
        internal int TimerEinblendenIntervall { get; set; }
        internal int TimerAnzeigenIntervall { get; set; }
        internal int TimerAusblendenIntervall { get; set; }

        internal Settings()
        {
            TimerEinblendenIntervall = 10;
            TimerAnzeigenIntervall = 1500;
            TimerAusblendenIntervall = 10;
        }
    }
}
