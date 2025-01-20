using Bogus;

namespace WpfAkka.Models;

internal static class Faker
{
    private static Faker<Patient> FakePatients = new Faker<Patient>()
        .RuleFor(p => p.Mrn, f => f.Random.Number(100000, 999999).ToString())
        .RuleFor(p => p.BillingNumber, f => f.Random.Number(100000, 999999).ToString())
        .RuleFor(p => p.FirstName, f => f.Name.FirstName())
        .RuleFor(p => p.LastName, f => f.Name.LastName());

    public static Patient GeneratePatient() => FakePatients.Generate();

    public static PatientContextChanged GenerateEventMessage()
    {
        var patient = GeneratePatient();
        return new PatientContextChanged() { Payload = $"{patient}"};
    }
}
