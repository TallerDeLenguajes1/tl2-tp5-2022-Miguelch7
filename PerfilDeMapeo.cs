using AutoMapper;
using SistemaCadeteriaMVC.Models;
using SistemaCadeteriaMVC.ViewModels;

public class PerfilDeMapeo : Profile
{
  public PerfilDeMapeo()
  {
    CreateMap<Cadete, CadeteViewModel>().ReverseMap();
    CreateMap<Pedido, PedidoViewModel>().ReverseMap();
    CreateMap<Cliente, ClienteViewModel>().ReverseMap();
    CreateMap<Pedido, PedidosCadetesViewModel>().ReverseMap();
    CreateMap<Pedido, PedidosCadetesClientesViewModel>().ReverseMap();
  }
}