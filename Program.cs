
using Reparar.Controlador;
using Reparar.Interfaz;

class Program {
    static void Main() {
        Sistema sistema = new Sistema();
        ConsolaUI ui = new ConsolaUI(sistema);
        ui.Iniciar();
    }
}
