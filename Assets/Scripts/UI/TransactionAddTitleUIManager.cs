using UnityEngine;
using TMPro;

public class TransactionAddTitleUIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _titleInputField;

    // Start is called before the first frame update
    public void Init(TransactionData data)
    {
        _titleInputField.onEndEdit.AddListener(delegate { OnEndEdit(); });
        if(data != null )
        {
            _titleInputField.text = data.title;
        }
    }

    private void OnEndEdit()
    {
        Events.OnTransactionTitleAdded?.Invoke(_titleInputField.text);
    }
}
