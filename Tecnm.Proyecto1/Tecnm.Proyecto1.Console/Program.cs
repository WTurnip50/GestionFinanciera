using System;
using System.ComponentModel.Design;
using Microsoft.VisualBasic.CompilerServices;
using Tecnm.Proyecto1.Core.Entities;
using Tecnm.Proyecto1.Core.Enums;
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
        //Prueba
        List<Usuario> retiros = new List<Usuario>();
        var service = new TransaccionesService();
        var managers = new TransaccionesManager(service);
        var select = 0;
        //var saldo = 0;
        if (name != string.Empty)
        {
            System.Console.WriteLine($"Bienvenido {name}");
            while (!salir)
            {
                System.Console.WriteLine("¿Qué operación desea realizar?");
                System.Console.Write("1 Ingresos, 2 Retiros, 3 Edo. de cuenta, 4 Presupuestos, 5 Salir: ");
                var id = int.Parse(System.Console.ReadLine() ?? throw new InvalidOperationException());
                var add = new Usuario { Opcion = id, Nom_usuario = name};
                switch (id)
                {
                    case 1:
                        Usuario u = managers.setIngresoUsuario(add);
                        var ing = u;
                        select = Categoria();
                        u = managers.setCategoriaUsuario(ing, id);
                        ingresos.Add(u);
                        break;
                    case 2:
                        if (ingresos.Count > 0)
                        {
                            Transacciones t = managers.getSaldo(ingresos);
                            Usuario user = managers.setRetiroUsuario(t, add);
                            select = Categoria();
                            var ret = user;
                            user = managers.setCategoriaUsuario(ret, select);
                            ingresos.Add(user);
                        }
                        else
                        {
                            System.Console.WriteLine("Aún no haz hecho ningun movimiento.");
                            System.Console.WriteLine("===================================");
                        }
                        break;
                    case 3:
                        if (ingresos.Count > 0)
                        {
                            Transacciones t = managers.getSaldo(ingresos);
                            System.Console.WriteLine("===RESUMEN DEL ESTADO FINANCIERO===");
                            System.Console.WriteLine("Ingresos: "+t.Ingresos);
                            System.Console.WriteLine("Retiros: "+t.Retiros);
                            System.Console.WriteLine("Saldo total :"+t.Total);
                            System.Console.WriteLine("===Ingresos===");
                            foreach (var ingreso in ingresos)
                            {
                                if (ingreso.Opcion == 1)
                                {
                                    System.Console.WriteLine($"Ingreso: {ingreso.Ingresos}, Concepto: {ingreso.Concepto}, Categoria: {ingreso.categoria}");
                                }
                            }
                            System.Console.WriteLine("===Retiros===");
                            foreach (var retiro in ingresos)
                            {
                                if (retiro.Opcion == 2)
                                {
                                    System.Console.WriteLine($"Retiro: {retiro.Retiros}, Concepto: {retiro.Concepto}, Categoria: {retiro.categoria}");
                                }
                            }
                        }
                        else
                        {
                            System.Console.WriteLine("Aún no haz hecho ningún movimiento.");
                            System.Console.WriteLine("===================================");
                        }
                        break;
                    case 4:
                        break;
                    case 5:
                        salir = true;
                        break;
                    default:
                        System.Console.WriteLine("Seleccione una opción del menú.");
                        break;
                }
            }
        }
    }
    public static int Categoria()
    {
        var categorias = Enum.GetValues(typeof(CategoriasType));
        int i = 0;
        int select = 0;
        bool flag = false;
        while (!flag)
        {
            System.Console.WriteLine("Elige una categoria: ");
            foreach (var item in categorias)
            {
                System.Console.Write($"{i} {item}, ");
                i++;
            }

            select = int.Parse(System.Console.ReadLine() ?? throw new InvalidOperationException());
            if (select != null)
            {
                System.Console.WriteLine("Transacción realizada con éxito.");
                System.Console.WriteLine("============================");
                flag = true;
            }
        }

        return select;
    }
}
