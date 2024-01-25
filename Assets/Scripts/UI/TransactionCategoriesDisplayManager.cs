using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TransactionCategoriesDisplayManager : MonoBehaviour
{
    [SerializeField] private List<TransactionCategoryData> _transactionCategoryDatas;
    [SerializeField] private TransactionCategoryUI _transactionCategoryPrefab;
    [SerializeField] private Transform _contentTransform;
    [SerializeField] private Toggle _entryToggle;

    private TransactionData _transactionData;
    // Start is called before the first frame update
    public void Init(TransactionData data)
    {
        if (data != null)
        {
            _entryToggle.isOn = data.entry;
            _transactionData = data;
        }
        else
        {
            _entryToggle.isOn = false;
        }

        _entryToggle.onValueChanged.AddListener(delegate { UpdateListing(); });
        UpdateListing();
    }

    private void OnDestroy()
    {
        _entryToggle.onValueChanged.RemoveListener(delegate { UpdateListing(); });
    }

    private void UpdateListing()
    {
        while (_contentTransform.childCount > 0)
        {
            DestroyImmediate(_contentTransform.GetChild(0).gameObject);
        }

        for (int i = 0; i < _transactionCategoryDatas.Count; i++)
        {
            if (_transactionCategoryDatas[i].entry != _entryToggle.isOn) continue;

            var newGO = Instantiate(_transactionCategoryPrefab, _contentTransform);
            
            if(_transactionData != null && _transactionData.transactionType == _transactionCategoryDatas[i].transactionType)
            {
                newGO.Init(_transactionCategoryDatas[i], true);
            }
            else
            {
                newGO.Init(_transactionCategoryDatas[i]);
            }
        }
    }
}
