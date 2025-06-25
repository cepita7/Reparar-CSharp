
using Reparar.Controlador;
using Reparar.Modelos;

namespace Reparar.Interfaz {
    public class ConsolaUI {
        private Sistema sistema;

        public ConsolaUI(Sistema sistema) => this.sistema = sistema;

        public void Iniciar() {
            int opcion;
            do {
                Console.WriteLine("\n--- MENU ---");
                Console.WriteLine("1. Establecer sueldos");
                Console.WriteLine("2. Registrar empleado");
                Console.WriteLine("3. Listar empleados");
                Console.WriteLine("4. Registrar obra");
                Console.WriteLine("5. Cambiar supervisor");
                Console.WriteLine("6. Asignar obrero a obra");
                Console.WriteLine("7. Listar empleados por obra");
                Console.WriteLine("8. Eliminar profesional");
                Console.WriteLine("0. Salir");
                Console.Write("Opcion: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion) {
                    case 1: EstablecerSueldos(); break;
                    case 2: RegistrarEmpleado(); break;
                    case 3: ListarEmpleados(); break;
                    case 4: RegistrarObra(); break;
                    case 5: CambiarSupervisor(); break;
                    case 6: AsignarObrero(); break;
                    case 7: ListarPorObra(); break;
                    case 8: EliminarProfesional(); break;
                }
            } while (opcion != 0);
        }

        private void EstablecerSueldos() {
            Console.Write("Monto referencia obrero: ");
            sistema.MontoReferencia = decimal.Parse(Console.ReadLine());
            Obrero.MontoReferencia = sistema.MontoReferencia;
            Profesional.MontoReferencia = sistema.MontoReferencia;
            Console.Write("Canon universal profesional: ");
            sistema.CanonUniversal = decimal.Parse(Console.ReadLine());
            Profesional.CanonUniversal = sistema.CanonUniversal;
        }

        private void RegistrarEmpleado() {
            Console.Write("Tipo (1=Obrero, 2=Profesional): ");
            int tipo = int.Parse(Console.ReadLine());

            Console.Write("Legajo: "); uint legajo = uint.Parse(Console.ReadLine());
            Console.Write("Apellido y Nombre: "); string nom = Console.ReadLine();

            if (tipo == 1) {
                Console.Write("Oficio: "); string oficio = Console.ReadLine();
                Console.Write("Categoria (0=Aprendiz, 1=MedioOficial, 2=Oficial): ");
                var cat = (Categoria)int.Parse(Console.ReadLine());
                sistema.RegistrarEmpleado(new Obrero {
                    Legajo = legajo,
                    ApellidoNombre = nom,
                    Oficio = Enum.Parse<Oficio>(oficio, true),
                    Categoria = cat
                });
            } else {
                Console.Write("Titulo: "); string titulo = Console.ReadLine();
                Console.Write("Matricula: "); ulong mat = ulong.Parse(Console.ReadLine());
                Console.Write("Consejo: "); string consejo = Console.ReadLine();
                Console.Write("% Aumento: "); decimal aumento = decimal.Parse(Console.ReadLine());
                sistema.RegistrarEmpleado(new Profesional {
                    Legajo = legajo,
                    ApellidoNombre = nom,
                    Titulo = titulo,
                    Matricula = mat,
                    ConsejoProfesional = consejo,
                    PorcentajeAumento = aumento
                });
            }
        }

        private void ListarEmpleados() {
            foreach (var e in sistema.ListarEmpleadosOrdenados())
                Console.WriteLine($"{e.Legajo} - {e.ApellidoNombre} - ${e.CalcularHaberMensual():0.00}");
        }

        private void RegistrarObra() {
            Console.Write("Codigo: "); string codigo = Console.ReadLine();
            Console.Write("Direccion: "); string dir = Console.ReadLine();
            Console.Write("Legajo supervisor: "); uint legSup = uint.Parse(Console.ReadLine());
            var supervisor = sistema.Empleados.OfType<Profesional>().FirstOrDefault(p => p.Legajo == legSup);
            if (supervisor != null)
                sistema.RegistrarObra(new Obra { Codigo = codigo, Direccion = dir, Supervisor = supervisor });
        }

        private void CambiarSupervisor() {
            Console.Write("Codigo obra: "); string cod = Console.ReadLine();
            Console.Write("Legajo nuevo supervisor: "); uint legSup = uint.Parse(Console.ReadLine());
            var nuevo = sistema.Empleados.OfType<Profesional>().FirstOrDefault(p => p.Legajo == legSup);
            if (nuevo != null)
                sistema.ModificarSupervisor(cod, nuevo);
        }

        private void AsignarObrero() {
            Console.Write("Codigo obra: "); string cod = Console.ReadLine();
            Console.Write("Legajo obrero: "); uint leg = uint.Parse(Console.ReadLine());
            var obrero = sistema.Empleados.OfType<Obrero>().FirstOrDefault(o => o.Legajo == leg);
            if (obrero != null && sistema.AsignarObrero(cod, obrero))
                Console.WriteLine("Asignado");
            else
                Console.WriteLine("Error al asignar");
        }

        private void ListarPorObra() {
            Console.Write("Codigo obra: "); string cod = Console.ReadLine();
            foreach (var e in sistema.EmpleadosPorObra(cod))
                Console.WriteLine($"{e.Legajo} - {e.ApellidoNombre} - ${e.CalcularHaberMensual():0.00}");
        }

        private void EliminarProfesional() {
            Console.Write("Legajo profesional: ");
            uint leg = uint.Parse(Console.ReadLine());
            if (sistema.EliminarProfesional(leg))
                Console.WriteLine("Eliminado");
            else
                Console.WriteLine("No se pudo eliminar");
        }
    }
}
