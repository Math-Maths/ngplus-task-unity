using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using System.Collections;
using System;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text characterName;

    [Header("Typing Settings")]
    [SerializeField] private float typingSpeed = 0.05f;

    public event Action OnDialougueEnd;

    private DialogueData currentDialogue;
    private int currentLineIndex = 0;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private InputSystem_Actions inputActions;

    private void Awake()
    {
        if (Instance == null) Instance = this;

        inputActions = new InputSystem_Actions();
        inputActions.UI.Enable();
        inputActions.UI.Submit.performed += OnSubmit;
    }

    private void OnDestroy()
    {
        inputActions.UI.Submit.performed -= OnSubmit;
    }

    public void StartDialogue(DialogueData dialogue, string name)
    {
        characterName.text = name;
        currentDialogue = dialogue;
        currentLineIndex = 0;
        dialoguePanel.SetActive(true);
        DialogueEvents.RaiseDialogueStarted();
        ShowNextLine();
    }

    private void OnSubmit(InputAction.CallbackContext context)
    {
        if (currentDialogue == null) return;

        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentDialogue.lines[currentLineIndex - 1];
            isTyping = false;
        }
        else
        {
            ShowNextLine();
        }
    }

    private void ShowNextLine()
    {
        if (currentLineIndex < currentDialogue.lines.Length)
        {
            typingCoroutine = StartCoroutine(TypeLine(currentDialogue.lines[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            EndDialogue();
            return;
        }

        
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private void EndDialogue()
    {
        OnDialougueEnd?.Invoke();
        dialoguePanel.SetActive(false);
        DialogueEvents.RaiseDialogueEnded();
        currentDialogue = null;
    }
}
