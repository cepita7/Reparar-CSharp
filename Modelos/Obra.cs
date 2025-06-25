
namespace Reparar.Modelos {
    public class Obra {
        public string Codigo { get; set; }
        public string Direccion { get; set; }
        public Profesional Supervisor { get; set; }
        public List<Obrero> ObrerosAsignados { get; set; } = new();
    }
}
