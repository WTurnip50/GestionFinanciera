using Tecnm.Proyecto1.Core.Entities;
using Tecnm.Proyecto1.Core.Services.Interfaces;

namespace Tecnm.Proyecto1.Core.Managers;

public class TransaccionesManager
{
    private readonly ITransaccionesService _service;

    public TransaccionesManager(ITransaccionesService service)
    {
        _service = service;
    }
    
    public Usuario setIngresoUsuario(Usuario usuario)
    {
        return _service.Ingreso(usuario);
    }

    public Transacciones getSaldo(List<Usuario> usuarios)
    {
        return _service.Saldo(usuarios);
    }

    public Usuario setRetiroUsuario(Transacciones transacciones, Usuario usuario)
    {
        return _service.Retiro(transacciones, usuario);
    }

    public Usuario setCategoriaUsuario(Usuario usuario, int id)
    {
        return _service.categoria(usuario,id);
    }
}