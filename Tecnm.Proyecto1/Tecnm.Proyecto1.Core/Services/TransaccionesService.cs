using Tecnm.Proyecto1.Core.Entities;
using Tecnm.Proyecto1.Core.Services.Interfaces;

namespace Tecnm.Proyecto1.Core.Services;

public class TransaccionesService : ITransaccionesService
{
    public Usuario Ingreso(Usuario usuario)
    {
        var user = new Usuario();
        System.Console.Write("Introduzca la cantidad deseada: ");
        var ingreso = double.Parse(System.Console.ReadLine() ??
                                   throw new InvalidOperationException());
        System.Console.Write("Introduzca el concepto de la operación: ");
        var concepto = System.Console.ReadLine();
        System.Console.WriteLine("Transacción realizada con éxito.");
        System.Console.WriteLine("============================");
        
        user.Ingresos = ingreso;
        user.Concepto = concepto;
        user.Opcion = usuario.Opcion;
        user.Nom_usuario = usuario.Nom_usuario;
        return user;
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
                    retiros = retiros + item.Ingresos;
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
            System.Console.Write("Introduzca la cantidad deseada: ");
            var ingreso = double.Parse(System.Console.ReadLine() ??
                                       throw new InvalidOperationException());
            if (ingreso > saldo)
            {
                Console.WriteLine(" No posee el saldo suficiente");
            }
            else
            {
                Console.Write("Introduzca el concepto de la operación: ");
                var concepto = Console.ReadLine();
                Console.WriteLine("Transacción realizada con éxito.");
                Console.WriteLine("================================");
                user.Ingresos = ingreso;
                user.Concepto = concepto;
                user.Opcion = usuario.Opcion;
                user.Nom_usuario = usuario.Nom_usuario;
                flag = true;
            }
        }
        return user;
    }
}