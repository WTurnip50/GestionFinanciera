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
        System.Console.Write("Ingrese el monto a ingresar: ");
        var ingreso = double.Parse(System.Console.ReadLine() ??
                                   throw new InvalidOperationException());
        System.Console.Write("Introduzca el concepto de la operación: ");
        var concepto = System.Console.ReadLine();
        user.Ingresos = ingreso;
        user.Concepto = concepto;
        user.Opcion = usuario.Opcion;
        user.Nom_usuario = usuario.Nom_usuario;
        return user;
    }

    public Usuario categoria(Usuario usuario, int id)
    {
        switch (id)
        {
            case 1:
                usuario.categoria = CategoriasType.Salario;
                break;
            case 2:
                usuario.categoria = CategoriasType.Deposito;
                break;
            case 3:
                usuario.categoria = CategoriasType.Efectivo;
                break;
            case 4:
                usuario.categoria = CategoriasType.Transaccion;
                break;
            case 5:
                usuario.categoria = CategoriasType.Comida;
                break;
            case 6:
                usuario.categoria = CategoriasType.Transporte;
                break;
            case 7:
                usuario.categoria = CategoriasType.Medicina;
                break;
            case 8:
                usuario.categoria = CategoriasType.Hogar;
                break;
            case 9:
                usuario.categoria = CategoriasType.Retiro;
                break;
            case 10:
                usuario.categoria = CategoriasType.PagoServicio;
                break;
        }
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
            var retiro = double.Parse(System.Console.ReadLine() ??
                                       throw new InvalidOperationException());
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
        return user;
    }
}