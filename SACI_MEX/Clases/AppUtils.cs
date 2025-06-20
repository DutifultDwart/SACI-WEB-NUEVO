using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SACI_MEX.Clases
{
    public class AppUtils
    {
    }


    public  struct MsgRegistros
    {
        public const string MsgSelectRegistro = "Debes seleccionar al menos un registro.";
        public const string MsgConfirmaElimna = "¿Estas seguro de eliminar el registro?";
        public const string MsgRegistroElimina = "Se eliminó correctamente el registro";
        public const string MsgRegistroExcel = "No hay información para exportar";
        public const string MsgRegistroActualiza = "Se actualizó correctamente el registro";
        public const string MsgRegistroAgregar = "Se agrego correctamente el registro";
        public const string MsgRegistroFechas = "El rango de fechas es incorrecto";
    }


    public struct MsgCatUsuarios
    {
        public const string UsuarioInvalido = "Usuario o contraseña invalida, verifique nuevamente los datos";
    }  

    public struct MsgAF
    {
        public const string PermisosAlertValidaClave = "Debe escribir una Clave";
    }

    public struct MsgSubmaquila
    {
        public const string SubmaquilaAlertValidaCode = "Debe escribir una Clave";
    }

    public struct MsgCategorias
    {
        public const string CategoriaAlertValidaCategory = "Debe escribir una Categoria";
    }

    public struct MsgDivisionAlmacenes
    {
        public const string DivisionAlmacenesAlertValidaStore = "Debe escribir un Almacén";
        public const string DivisionAlmacenesAlertValidaDescription = "Debe escribir una Descripción";
    }

    public struct MsgUnidades
    {
        public const string UnidadAlertValidaCodeUnit = "Debe escribir una Clave de Unidad";
        public const string UnidadAlertValidaName = "Debe escribir un Nombre";
        public const string UnidadAlertValidaAlias = "Debe escribir un Alias";
    }

    public struct MsgPermisos
    {
        public const string PermisosAlertValidaNumPermiso = "Debe escribir un Num. de Permiso";
    }

    public struct MsgRegistroSACI
    {
        public const string RegistroInvalido = "Su registro ya no tiene vigencia, favor de contactar a soporte (soporte@gruposac.com.mx)";
        public const string RegistroVacio = "Error al validar su registro, sin datos.";
    }
}