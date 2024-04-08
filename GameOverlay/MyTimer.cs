namespace GameOverlay
{
    public class MyTimer
    {
        public bool Enabled { get; set; }
        public int InitialTime { get; set; }

        public MyTimer(int initialTime)
        {
            Enabled = false;
            InitialTime = initialTime;
        }
    }
}
