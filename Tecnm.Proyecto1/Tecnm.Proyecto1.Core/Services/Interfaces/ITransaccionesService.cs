using Tecnm.Proyecto1.Core.Entities;
using Tecnm.Proyecto1.Core.Enums;
namespace Tecnm.Proyecto1.Core.Services.Interfaces;

public interface ITransaccionesService
{
    Usuario Ingreso(Usuario usuario);
    Usuario categoria(Usuario usuario,int id);
    Transacciones Saldo(List<Usuario> usuarios);
    Usuario Retiro(Transacciones transacciones, Usuario usuario);
    Transacciones Meta(Transacciones transacciones);
}