using System.Runtime.InteropServices;
using Tecnm.Proyecto1.Core.Entities;
using Tecnm.Proyecto1.Core.Enums;
using Tecnm.Proyecto1.Core.Services.Interfaces;

namespace Tecnm.Proyecto1.Core.Services;

public class TransaccionesService : ITransaccionesService
{
    public Usuario Ingreso(Usuario usuario)
    {
        var user = new Usuario();
        while (true)
        {
            System.Console.Write("Ingrese el monto a ingresar: ");
            var ingreso = 0.0;
            var validar1 = double.TryParse(System.Console.ReadLine(), out ingreso);
            if (validar1)
            {
                System.Console.Write("Introduzca el concepto de la operación: ");
                var concepto = System.Console.ReadLine();
                user.Ingresos = ingreso;
                user.Concepto = concepto;
                user.Opcion = usuario.Opcion;
                user.Nom_usuario = usuario.Nom_usuario;
                break;
            }
            System.Console.Write("Introduzca una cantidad valida.");
        }
       
        return user;
    }

    public Usuario categoria(Usuario usuario, int id)
    {
        usuario.categoria = id switch
        {
            0 => CategoriasType.Salario,
            1 => CategoriasType.Deposito,
            2 => CategoriasType.Efectivo,
            3 => CategoriasType.Transaccion,
            4 => CategoriasType.Comida,
            5 => CategoriasType.Transporte,
            6 => CategoriasType.Medicina,
            7 => CategoriasType.Hogar,
            8 => CategoriasType.Retiro,
            9 => CategoriasType.PagoServicio,
            10 => CategoriasType.Otros,
            _ => usuario.categoria
        };
        return usuario;
    }

    public Transacciones Saldo(List<Usuario> usuarios)
    {
        double saldo = 0;
        double ingresos = 0;
        double retiros = 0;
        foreach (var item in usuarios)
        {
            switch (item.Opcion)
            {
                case 1:
                    ingresos = ingresos + item.Ingresos;
                    break;
                case 2:
                    retiros = retiros + item.Retiros;
                    break;
            }
        }
        saldo = ingresos - retiros;
        var t = new Transacciones()
        {
            Total = saldo,
            Ingresos = ingresos,
            Retiros = retiros
        };
        return t;
    }

    public Usuario Retiro(Transacciones transacciones, Usuario usuario)
    {
        var saldo = transacciones.Total;
        var user = new Usuario();
        
        var flag = false;
        while (!flag)
        {
            System.Console.Write("Ingrese el monto a retirar: ");
            double retiro = 0;
            var validar = double.TryParse(System.Console.ReadLine(),out retiro);
            if (validar)
            {
                if (retiro > saldo)
                {
                    Console.WriteLine(" No posee el saldo suficiente");
                }
                else
                {
                    Console.Write("Introduzca el concepto de la operación: ");
                    var concepto = Console.ReadLine();
                    user.Retiros = retiro;
                    user.Concepto = concepto;
                    user.Opcion = usuario.Opcion;
                    user.Nom_usuario = usuario.Nom_usuario;
                    flag = true;
                }
            }
            else
            {
                Console.WriteLine("Valor incorrecto, favor de introducir una cantidad en numeros.");
            }
        }
        return user;
    }

    public Transacciones Meta(Transacciones transacciones)
    {
        double cantidad;
        switch (transacciones.tipo)
        {
            case 1:
                while (true)
                {
                    System.Console.Write("Introduzca la cantidad que desea almacenar como meta:");
                    cantidad = 0.0;
                    var validar = double.TryParse(System.Console.ReadLine(), out cantidad);
                    if (validar)
                    {
                        if (cantidad > 0)
                        {
                            transacciones.Total = cantidad;
                            transacciones.activo = true;
                            break;
                        }
                        System.Console.Write("Introduzca una cantidad valida");
                        System.Console.WriteLine("============================");
                    }
                }
                break;
            case 2:
                while (true)
                {
                    System.Console.Write("Introduzca la cantidad que desea almacenar como presupuesto:");
                    cantidad = 0.0;
                    var validar = double.TryParse(System.Console.ReadLine(), out cantidad);
                    if (validar)
                    {
                        if (cantidad > 0)
                        {
                            transacciones.Total = cantidad;
                            transacciones.activo = true;
                            break;
                        }
                        System.Console.Write("Introduzca una cantidad valida");
                        System.Console.WriteLine("============================");

                    }
                }
                break;
        }
        return transacciones;
    }
}