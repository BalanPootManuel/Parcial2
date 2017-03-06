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
  public partial class fCliente : Form
  {
    public fCliente()
    {
      InitializeComponent();
    }

    private Entidades.Cliente regActual;

    private void ActivarPanel(bool estado)
    {
      pDato.Enabled = estado;
      pCatalogo.Enabled = !estado;
      if (estado) txtNombre.Focus();
      else txtBuscar.Focus();
    }

    private void Leer(string dato)
    {
      try
      {
        dgvLista.DataSource = Negocio.cnCliente.Listar(dato);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    private void LeerDistrito()
    {
      try
      {
        cbDistrito.ValueMember = "codcategoria";
        cbDistrito.DisplayMember = "nomcategoria";
        cbDistrito.DataSource = Negocio.cnCategroia.Listar("");
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    private void PresentarDatos()
    {
      cbDistrito.Text = regActual.nombrecategoria;
      txtNombre.Text = regActual.nombre;
      txtapellidos.Text = regActual.apellidos;
      txtDireccion.Text = regActual.direccion;
      txtTelefono.Text = regActual.telefono;
    }

    private void fAmigo_Load(object sender, EventArgs e)
    {
      dgvLista.AutoGenerateColumns = false;
      LeerDistrito();
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
      txtNombre.Clear();
      ActivarPanel(true);
    }

    private void btnEditar_Click(object sender, EventArgs e)
    {
      if (dgvLista.CurrentRow != null)
      {
        regActual = (Entidades.Cliente)dgvLista.CurrentRow.DataBoundItem;
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
        Negocio.cnCliente.Eliminar((Entidades.Cliente)dgvLista.CurrentRow.DataBoundItem);
        Leer("");
      }
      else
        MessageBox.Show("Debe seleccionar un registro...", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    private void btnAceptar_Click(object sender, EventArgs e)
    {
      var oEntidad = new Entidades.Cliente();
      if (regActual != null)
        oEntidad.codcliente = regActual.codcliente;

      oEntidad.ecategoria = (Entidades.Categoria)cbDistrito.SelectedItem;
      oEntidad.nombre = txtNombre.Text.Trim();
      oEntidad.apellidos = txtapellidos.Text;
      oEntidad.direccion = txtDireccion.Text.Trim();
      oEntidad.telefono = txtTelefono.Text.Trim();

      try
      {
        Negocio.cnCliente.Grabar(oEntidad);
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

    private void dgvLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

    }
  }
}
