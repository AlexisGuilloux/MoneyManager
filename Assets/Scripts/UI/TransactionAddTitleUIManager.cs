using UnityEngine;
using TMPro;

public class TransactionAddTitleUIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _titleInputField;

    // Start is called before the first frame update
    void Start()
    {
        _titleInputField.onEndEdit.AddListener(delegate { OnEndEdit(); });
    }

    private void OnEndEdit()
    {
        Events.OnTransactionTitleAdded?.Invoke(_titleInputField.text);
    }
}
