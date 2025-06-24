using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXX_IMG_ControlPresupuestal.Domain.Entities
{
    public class PartidaPresupuestal
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public string CodProyecto { get; set; }
        public string Etapa { get; set; }
        public string SubEtapa { get; set; }
        public int CodSucursal { get; set; }
        public string Gerencia { get; set; }
        public string CodPresupuesto { get; set; }
        public string CodCentroCosto { get; set; }
        public double TotalDisponible { get; set; }
        public double MontoTransferir { get; set; }
        public IEnumerable< PartidaPresupuestal> Detalle { get; set; }
    }

    public class PartidaPresupuestalDetalle
    {
        public string CodCentroCosto { get; set; }
        public string CodigoPartPres { get; set; }
        public string DescripcionPartPres { get; set; }
        public double TotalDisponible { get; set; }
        public double MontoAdicional { get; set; }
    }
}
