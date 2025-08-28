using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SAPbouiCOM;
using SAPbouiCOM.Framework;

namespace EXX_IMG_ControlPresupuestal.Presentation.Forms.UDOForms
{
    [FormAttribute("UDO_FT_EXD_OGERUSU")]
    class FormUDO_FT_EXD_OGERUSU : UDOFormBase
    {
        protected override void OnFormDataLoadAfter(ref BusinessObjectInfo pVal)
        {
            base.OnFormDataLoadAfter(ref pVal);
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                if (UIAPIRawForm.Mode == BoFormMode.fm_UPDATE_MODE) UIAPIRawForm.Mode = BoFormMode.fm_OK_MODE;
            });
        }
    }
}
