
namespace Reparar.Modelos {
    public abstract class Empleado {
        public uint Legajo { get; set; }
        public string ApellidoNombre { get; set; }
        public abstract decimal CalcularHaberMensual();
    }
}
