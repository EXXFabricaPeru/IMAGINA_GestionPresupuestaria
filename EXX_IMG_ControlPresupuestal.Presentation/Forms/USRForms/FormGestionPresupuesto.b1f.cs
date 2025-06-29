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
using EXX_IMG_ControlPresupuestal.Domain.Entities;
using System.Xml.Linq;
using System.Threading.Tasks;

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
        private SAPbouiCOM.EditText EditText5;
        private SAPbouiCOM.StaticText StaticText6;
        private SAPbouiCOM.ComboBox cmbSeries;
        private SAPbouiCOM.EditText EditText0;
        private SAPbouiCOM.EditText EditText1;
        private SAPbouiCOM.EditText edtFchCreacion;
        private SAPbouiCOM.StaticText StaticText7;
        private SAPbouiCOM.StaticText StaticText8;
        private SAPbouiCOM.EditText EditText2;
        private SAPbouiCOM.EditText EditText3;
        private SAPbouiCOM.EditText EditText4;
        private SAPbouiCOM.EditText EditText6;
        private SAPbouiCOM.StaticText StaticText9;

        //Datasources
        SAPbouiCOM.DBDataSource dbsOGPR = null;
        SAPbouiCOM.DBDataSource dbsGPR1 = null;

        //UserDataSources
        SAPbouiCOM.UserDataSource udsSPATDP = null;

        //Flags
        private bool validarCambioDeMontoDisp = true;
        private bool reclasificacionHecha = false;


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

            mtxPresupuestos.AutoResizeColumns();

            LoadDataOnAddMode();
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
            this.mtxPresupuestos.LostFocusAfter += new SAPbouiCOM._IMatrixEvents_LostFocusAfterEventHandler(this.mtxPresupuestos_LostFocusAfter);
            this.mtxPresupuestos.ValidateAfter += new SAPbouiCOM._IMatrixEvents_ValidateAfterEventHandler(this.mtxPresupuestos_ValidateAfter);
            this.mtxPresupuestos.ValidateBefore += new SAPbouiCOM._IMatrixEvents_ValidateBeforeEventHandler(this.mtxPresupuestos_ValidateBefore);
            this.mtxPresupuestos.DoubleClickAfter += new SAPbouiCOM._IMatrixEvents_DoubleClickAfterEventHandler(this.mtxPresupuestos_DoubleClickAfter);
            this.mtxPresupuestos.PressedAfter += new SAPbouiCOM._IMatrixEvents_PressedAfterEventHandler(this.mtxPresupuestos_PressedAfter);
            this.Button0 = ((SAPbouiCOM.Button)(this.GetItem("1").Specific));
            this.Button0.PressedBefore += new SAPbouiCOM._IButtonEvents_PressedBeforeEventHandler(this.Button0_PressedBefore);
            this.Button1 = ((SAPbouiCOM.Button)(this.GetItem("2").Specific));
            this.dbsOGPR = this.UIAPIRawForm.GetDBDataSource("@EXD_OGPR");
            this.dbsGPR1 = this.UIAPIRawForm.GetDBDataSource("@EXD_GPR1");
            this.EditText5 = ((SAPbouiCOM.EditText)(this.GetItem("Item_20").Specific));
            this.StaticText6 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_13").Specific));
            this.cmbSeries = ((SAPbouiCOM.ComboBox)(this.GetItem("Item_14").Specific));
            this.EditText0 = ((SAPbouiCOM.EditText)(this.GetItem("Item_15").Specific));
            this.EditText1 = ((SAPbouiCOM.EditText)(this.GetItem("Item_16").Specific));
            this.edtFchCreacion = ((SAPbouiCOM.EditText)(this.GetItem("Item_17").Specific));
            this.StaticText7 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_18").Specific));
            this.StaticText8 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_19").Specific));
            this.EditText2 = ((SAPbouiCOM.EditText)(this.GetItem("Item_21").Specific));
            this.EditText3 = ((SAPbouiCOM.EditText)(this.GetItem("Item_22").Specific));
            this.EditText4 = ((SAPbouiCOM.EditText)(this.GetItem("Item_23").Specific));
            this.EditText6 = ((SAPbouiCOM.EditText)(this.GetItem("Item_24").Specific));
            this.StaticText9 = ((SAPbouiCOM.StaticText)(this.GetItem("Item_25").Specific));
            this.udsSPATDP = this.UIAPIRawForm.DataSources.UserDataSources.Item("UD_SPATDP");
            this.EditText7 = ((SAPbouiCOM.EditText)(this.GetItem("Item_26").Specific));
            this.OnCustomInitialize();

        }

        /// <summary>
        /// Initialize form event. Called by framework before form creation.
        /// </summary>
        public override void OnInitializeFormEvents()
        {
            this.DataAddAfter += new DataAddAfterHandler(this.Form_DataAddAfter);

        }

        private SAPbouiCOM.StaticText StaticText0;

        private void OnCustomInitialize()
        {
            mtxPresupuestos.Columns.Item("Col_6").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
            mtxPresupuestos.Columns.Item("Col_7").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
            mtxPresupuestos.Columns.Item("Col_8").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
            mtxPresupuestos.Columns.Item("Col_9").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;
            mtxPresupuestos.Columns.Item("Col_10").ColumnSetting.SumType = SAPbouiCOM.BoColumnSumType.bst_Auto;

            mtxPresupuestos.Columns.Item("Col_11").Visible = false;
            mtxPresupuestos.Columns.Item("Col_12").Visible = false;
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

            try
            {
                recSet.DoQuery(sqlQry);

                sqlQry = $"select distinct T1.U_EXC_GERENCIA,T1.U_EXC_GERENCIA from \"@EXC_PRESGENE\" T0 inner join \"@EXC_PRESGEN1\" T1 on T0.\"Code\" = T1.\"Code\" " +
                    $"where T0.U_EXC_CODIPROY = '{codProyecto}' and T0.U_EXC_ETAPA = '{codEtapa}' and T0.U_EXC_SUBETA = '{codSubEtapa}'";

                recSet.DoQuery(sqlQry);
                cmbGerencia.LoadValidValues(recSet);

                sqlQry = $"select distinct T1.\"Code\",T1.\"Code\" from \"@EXC_PRESGENE\" T0 inner join \"@EXC_PRESGEN1\" T1 on T0.\"Code\" = T1.\"Code\" " +
                    $"where T0.U_EXC_CODIPROY = '{codProyecto}' and T0.U_EXC_ETAPA = '{codEtapa}' and T0.U_EXC_SUBETA = '{codSubEtapa}'";

                recSet.DoQuery(sqlQry);
                cmbPresupuesto.LoadValidValues(recSet);

                sqlQry = $"select distinct U_EXC_IDEMPRE from \"@EXC_PRESGENE\" T0 inner join \"@EXC_PRESGEN1\" T1 on T0.\"Code\" = T1.\"Code\" " +
                    $"where T0.U_EXC_CODIPROY = '{codProyecto}' and T0.U_EXC_ETAPA = '{codEtapa}' and T0.U_EXC_SUBETA = '{codSubEtapa}'";

                recSet.DoQuery(sqlQry);
                if (!recSet.EoF)
                {
                    dbsOGPR.SetValueExt("U_COD_SUCURSAL", recSet.Fields.Item(0).Value.ToString());
                }

                dbsOGPR.SetValueExt("U_GERENCIA", null);
                dbsOGPR.SetValueExt("U_COD_PRESUP", null);

                dbsGPR1.Clear();
                mtxPresupuestos.LoadFromDataSource();

            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusErrorMessage(ex.Message);

            }
        }

        private void CargarPresupuestos(string codPrespuesto, string codGerencia)
        {
            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            var sqlQry = $"EXEC EXD_SP_GP_LISTAR_PRESUPUESTOS '{codPrespuesto}','{codGerencia}'";

            if (SBOCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
            {
                sqlQry = $"CALL EXD_SP_GP_LISTAR_PRESUPUESTOS('{codPrespuesto}','{codGerencia}')";
            }

            dbsGPR1.Clear();
            mtxPresupuestos.LoadFromDataSource();
            recSet.DoQuery(sqlQry);
            if (recSet.RecordCount > 0)
            {
                validarCambioDeMontoDisp = false;
                this.UIAPIRawForm.Freeze(true);
                LoadMatrixFromRecordSet(recSet);
                mtxPresupuestos.Columns.Item("Col_10").Cells.Item(1).Click(SAPbouiCOM.BoCellClickType.ct_Regular);
                cmbPresupuesto.Active = true;
                this.UIAPIRawForm.Freeze(false);
                validarCambioDeMontoDisp = true;
            }
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
                        new CellDBS{ Uid = "U_GERENCIA", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_GERENCIA").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_COD_PRTPRSP", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_CODPARPR").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_DSC_PRTPRSP", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_DESPARPR").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_CTRL_PRSP", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_CONTPRE").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_TIPO", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_TIPO").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_TOT_PLAN", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_TOTPLANI").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_TOT_META", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_TOTMETA").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_TOT_COMP", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_TOTCOMPR").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_TOT_EJEC", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_TOTEJECU").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_TOT_DISP", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_TOTDIS").InnerText ??  string.Empty},
                        new CellDBS{ Uid = "U_TOT_DISP_AUX", Value = rowsRS.FirstOrDefault( r => r.LocalName == "U_EXC_TOTDIS").InnerText ??  string.Empty},
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

        private void mtxPresupuestos_DoubleClickAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if(pVal.ColUID == "#" && pVal.Row >0)
            {
                var codPartida = dbsGPR1.GetValue("U_COD_PRTPRSP", pVal.Row - 1);
                var dscPartida = dbsGPR1.GetValue("U_DSC_PRTPRSP", pVal.Row - 1);
                var codCentCos = dbsGPR1.GetValue("U_CENTRO_COSTO", pVal.Row - 1);
                var totalDisponible = dbsGPR1.GetValue("U_TOT_DISP", pVal.Row - 1);

                var partidaPresupuestal = new PartidaPresupuestal()
                {
                    CodProyecto = dbsOGPR.GetValueExt("U_COD_PROYECTO"),
                    Etapa = dbsOGPR.GetValueExt("U_ETAPA"),
                    SubEtapa = dbsOGPR.GetValueExt("U_SUB_ETAPA"),
                    CodSucursal = Convert.ToInt32(dbsOGPR.GetValueExt("U_COD_SUCURSAL")),
                    Gerencia = dbsOGPR.GetValueExt("U_GERENCIA"),
                    CodPresupuesto = dbsOGPR.GetValueExt("U_COD_PRESUP"),
                    Codigo = codPartida,
                    Descripcion = dscPartida,
                    CodCentroCosto = codCentCos,
                    TotalDisponible = Convert.ToDouble(totalDisponible)
                };

                var formReclasPartPresup = new FormReclasificarPartidaPresup(partidaPresupuestal);
                formReclasPartPresup.Show();
            }
        }

        public void LoadDataOnAddMode()
        {
            reclasificacionHecha = false;
            cmbSeries.ValidValues.LoadSeries(UIAPIRawForm.BusinessObject.Type, SAPbouiCOM.BoSeriesMode.sf_Add);
            if (cmbSeries.ValidValues.Count > 0) cmbSeries.Select(0, SAPbouiCOM.BoSearchKey.psk_Index);
            dbsOGPR.SetValue("DocNum", 0, UIAPIRawForm.BusinessObject.GetNextSerialNumber(dbsOGPR.GetValue("Series", 0).Trim(), UIAPIRawForm.BusinessObject.Type).ToString());
            dbsOGPR.SetValueExt("CreateDate", DateTime.Today.ToString("yyyyMMdd"));
            dbsOGPR.SetValueExt("Creator", SBOCompany.UserName);
        }

        private void mtxPresupuestos_ValidateBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;
            if (validarCambioDeMontoDisp)
            {
                //var saldoPorAsignar = Convert.ToDouble(udsSPATDP.ValueEx);
                this.UIAPIRawForm.Freeze(true);

                mtxPresupuestos.FlushToDataSource();
                var saldoDisponible = Convert.ToDouble(dbsGPR1.GetValue("U_TOT_DISP", pVal.Row - 1));
                var saldoDisponibleAux = Convert.ToDouble(dbsGPR1.GetValue("U_TOT_DISP_AUX", pVal.Row - 1));

                dbsGPR1.SetValue("U_SALDO_ASIGNAR", pVal.Row - 1, (saldoDisponibleAux - saldoDisponible).ToString());

                mtxPresupuestos.LoadFromDataSourceEx();

                var saldoPorAsignarTot = 0.00;
                for (int i = 0; i < dbsGPR1.Size; i++)
                {
                    saldoPorAsignarTot += Convert.ToDouble(dbsGPR1.GetValue("U_SALDO_ASIGNAR", i));
                }

                this.UIAPIRawForm.Freeze(false);

                if (saldoPorAsignarTot != 0) reclasificacionHecha = true;

                udsSPATDP.ValueEx = saldoPorAsignarTot.ToString();
                if (saldoPorAsignarTot < 0.00)
                {
                    Application.SBO_Application.SetStatusErrorMessage("No es posible realizar un incremento que excede el saldo");
                    BubbleEvent = false;
                }
            }
        }

        private void mtxPresupuestos_ValidateAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            /*
            if (validarCambioDeMontoDisp)
            {
                mtxPresupuestos.FlushToDataSource();
                var saldoDisponible = Convert.ToDouble(dbsGPR1.GetValue("U_TOT_DISP", pVal.Row - 1));
                var saldoDisponibleAux = Convert.ToDouble(dbsGPR1.GetValue("U_TOT_DISP_AUX", pVal.Row - 1));

                dbsGPR1.SetValue("U_SALDO_ASIGNAR", pVal.Row - 1, (saldoDisponibleAux - saldoDisponible).ToString());

                mtxPresupuestos.LoadFromDataSourceEx();

                var saldoPorAsignarTot = 0.00;
                for (int i = 0; i < dbsGPR1.Size; i++)
                {
                    saldoPorAsignarTot += Convert.ToDouble(dbsGPR1.GetValue("U_SALDO_ASIGNAR", i));
                }

                //if (saldoPorAsignarTot < 0) saldoPorAsignarTot = 0.00;

                udsSPATDP.ValueEx = saldoPorAsignarTot.ToString();
            }
            */
        }

        private void mtxPresupuestos_LostFocusAfter(object sboObject, SAPbouiCOM.SBOItemEventArg pVal)
        {
            if (validarCambioDeMontoDisp)
            {

            }

        }

        private void Button0_PressedBefore(object sboObject, SAPbouiCOM.SBOItemEventArg pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;


            if (!reclasificacionHecha)
            {
                Application.SBO_Application.SetStatusErrorMessage("Debe realizar al menos una reclasificación para poder crear");
                BubbleEvent = false;
                return;
            }

            var saldoPorAsignar = Convert.ToDouble(udsSPATDP.ValueEx);
            if (saldoPorAsignar > 0.00)
            {
                Application.SBO_Application.SetStatusErrorMessage("No se puede crear la reclasificación cuando hay un saldo pendiente por asignar");
                BubbleEvent = false;
                return;
            }
        }

        private SAPbouiCOM.EditText EditText7;

        private void Form_DataAddAfter(ref SAPbouiCOM.BusinessObjectInfo pVal)
        {
            var recSet = (SAPbobsCOM.Recordset)SBOCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
            var newKey = XDocument.Parse(pVal.ObjectKey).Descendants("DocEntry").FirstOrDefault()?.Value;
            var sqlQry = $"update T0 set T0.U_EXC_TOTPLANI = T1.U_TOT_DISP,T0.U_EXC_TOTDIS =" +
                $" T1.U_TOT_DISP from  \"@EXC_PRESGEN1\" T0 inner join \"@EXD_GPR1\" T1 on T0.U_EXC_GERENCIA =" +
                $" T1.U_GERENCIA and  T0.U_EXC_CODPARPR = T1.U_COD_PRTPRSP and T1.\"DocEntry\" = '{newKey}'";

            if (SBOCompany.DbServerType == SAPbobsCOM.BoDataServerTypes.dst_HANADB)
            {
                sqlQry = $"update \"@EXC_PRESGEN1\" T0 set T0.U_EXC_TOTPLANI = (T0.U_EXC_TOTPLANI - T2.U_SALDO_ASIGNAR)," +
                    $"T0.U_EXC_TOTDIS = T2.U_TOT_DISP from  \"@EXC_PRESGEN1\" T0 " +
                    $"inner join \"@EXD_OGPR\" T1 on T0.\"Code\" = T1.U_COD_PRESUP " +
                    $"inner join \"@EXD_GPR1\" T2 on T2.\"DocEntry\" = T1.\"DocEntry\" " +
                    $"where T0.U_EXC_GERENCIA = T2.U_GERENCIA and  T0.U_EXC_CODPARPR = T2.U_COD_PRTPRSP " +
                    $"and T1.\"DocEntry\" = '{newKey}'";
            }
            recSet.DoQuery(sqlQry);
            reclasificacionHecha = false;
            Task.Factory.StartNew(() =>
            {
                LoadDataOnAddMode();
            });
        }
    }
}
