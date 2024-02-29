using System;
using Tecnm.Proyecto1.Core.Entities;
using Tecnm.Proyecto1.Core.Services;
using Tecnm.Proyecto1.Core.Enums;
namespace Tecnm.Proyecto1.Test;

public class UnitTest1
{
    [Fact]
    public void VerificacionDeCategorias()
    {
        // Arrange
        var service = new TransaccionesService();
        var usuario = new Usuario
        {
            categoria = CategoriasType.Otros
        };
        var categoryId = 1;

        // Act
        var result = service.categoria(usuario, categoryId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(CategoriasType.Deposito, result.categoria);
    }

    [Fact]
    public void CalculoCorrectoDeSaldo()
    {
        // Arrange
        var service = new TransaccionesService();
        var usuarios = new List<Usuario>
        {
            new Usuario { Opcion = 1, Ingresos = 100 },
            new Usuario { Opcion = 2, Retiros = 50 }
        };

        // Act
        var result = service.Saldo(usuarios);

        // Assert
        Assert.Equal(50, result.Total);
        Assert.Equal(100, result.Ingresos);
        Assert.Equal(50, result.Retiros);
    }

    [Fact]
    public void CalculodeIngresos()
    {
        // Arrange
        var service = new TransaccionesService();
        var usuarios = new List<Usuario>
        {
            new Usuario { Opcion = 1, Ingresos = 500 },
            new Usuario { Opcion = 1, Ingresos = 300 }
        };

        // Act
        var result = service.Saldo(usuarios);

        // Assert
        Assert.Equal(800, result.Total); 
        Assert.Equal(800, result.Ingresos); 
        Assert.Equal(0, result.Retiros);
    }
    
    [Fact]
    public void CalculodeRetiros()
    {
        // Arrange
        var service = new TransaccionesService();
        var usuarios = new List<Usuario>
        {
            new Usuario { Opcion = 2, Retiros = 200 },
            new Usuario { Opcion = 2, Retiros = 100 }
        };

        // Act
        var result = service.Saldo(usuarios);

        // Assert
        Assert.Equal(-300, result.Total);
        Assert.Equal(0, result.Ingresos); 
        Assert.Equal(300, result.Retiros); 
    }
    [Fact]
    public void EstablecerMetaCorrectamente()
    {
        // Arrange
        var service = new TransaccionesService();
        var transacciones = new Transacciones { tipo = 1 };
        var validAmount = 1000;
        var consoleInput = new System.IO.StringReader(validAmount.ToString()); 

        // Act
        System.Console.SetIn(consoleInput); 
        var result = service.Meta(transacciones);

        // Assert
        Assert.Equal(validAmount, result.Total);
    }
}