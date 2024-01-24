using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TransactionCategoriesDisplayManager : MonoBehaviour
{
    [SerializeField] private List<TransactionCategoryData> _transactionCategoryDatas;
    [SerializeField] private TransactionCategoryUI _transactionCategoryPrefab;
    [SerializeField] private Transform _contentTransform;
    [SerializeField] private Toggle _entryToggle;

    // Start is called before the first frame update
    void Start()
    {
        _entryToggle.isOn = false;
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
            newGO.Init(_transactionCategoryDatas[i]);
        }
    }
}
