namespace WpfAkka.Models;

internal class Patient
{
    public string Mrn { get; set; }
    public string BillingNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public override string ToString() => $"{FirstName} {LastName} (mrn:{Mrn} billno:{BillingNumber})";
}
