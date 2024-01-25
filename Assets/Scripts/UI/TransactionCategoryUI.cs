using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransactionCategoryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Toggle _toggle;

    private TransactionCategoryData _data;

    public void Init(TransactionCategoryData data, bool isPressedByDefault = false)
    {
        _titleText.text = data.categoryName;
        _iconImage.sprite = data.sprite;
        _data = data;

        _toggle.onValueChanged.AddListener(delegate { OnTogglePressed(); });
        _toggle.isOn = isPressedByDefault;
    }

    private void OnDestroy()
    {
        _toggle.onValueChanged.RemoveListener(delegate { OnTogglePressed(); });
    }

    private void OnTogglePressed()
    {
        if (!_toggle.isOn) return;
        Events.OnTransactionCategoryAdded?.Invoke(_data);
    }
}
