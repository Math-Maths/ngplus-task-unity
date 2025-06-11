using System;

public static class DialogueEvents
{
    public static event Action OnDialogueStarted;
    public static event Action OnDialogueEnded;

    public static void RaiseDialogueStarted()
    {
        OnDialogueStarted?.Invoke();
    }

    public static void RaiseDialogueEnded()
    {
        OnDialogueEnded?.Invoke();
    }
}
