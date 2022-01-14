using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace Punto_de_venta.ConexionDt
{
    class Desencryptacion
    {
        static private AES aes = new AES();
        static public string CnString;
        static string dbcnString;
        //fase 2 de encryptacion mas segura, se pone lo que uno quieres de manera que quede bien encriptada
        static public string appPwdUnique = "Jojama01.mono08.BASEJOJAMA.Hola_Mundo";

        //CADENA DE CONEXION
        public static object checkServer()
        {//desencrypta el archivo xml que esta en debug
            XmlDocument doc = new XmlDocument();
            doc.Load("ConnectionString.xml");
            XmlElement root = doc.DocumentElement;
            dbcnString = root.Attributes[0].Value;
            CnString = (aes.Decrypt(dbcnString, appPwdUnique, int.Parse("256")));
            return CnString;

        }
        //public static object checkServerWEB()
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load("ConnectionString.xml");
        //    XmlElement root = doc.DocumentElement;
        //    dbcnString = root.Attributes[0].Value;
        //    CnString = (aes.Decrypt(dbcnString, appPwdUnique, int.Parse("256")));
        //    return CnString;

        //}
        internal class label
        {

        }
        public static object UsuariosEncryp()
        {
            XmlDocument doc = new XmlDocument();
            label root = new label();

            dbcnString = root.ToString();
            CnString = (aes.Decrypt(dbcnString, appPwdUnique, int.Parse("256")));
            return CnString;

        }
    }
}
