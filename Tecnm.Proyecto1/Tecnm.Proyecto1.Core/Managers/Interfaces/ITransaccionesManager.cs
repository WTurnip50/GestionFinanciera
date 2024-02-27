namespace Tecnm.Proyecto1.Core.Managers.Interfaces;
using Tecnm.Proyecto1.Core.Entities;
public interface ITransaccionesManager
{
    Usuario SetIngresoUsuario(Usuario usuario);
    Transacciones GetSaldo(List<Usuario> usuarios);
    Usuario SetRetiroUsuario(Transacciones transacciones, Usuario usuario);
    Usuario setCategoriaUsuario(Usuario usuario);
}