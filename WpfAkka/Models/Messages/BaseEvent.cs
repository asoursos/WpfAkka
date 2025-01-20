namespace WpfAkka.Models.Messages;

internal abstract class BaseEvent 
{
    private static int _id = 1000;

    public BaseEvent()
    {
        // increment the id for each new message
        Id = ++_id;
        Time = DateTime.Now;
    }

    public int Id { get; set; }
    public DateTime Time { get; set; }
    public string? Payload { get; set; }
}
