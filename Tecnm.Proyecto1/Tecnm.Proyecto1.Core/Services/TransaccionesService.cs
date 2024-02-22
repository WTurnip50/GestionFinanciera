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
        user.Ingresos = ingreso;
        user.concepto = concepto;
        user.Opcion = usuario.Opcion;
        user.nom_usuario = usuario.nom_usuario;
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
        var t = new Transacciones() { total = saldo};
        return t;
    }

    public Usuario Retiro(Transacciones transacciones, Usuario usuario)
    {
        var saldo = transacciones.total;
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
                user.Ingresos = ingreso;
                user.concepto = concepto;
                user.Opcion = usuario.Opcion;
                user.nom_usuario = usuario.nom_usuario;
                flag = true;
            }
        }
        return user;
    }
}