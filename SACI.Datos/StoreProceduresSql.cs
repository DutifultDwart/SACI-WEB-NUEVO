using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SACI.Datos
{
    public class StoreProceduresSql
    {
        #region CATALOGOS

        #endregion

    }

    public struct sp_Catalogos
    {
        public const string SP_CAT_USUARIOS = "SP_CAT_USUARIOS ";
        public const string SpBitacora = "EXEC sp_InsertBitacora ";
        public const string SP_CAT_ACTIVIDADES = "EXEC SP_CAT_ACTIVIDADES ";
        public const string SP_CAT_PERFILES = "EXEC SP_CAT_PERFILES ";
        public const string SP_CAT_CATEGORIAS = "EXEC SACIWEB_CAT_CATEGORIAS ";
        public const string SP_CAT_AF = "EXEC SACIWEB_CAT_AF ";
        public const string SP_CAT_SUBMAQUILA = "EXEC SACIWEB_CAT_SUBMAQUILA ";
        public const string SP_CAT_DIVISIONYALMACENES = "EXEC SACIWEB_CAT_DIVISIONES_ALMACENES ";
        public const string SP_CAT_UNIDADES = "EXEC SACIWEB_CAT_UNIDADES ";
        public const string SP_CAT_PERMISOS = "EXEC SACIWEB_CAT_PERMISOS ";
        public const string SP_CAT_TIPOMATERIAL = "EXEC SACIWEB_CAT_TIPOMATERIAL ";
        public const string SP_CAT_MATERIALES = "EXEC SACIWEB_CAT_MATERIAL ";
        public const string SP_CAT_ALMACENES = "EXEC SACIWEB_CAT_ALMACENES ";
        public const string SP_CAT_DATOSGENERALES = "EXEC SACIWEB_CAT_DATOSGENERALES ";
        public const string SP_CAT_PRODUCTOS = "EXEC SACIWEB_CAT_PRODUCTOS ";
        public const string SP_CAT_PROVEEDORES = "EXEC SACIWEB_CAT_PROVEEDORES ";
        public const string SP_CAT_CLIENTES = "EXEC SACIWEB_CAT_CLIENTES ";
        public const string SP_CAT_AGENTES = "EXEC SACIWEB_CAT_AGENTES ";
        public const string SP_REL_ACTIVIDAD_PERFIL = "EXEC SACIWEB_REL_ACTIVIDAD_PERFIL ";
        public const string SACIWEB_CAT_TIPO_NAC_IMP = "EXEC SACIWEB_CAT_TIPO_NAC_IMP ";
        public const string SACIWEB_CR_MATERIAL = "EXEC SACIWEB_CR_MATERIAL ";
        public const string SACIWEB_CR_PROD_ESTRUCTURA = "EXEC SACIWEB_CR_PROD_ESTRUCTURA ";
        public const string SACIWEB_ESTRUCTURAS = "EXEC SACIWEB_ESTRUCTURAS ";
        //public const string SACIWEB_CR_ESTRUCT_MATE = "EXEC SACIWEB_CR_ESTRUCT_MATE ";
        public const string SACIWEB_CR_MATE_ALTERNA = "EXEC SACIWEB_CR_MATE_ALTERNA ";
        public const string SACIWEB_CR_PERMISO_DETALLE = "EXEC SACIWEB_CR_PERMISO_DETALLE ";
        public const string SACIWEB_CR_PROD_FACTOR = "EXEC SACIWEB_CR_PROD_FACTOR ";
        public const string SACIWEB_CAT_PLANTAS = "EXEC SACIWEB_CAT_PLANTAS ";
        public const string SP_ACCESO_USUARIO = "EXEC SP_ACCESO_USUARIO ";
        public const string SACIWEB_CR_PRODUCTOMATERIAL = "EXEC SACIWEB_CR_PRODUCTOMATERIAL ";
        public const string SACIWEB_PAISES = "EXEC SACIWEB_PAISES ";
        public const string SACIWEB_CAT_INCOTERM = "EXEC SACIWEB_CAT_INCOTERM ";
        public const string SACIWEB_CAT_FORMAS_PAGO = "EXEC SACIWEB_CAT_FORMAS_PAGO ";
        public const string SACIWEB_WARNINGS = "EXEC SACIWEB_WARNINGS ";
        public const string SACIWEB_DETALLE_WARNINGS = "EXEC SACIWEB_DETALLE_WARNINGS ";
        //SP para registro de aplicacion
        public const string SACIWEB_REGISTRO = "SACIWEB_REGISTRO ";
        public const string SACIWEB_CAT_CONSULTAS = "EXEC SACIWEB_CAT_CONSULTAS ";
        public const string SACIWEB_CAT_CONSULTAS_CRUD = "SACIWEB_CAT_CONSULTAS_CRUD";
        //
        public const string SACIWEB_CAT_DG_CERTIFICACIONES = "EXEC SACIWEB_CAT_DG_CERTIFICACIONES ";
        public const string SP_REL_ACTIVIDAD_PERFIL2 = "EXEC SACIWEB_REL_ACTIVIDAD_PERFIL2 ";
        //
        public const string SACIWEB_ACCESO = "EXEC SACIWEB_ACCESO ";
        public const string SACIWEB_MAILFROM = "EXEC SACIWEB_MAILFROM ";
        public const string SP_BITACORA_MOVIMIENTOS = "EXEC SP_BITACORA_MOVIMIENTOS ";

        public const string SP_ESTATUS_ARCHI = "EXEC SACIWEB_SELECT_ESTATUS_ARCHI ";
        public const string SACIWEB_PLANTA_USUARIO = "EXEC SACIWEB_PLANTA_USUARIO ";
        public const string SP_CAT_TRATADO = "EXEC SP_CAT_TRATADO ";
        public const string SP_CAT_CAPITULO = "EXEC SP_CAT_CAPITULO ";
        public const string SP_CAT_REGLA_ORIGEN = "EXEC SP_CAT_Reglas_Origen ";
        public const string SP_CAT_REGION = "EXEC SP_CAT_REGION ";
        public const string SP_CAT_NOTAS_CAPITULO = "EXEC SP_CAT_NOTAS_CAPITULO ";
        public const string SP_CAT_Notas = "EXEC SP_CAT_Notas ";
        public const string SP_CAT_CASOS_REGLA = "EXEC SP_CAT_Casos_Regla ";
    }


    public struct Informes
    {
        public const string SACIWEB_MS_SALDOS = "EXEC SACIWEB_MS_SALDOS ";
        public const string SACIWEB_MS_SALDOS_FECHA = "EXEC SACIWEB_MS_SALDOS_FECHA ";
        public const string SACIWEB_MS_IMPORTACIONES = "EXEC SACIWEB_MS_IMPORTACIONES ";
        public const string SACIWEB_MS_EXPORTACIONES = "EXEC SACIWEB_MS_EXPORTACIONES ";
        public const string SACIWEB_MS_HISTORIADESCARGAE = "EXEC SACIWEB_MS_HISTORIADESCARGAE ";
        public const string SACIWEB_MS_HISTORIADESCARGAE_PAGINADO = "EXEC SACIWEB_MS_HISTORIADESCARGAE_PAGINADO ";
        public const string SACIWEB_MS_HISTORIADESCARGAS = "EXEC SACIWEB_MS_HISTORIADESCARGAS ";
        public const string SACIWEB_MS_HISTORIADESCARGAS_PAGINADO = "EXEC SACIWEB_MS_HISTORIADESCARGAS_PAGINADO ";       
        public const string SACIWEB_ANALISIS_DESCARGA = "EXEC SACIWEB_ANALISIS_DESCARGA ";
        public const string SACIWEB_RPT_VENCIMIENTOS = "EXEC SACIWEB_RPT_VENCIMIENTOS ";
        public const string SACIWEB_RPT_ESTRUCTURAS = "EXEC SACIWEB_RPT_ESTRUCTURAS ";
        //[LCG][27/12/2019][Se declara variable para el reporte de Compulsa]
        public const string SACIWEB_MS_COMPULSA = "EXEC SACIWEB_COMPULSADSA24 ";
        //[LCG][23/03/2020][Se declaran variables para el reporte de F4 CTMA y F4 Descargos]
        public const string SACIWEB_REP_F4CTMA = "EXEC SACIWEB_REP_F4CTMA ";
        public const string SACIWEB_REP_F4DESPERDICIOS = "EXEC SACIWEB_REP_F4DESPERDICIOS ";
        //[LCG][27/04/2020][Se declara variable para el nuevo reporte de estructuras]
        public const string SACIWEB_INFORME_ESTRUCTURAS = "EXEC SACIWEB_INFORME_ESTRUCTURAS ";
        public const string SACIWEB_INFORME_DIRIGIDOS = "EXEC SACIWEB_INFORME_DIRIGIDOS ";
        //[LCG][02/12/2020][Se declara constante para el nuevo repote de Descargas SCRAP]
        public const string SACIWEB_INFORME_DESCARGAS_SCRAP = "EXEC SACIWEB_INFORME_DESCARGAS_SCRAP ";
        //[MBA][19/01/2021][nuevo repote de informe de permisos]
        public const string SACIWEB_INFORME_PERMISOS = "EXEC SACIWEB_INFORME_PERMISOS ";
        //[MBA][19/01/2021][nuevo repote de informe de ajustes]	
        public const string SACIWEB_INFORME_AJUSTES = "EXEC SACIWEB_INFORME_AJUSTES ";
        //[MBA][02/05/2024][nuevo repote de informe de ajustes]	
        public const string SP_SACIWEB_INFORME_AJUSTES = "SACIWEB_INFORME_AJUSTES ";
        //[MBA][09/08/2022][nuevos repotes de informes ]
        public const string SACIWEB_MS_CADENA_RECTIFICACION = "EXEC SACIWEB_MS_CADENA_RECTIFICACION ";
        public const string SACIWEB_MS_EXPORTAR_INFORME_DESCARGOS = "EXEC SACIWEB_MS_EXPORTAR_INFORME_DESCARGOS ";
        //Reporte Complementario (T-MEC antes TLCAN)
        public const string SACIWEB_MS_TLCUE = "EXEC SACIWEB_MS_TLCUE ";

        //[MBA][28/04/2023][7 nuevos repotes de informes]	
        public const string SACIWEB_CONSOLIDA_F4CTMA = "EXEC SACIWEB_CONSOLIDA_F4CTMA ";
        public const string SACIWEB_CTMFACTURA_CONSOLIDADO = "EXEC SACIWEB_CTMFACTURA_CONSOLIDADO ";
        public const string SACIWEB_CONSOLIDA_HDE = "EXEC SACIWEB_CONSOLIDA_HDE ";
        public const string SACIWEB_CONSOLIDA_SALDOS = "EXEC SACIWEB_CONSOLIDA_SALDOS ";
        public const string SACIWEB_CONSOLIDA_MATERIAL = "EXEC SACIWEB_CONSOLIDA_MATERIAL ";
        public const string SACIWEB_CONSOLIDA_PRODUCTOS = "EXEC SACIWEB_CONSOLIDA_PRODUCTOS ";
        public const string SACIWEB_CONSOLIDA_ESTRUCTURAS = "EXEC SACIWEB_CONSOLIDA_ESTRUCTURAS ";

        public const string INFORME_MATERIALES_UTILIZADOS = "EXEC Informe_materiales_utilizados ";
        //Informe de estructuras Paginado
        public const string SACIWEB_INFORME_ESTRUCTURAS_PAGINADO = "EXEC SACIWEB_INFORME_ESTRUCTURAS_PAGINADO ";

        public const string SACIWEB_MS_EXPORTACIONES_PAGINADO = "EXEC SACIWEB_MS_EXPORTACIONES_PAGINADO ";
        public const string SACIWEB_MS_IMPORTACIONES_PAGINADO = "EXEC SACIWEB_MS_IMPORTACIONES_PAGINADO ";
        
        //Reporte de modulo de activo fijo
        public const string SACIWEB_INFORME_DE_MODULO_DE_ACTIVO_FIJO = "EXEC SACIWEB_INFORME_DE_MODULO_DE_ACTIVO_FIJO ";


    }


    public struct sp_Importaciones
    {
        public const string SACIWEB_MC_IMPO_AÑO_MES = "EXEC SACIWEB_MC_IMPO_AÑO_MES ";
        public const string SACIWEB_MC_IMPO_BUSCAR = "EXEC SACIWEB_MC_IMPO_BUSCAR ";
        public const string SACIWEB_SEL_CVE_PEDIMIENTO = "EXEC SACIWEB_SEL_CVE_PEDIMIENTO ";
        public const string SACEWEB_SEL_PEDIMENTOS = "EXEC SACIWEB_SEL_PEDIMENTOS_IMPO ";
        public const string SACIWEB_MC_IMPO_DATOS = "EXEC SACIWEB_MC_IMPO_DATOS ";
        public const string SACIWEB_MC_IMPO_PARTIDAS = "EXEC SACIWEB_MC_IMPO_PARTIDAS ";
        public const string SACIWEB_MC_IMPO_TOTALES = "EXEC SACIWEB_MC_IMPO_TOTALES ";
    }


    public struct sp_ActivoFijo
    {
        public const string SACIWEB_MC_AF_AÑO_MES = "EXEC SACIWEB_MC_AF_AÑO_MES ";
        public const string SACIWEB_MC_AF_BUSCAR = "EXEC SACIWEB_MC_AF_BUSCAR ";
        public const string SACIWEB_SEL_CVE_PEDIMIENTO = "EXEC SACIWEB_SEL_CVE_PEDIMIENTO ";
        public const string SACEWEB_SEL_PEDIMENTOS = "EXEC SACIWEB_SEL_PEDIMENTOS_IMPO ";
        public const string SACIWEB_MC_AF_DATOS = "EXEC SACIWEB_MC_AF_DATOS ";
        public const string SACIWEB_MC_AF_PARTIDAS = "EXEC SACIWEB_MC_AF_PARTIDAS ";
        public const string SACIWEB_MC_AF_TOTALES = "EXEC SACIWEB_MC_AF_TOTALES ";
    }



    public struct sp_Exportaciones
    {
        public const string SACIWEB_MC_EXPO_AÑO_MES = "EXEC SACIWEB_MC_EXPO_AÑO_MES ";
        public const string SACIWEB_MC_EXPO_BUSCAR = "EXEC SACIWEB_MC_EXPO_BUSCAR ";
        public const string SACIWEB_SEL_PEDIMENTOS_EXPO = "EXEC SACIWEB_SEL_PEDIMENTOS_EXPO  ";
        public const string SACIWEB_MC_EXPO_DATOS = "EXEC SACIWEB_MC_EXPO_DATOS  ";
        public const string SACIWEB_MC_EXPO_TOTALES = "EXEC SACIWEB_MC_EXPO_TOTALES  ";
        public const string SACIWEB_MC_EXPO_PARTIDAS = "EXEC SACIWEB_MC_EXPO_PARTIDAS  ";
    }

    public struct sp_CambioRegimen
    {
        public const string SACIWEB_MC_CR_AÑO_MES = "EXEC SACIWEB_MC_CR_AÑO_MES ";
        public const string SACIWEB_MC_CR_BUSCAR = "EXEC SACIWEB_MC_CR_BUSCAR ";
        public const string SACIWEB_SEL_PEDIMENTOS_EXPO = "EXEC SACIWEB_SEL_PEDIMENTOS_EXPO  ";
        public const string SACIWEB_MC_CR_DATOS = "EXEC SACIWEB_MC_CR_DATOS  ";
        public const string SACIWEB_MC_CR_TOTALES = "EXEC SACIWEB_MC_CR_TOTALES  ";
        public const string SACIWEB_MC_CR_PARTIDAS = "EXEC SACIWEB_MC_CR_PARTIDAS  ";



        public const string SACIWEB_MC_CREG_AÑO_MES = "SACIWEB_MC_CREG_AÑO_MES ";
        public const string SACIWEB_MC_CREG_BUSCAR = "SACIWEB_MC_CREG_BUSCAR ";
        public const string SACIWEB_MC_CREG_DATOS = "SACIWEB_MC_CREG_DATOS ";
        public const string SACIWEB_MC_CREG_PARTIDAS = "SACIWEB_MC_CREG_PARTIDAS ";
        public const string SACIWEB_MC_CREG_TOTALES = "SACIWEB_MC_CREG_TOTALES ";
    }



    public struct sp_DescargasDir
    {
        public const string SACIWEB_SEL_DOC_EXPO = "EXEC SACIWEB_SEL_DOC_EXPO ";
        public const string SACIWEB_SEL_PARTIDAS_EXPO = "EXEC SACIWEB_SEL_PARTIDAS_EXPO ";
        public const string SACIWEB_SEL_IMPORTACIONES = "EXEC SACIWEB_SEL_IMPORTACIONES ";
        public const string SACIWEB_INSERTA_DIRIGIDO = "EXEC SACIWEB_INSERTA_DIRIGIDO ";
        public const string DESCARGATODOSDIRIGIDOS = "EXEC DESCARGATODOSDIRIGIDOS ";
        public const string SACIWEB_INFORME_DESCARGOS_DIRIGIDOS = "EXEC SACIWEB_INFORME_DESCARGOS_DIRIGIDOS ";
    }



    public struct sp_Descargos
    {
        public const string DESCARGAXFECHA51 = "EXEC SACIWEB_CORREDESCARGA ";
        public const string SACIWEB_BUSCA_BLOQUEOS_DESCARGA = "EXEC SACIWEB_BUSCA_BLOQUEOS_DESCARGA ";
        public const string CONVIERTEDIRIGIDOPED = "EXEC CONVIERTEDIRIGIDOPED ";
        public const string BLOQUEA_DOCUMENTO = "EXEC BLOQUEA_DOCUMENTO ";
        public const string SACIWEB_SEL_PEDIMENTO_DESCARGA = "EXEC SACIWEB_SEL_PEDIMENTO_DESCARGA ";
        public const string SACIWEB_ESTADODESCARGA = "EXEC SACIWEB_ESTADODESCARGA ";
        public const string SACIWEB_INFORME_ERRORESDESCARGA = "EXEC SACIWEB_INFORME_ERRORESDESCARGA ";
    }
    //[LCG][14/01/2020][Se agrega estructura para Constancias de Transferencia]
    public struct sp_ConstanciasTransferencia
    {
        public const string SACIWEB_MC_CT_AÑO_MES = "EXEC SACIWEB_MC_CT_AÑO_MES ";
        public const string SACIWEB_MC_CT_BUSCAR = "EXEC SACIWEB_MC_CT_BUSCAR ";
        public const string SACIWEB_MC_CT_DATOS = "EXEC SACIWEB_MC_CT_DATOS ";
        public const string SACIWEB_MC_CT_PARTIDAS = "EXEC SACIWEB_MC_CT_PARTIDAS ";
    }
    //[LCG][20/03/2020][Se agrega estructura para Actas de destruccion]
    public struct sp_ActasDestruccion
    {
        public const string SACIWEB_MC_ACTASD_AÑO_MES = "EXEC SACIWEB_MC_ACTASD_AÑO_MES ";
        public const string SACIWEB_MC_ACTASD_BUSCAR = "EXEC SACIWEB_MC_ACTASD_BUSCAR ";
        public const string SACIWEB_MC_DESP_DATOS = "EXEC SACIWEB_MC_DESP_DATOS ";
        public const string SACIWEB_MC_DESP_PARTIDAS = "EXEC SACIWEB_MC_DESP_PARTIDAS ";
    }

    //[LCG][20/03/2020][Se agrega estructura para Transferencia Submaquila]
    public struct sp_TransferenciaSubmaqila
    {
        public const string SACIWEB_MC_TRANSUBMAQ_AÑO_MES = "EXEC SACIWEB_MC_TRANSUBMAQ_AÑO_MES ";
        public const string SACIWEB_MC_TRANSUBMAQ_BUSCAR = "EXEC SACIWEB_MC_TRANSUBMAQ_BUSCAR ";
        public const string SACIWEB_MC_TRANSUBMAQ_DATOS = "EXEC SACIWEB_MC_TRANSUBMAQ_DATOS ";
        public const string SACIWEB_MC_TRANSUBMAQ_PARTIDAS = "EXEC SACIWEB_MC_TRANSUBMAQ_PARTIDAS ";
    }



    public struct sp_CTM
    {
        public const string SACIWEB_IMPORTA_CTM = "SACIWEB_IMPORTA_CTM";
        public const string SACIWEB_INTERFACE_CARGA_CONSTANCIAS = "SACIWEB_INTERFACE_CARGA_CONSTANCIAS";
    }

    public struct sp_Interfaces
    {
        //Materiales
        public const string SACIWEB_IMPORTA_MATERIALES = "SACIWEB_IMPORTA_MATERIALES ";
        public const string SACIWEB_INTERFACE_CARGA_MATERIALES = "SACIWEB_INTERFACE_CARGA_MATERIALES ";
        //Productos
        public const string SACIWEB_IMPORTA_PRODUCTOS = "SACIWEB_IMPORTA_PRODUCTOS ";
        public const string SACIWEB_INTERFACE_CARGA_PRODUCTOS = "SACIWEB_INTERFACE_CARGA_PRODUCTOS ";
        //Clientes
        public const string SACIWEB_IMPORTA_CLIENTES = "SACIWEB_IMPORTA_CLIENTES ";
        public const string SACIWEB_INTERFACE_CARGA_CLIENTES = "SACIWEB_INTERFACE_CARGA_CLIENTES ";
        //Proveedores
        public const string SACIWEB_IMPORTA_PROVEEDORES = "SACIWEB_IMPORTA_PROVEEDORES ";
        public const string SACIWEB_INTERFACE_CARGA_PROVEEDORES = "SACIWEB_INTERFACE_CARGA_PROVEEDORES ";
        //Actas destruccion
        public const string SACIWEB_IMPORTA_ACTAS_DESTRUCCION = "SACIWEB_IMPORTA_ACTAS_DESTRUCCION ";
        public const string SACIWEB_INTERFACE_CARGA_ACTAS_DESTRUCCION = "SACIWEB_INTERFACE_CARGA_ACTAS_DESTRUCCION ";
        //fACTURA
        public const string SACIWEB_IMPORTA_FACTURAS = "SACIWEB_IMPORTA_FACTURAS ";
        public const string SACIWEB_INTERFACE_CARGA_FACTURAS = "SACIWEB_INTERFACE_CARGA_FACTURAS ";
        public const string SACIWEB_IMPORTA_FACTURAS_SERVICIOS = "SACIWEB_IMPORTA_FACTURAS_SERVICIOS ";
        public const string SACIWEB_INTERFACE_CARGA_FACTURAS_SERVICIOS = "SACIWEB_INTERFACE_CARGA_FACTURAS_SERVICIOS ";
        //Pedimentos
        public const string SACIWEB_IMPORTA_PEDIMENTOS = "SACIWEB_IMPORTA_PEDIMENTOS ";
        public const string SACIWEB_CARGAPEDIMENTOS = "EXEC SACIWEB_CARGAPEDIMENTOS ";
        //Ordenes Fabricacon
        public const string SACIWEB_IMPORTA_ORDENES_FAB = "SACIWEB_IMPORTA_ORDENES_FAB ";
        public const string SACIWEB_Crea_Ordenes = "SACIWEB_Crea_Ordenes ";
        //Catalogo de Activo Fijo
        public const string SACIWEB_IMPORTA_ACTIVOFIJO = "SACIWEB_IMPORTA_ACTIVOFIJO ";
        public const string SACIWEB_INTERFACE_CARGA_ACTIVOFIJO = "SACIWEB_INTERFACE_CARGA_ACTIVOFIJO ";
        //Dirigidos
        public const string SACIWEB_IMPORTA_DIRIGIDOS = "SACIWEB_IMPORTA_DIRIGIDOS ";
        public const string SACIWEB_INTERFACE_CARGA_DIRIGIDOS = "SACIWEB_INTERFACE_CARGA_DIRIGIDOS ";
    }

    public struct sp_Cartamateriales
    {
        public const string SACIWEB_INTERFACE_CREAESTRUCTURAS = "SACIWEB_INTERFACE_CREAESTRUCTURAS ";
        public const string SACIWEB_IMPORTA_CARTA_MATERIALES = "SACIWEB_IMPORTA_CARTA_MATERIALES ";
        public const string SACIWEB_IMPORTA_BOM = "SACIWEB_IMPORTA_BOM ";
        public const string SP_PROCESA_LAYOUT_BOM = "SP_PROCESA_CARGA_LAYOUT_ORIGEN ";

    }
    public struct Informes_Anexo_31
    {
        public const string SACIWEB_CAT_PERIODO_A31 = "EXEC SACIWEB_CAT_PERIODO_A31 ";
        public const string SACIWEB_INFORME_A31 = "EXEC SACIWEB_INFORME_A31 ";
        public const string SACIWEB_CAT_DESTINATARIO_A31 = "EXEC SACIWEB_CAT_DESTINATARIO_A31 ";
        public const string SACIWEB_GUARDA_A31 = "EXEC SACIWEB_GUARDA_A31 ";
        public const string SACIWEB_GUARDA_A31_CONSOLIDADO = "EXEC SACIWEB_GUARDA_A31_CONSOLIDADO ";
        public const string SPWEB_A30_InformeEntradas = "EXEC SPWEB_A30_InformeEntradas ";
        public const string INSERTAFALTANTESA31 = "EXEC INSERTAFALTANTESA31 ";
        public const string SACIWEB_PERIODOS = "EXEC SACIWEB_PERIODOS ";
        public const string SACIWEB_SEL_CVEPEDIMENTO = "EXEC SACIWEB_SEL_CVEPEDIMENTO ";
        public const string SACIWEB_SEL_DATOS_PEDIMENTOS = "EXEC SACIWEB_SEL_DATOS_PEDIMENTOS ";
        public const string SACIWEB_SEL_DESCARGA = "EXEC SACIWEB_SEL_DESCARGA ";
        public const string SACIWEB_ANALISIS_DESCARGA = "EXEC SACIWEB_ANALISIS_DESCARGA_ANEXO30 ";
        public const string SACIWEB_COMPARATIVA_DIFERENCIAS = "EXEC SACIWEB_COMPARATIVA_DIFERENCIAS ";
        public const string SACIWEB_ANALISISPERIODO = "EXEC SACIWEB_ANALISISPERIODO ";
        public const string SACIWEB_VENCIMIENTOS = "EXEC SACIWEB_VENCIMIENTOS ";
        public const string DESCARGAS_A31 = "EXEC DESCARGAS_A31 ";

    }

    public struct sp_Ajuste_Anual
    {
        public const string SACIWEB_AANUAL_IMPORTA_VENTAS = "SACIWEB_AANUAL_IMPORTA_VENTAS ";
        public const string SACIWEB_AANUAL_SELANIO_CTM = "EXEC SACIWEB_AANUAL_SELANIO_CTM ";
        public const string SACIWEB_AANUAL_CTM = "EXEC SACIWEB_AANUAL_CTM ";
        public const string SACIWEB_AANUAL_VENTAS = "EXEC SACIWEB_AANUAL_VENTAS ";
        public const string SACIWEB_AANUAL_LIMPIAR_TODO = "EXEC SACIWEB_AANUAL_LIMPIAR_TODO";
        public const string SACIWEB_AANUAL_INV_FINAL = "SACIWEB_AANUAL_INV_FINAL ";
        public const string SACIWEB_AANUAL_INV_INICIAL = "SACIWEB_AANUAL_INV_INICIAL ";
        public const string SACIWEB_AANUAL_CARGA_CTM = "EXEC SACIWEB_AANUAL_CARGA_CTM ";
    }

    public struct sp_Facturacion
    {
        public const string SACIWEB_MC_FACTURACION_ANIO_MES = "EXEC SACIWEB_MC_FACTURACION_ANIO_MES ";
        public const string SACIWEB_MC_FACTURAS_BUSCAR = "EXEC SACIWEB_MC_FACTURAS_BUSCAR ";
        public const string SACIWEB_MC_FACT_DATOS = "EXEC SACIWEB_MC_FACT_DATOS ";
        public const string SACIWEB_MC_FACT_PARTIDAS = "EXEC SACIWEB_MC_FACT_PARTIDAS ";
        public const string SACIWEB_MC_FACTURASERV_ANIO_MES = "EXEC SACIWEB_MC_FACTURASERV_ANIO_MES ";
        public const string SACIWEB_MC_FACTURASERV_BUSCAR = "EXEC SACIWEB_MC_FACTURASERV_BUSCAR ";
        public const string SACIWEB_MC_FACTURASERV_DATOS = "EXEC SACIWEB_MC_FACTURASERV_DATOS ";
        public const string SACIWEB_MC_FACTSERV_PARTIDAS = "EXEC SACIWEB_MC_FACTURASERV_PARTIDAS ";
    }

    public struct sp_Archivos
    {
        //public const string DSWEB_FILES = "DSWEB_FILES";
        //public const string SP_FILES = "EXEC DSWEB_FILES ";
        //public const string DSWEB_PROCESADSZIP = "EXEC DSWEB_PROCESADSZIP ";
        public const string SACIWEB_PROC_ARCHIVOS_DESCARGA = "EXEC SACIWEB_PROC_ARCHIVOS_DESCARGA ";
        public const string SACIWEB_PROC1_ARCHIVOS_DESCARGA = "SACIWEB_PROC_ARCHIVOS_DESCARGA";
        public const string SACIWEB_PROCESA_ARCHIVO_DESCARGA = "EXEC SACIWEB_PROCESA_ARCHIVO_DESCARGA ";
        //public const string SP_PROCESADOS_VER = "EXEC SP_PROCESADOS_VER ";

        public const string SACIWEB_INTERFACE_PROCESOS = "SACIWEB_INTERFACE_PROCESOS ";
        public const string SACIWEB_SELECT_INTERFACE_ARCHIVO = "EXEC SACIWEB_SELECT_INTERFACE_ARCHIVO ";
        

        public const string SP_CARGA_BOM = "EXEC SP_CARGA_BOM ";
        public const string SP_CARGA_TXT_RM = "EXEC SP_CARGA_TXT_RM ";
        public const string SP_CARGA_TXT_FG = "EXEC SP_CARGA_TXT_FG ";
        public const string SP_CARGA_IMPO = "EXEC SP_CARGA_IMPO ";
        public const string SP_CARGA_EXPO = "EXEC SP_CARGA_EXPO "; 
    }

    public struct sp_Analisiss
    {
        public const string SP_ANALISIS_ESTRUCTURA = "SP_ANALISIS_ESTRUCTURA ";

    }
}


