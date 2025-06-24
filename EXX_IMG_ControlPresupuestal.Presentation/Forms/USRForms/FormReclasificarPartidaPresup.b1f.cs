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
        private SAPbouiCOM.ComboBox ComboBox1;
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

        private SAPbouiCOM.DataTable dttPartidasPrespuestales = null;


        public FormReclasificarPartidaPresup(PartidaPresupuestal partidaPresupuestal)
        {
            this._partidaPresupuestal = partidaPresupuestal;
            MostrarDescProyecto(_partidaPresupuestal.CodProyecto);
            udsETPA.Value = _partidaPresupuestal.Etapa;
            udsGRNC.Value = _partidaPresupuestal.Gerencia;
            udsCPRS.Value = _partidaPresupuestal.CodPresupuesto;
            udsCPRT.Value = _partidaPresupuestal.Codigo;
            udsDPRT.Value = _partidaPresupuestal.Descripcion;
            udsCNCS.Value = _partidaPresupuestal.CodCentroCosto;

            CargarPartidasPresupuestales(_partidaPresupuestal.CodPresupuesto, _partidaPresupuestal.Gerencia);
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
            this.ComboBox1 = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_13").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_14").Specific));
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_15").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_16").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("Item_17").Specific));
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_18").Specific));
            this.EditText7 = ((SAPbouiCOM.EditText)(this.GetItem("Item_19").Specific));
            this.StaticText10 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_20").Specific));
            this.EditText8 = ((SAPbouiCOM.EditText)(this.GetItem("Item_21").Specific));
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.mtxPartidasPresup = ((SAPbouiCOM.Matrix)(this.GetItem("Item_25").Specific));
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

        }

        private void MostrarDescProyecto(string codProyecto)
        {
            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            var sqlQry = $"select \"PrjName\" from OPRJ where \"PrjCode\" = '{codProyecto}'";

            recSet.DoQuery(sqlQry);

            if (!recSet.EoF) udsPROY.Value = recSet.Fields.Item(0).Value.ToString();
        }

        private void CargarPartidasPresupuestales(string codPresupuesto, string codGerencia)
        {
            var sqlQry = $"EXEC EXD_SP_GP_DETALLE_PARTIDA_PRESUPUESTAL '{codPresupuesto}','{codGerencia}'";

            if (SBOCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
            {
                sqlQry = $"CALL EXD_SP_GP_DETALLE_PARTIDA_PRESUPUESTAL('{codPresupuesto}','{codGerencia}')";
            }

            dttPartidasPrespuestales.ExecuteQuery(sqlQry);
            mtxPartidasPresup.LoadFromDataSource();
        }
    }
}
