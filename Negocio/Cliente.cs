using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Objetos;

namespace Negocio
{
    public class Cliente
    {
        BDCliente bd = new BDCliente();
        public void insetarCliente(ObjCliente obj) {
            bd.InsertCliente(obj);
        }

        public void ActulizarCliente(ObjCliente obj) { 
            bd.UpdateCliente(obj);
        }

        public void ActulizarClienteDeEmpresa(int cedulaCliente, int idCliente) { 
            bd.ActualizarEmpresaCliente(cedulaCliente, idCliente);
        }
    }
}
