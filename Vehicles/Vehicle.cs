using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Shared;

namespace CleanArchitecture.Domain.Vehicles;

public sealed class Vehicle : Entity
{
    public Vehicle(
        Guid id,
        Model model,
        Vin vin,
        Money price,
        Money maintenance,
        DateTime? dateLastRent,
        List<Accesory> accesory,
        Address? address
        ) : base(id)
    {
        Modelo = model;
        Vin = vin;
        Price = price;
        Maintenance = maintenance;
        DateLastRent = dateLastRent;
        Accesorys = accesory;
        Address = address;
    }
    public Model? Modelo { get; private set; }
    public Vin? Vin { get; private set; }
    public Address? Address { get; private set; }
    public Money? Price { get; private set; }
    public Money? Maintenance { get; private set; }
    public DateTime? DateLastRent { get; internal set; }
    public List<Accesory> Accesorys { get; private set; } = new List<Accesory>();
}