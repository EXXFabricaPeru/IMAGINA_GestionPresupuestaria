using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static EXX_IMG_ControlPresupuestal.Common.Utiles.Global;
using JF_SBOAddon.Utiles.Extensions;
using EXX_PP_ReceptorFacturasProv.Domain.Entities;
using System.Xml.Serialization;
using System.IO;

namespace EXX_IMG_ControlPresupuestal.Presentation.Forms.USRForms
{
    [FormAttribute("FormGestionPresupuesto", "Forms/USRForms/FormGestionPresupuesto.b1f")]
    class FormGestionPresupuesto : UserFormBase
    {
        private SAPbouiCOM.ComboBox cmbProyecto;
        private SAPbouiCOM.StaticText StaticText1;
        private SAPbouiCOM.ComboBox cmbEtapa;
        private SAPbouiCOM.StaticText StaticText2;
        private SAPbouiCOM.ComboBox cmbSubEtapa;
        private SAPbouiCOM.StaticText StaticText3;
        private SAPbouiCOM.ComboBox cmbSucursal;
        private SAPbouiCOM.StaticText StaticText4;
        private SAPbouiCOM.ComboBox cmbGerencia;
        private SAPbouiCOM.StaticText StaticText5;
        private SAPbouiCOM.ComboBox cmbPresupuesto;
        private SAPbouiCOM.Matrix mtxPresupuestos;
        private SAPbouiCOM.Button Button0;
        private SAPbouiCOM.Button Button1;

        //Datasources

        SAPbouiCOM.DBDataSource dbsOGPR = null;
        SAPbouiCOM.DBDataSource dbsGPR1 = null;


        public FormGestionPresupuesto()
        {
            //Carganado datoa al formuario
            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            var sqlQry = "select \"PrjCode\",\"PrjName\" from OPRJ order by 2";

            //Proyecto
            recSet.DoQuery(sqlQry);
            cmbProyecto.LoadValidValues(recSet);

            //Etapa
            sqlQry = "SELECT \"PrcCode\", \"PrcName\" FROM OPRC WHERE \"DimCode\" = 1 AND \"Active\" = 'Y'";
            recSet.DoQuery(sqlQry);
            cmbEtapa.LoadValidValues(recSet);

            //SubEtapa
            cmbSubEtapa.ValidValues.Add("SET00", "SET00");

            //Sucursal
            sqlQry = "select \"BPLId\",\"BPLName\" from OBPL";
            recSet.DoQuery(sqlQry);
            cmbSucursal.LoadValidValues(recSet);

        }

        /// <summary>
        /// Initialize components. Called by framework after form created.
        /// </summary>
        public override void OnInitializeComponent()
        {
            this.StaticText0 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_0").Specific));
            this.cmbProyecto = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_1").Specific));
            this.cmbProyecto.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.cmbProyecto_ComboSelectAfter);
            this.StaticText1 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_2").Specific));
            this.cmbEtapa = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_3").Specific));
            this.cmbEtapa.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.cmbEtapa_ComboSelectAfter);
            this.StaticText2 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_4").Specific));
            this.cmbSubEtapa = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_5").Specific));
            this.cmbSubEtapa.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.cmbSubEtapa_ComboSelectAfter);
            this.StaticText3 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_6").Specific));
            this.cmbSucursal = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_7").Specific));
            this.StaticText4 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_8").Specific));
            this.cmbGerencia = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_9").Specific));
            this.cmbGerencia.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.cmbGerencia_ComboSelectAfter);
            this.StaticText5 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_10").Specific));
            this.cmbPresupuesto = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_11").Specific));
            this.cmbPresupuesto.ComboSelectAfter += new SAPbouiCOM._IComboBoxEvents_ComboSelectAfterEventHandler(this.cmbPresupuesto_ComboSelectAfter);
            this.mtxPresupuestos = ((SAPbouiCOM.Matrix)(this.GetItem("Item_12").Specific));
            this.mtxPresupuestos.PressedAfter += new SAPbouiCOM._IMatrixEvents_PressedAfterEventHandler(this.mtxPresupuestos_PressedAfter);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.dbsOGPR = this.UIAPIRawForm.GetDBDataSource("@EXD_OGPR");
            this.dbsGPR1 = this.UIAPIRawForm.GetDBDataSource("@EXD_GPR1");
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
            mtxPresupuestos.Columns.Item("Col_6").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
            mtxPresupuestos.Columns.Item("Col_7").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
            mtxPresupuestos.Columns.Item("Col_8").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
            mtxPresupuestos.Columns.Item("Col_9").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
            mtxPresupuestos.Columns.Item("Col_10").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
        }

        private void cmbSubEtapa_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            SeleccionarSucursalGerenciaCodPresup(cmbProyecto.Value, cmbEtapa.Value, cmbSubEtapa.Value);
        }

        private void cmbEtapa_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            SeleccionarSucursalGerenciaCodPresup(cmbProyecto.Value, cmbEtapa.Value, cmbSubEtapa.Value);
        }

        private void cmbProyecto_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            SeleccionarSucursalGerenciaCodPresup(cmbProyecto.Value, cmbEtapa.Value, cmbSubEtapa.Value);
        }


        private void SeleccionarSucursalGerenciaCodPresup(string codProyecto, string codEtapa, string codSubEtapa)
        {
            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            var sqlQry = $"select U_EXC_IDEMPRE from \"@EXC_PRESGENE\" where U_EXC_CODIPROY = '{codProyecto}' and U_EXC_ETAPA = '{codEtapa}' and U_EXC_SUBETA = '{codSubEtapa}'";

            recSet.DoQuery(sqlQry);


            sqlQry = $"select distinct T1.U_EXC_GERENCIA,T1.U_EXC_GERENCIA from \"@EXC_PRESGENE\" T0 inner join \"@EXC_PRESGEN1\" T1 on T0.\"Code\" = T1.\"Code\" " +
                $"where T0.U_EXC_CODIPROY = '{codProyecto}' and T0.U_EXC_ETAPA = '{codEtapa}' and T0.U_EXC_SUBETA = '{codSubEtapa}'";

            recSet.DoQuery(sqlQry);
            cmbGerencia.LoadValidValues(recSet);

            sqlQry = $"select distinct T1.\"Code\",T1.\"Code\" from \"@EXC_PRESGENE\" T0 inner join \"@EXC_PRESGEN1\" T1 on T0.\"Code\" = T1.\"Code\" " +
                $"where T0.U_EXC_CODIPROY = '{codProyecto}' and T0.U_EXC_ETAPA = '{codEtapa}' and T0.U_EXC_SUBETA = '{codSubEtapa}'";

            recSet.DoQuery(sqlQry);
            cmbPresupuesto.LoadValidValues(recSet);
        }

        private void CargarPresupuestos(string codPrespuesto, string codGerencia)
        {
            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            var sqlQry = $"EXEC EXD_SP_GP_LISTAR_PRESUPUESTOS '{codPrespuesto}','{codGerencia}'";

            if (SBOCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
            {
                sqlQry = $"CALL EXD_SP_GP_LISTAR_PRESUPUESTOS('{codPrespuesto}','{codGerencia}')";
            }

            recSet.DoQuery(sqlQry);
            LoadMatrixFromRecordSet(recSet);
        }

        private void LoadMatrixFromRecordSet(SAPbobsCOM.Recordset recSet)
        {
            var _dsrXmlDBDataSource = new XMLDBDataSource();
            var _xmlSerializer = new XmlSerializer(typeof(XMLRecordSet));
            var _dsrRecSet = (XMLRecordSet)_xmlSerializer.Deserialize(new StringReader(recSet.GetAsXML()));

            _dsrXmlDBDataSource.Rows = _dsrRecSet.BO.Rows.Select(d =>
            {
                var rowsRS = (System.Xml.XmlNode[])d;
                return new RowDBS
                {
                    Cells = new List<CellDBS>
                    {
                        new CellDBS{ Uid = "U_CENTRO_COSTO", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_CENCOSTO").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_GERENCIA", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_CENCOSTO").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_COD_PRTPRSP", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_CODPARPR").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_DSC_PRTPRSP", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_DESPARPR").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_CTRL_PRSP", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_CONTPRE").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_TIPO", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_TIPO").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_TOT_PLAN", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_TOTPLANI").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_TOT_META", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_TOTMETA").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_TOT_COMP", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_TOTCOMPR").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_TOT_EJEC", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_TOTEJECU").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_TOT_DISP", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_TOTDIS").InnerText ??  string.Empty},
                    }.ToArray()
                };
            }).ToArray();

            _xmlSerializer = new XmlSerializer(typeof(XMLDBDataSource));
            using (var strWritter = new StringWriter())
            {
                _xmlSerializer.Serialize(strWritter, _dsrXmlDBDataSource);
                dbsGPR1.LoadFromXML(strWritter.ToString());
                mtxPresupuestos.LoadFromDataSource();
                mtxPresupuestos.AutoResizeColumns();
            }
            mtxPresupuestos.Columns.Item("Col_6").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
            mtxPresupuestos.Columns.Item("Col_7").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
            mtxPresupuestos.Columns.Item("Col_8").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
            mtxPresupuestos.Columns.Item("Col_9").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
            mtxPresupuestos.Columns.Item("Col_10").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;

        }

        private void cmbGerencia_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            CargarPresupuestos(cmbPresupuesto.Value, cmbGerencia.Value);
        }

        private void cmbPresupuesto_ComboSelectAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            CargarPresupuestos(cmbPresupuesto.Value, cmbGerencia.Value);
        }

        private void mtxPresupuestos_PressedAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            //Application.SBO_Application.MessageBox("Hola mundo");

        }
    }
}
