using BlazorControlCefa.Data.Entities;

namespace BlazorControlCefa.Data.Seeders
{
    public static class InitialDataSeeder
    {
        public static void SeedInitialData(ApplicationDbContext context)
        {
            if (!context.PersonType.Any())
            {

                context.PersonType.Add(new PersonType
                {
                    Name = "Empleado",
                    IsActive = true
                });

                context.PersonType.Add(new PersonType
                {
                    Name = "Visitante",
                    IsActive = true
                });

                context.PersonType.Add(new PersonType
                {
                    Name = "Contratista",
                    IsActive = true
                });

                context.PersonType.Add(new PersonType
                {
                    Name = "Visitante Bodega",
                    IsActive = true
                });

                context.SaveChanges();
            }

            if (!context.VisitTypes.Any())
            {

                context.VisitTypes.Add(new VisitType
                {
                    Name = "Empleado",
                    IsActive = true
                });

                context.VisitTypes.Add(new VisitType
                {
                    Name = "Visitante",
                    IsActive = true
                });

                context.VisitTypes.Add(new VisitType
                {
                    Name = "Contratista",
                    IsActive = true
                });

                context.VisitTypes.Add(new VisitType
                {
                    Name = "Visitante Bodega",
                    IsActive = true
                });

                context.SaveChanges();
            }

            if (!context.Departments.Any())
            {

                context.Departments.Add(new Department
                {
                    Name = "N/A",
                    IsActive = true
                });

                context.SaveChanges();
            }

            if (!context.Positions.Any())
            {

                context.Positions.Add(new Position
                {
                    Name = "N/A",
                    IsActive = true
                });

                context.SaveChanges();
            }


            if (!context.VehicleTypes.Any())
            {

                context.VehicleTypes.Add(new VehicleType
                {
                    Name = "Otro",
                    IsActive = true
                });

                context.VehicleTypes.Add(new VehicleType
                {
                    Name = "Sedan",
                    IsActive = true
                });

                context.VehicleTypes.Add(new VehicleType
                {
                    Name = "Moto",
                    IsActive = true
                });

                context.VehicleTypes.Add(new VehicleType
                {
                    Name = "Camioneta 1 Cabina",
                    IsActive = true
                });

                context.VehicleTypes.Add(new VehicleType
                {
                    Name = "Camioneta 2 Cabina",
                    IsActive = true
                });

                context.VehicleTypes.Add(new VehicleType
                {
                    Name = "SUB",
                    IsActive = true
                });

                context.VehicleTypes.Add(new VehicleType
                {
                    Name = "Panelito",
                    IsActive = true
                });

                context.VehicleTypes.Add(new VehicleType
                {
                    Name = "Tipo Prado",
                    IsActive = true
                });

                context.VehicleTypes.Add(new VehicleType
                {
                    Name = "Camión Pequeño",
                    IsActive = true
                });

                context.VehicleTypes.Add(new VehicleType
                {
                    Name = "Camión Mediano",
                    IsActive = true
                });

                context.VehicleTypes.Add(new VehicleType
                {
                    Name = "Camión Grande",
                    IsActive = true
                });

                context.SaveChanges();

            }

            if (!context.VehicleBrands.Any())
            {
                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Otro",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Toyota",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Kia",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Susuki",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Hyundai",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Mazda",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Yamaha",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Mercedes Benz",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "BMW",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Wolkswagen",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Ford",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Opel",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Seat",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Mitsubishi",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Fiat",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Honda",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Isuzu",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Lada",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Land Rover",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Nissan",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Peugeot",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Renault",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Subaru",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Volvo",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Raybar",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Hero",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Bajaj",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Genesis",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Kawasaki",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "KTM",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "AKT",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Boxer",
                    IsActive = true
                });

                context.VehicleBrands.Add(new VehicleBrand
                {
                    Name = "Jialing",
                    IsActive = true
                });
                context.SaveChanges();
            }

            if (!context.Reasons.Any())
            {
                context.Reasons.Add(new Reason
                {
                    Name = "Jornada Laboral",
                    IsActive = true
                });

                context.Reasons.Add(new Reason
                {
                    Name = "Visita Personal",
                    IsActive = true
                });

                context.Reasons.Add(new Reason
                {
                    Name = "Visita de Proveedor",
                    IsActive = true
                });

                context.SaveChanges();
            }



        }
    }
}
