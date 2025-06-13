public interface INPCSavable
{
    string NPCId { get; }
    int GetCurrentAmount();
    void SetCurrentAmount(int amount);
}
