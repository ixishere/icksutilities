using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace icksutilities
{
    public partial class Form1 : Form
    {
        /* Software Information */
        string title = "icks utilities";
        string version = "0.1";

        /* Theming Information
            text: #ffffff;
            background: #1a1a1a;
            primary: #b300ff;
            secondary: #a200ff;
            accent: #00c3ff;
        */

        /* Rounded Edges DLL */
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        /* Form Setup */
        public Form1()
        {
            InitializeComponent(); // Components
            AttachEventHandlers();
            FormBorderStyle = FormBorderStyle.None; // Remove header
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25)); // Rounded Edges
            Text = string.Format("{0} v{1}", title, version); // Title, not needed but why not
            labelUsername.Text = WindowsIdentity.GetCurrent().Name;
        }

        /* Double click for when minimized - down in system tray */
        private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;

            if (WindowState == FormWindowState.Normal)
            {
                ShowInTaskbar = true;
                trayIcon.Visible = false;
            }
        }

        /* Set to minimized in system tray */
        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            bool MousePointerNotOnTaskBar = Screen.GetWorkingArea(this).Contains(Cursor.Position);

            if (WindowState == FormWindowState.Minimized && MousePointerNotOnTaskBar)
            {
                trayIcon.Text = string.Format("{0} v{1}", title, version);
                //trayIcon.Icon = Properties.Resources.test;
                trayIcon.BalloonTipText = "Your program has been minimized to system tray";
                trayIcon.ShowBalloonTip(1000);
                ShowInTaskbar = false;
                trayIcon.Visible = true;
            }
        }

        /* Drag Listener DLLs */
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        /* Drag listener for form */
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        /* Drag listener for header panel */
        private void headerPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        /* Exit on close icon click */
        private void exitPicture_Click(object sender, EventArgs e)
        {
            Environment.Exit(1);
        }

        /* Set to minimized in system tray */
        private void minimisePicture_Click(object sender, EventArgs e)
        {
            bool MousePointerNotOnTaskBar = Screen.GetWorkingArea(this).Contains(Cursor.Position);

            WindowState = FormWindowState.Minimized;

            if (WindowState == FormWindowState.Minimized && MousePointerNotOnTaskBar)
            {
                trayIcon.Text = string.Format("{0} v{1}", title, version);
                //trayIcon.Icon = Properties.Resources.test;
                trayIcon.BalloonTipText = "Your program has been minimized to system tray";
                trayIcon.ShowBalloonTip(1000);
                ShowInTaskbar = false;
                trayIcon.Visible = true;
            }
        }

        private void ImageGrow_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            if (pictureBox != null)
            {
                pictureBox.Width += 1;
                pictureBox.Height += 1;
            }
        }

        private void ImageGrow_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;

            if (pictureBox != null)
            {
                pictureBox.Width -= 1;
                pictureBox.Height -= 1;
            }
        }

        private void PanelColor_MouseEnter(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;

            if (panel != null)
            {
                panel.BackColor = Color.FromArgb(37, 37, 37);
            }
        }

        private void PanelColor_MouseLeave(object sender, EventArgs e)
        {
            Panel panel = sender as Panel;

            if (panel != null)
            {
                panel.BackColor = Color.FromArgb(29, 29, 29);
            }
        }

        // Attach the common event handler to all panels
        private void AttachEventHandlers()
        {
            panel8.MouseEnter += PanelColor_MouseEnter;
            panel8.MouseLeave += PanelColor_MouseLeave;
            panel7.MouseEnter += PanelColor_MouseEnter;
            panel7.MouseLeave += PanelColor_MouseLeave;
            panel3.MouseEnter += PanelColor_MouseEnter;
            panel3.MouseLeave += PanelColor_MouseLeave;
            panel4.MouseEnter += PanelColor_MouseEnter;
            panel4.MouseLeave += PanelColor_MouseLeave;
            panel5.MouseEnter += PanelColor_MouseEnter;
            panel5.MouseLeave += PanelColor_MouseLeave;
            panel6.MouseEnter += PanelColor_MouseEnter;
            panel6.MouseLeave += PanelColor_MouseLeave;

            exitPicture.MouseEnter += ImageGrow_MouseEnter;
            exitPicture.MouseLeave += ImageGrow_MouseLeave;
            minimisePicture.MouseEnter += ImageGrow_MouseEnter;
            minimisePicture.MouseLeave += ImageGrow_MouseLeave;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
