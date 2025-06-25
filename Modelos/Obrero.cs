
namespace Reparar.Modelos {
    public enum Categoria { Aprendiz, MedioOficial, Oficial }
    public enum Oficio { Albañil, Pintor, Plomero, Yesero, Electricista, Otro }

    public class Obrero : Empleado {
        public Oficio Oficio { get; set; }
        public Categoria Categoria { get; set; }
        public static decimal MontoReferencia { get; set; }

        public override decimal CalcularHaberMensual() =>
            Categoria switch {
                Categoria.Oficial => MontoReferencia,
                Categoria.MedioOficial => MontoReferencia * 0.65m,
                Categoria.Aprendiz => MontoReferencia * 0.25m,
                _ => 0
            };
    }
}
