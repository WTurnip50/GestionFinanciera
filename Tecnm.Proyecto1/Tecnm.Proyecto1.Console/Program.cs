using System;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Security.Principal;
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
        List<Transacciones> metas = new List<Transacciones>();
        var service = new TransaccionesService();
        var managers = new TransaccionesManager(service);
        var meta = new Transacciones();
        meta.Total = 0;
        var presupuesto = new Transacciones();
        presupuesto.Total = 0;
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
                        u = managers.setCategoriaUsuario(ing, select);
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
                            if (user.Retiros > presupuesto.Total)
                            {
                                System.Console.WriteLine("Se ha superado el presupuesto establecido");
                                presupuesto.Total = 0;
                            }
                            else
                            {
                                presupuesto.Total = presupuesto.Total - user.Retiros;
                            }
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
                        bool access = false;
                        while (!access)
                        {
                            System.Console.WriteLine("1. Meta, 2.Presupuesto");
                            int op = int.Parse(System.Console.ReadLine());
                            switch (op)
                            {
                             case 1:
                                 var obj = meta;
                                 obj.tipo = 1;
                                 if (meta.Total == 0)
                                 {
                                     meta = managers.Set_Meta(obj);
                                     access = true;
                                 }
                                 else
                                 {
                                     var saldo = managers.getSaldo(ingresos);
                                     var avance = (saldo.Total * 100) / meta.Total;
                                     if (avance >= 100)
                                     {
                                         System.Console.WriteLine("Su meta ya fue completada");
                                         meta.Total = 0;
                                         access = true;
                                     }
                                     else
                                     {
                                         System.Console.WriteLine($"Lleva completado un {avance}%");
                                         access = true;
                                     }
                                 }
                                 break;
                             case 2:
                                 var obj2 = presupuesto;
                                 if (presupuesto.Total == 0)
                                 {
                                     obj2.tipo = 2;
                                     presupuesto = managers.Set_Meta(obj2);
                                     access = true;
                                 }
                                 else
                                 {
                                     System.Console.WriteLine($"Aun le falta por usar: {obj2.Total} ");
                                     access = true;
                                 }
                                 break;
                            }
                        }
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
