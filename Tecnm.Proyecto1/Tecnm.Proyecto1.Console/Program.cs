using System;
using Microsoft.VisualBasic.CompilerServices;
using Tecnm.Proyecto1.Core.Entities;
using Tecnm.Proyecto1.Core.Managers;
using Tecnm.Proyecto1.Core.Services;
namespace Tecnm.Proyecto1.Console;

public class Program
{
    public static void Main(string[]args)
    {
        System.Console.Write("Introduce el nombre de usuario: ");
        string? name = System.Console.ReadLine();
        bool salir = false;
        List<Usuario> ingresos = new List<Usuario>();
        var service = new TransaccionesService();
        var managers = new TransaccionesManager(service);
        //var saldo = 0;
        if (name != string.Empty)
        {
            System.Console.WriteLine($"Bienvenido {name}");
            while (!salir)
            {
                System.Console.WriteLine("¿Qué operación desea realizar?");
                System.Console.Write("1 Ingresos, 2 Retiros, 3 Edo. de cuenta, 4 Presupuestos, 5 Salir: ");
                var id = int.Parse(System.Console.ReadLine() ?? throw new InvalidOperationException());
                var add = new Usuario { Opcion = id, nom_usuario = name};
                switch (id)
                {
                    case 1:
                        Usuario u = managers.setIngresoUsuario(add);
                        ingresos.Add(u);
                        break;
                    case 2:
                        if (ingresos.Count > 0)
                        {
                            Transacciones t = managers.getSaldo(ingresos);
                            Usuario user = managers.setRetiroUsuario(t, add);
                            ingresos.Add(user);
                        }
                        break;
                    case 3:
                        if (ingresos.Count > 0)
                        {
                            Transacciones t = managers.getSaldo(ingresos);
                            System.Console.WriteLine("Saldo total:"+t.total);
                        }
                        break;
                    case 4:
                        break;
                    case 5:
                        salir = true;
                        break;
                    default:
                        System.Console.WriteLine("Seleccione una opcion del menu");
                        break;
                }
            }
        }
    }
}
