namespace WpfAkka.Models;


internal class PatientContextChanged
{
    private static int _id = 1000;

    public PatientContextChanged()
    {
        // increment the id for each new message
        Id = ++_id;
        Time = DateTime.Now;
        Payload = Faker.GeneratePatient().ToString();
    }

    public int Id { get; set; }
    public DateTime Time { get; set; }
    public string? Payload { get; set; }
}