using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Objetos;

namespace Negocio
{
    public class Empresa
    {
        BDEmpresa bd = new BDEmpresa();

        public void AgregarEmpresa(ObjEmpresa objEmpresa) { 
            bd.InsertEmpresa(objEmpresa);
        }

        public void ModificarEmpresa(ObjEmpresa objEmpresa) { 
            bd.UpdateEmpresa(objEmpresa);   
        }
    }
}
