using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using App.core;
using App.Core;

namespace App.Data
{
    public static class PersistenciaTrabajadores
    {
        private const string ARCHIVO_TRABAJADOR = "trabajador.xml";
        private const string ETIQUETA_TRABAJADORES = "trabajadores";

        public static void GuardarTrabajadores(List<Trabajador> trabajadores)
        {
            try
            {
                XElement trabajadoresElement = new(ETIQUETA_TRABAJADORES);
                foreach (Trabajador empleado in trabajadores)
                    trabajadoresElement.Add(empleado.ToXML());
                trabajadoresElement.Save(ARCHIVO_TRABAJADOR);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar trabajador: {ex.Message}");
            }
        }

        public static List<Trabajador> CargarTrabajadores()
        {
            var lista = new List<Trabajador>();

            if (!File.Exists(ARCHIVO_TRABAJADOR))
            {
                Console.WriteLine("No se encontró fichero de trabajadores.");
                return lista;
            }

            try
            {
                XElement trabajadores = XDocument.Load(ARCHIVO_TRABAJADOR).Descendants(ETIQUETA_TRABAJADORES).FirstOrDefault(new XElement(ETIQUETA_TRABAJADORES));
                foreach (Trabajador? p in trabajadores.Descendants(Usuario.ETIQUETA_USUARIO).Select(nodo => Trabajador.FromXML(nodo)))
                {
                    if (p != null)
                    {
                        lista.Add(p);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar trabajador: {ex.Message}");
            }

            return lista;
        }
    }
}