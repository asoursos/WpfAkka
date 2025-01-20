namespace WpfAkka.Models.Messages;


internal class PatientOpenedInEMR : BaseEvent
{
    public PatientOpenedInEMR() : base()
    {
        Payload = Faker.GeneratePatient().ToString();
    }
}
