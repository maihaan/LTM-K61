using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            Graphics g = this.CreateGraphics();
            g.DrawRectangle(Pens.Blue, new Rectangle(x, y, 1, 1));

            ClientSocket c = new ClientSocket();
            c.Send(x, y);
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;

            Graphics g = this.CreateGraphics();
            g.DrawRectangle(Pens.Blue, new Rectangle(x, y, 1, 1));

            ClientSocket c = new ClientSocket();
            c.Send(x, y);
        }
    }
}
