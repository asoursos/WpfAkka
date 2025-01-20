namespace WpfAkka.Models.Messages;

internal abstract class UserBaseEvent : BaseEvent
{
    protected UserBaseEvent() : base()
    {
        Payload = Faker.GenerateUser().ToString();
    }

    public string? Username { get; set; }
}

internal class UserLoggingIn : UserBaseEvent
{
}

internal class UserLoggedIn : UserBaseEvent
{
}

internal class UserLoggingOut : UserBaseEvent
{
}

internal class UserLoggedOut : UserBaseEvent
{
}