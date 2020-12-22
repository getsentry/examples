using System;
using System.Windows.Forms;
using Sentry;

namespace WindowsFormsCSharp
{
    public partial class Form1 : Form
    {
        public Form1() => InitializeComponent();

        private void button1_Click(object sender, EventArgs e)
        {
            SentrySdk.AddBreadcrumb($"{nameof(button1_Click)} clicked.", "ui.lifecycle");

            throw null;
        }
    }
}
