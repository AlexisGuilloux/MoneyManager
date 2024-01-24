using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class DetailViewUIManager : MonoBehaviour
{
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private TransactionUI _transactionUIPrefab;
    [SerializeField] private DayGroupUI _dayUIPrefab;

    private List<DayGroupUI> _dayGroupUIList = new List<DayGroupUI>();
    private CanvasGroup _canvasGroup;


    // Start is called before the first frame update
    void Start()
    {

        _canvasGroup = _scrollRect.content.GetComponent<CanvasGroup>();

        InitMonth();
        Events.OnSelectedMonthChanged += HandleOnSelectedMonthChanged;
    }

    private void OnDestroy()
    {
        Events.OnSelectedMonthChanged -= HandleOnSelectedMonthChanged;
    }


    private void InitMonth()
    {
        _canvasGroup.alpha = 0f;

        while (_scrollRect.content.childCount > 0)
        {
            DestroyImmediate(_scrollRect.content.GetChild(0).gameObject);
        }
        _dayGroupUIList.Clear();

        var transactionDataList = GameManager.Instance.TransactionManager.GetTransactionsForGivenMonthAndYear();
        //Create a transactionUI for every transaction
        for (int i = 0; i < transactionDataList.Count; i++)
        {
            //Check if a dayGroupUI has already been created or create a new one
            bool isDayExisting = false;
            DayGroupUI parentDay = null;
            for (int j = 0; j < _dayGroupUIList.Count; j++)
            {

                if (_dayGroupUIList[j].dayDate.Date == Utilities.Converter.GetDateFromString(transactionDataList[i].date))
                {
                    isDayExisting = true;
                    parentDay = _dayGroupUIList[j];
                    break;
                }
            }

            //Create the daygroupUI if needed and init the object
            if (!isDayExisting)
            {
                var newDayGroupUI = Instantiate(_dayUIPrefab, _scrollRect.content.transform);
                newDayGroupUI.Init(transactionDataList[i]);
                parentDay = newDayGroupUI;
                _dayGroupUIList.Add(newDayGroupUI);
            }

            //Create the new transactionUI and init the object
            var newTransaction = Instantiate(_transactionUIPrefab, parentDay.DayDetailsTransform);
            newTransaction.Init(transactionDataList[i]);
        }
        SortDays();

        StartCoroutine(DelayedForceLayoutRefresh());
    }

    private IEnumerator DelayedForceLayoutRefresh()
    {
        while(true)
        {
            yield return new WaitForEndOfFrame();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_scrollRect.content);
            _canvasGroup.alpha = 1f;
            yield return new WaitForEndOfFrame();
            if (_dayGroupUIList.Count > 0)
            {
                StartCoroutine(Utilities.ScrollHelper.ScrollToTarget(_scrollRect, _dayGroupUIList[0].GetComponent<RectTransform>()));
            }
            break;
        }
        

        yield return null;
    }


    private void SortDays()
    {
        List<DayGroupUI> sortedList = new List<DayGroupUI>(31);

        for (int i = 0; i < 31; i++)
        {
            sortedList.Add(null);
        }

        for (int i = 0; i < _dayGroupUIList.Count; i++)
        {
            sortedList[_dayGroupUIList[i].dayDate.Day-1] =  _dayGroupUIList[i];
        }

        sortedList.RemoveAll(x => x == null);
        sortedList.Reverse();
        _dayGroupUIList = sortedList;
        for (int i = 0; i < _dayGroupUIList.Count; i++)
        {
            _dayGroupUIList[i].transform.SetSiblingIndex(i);
        }
    }

    #region Handlers

    private void HandleOnSelectedMonthChanged()
    {
        InitMonth();
    }

    #endregion

}
