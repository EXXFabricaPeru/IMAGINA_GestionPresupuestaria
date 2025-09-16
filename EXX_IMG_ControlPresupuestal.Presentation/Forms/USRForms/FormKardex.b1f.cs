using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static EXX_IMG_ControlPresupuestal.Common.Utiles.Global;
using JF_SBOAddon.Utiles.Extensions;

namespace EXX_IMG_ControlPresupuestal.Presentation.Forms.USRForms
{
    [FormAttribute("FormKardex", "Forms/USRForms/FormKardex.b1f")]
    class FormKardex : UserFormBase
    {
        private SAPbouiCOM.StaticText StaticText0;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.ComboBox cmbSucursales;
        private SAPbouiCOM.ComboBox cmbProyectos;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.ComboBox cmbEtapas;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.ComboBox cmbSubEtapas;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.ComboBox cmbPartidasPresup;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.Button btnBuscar;
        private SAPbouiCOM.Grid grdPresupuestos;
        private SAPbouiCOM.StaticText StaticText7;
        private SAPbouiCOM.StaticText StaticText8;
        private SAPbouiCOM.ComboBox cmbGerencias;
        private SAPbouiCOM.ComboBox cmbCentrosDeCosto;
        private SAPbouiCOM.StaticText StaticText9;
        private SAPbouiCOM.ComboBox cmbCodigosPartidaPresp;
        private SAPbouiCOM.Button btnComprimir;
        private SAPbouiCOM.Button btnExpandir;

        //Datasources
        private SAPbouiCOM.UserDataSource udsSUCU = null;
        private SAPbouiCOM.UserDataSource udsPROY = null;
        private SAPbouiCOM.UserDataSource udsETAP = null;
        private SAPbouiCOM.UserDataSource udsSETA = null;
        private SAPbouiCOM.UserDataSource udsPPRE = null;
        private SAPbouiCOM.UserDataSource udsFINI = null;
        private SAPbouiCOM.UserDataSource udsFFIN = null;
        private SAPbouiCOM.UserDataSource udsGERE = null;
        private SAPbouiCOM.UserDataSource udsCCOS = null;
        private SAPbouiCOM.UserDataSource udsCPRE = null;
        //DataTables
        private SAPbouiCOM.DataTable dttPresupuestos = null;

        public FormKardex()
        {
            //Sucursales
            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            var sqlQry = $"select T0.\"BPLId\",T0.\"BPLName\" from OBPL T0 inner join USR6 T1 on T0.\"BPLId\" = T1.\"BPLId\" where \"UserCode\" = '{SBOCompany.UserName}'";
            recSet.DoQuery(sqlQry);
            cmbSucursales.LoadValidValues(recSet, "-1", "Todas");

            //Gerencias
            var gerenciaPorDefecto = string.Empty;
            sqlQry = $"select U_COD_GERENCIA,U_COD_GERENCIA,U_POR_DEFECTO from \"@EXD_GERUSU1\" T0 inner join \"@EXD_OGERUSU\" T1 on T0.\"Code\" = T1.\"Code\" where T1.\"Code\" = '{SBOCompany.UserName}'";
            recSet.DoQuery(sqlQry);
            while (cmbGerencias.ValidValues.Count > 0) cmbGerencias.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);
            cmbGerencias.ValidValues.Add("*", "Todas");
            while (!recSet.EoF)
            {
                cmbGerencias.ValidValues.Add(recSet.Fields.Item(0).Value.ToString(), recSet.Fields.Item(0).Value.ToString());
                if (recSet.Fields.Item(2).Value.ToString() == "Y") gerenciaPorDefecto = recSet.Fields.Item(0).Value.ToString();
                recSet.MoveNext();
            }
            if (!string.IsNullOrWhiteSpace(gerenciaPorDefecto)) cmbGerencias.Select(gerenciaPorDefecto, SAPbouiCOM.BoSearchKey.psk_ByValue);

        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_1").Specific));
            this.cmbSucursales = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_2").Specific));
            this.cmbSucursales.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.cmbSucursales_ComboSelectAfter);
            this.cmbProyectos = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_3").Specific));
            this.cmbProyectos.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.cmbProyectos_ComboSelectAfter);
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.cmbEtapas = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_5").Specific));
            this.cmbEtapas.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.cmbEtapas_ComboSelectAfter);
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_6").Specific));
            this.cmbSubEtapas = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_7").Specific));
            this.cmbSubEtapas.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.cmbSubEtapas_ComboSelectAfter);
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_8").Specific));
            this.cmbPartidasPresup = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_9").Specific));
            this.cmbPartidasPresup.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.cmbPartidasPresup_ComboSelectAfter);
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_10").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_11").Specific));
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_13").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_14").Specific));
            this.grdPresupuestos = ((SAPbouiCOM.Grid)(this.GetItem("Item_15").Specific));
            this.btnBuscar = ((SAPbouiCOM.Button)(this.GetItem("Item_16").Specific));
            this.btnBuscar.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.btnBuscar_PressedAfter);
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_12").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_17").Specific));
            this.cmbGerencias = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_18").Specific));
            this.cmbGerencias.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.cmbGerencias_ComboSelectAfter);
            this.cmbCentrosDeCosto = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_19").Specific));
            this.cmbCentrosDeCosto.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.cmbCentrosDeCosto_ComboSelectAfter);
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_20").Specific));
            this.cmbCodigosPartidaPresp = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_21").Specific));
            this.cmbCodigosPartidaPresp.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.cmbCodigosPartidaPresp_ComboSelectAfter);
            this.udsSUCU = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_SUCU");
            this.udsPROY = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_PROY");
            this.udsETAP = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_ETAP");
            this.udsSETA = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_SETA");
            this.udsPPRE = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_PPRE");
            this.udsFINI = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_FINI");
            this.udsFFIN = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_FFIN");
            this.udsCCOS = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_CCOS");
            this.udsGERE = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_GERE");
            this.udsCPRE = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_CPRE");
            this.dttPresupuestos = this.UIAPIRawForm.DataSources.DataTables.Item("DT_PRSP");
            this.btnComprimir = ((SAPbouiCOM.Button)(this.GetItem("Item_22").Specific));
            this.btnComprimir.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.btnComprimir_PressedAfter);
            this.btnExpandir = ((SAPbouiCOM.Button)(this.GetItem("Item_23").Specific));
            this.btnExpandir.PressedAfter += new SAPbouiCOM._IButtonEvents_PressedAfterEventHandler(this.btnExpandir_PressedAfter);
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {

        }
        private void OnCustomInitialize()
        {

        }

          private void cmbCentrosDeCosto_ComboSelectAfter(object sboObject, SBOItemEventArg pVal)
          {
              udsPPRE.Value = null;
              cmbPartidasPresup.ClearValidValues();
        
              var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
              var sqlQry = $"select distinct U_EXC_CODPARPR,U_EXC_CODPARPR from \"@EXC_PRESGENE\" T0 inner join \"@EXC_PRESGEN1\" T1 " +
                  $"on T0.\"Code\" = T1.\"Code\" where T0.\"Code\" = '{udsCPRE.Value}' and T1.\"U_EXC_GERENCIA\"= '{udsGERE.Value}' and T1.\"U_EXC_CENCOSTO\"= '{udsCCOS.Value}' order by 1 asc  ";
        
              recSet.DoQuery(sqlQry);
              cmbPartidasPresup.LoadValidValues(recSet, "*", "Todos");
          }
        
          private void cmbGerencias_ComboSelectAfter(object sboObject, SBOItemEventArg pVal)
          {
              udsPPRE.Value = null;
              cmbPartidasPresup.ClearValidValues();
        
              dttPresupuestos.Rows.Clear();
              if (!string.IsNullOrEmpty(udsCPRE.Value))
              {
                  var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                  var sqlQry = $"select distinct U_EXC_CODPARPR,U_EXC_CODPARPR from \"@EXC_PRESGENE\" T0 inner join \"@EXC_PRESGEN1\" T1 " +
                      $"on T0.\"Code\" = T1.\"Code\" where T0.\"Code\" = '{udsCPRE.Value}' and T1.\"U_EXC_GERENCIA\"= '{udsGERE.Value}' ";
        
                  recSet.DoQuery(sqlQry);
                  cmbPartidasPresup.LoadValidValues(recSet, "*", "Todos");
        
                  sqlQry = $"select distinct T0.\"OcrCode\",T0.\"OcrName\" from OOCR T0 inner join \"@EXC_PRESGEN1\" T1 on T0.\"OcrCode\" = T1.U_EXC_CENCOSTO where T1.\"Code\" = '{udsCPRE.Value}' and T1.\"U_EXC_GERENCIA\"= '{udsGERE.Value}'";
                  recSet.DoQuery(sqlQry);
                  cmbCentrosDeCosto.LoadValidValues(recSet, "*", "Todos");
              }
        
             
          }
        private void cmbSucursales_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            var sqlQry = $"select \"PrjCode\",\"PrjName\" from OPRJ where U_EXC_IDEMPRES = '{udsSUCU.Value}'order by 2";

            if (udsSUCU.Value == "-1")
            {
                sqlQry = $"select \"PrjCode\",\"PrjName\" from USR6 T0 inner join OBPL T1 on T0.\"BPLId\" = T1.\"BPLId\" inner join OPRJ T2 on T2.U_EXC_IDEMPRES = T1.\"BPLId\" where T0.\"UserCode\" = '{SBOCompany.UserName}'";
            }

            udsPROY.Value = null;
            cmbProyectos.ClearValidValues();
            udsETAP.Value = null;
            cmbEtapas.ClearValidValues();
            udsSETA.Value = null;
            cmbSubEtapas.ClearValidValues();
            udsCPRE.Value = null;
            cmbCodigosPartidaPresp.ClearValidValues();
            udsCCOS.Value = null;
            cmbCentrosDeCosto.ClearValidValues();
            udsPPRE.Value = null;
            cmbPartidasPresup.ClearValidValues();

            dttPresupuestos.Rows.Clear();

            recSet.DoQuery(sqlQry);
            cmbProyectos.LoadValidValues(recSet, "*", "Todos");
        }

        private void cmbProyectos_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            udsETAP.Value = null;
            cmbEtapas.ClearValidValues();
            udsSETA.Value = null;
            cmbSubEtapas.ClearValidValues();
            udsCPRE.Value = null;
            cmbCodigosPartidaPresp.ClearValidValues();
            udsCCOS.Value = null;
            cmbCentrosDeCosto.ClearValidValues();
            udsPPRE.Value = null;
            cmbPartidasPresup.ClearValidValues();
            dttPresupuestos.Rows.Clear();

            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            var sqlQry = $"select distinct \"PrcCode\",\"PrcName\" from \"@EXC_PRESGENE\" T0 inner join OPRC T1 on T0.U_EXC_ETAPA = T1.\"PrcCode\" where T0.U_EXC_CODIPROY = '{udsPROY.Value}' and U_EXC_IDEMPRE = {udsSUCU.Value}";

            recSet.DoQuery(sqlQry);
            cmbEtapas.LoadValidValues(recSet, "*", "Todas");
        }

        private void cmbEtapas_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            udsSETA.Value = null;
            cmbSubEtapas.ClearValidValues();
            udsCCOS.Value = null;
            cmbCentrosDeCosto.ClearValidValues();
            udsCPRE.Value = null;
            cmbCodigosPartidaPresp.ClearValidValues();
            udsPPRE.Value = null;
            cmbPartidasPresup.ClearValidValues();

            dttPresupuestos.Rows.Clear();

            //SeleccionarSucursalGerenciaCodPresup(cmbProyecto.Value, cmbEtapa.Value, cmbSubEtapa.Value);
            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);

            var sqlQry = $"select distinct U_EXC_SUBETA,U_EXC_SUBETA from \"@EXC_PRESGENE\" where U_EXC_IDEMPRE = '{udsSUCU.Value}' " +
                $"and U_EXC_CODIPROY = '{udsPROY.Value}' and U_EXC_ETAPA = '{udsETAP.Value}'";

            recSet.DoQuery(sqlQry);
            cmbSubEtapas.LoadValidValues(recSet, "*", "Todas");
        }

        private void cmbSubEtapas_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            /*
            udsPPRE.Value = null;
            cmbPartidasPresup.ClearValidValues();

            dttPresupuestos.Rows.Clear();

            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            var sqlQry = $"select distinct U_EXC_CODPARPR,U_EXC_CODPARPR from \"@EXC_PRESGENE\" T0 inner join \"@EXC_PRESGEN1\" T1 on T0.\"Code\" = T1.\"Code\" " +
            $"where T0.U_EXC_CODIPROY = '{udsPROY.Value}' and T0.U_EXC_ETAPA = '{udsETAP.Value}' and T0.U_EXC_SUBETA = '{udsSETA.Value}' and coalesce(T0.U_EXC_ESTADO,'') = 'A' order by 1";

            recSet.DoQuery(sqlQry);
            cmbPartidasPresup.LoadValidValues(recSet);
            */
            udsCCOS.Value = null;
            cmbCentrosDeCosto.ClearValidValues();
            udsCPRE.Value = null;
            cmbCodigosPartidaPresp.ClearValidValues();
            udsPPRE.Value = null;
            cmbPartidasPresup.ClearValidValues();

            dttPresupuestos.Rows.Clear();

            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            var sqlQry = $"select distinct T1.\"Code\",T1.\"Code\" from \"@EXC_PRESGENE\" T0 inner join \"@EXC_PRESGEN1\" T1 on T0.\"Code\" = T1.\"Code\" " +
                $"where T0.U_EXC_CODIPROY = '{udsPROY.Value}' and T0.U_EXC_ETAPA = '{udsETAP.Value}' and T0.U_EXC_SUBETA = '{udsSETA.Value}' and coalesce(T0.U_EXC_ESTADO,'') = 'A'";

            recSet.DoQuery(sqlQry);
            cmbCodigosPartidaPresp.LoadValidValues(recSet, "*", "Todas");
        }

        private void cmbPartidasPresup_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            CargarKardex();
        }

        private void btnBuscar_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            CargarKardex();
        }
        private void CargarKardex()
        {
         try
 {
            var codSucursal = udsSUCU.Value == "-1" ? "*" : udsSUCU.Value;
            var codProyecto = udsPROY.Value;
            var codEtapa = udsETAP.Value;
            var codSubEtapa = udsSETA.Value;
            var codPartPresu = udsPPRE.Value;
            var codCentroDeCosto = udsCCOS.Value;
            var codGerencia = udsGERE.Value;
            var fechaIni = string.IsNullOrWhiteSpace(udsFINI.ValueEx) ? "19000101" : udsFINI.ValueEx;
            var fechaFin = string.IsNullOrWhiteSpace(udsFFIN.ValueEx) ? "99991231" : udsFFIN.ValueEx;

            var sqlQry = $"CALL REPORTE_KARDEX_PRESUPUESTAL('{codSucursal}','{codProyecto}','{codEtapa}','{codSubEtapa}','{codPartPresu}','{codCentroDeCosto}','{codGerencia}','{fechaIni}','{fechaFin}')";

            dttPresupuestos.ExecuteQuery(sqlQry);

            var colTotComp = (SAPbouiCOM.EditTextColumn)grdPresupuestos.Columns.Item("TotalComprometido");
            var colTotEjec = (SAPbouiCOM.EditTextColumn)grdPresupuestos.Columns.Item("TotalEjecutado");
            //var colTotPlanDet = (SAPbouiCOM.EditTextColumn)grdPresupuestos.Columns.Item("TotalPlanificadoDetallado");
            //var colTotDispDet = (SAPbouiCOM.EditTextColumn)grdPresupuestos.Columns.Item("TotalDisponibleDetallado");
            var colTotPlanif = (SAPbouiCOM.EditTextColumn)grdPresupuestos.Columns.Item("TotalPlanificado");
            var colTotDispon = (SAPbouiCOM.EditTextColumn)grdPresupuestos.Columns.Item("TotalDisponible");

            colTotComp.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
            colTotEjec.ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;

            colTotComp.RightJustified = true;
            colTotEjec.RightJustified = true;
            //colTotPlanDet.RightJustified = true;
            //colTotDispDet.RightJustified = true;
            colTotPlanif.RightJustified = true;
            colTotDispon.RightJustified = true;

            grdPresupuestos.CollapseLevel = 1;
 }
 catch (Exception)
 {

 }
            this.UIAPIRawForm.Refresh();
        }

        private void cmbCodigosPartidaPresp_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            udsPPRE.Value = null;
            cmbPartidasPresup.ClearValidValues();

            dttPresupuestos.Rows.Clear();

            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            var sqlQry = $"select distinct U_EXC_CODPARPR,U_EXC_CODPARPR from \"@EXC_PRESGENE\" T0 inner join \"@EXC_PRESGEN1\" T1 " +
                $"on T0.\"Code\" = T1.\"Code\" where T0.\"Code\" = '{udsCPRE.Value}' and T1.\"U_EXC_GERENCIA\"= '{udsGERE.Value}' ";

            recSet.DoQuery(sqlQry);
            cmbPartidasPresup.LoadValidValues(recSet, "*", "Todos");

            sqlQry = $"select distinct T0.\"OcrCode\",T0.\"OcrName\" from OOCR T0 inner join \"@EXC_PRESGEN1\" T1 on T0.\"OcrCode\" = T1.U_EXC_CENCOSTO where T1.\"Code\" = '{udsCPRE.Value}' and T1.\"U_EXC_GERENCIA\"= '{udsGERE.Value}'"; 
            recSet.DoQuery(sqlQry);
            cmbCentrosDeCosto.LoadValidValues(recSet, "*", "Todos");
        }

        private void btnExpandir_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            grdPresupuestos.Rows.ExpandAll();
        }

        private void btnComprimir_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            grdPresupuestos.Rows.CollapseAll();
        }
    }
}
