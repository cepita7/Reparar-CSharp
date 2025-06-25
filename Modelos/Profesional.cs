
namespace Reparar.Modelos {
    public class Profesional : Empleado {
        public string Titulo { get; set; }
        public ulong Matricula { get; set; }
        public string ConsejoProfesional { get; set; }
        public decimal PorcentajeAumento { get; set; }
        public bool SupervisaObra { get; set; }

        public static decimal MontoReferencia { get; set; }
        public static decimal CanonUniversal { get; set; }

        public override decimal CalcularHaberMensual() {
            decimal haber = MontoReferencia * (1 + PorcentajeAumento / 100);
            if (SupervisaObra)
                haber += CanonUniversal;
            return haber;
        }
    }
}
