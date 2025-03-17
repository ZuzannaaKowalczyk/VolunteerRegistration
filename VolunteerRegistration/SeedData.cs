using System;
using System.Linq;
using Bogus;
using VolunteerRegistration.Models;

public static class SeedData
{
    public static void GenerateVolunteers(VolunteerRegistrationContext context)
    {
        var faker = new Faker("pl");

        var volunteers = Enumerable.Range(1, 5).Select(_ => new Volunteer
        {
            Firstname = faker.Name.FirstName(),
            Lastname = faker.Name.LastName(),
            Email = faker.Internet.Email(),
            PhoneNumber = faker.Phone.PhoneNumber("601#######"), // Format polskiego numeru
            BirthDate = faker.Date.Past(30, DateTime.Now.AddYears(-18)) // Wiek 18-48 lat
        }).ToList();

        context.Volunteers.AddRange(volunteers);
        context.SaveChanges();

        Console.WriteLine("5 nowych wolontariuszy dodanych!");
    }
}
