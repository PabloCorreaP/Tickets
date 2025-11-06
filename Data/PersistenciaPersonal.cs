using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using PracticaDIA.UI.Core.Personal;

namespace PracticaDIA.UI.Data
{
    public static class PersistenciaPersonal
    {
        private const string ARCHIVO_PERSONAL = "personal.xml";

        public static void GuardarPersonal(List<Trabajador> trabajadores)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Trabajador>));
                using (FileStream fs = new FileStream(ARCHIVO_PERSONAL, FileMode.Create))
                {
                    serializer.Serialize(fs, trabajadores);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar personal: {ex.Message}");
            }
        }

        public static List<Trabajador> CargarPersonal()
        {
            var lista = new List<Trabajador>();

            if (!File.Exists(ARCHIVO_PERSONAL))
            {
                Console.WriteLine("No se encontró fichero de personal.");
                return lista;
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Trabajador>));
                using (FileStream fs = new FileStream(ARCHIVO_PERSONAL, FileMode.Open))
                {
                    var datos = (List<Trabajador>?)serializer.Deserialize(fs);
                    if (datos != null)
                        lista = datos;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar personal: {ex.Message}");
            }

            return lista;
        }
    }
}
