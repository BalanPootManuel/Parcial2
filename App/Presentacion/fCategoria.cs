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
  public partial class fCategoria : Form
  {

      
    public fCategoria()
    {
      InitializeComponent();

    }

    private Entidades.Categoria regActual;

    private void ActivarPanel(bool estado)
    {
      pDato.Enabled = estado;
      pCatalogo.Enabled = !estado;
      if (estado) txtCategoria.Focus();
      else txtBuscar.Focus();
    }

    private void Leer(string dato)
    {
      try
      {
        dgvLista.DataSource = Negocio.cnCategroia.Listar(dato);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    private void PresentarDatos()
    {
      txtCategoria.Text = regActual.nomcategoria;
    }

    private void fDistrito_Load(object sender, EventArgs e)
    {
      dgvLista.AutoGenerateColumns = false;
      Leer("");
    }

    private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
        Leer(txtBuscar.Text.Trim());
    }

    private void btnNuevo_Click(object sender, EventArgs e)
    {
      regActual = null;
      txtCategoria.Clear();
      ActivarPanel(true);
    }

    private void btnEditar_Click(object sender, EventArgs e)
    {
      if (dgvLista.CurrentRow != null)
      {
        regActual = (Entidades.Categoria)dgvLista.CurrentRow.DataBoundItem;
        PresentarDatos();
        ActivarPanel(true);
      }
      else
        MessageBox.Show("Debe seleccionar un registro...", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private void btnEliminar_Click(object sender, EventArgs e)
    {
      if (dgvLista.CurrentRow != null)
      {
        Negocio.cnCategroia.Eliminar((Entidades.Categoria)dgvLista.CurrentRow.DataBoundItem);
        Leer("");
      }
      else
        MessageBox.Show("Debe seleccionar un registro...", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private void btnAceptar_Click(object sender, EventArgs e)
    {
      var oEntidad = new Entidades.Categoria();
      if (regActual != null)
        oEntidad.codcategoria = regActual.codcategoria;

      oEntidad.nomcategoria = txtCategoria.Text.Trim();

      try
      {
        Negocio.cnCategroia.Grabar(oEntidad);
        ActivarPanel(false);
        Leer("");
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
      finally { oEntidad = null; }
    }

    private void btnCancelar_Click(object sender, EventArgs e)
    {
      ActivarPanel(false);
    }

    private void pCatalogo_Paint(object sender, PaintEventArgs e)
    {

    }

    private void dgvLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }

    private void button1_Click(object sender, EventArgs e)
    {
         new fMenu().ShowDialog();
         this.Hide(); 
         
    }
  }
}
