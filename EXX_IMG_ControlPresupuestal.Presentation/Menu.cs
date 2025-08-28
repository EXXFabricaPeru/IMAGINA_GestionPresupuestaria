using SAPbouiCOM.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using JF_SBOAddon.Utiles.Extensions;
using EXX_IMG_ControlPresupuestal.Presentation.Forms.USRForms;

namespace EXX_IMG_ControlPresupuestal.Presentation
{
    class Menu
    {
        private FormGestionPresupuesto formGestionPresupuesto = null;

        public void AddMenuItems()
        {
            try
            {
                SAPbouiCOM.Menus oMenus = null;
                SAPbouiCOM.MenuItem oMenuItem = null;

                oMenus = Application.SBO_Application.Menus;

                SAPbouiCOM.MenuCreationParams oCreationPackage = null;
                oCreationPackage = ((SAPbouiCOM.MenuCreationParams)(Application.SBO_Application.CreateObject(SAPbouiCOM.BoCreatableObjectType.cot_MenuCreationParams)));

                Application.SBO_Application.SetStatusSuccessMessage("Control presupuestal: cargando opciones de menú");

                Application.SBO_Application.Forms.GetForm("169", 1)?.Freeze(true);

                oMenuItem = Application.SBO_Application.Menus.Item("1536"); //Finanzas'               

                if (!oMenus.Exists("MNU_OGPR"))
                {
                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_POPUP;
                    oCreationPackage.UniqueID = "MNU_OGPR";
                    oCreationPackage.String = "Control presupuestal";
                    oCreationPackage.Enabled = true;
                    oCreationPackage.Position = -1;

                    oMenus = oMenuItem.SubMenus;
                    oMenus.AddEx(oCreationPackage);
                }

                if (!oMenus.Exists("MNU_OGPR_001"))
                {
                    // Get the menu collection of the newly added pop-up item
                    oMenuItem = Application.SBO_Application.Menus.Item("MNU_OGPR");
                    oMenus = oMenuItem.SubMenus;
                    // Create s sub menu
                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                    oCreationPackage.UniqueID = "MNU_OGPR_001";
                    oCreationPackage.String = "Gestión de presupuesto";
                    oCreationPackage.Position = 1;
                    oMenus.AddEx(oCreationPackage);
                }

                if (!oMenus.Exists("MNU_OGPR_002"))
                {
                    // Get the menu collection of the newly added pop-up item
                    oMenuItem = Application.SBO_Application.Menus.Item("MNU_OGPR");
                    oMenus = oMenuItem.SubMenus;
                    // Create s sub menu
                    oCreationPackage.Type = SAPbouiCOM.BoMenuType.mt_STRING;
                    oCreationPackage.UniqueID = "MNU_OGPR_002";
                    oCreationPackage.String = "Kardex";
                    oCreationPackage.Position = 2;
                    oMenus.AddEx(oCreationPackage);
                }
            }
            catch (Exception ex)
            {
                Application.SBO_Application.SetStatusBarMessage(ex.Message, SAPbouiCOM.BoMessageTime.bmt_Short, true);
            }
            finally
            {
                Application.SBO_Application.Forms.GetForm("169", 1)?.Freeze(false);
                Application.SBO_Application.Forms.GetForm("169", 1)?.Update();
                Application.SBO_Application.SetStatusSuccessMessage("Control presupuestal: opciones de menú cargados correctamente");
            }
        }

        public void SBO_Application_MenuEvent(ref SAPbouiCOM.MenuEvent pVal, out bool BubbleEvent)
        {
            BubbleEvent = true;

            try
            {
                if (pVal.BeforeAction && pVal.MenuUID == "MNU_OGPR_001")
                {
                    formGestionPresupuesto = new FormGestionPresupuesto();
                    formGestionPresupuesto.Show();
                }

                if (pVal.BeforeAction && pVal.MenuUID == "MNU_OGPR_002")
                {
                    new FormKardex().Show();
                }

                if (pVal.MenuUID == "1282")
                {
                    if (!pVal.BeforeAction && Application.SBO_Application.Forms.ActiveForm != null &&
                        Application.SBO_Application.Forms.ActiveForm.TypeEx == "FormGestionPresupuesto")
                    {
                        formGestionPresupuesto.LoadDataOnAddMode();
                    }
                }

            }
            catch (Exception ex)
            {
                Application.SBO_Application.MessageBox(ex.ToString(), 1, "Ok", "", "");
            }
        }

    }
}
