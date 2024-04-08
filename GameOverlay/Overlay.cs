using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace GameOverlay
{
    public partial class Overlay : Form
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;

        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = SetHook(_proc);

        private static MyTimer timer1;
        private static MyTimer timer2;
        private static MyTimer timer3;

        private static Label FirstLabel;
        private static Label SecondLabel;
        private static Label ThirdLabel;

        private readonly Thread timerThread1;
        private readonly Thread timerThread2;
        private readonly Thread timerThread3;

        private static Settings settings;

        public Overlay()
        {
            InitializeComponent();

            InitializeSettings();

            FirstLabel = new Label()
            {
                Parent = FirstSpellPicture,
                BackColor = Color.Gold,
                Size = FirstSpellPicture.Image.Size,
                Font = new Font("Arial", 16),
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };

            SecondLabel = new Label()
            {
                Parent = SecondSpellPicture,
                BackColor = Color.Gold,
                Size = SecondSpellPicture.Image.Size,
                Font = new Font("Arial", 16),
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };

            ThirdLabel = new Label()
            {
                Parent = ThirdSpellPicture,
                BackColor = Color.FromArgb(75, Color.Gold),
                Size = ThirdSpellPicture.Image.Size,
                Font = new Font("Arial", 16),
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };

            FirstSpellPicture.ImageLocation = settings.FirstSpell.Image;
            SecondSpellPicture.ImageLocation = settings.SecondSpell.Image;
            ThirdSpellPicture.ImageLocation = settings.ThirdSpell.Image;

            timer1 = new MyTimer(settings!.FirstSpell.Time);
            timer2 = new MyTimer(settings.SecondSpell.Time);
            timer3 = new MyTimer(settings.ThirdSpell.Time);

            timerThread1 = new Thread(() => StartTimer(timer1));
            timerThread2 = new Thread(() => StartTimer(timer2));
            timerThread3 = new Thread(() => StartTimer(timer3));

            timerThread1.Start();
            timerThread2.Start();
            timerThread3.Start();
        }

        private Point mouseOffset;
        private bool isMouseDown = false;

        private void InitializeSettings()
        {
            var path = "settings.json";

            if (!File.Exists(path))
            {
                Environment.Exit(1);
            }
            else
            {
                using FileStream fs = new FileStream(path, FileMode.Open);
                settings = JsonSerializer.Deserialize<Settings>(fs);
            }

        }

        private void PictureMouseDown(object sender, MouseEventArgs e)
        {
            int xOffset;
            int yOffset;

            if (e.Button == MouseButtons.Left)
            {
                xOffset = -e.X - SystemInformation.FrameBorderSize.Width;
                yOffset = -e.Y - SystemInformation.CaptionHeight - SystemInformation.FrameBorderSize.Height;
                mouseOffset = new Point(xOffset, yOffset);
                isMouseDown = true;
            }
        }

        private void StartTimer(MyTimer timer)
        {
            try
            {
                while (true)
                {
                    if (timer.Enabled)
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            timer.InitialTime--;
                            if (timer.InitialTime <= 0)
                            {
                                PerformActionOnTimerCompletion(timer);
                            }
                            else
                            {
                                UpdateDisplay(timer);
                            }
                        });
                    }
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                Environment.Exit(1);
            }
        }

        private void PerformActionOnTimerCompletion(MyTimer timer)
        {
            timer.Enabled = false;

            if (timer == timer1)
            {
                Invoke((MethodInvoker)delegate
                {
                    FirstLabel.Visible = false;
                    timer1.InitialTime = settings.FirstSpell.Time;
                });
            }
            else if (timer == timer2)
            {
                Invoke((MethodInvoker)delegate
                {
                    SecondLabel.Visible = false;
                    timer2.InitialTime = settings.SecondSpell.Time;
                });
            }
            else if (timer == timer3)
            {
                Invoke((MethodInvoker)delegate
                {
                    ThirdLabel.Visible = false;
                    timer3.InitialTime = settings.ThirdSpell.Time;
                });
            }
        }

        private void PictureMouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                PictureBox? pictureBox = sender as PictureBox;

                if (pictureBox != null)
                {
                    Point mousePos = MousePosition;
                    mousePos.Offset(mouseOffset.X, mouseOffset.Y);
                    pictureBox.Location = mousePos;
                }
            }
        }

        private void PictureMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void UpdateDisplay(MyTimer timer)
        {
            if (timer == timer1)
            {
                Invoke((MethodInvoker)delegate
                {
                    FirstLabel.Text = timer.InitialTime.ToString();
                });
            }
            else if (timer == timer2)
            {
                Invoke((MethodInvoker)delegate
                {
                    SecondLabel.Text = timer.InitialTime.ToString();
                });
            }
            else if (timer == timer3)
            {
                Invoke((MethodInvoker)delegate
                {
                    ThirdLabel.Text = timer.InitialTime.ToString();
                });
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            UnhookWindowsHookEx(_hookID);
            base.Dispose(disposing);
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);

                if (vkCode == settings.FirstSpell.KeyCode && !timer1.Enabled)
                {
                    timer1.Enabled = true;
                    FirstLabel.Visible = true;
                    FirstLabel.Text = timer1.InitialTime.ToString();
                }

                if (vkCode == settings.FirstSpell.KeyCode && ModifierKeys.HasFlag(Keys.Control) && timer1.Enabled)
                {
                    timer1.Enabled = false;
                    FirstLabel.Visible = false;
                }

                if (vkCode == settings.SecondSpell.KeyCode && !timer2.Enabled)
                {
                    timer2.Enabled = true;
                    SecondLabel.Visible = true;
                    SecondLabel.Text = timer2.InitialTime.ToString();
                }

                if (vkCode == settings.SecondSpell.KeyCode && ModifierKeys.HasFlag(Keys.Control) && timer2.Enabled)
                {
                    timer2.Enabled = false;
                    SecondLabel.Visible = false;
                }

                if (vkCode == settings.ThirdSpell.KeyCode && !timer3.Enabled)
                {
                    timer3.Enabled = true;
                    ThirdLabel.Visible = true;
                    ThirdLabel.Text = timer3.InitialTime.ToString();
                }

                if (vkCode == settings.ThirdSpell.KeyCode && ModifierKeys.HasFlag(Keys.Control) && timer3.Enabled)
                {
                    timer3.Enabled = false;
                    ThirdLabel.Visible = false;
                }
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
