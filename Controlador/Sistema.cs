
using Reparar.Modelos;
using System.Linq;

namespace Reparar.Controlador {
    public class Sistema {
        public List<Empleado> Empleados { get; private set; } = new();
        public List<Obra> Obras { get; private set; } = new();

        public decimal MontoReferencia { get; set; }
        public decimal CanonUniversal { get; set; }

        public bool RegistrarEmpleado(Empleado e) {
            if (Empleados.Any(x => x.Legajo == e.Legajo)) return false;
            Empleados.Add(e);
            return true;
        }

        public List<Empleado> ListarEmpleadosOrdenados() =>
            Empleados.OrderBy(e => e.ApellidoNombre).ToList();

        public bool RegistrarObra(Obra o) {
            if (Obras.Any(x => x.Codigo == o.Codigo)) return false;
            Obras.Add(o);
            o.Supervisor.SupervisaObra = true;
            return true;
        }

        public bool ModificarSupervisor(string codigoObra, Profesional nuevo) {
            var obra = Obras.FirstOrDefault(o => o.Codigo == codigoObra);
            if (obra == null) return false;
            obra.Supervisor.SupervisaObra = false;
            obra.Supervisor = nuevo;
            nuevo.SupervisaObra = true;
            return true;
        }

        public bool AsignarObrero(string codigoObra, Obrero obrero) {
            if (Obras.Any(o => o.ObrerosAsignados.Contains(obrero))) return false;
            var obra = Obras.FirstOrDefault(o => o.Codigo == codigoObra);
            if (obra == null) return false;
            obra.ObrerosAsignados.Add(obrero);
            return true;
        }

        public List<Empleado> EmpleadosPorObra(string codigoObra) {
            var obra = Obras.FirstOrDefault(o => o.Codigo == codigoObra);
            if (obra == null) return new();
            var lista = new List<Empleado> { obra.Supervisor };
            lista.AddRange(obra.ObrerosAsignados);
            return lista;
        }

        public bool EliminarProfesional(uint legajo) {
            var profesional = Empleados.OfType<Profesional>().FirstOrDefault(p => p.Legajo == legajo);
            if (profesional == null || Obras.Any(o => o.Supervisor.Legajo == legajo)) return false;
            Empleados.Remove(profesional);
            return true;
        }
    }
}
