using TMPro;
using UnityEngine;

public class TransactionAddDateUIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _dateInputField;

    // Start is called before the first frame update
    public void Init(TransactionData data)
    {
        _dateInputField.onEndEdit.AddListener(delegate { OnEndEdit(); });
        if(data != null)
        {
            _dateInputField.text = data.date;
        }
        else
        {
            _dateInputField.text = System.DateTime.Now.Date.ToString();
        }
        
    }

    private void OnEndEdit()
    {
        Events.OnTransactionDateAdded?.Invoke(_dateInputField.text);
    }
}
