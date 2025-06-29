using EXX_IMG_ControlPresupuestal.Domain.Entities;
using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static EXX_IMG_ControlPresupuestal.Common.Utiles.Global;
using JF_SBOAddon.Utiles.Extensions;

namespace EXX_IMG_ControlPresupuestal.Presentation.Forms.USRForms
{
    [FormAttribute("FormReclasificarPartidaPresup", "Forms/USRForms/FormReclasificarPartidaPresup.b1f")]
    class FormReclasificarPartidaPresup : UserFormBase
    {
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.ComboBox ComboBox0;
        private SAPbouiCOM.ComboBox cmbSucursales;
        private SAPbouiCOM.StaticText StaticText7;
        private SAPbouiCOM.EditText EditText5;
        private SAPbouiCOM.StaticText StaticText8;
        private SAPbouiCOM.EditText EditText6;
        private SAPbouiCOM.StaticText StaticText9;
        private SAPbouiCOM.EditText EditText7;
        private SAPbouiCOM.StaticText StaticText10;
        private SAPbouiCOM.EditText EditText8;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Button Button1;
        private SAPbouiCOM.Matrix mtxPartidasPresup;
        private SAPbouiCOM.StaticText StaticText11;
        private SAPbouiCOM.EditText EditText9;

        private PartidaPresupuestal _partidaPresupuestal = null;

        private SAPbouiCOM.UserDataSource udsPROY = null;
        private SAPbouiCOM.UserDataSource udsETPA = null;
        private SAPbouiCOM.UserDataSource udsSTPA = null;
        private SAPbouiCOM.UserDataSource udsSUCR = null;
        private SAPbouiCOM.UserDataSource udsGRNC = null;
        private SAPbouiCOM.UserDataSource udsCPRS = null;
        private SAPbouiCOM.UserDataSource udsCPRT = null;
        private SAPbouiCOM.UserDataSource udsDPRT = null;
        private SAPbouiCOM.UserDataSource udsCNCS = null;
        private SAPbouiCOM.UserDataSource udsTTDS = null;
        private SAPbouiCOM.UserDataSource udsMNTR = null;
        private SAPbouiCOM.UserDataSource udsSLAS = null;

        private SAPbouiCOM.DataTable dttPartidasPrespuestales = null;


        public FormReclasificarPartidaPresup(PartidaPresupuestal partidaPresupuestal)
        {
            this._partidaPresupuestal = partidaPresupuestal;

            MostrarDescProyecto(_partidaPresupuestal.CodProyecto);
            CargarSucursales();
            udsETPA.Value = _partidaPresupuestal.Etapa;
            udsSTPA.Value = _partidaPresupuestal.SubEtapa;
            udsSUCR.Value = _partidaPresupuestal.CodSucursal.ToString();
            udsGRNC.Value = _partidaPresupuestal.Gerencia;
            udsCPRS.Value = _partidaPresupuestal.CodPresupuesto;
            udsCPRT.Value = _partidaPresupuestal.Codigo;
            udsDPRT.Value = _partidaPresupuestal.Descripcion;
            udsCNCS.Value = _partidaPresupuestal.CodCentroCosto;
            udsTTDS.ValueEx = _partidaPresupuestal.TotalDisponible.ToString();

            CargarPartidasPresupuestales(_partidaPresupuestal.CodPresupuesto, _partidaPresupuestal.Gerencia, _partidaPresupuestal.Codigo);
        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_1").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_3").Specific));
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_5").Specific));
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_6").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_7").Specific));
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_8").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_9").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_10").Specific));
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_11").Specific));
            this.ComboBox0 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_12").Specific));
            this.cmbSucursales = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_13").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_14").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_15").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_16").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("Item_17").Specific));
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_18").Specific));
            this.EditText7 = ((SAPbouiCOM.EditText)(this.GetItem("Item_19").Specific));
            this.StaticText10 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_20").Specific));
            this.EditText8 = ((SAPbouiCOM.EditText)(this.GetItem("Item_21").Specific));
            this.EditText8.ValidateBefore += new SAPbouiCOM._IEditTextEvents_ValidateBeforeEventHandler(this.EditText8_ValidateBefore);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button0.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.Button0_PressedAfter);
            this.Button0.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.Button0_PressedBefore);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.mtxPartidasPresup = ((SAPbouiCOM.Matrix)(this.GetItem("Item_25").Specific));
            this.mtxPartidasPresup.ValidateBefore += new SAPbouiCOM._IMatrixEvents_ValidateBeforeEventHandler(this.mtxPartidasPresup_ValidateBefore);
            this.StaticText11 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_26").Specific));
            this.EditText9 = ((SAPbouiCOM.EditText)(this.GetItem("Item_27").Specific));
            this.udsPROY = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_PROY");
            this.udsETPA = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_ETPA");
            this.udsSTPA = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_STPA");
            this.udsSUCR = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_SUCR");
            this.udsGRNC = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_GRNC");
            this.udsCPRS = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_CPRS");
            this.udsCPRT = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_CPRT");
            this.udsDPRT = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_DPRT");
            this.udsCNCS = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_CNCS");
            this.udsTTDS = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_TTDS");
            this.udsMNTR = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_MNTR");
            this.udsSLAS = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_SLAS");
            this.dttPartidasPrespuestales = this.UIAPIRawForm.DataSources.DataTables.Item("DT_PP");
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
        }

        private SAPbouiCOM.StaticText StaticText0;

        private void OnCustomInitialize()
        {
            mtxPartidasPresup.Columns.Item("Col_3").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Manual;
            mtxPartidasPresup.Columns.Item("Col_4").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Manual;
        }

        private void MostrarDescProyecto(string codProyecto)
        {
            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            var sqlQry = $"select \"PrjName\" from OPRJ where \"PrjCode\" = '{codProyecto}'";

            recSet.DoQuery(sqlQry);

            if (!recSet.EoF) udsPROY.Value = recSet.Fields.Item(0).Value.ToString();
        }

        private void CargarSucursales()
        {
            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            var sqlQry = $"select \"BPLId\",\"BPLName\" from OBPL";

            recSet.DoQuery(sqlQry);

            cmbSucursales.LoadValidValues(recSet);
        }

        private void CargarPartidasPresupuestales(string codPresupuesto, string codGerencia, string codPartida)
        {
            var sqlQry = $"EXEC EXD_SP_GP_DETALLE_PARTIDA_PRESUPUESTAL '{codPresupuesto}','{codGerencia}','{codPartida}'";

            if (SBOCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
            {
                sqlQry = $"CALL EXD_SP_GP_DETALLE_PARTIDA_PRESUPUESTAL('{codPresupuesto}','{codGerencia}','{codPartida}')";
            }

            dttPartidasPrespuestales.ExecuteQuery(sqlQry);
            mtxPartidasPresup.LoadFromDataSource();
            MostrarTotales();
            mtxPartidasPresup.AutoResizeColumns();
        }

        private void EditText8_ValidateBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            var totalDisponible = Convert.ToDouble(udsTTDS.ValueEx);
            var montoTransferir = Convert.ToDouble(udsMNTR.ValueEx);

            if ((totalDisponible - montoTransferir) < 0)
            {
                Application.SBO_Application.SetStatusErrorMessage("No se puede tranferir un monto mayor al total disponible");
                BubbleEvent = false;
                return;
            }

            udsSLAS.ValueEx = montoTransferir.ToString();
        }

        private void mtxPartidasPresup_ValidateBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            mtxPartidasPresup.FlushToDataSource();
            var montoTransferir = Convert.ToDouble(udsMNTR.ValueEx);
            var totalAsignado = 0.00;

            for (int i = 0; i < dttPartidasPrespuestales.Rows.Count; i++)
            {
                totalAsignado += Convert.ToDouble(dttPartidasPrespuestales.GetValue("Monto adicional", i).ToString());
            }

            MostrarTotales();

            var saldoPorAsignar = montoTransferir - totalAsignado;

            udsSLAS.ValueEx = saldoPorAsignar.ToString();
        }

        private void Button0_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            if (this.UIAPIRawForm.Mode != SAPbouiCOM.BoFormMode.fm_UPDATE_MODE) return;
            var saldoPorAsignar = Convert.ToDouble(udsSLAS.ValueEx);
            if (saldoPorAsignar > 0.00)
            {
                Application.SBO_Application.SetStatusErrorMessage("No se puede generar la reclasificacion cuando hay un saldo mayor a cero");
                BubbleEvent = false;
                return;
            }
        }

        private void Button0_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            var sqlQryUpdt = string.Empty;
            var montoAdicional = 0.00;
            var gerencia = udsGRNC.Value;
            var partidaPresup = udsCPRT.Value;
            var codPresup = udsCPRS.Value;

            var sqlQry = $"update \"@EXC_PRESGEN1\" set U_EXC_TOTPLANI = U_EXC_TOTPLANI - " +
                $"{Convert.ToDouble(udsMNTR.ValueEx)} ,U_EXC_TOTDIS = U_EXC_TOTDIS - " +
                $"{Convert.ToDouble(udsMNTR.ValueEx)}  where  \"Code\" = '{codPresup}' and U_EXC_GERENCIA = '{gerencia}' " +
                $"and U_EXC_CODPARPR = '{partidaPresup}'";
            recSet.DoQuery(sqlQry);

            sqlQry = "update \"@EXC_PRESGEN1\" set U_EXC_TOTPLANI = U_EXC_TOTPLANI + {0}, U_EXC_TOTDIS = U_EXC_TOTDIS + {0}  where \"Code\" = '"+ codPresup +"' and U_EXC_GERENCIA = '{1}' and U_EXC_CODPARPR = '{2}'";

            for (int i = 0; i < dttPartidasPrespuestales.Rows.Count; i++)
            {
                montoAdicional = Convert.ToDouble(dttPartidasPrespuestales.GetValue("Monto adicional", i));
                partidaPresup = dttPartidasPrespuestales.GetValue("Codigo partida presup", i).ToString();
                if (montoAdicional > 0.00)
                {
                    sqlQryUpdt = string.Format(sqlQry, montoAdicional, gerencia, partidaPresup);
                    recSet.DoQuery(sqlQryUpdt);
                }
            }
        }


        private void MostrarTotales()
        {
            var totTotalDisp = 0.00;
            var totMontoAdic = 0.00;

            for (int i = 0; i < dttPartidasPrespuestales.Rows.Count; i++)
            {
                totTotalDisp += Convert.ToDouble(dttPartidasPrespuestales.GetValue("Total disponible", i));
                totMontoAdic += Convert.ToDouble(dttPartidasPrespuestales.GetValue("Monto adicional", i));
            }

            mtxPartidasPresup.Columns.Item("Col_3").ColumnSetting.SumValue = totTotalDisp.ToString();
            mtxPartidasPresup.Columns.Item("Col_4").ColumnSetting.SumValue = totMontoAdic.ToString();
        }
    }
}
