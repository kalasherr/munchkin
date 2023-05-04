using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace munchkin
{
    public partial class enter : Form
    {
        public enter()
        {
            InitializeComponent();
        }

        private void startgame_Click(object sender, EventArgs e)
        {
            
        }

        private void developers_Click(object sender, EventArgs e)
        {
            developers developer_form = new developers();
            developer_form.Show();
        }
    }
}
