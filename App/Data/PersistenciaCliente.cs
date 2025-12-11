using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using App.core;

namespace App.Data
{
    public static class PersistenciaClientes
    {
        private const string ARCHIVO_USUARIO = "usuario.xml";
        private const string ETIQUETA_USUARIOS = "usuarios";

        public static void GuardarClientes(List<Cliente> usuarios)
        {
            try
            {
                XElement usuariosElement = new(ETIQUETA_USUARIOS);
                foreach (Usuario usuario in usuarios)
                    usuariosElement.Add(usuario.ToXML());
                usuariosElement.Save(ARCHIVO_USUARIO);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar usuario: {ex.Message}");
            }
        }

        public static List<Cliente> CargarClientes()
        {
            var lista = new List<Cliente>();

            if (!File.Exists(ARCHIVO_USUARIO))
            {
                Console.WriteLine("No se encontró fichero de usuarios.");
                return lista;
            }

            try
            {
                XElement usuarios = XDocument.Load(ARCHIVO_USUARIO).Descendants(ETIQUETA_USUARIOS).FirstOrDefault(new XElement(ETIQUETA_USUARIOS));
                foreach (Cliente? p in usuarios.Descendants(Usuario.ETIQUETA_USUARIO).Select(nodo => Usuario.FromXML(nodo)))
                {
                    if (p != null)
                    {
                        lista.Add(p);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar usuario: {ex.Message}");
            }

            return lista;
        }
    }
}