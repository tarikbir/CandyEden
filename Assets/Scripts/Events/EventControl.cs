using Sigtrap.Relays;

public class EventControl : SingletonMB<EventControl>
{
    public Relay<Ability, int> OnAbilityEquipped = new();
    public Relay<Ability, bool> OnAbilityGained = new();

    public Relay OnGetHit = new();

    public Relay<Ability> OnAbilityActivate = new();

    public Relay OnTimedEvent = new();
}

