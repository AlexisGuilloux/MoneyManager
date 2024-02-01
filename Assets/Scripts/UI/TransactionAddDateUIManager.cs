using FantomLib;
using TMPro;
using UnityEngine;

public class TransactionAddDateUIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _dateInputField;

#if UNITY_ANDROID
    private FantomLib.DatePickerController datePickerController;
#endif


    private void OnDestroy()
    {
        _dateInputField.onEndEdit.RemoveListener(delegate { OnEndEdit(_dateInputField.text); });
        _dateInputField.onSelect.RemoveListener(delegate { OnSelect(); });

#if UNITY_ANDROID
        datePickerController.OnResult.RemoveListener(OnEndEdit);
#endif
    }

    public void Init(TransactionData data)
    {
#if UNITY_ANDROID
        datePickerController = gameObject.AddComponent<DatePickerController>();
        datePickerController.OnResult = new DatePickerController.ResultHandler();
        _dateInputField.shouldHideMobileInput = true;
#endif

        _dateInputField.onEndEdit.AddListener(delegate { OnEndEdit(_dateInputField.text); });
        _dateInputField.onSelect.AddListener(delegate { OnSelect(); });
        if(data != null)
        {
            _dateInputField.text = data.date;
        }
        else
        {
            _dateInputField.text = System.DateTime.Now.Date.ToString();
        }
    }

    private void OnSelect()
    {
#if UNITY_ANDROID
        datePickerController.OnResult.AddListener(OnEndEdit);
        datePickerController.Show();
#endif
    }

    private void OnEndEdit(string userInput)
    {
        _dateInputField.text = userInput;
        Events.OnTransactionDateAdded?.Invoke(userInput);
    }
}
