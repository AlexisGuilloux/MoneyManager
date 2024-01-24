using TMPro;
using UnityEngine;

public class TransactionAddDateUIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _dateInputField;

    // Start is called before the first frame update
    void Start()
    {
        _dateInputField.onEndEdit.AddListener(delegate { OnEndEdit(); });
        //_dateInputField.text = DateTime.Now.Date.ToString();
    }

    private void OnEndEdit()
    {
        Events.OnTransactionDateAdded?.Invoke(_dateInputField.text);
    }
}
