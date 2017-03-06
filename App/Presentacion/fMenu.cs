using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class fMenu : Form
    {
        public fMenu()
        {
            InitializeComponent();
          
        }

        private void btnDistrito_Click(object sender, EventArgs e)
        {
           
          new fCategoria().ShowDialog();
         

         
        }

        private void btnAmigo_Click(object sender, EventArgs e)
        {
          new fCliente().ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new fCategoria().ShowDialog();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new fCliente().ShowDialog();
        }

        private void fMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
