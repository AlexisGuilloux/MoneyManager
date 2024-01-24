using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeaderUIManager : MonoBehaviour
{
    [SerializeField] private Button _previousMonthBtn;
    [SerializeField] private Button _nextMonthBtn;
    [SerializeField] private Button _previousYearBtn;
    [SerializeField] private Button _nextYearBtn;
    [SerializeField] private TextMeshProUGUI _monthText;
    [SerializeField] private TextMeshProUGUI _yearText;

    // Start is called before the first frame update
    void Start()
    {
        _previousMonthBtn.onClick.AddListener(OnPreviousMonthBtnPressed);
        _nextMonthBtn.onClick.AddListener(OnNextMonthBtnPressed);

        _previousYearBtn.onClick.AddListener(OnPreviousYearBtnPressed);
        _nextYearBtn.onClick.AddListener(OnNextYearBtnPressed);

        UpdateHeaderInfos();
    }

    private void OnDestroy()
    {
        _previousMonthBtn.onClick.RemoveListener(OnPreviousMonthBtnPressed);
        _nextMonthBtn.onClick.RemoveListener(OnNextMonthBtnPressed);

        _previousYearBtn.onClick.RemoveListener(OnPreviousYearBtnPressed);
        _nextYearBtn.onClick.RemoveListener(OnNextYearBtnPressed);
    }

    private void OnPreviousMonthBtnPressed()
    {
        if ((int)GameManager.SelectedMonth == 0)
        {
            GameManager.SelectedMonth = Month.December;

        }
        else
        {
            GameManager.SelectedMonth--;
        }

        UpdateHeaderInfos();
    }

    private void OnNextMonthBtnPressed()
    {
        if ((int)GameManager.SelectedMonth == 11)
        {
            GameManager.SelectedMonth = Month.January;
        }
        else
        {
            GameManager.SelectedMonth++;
        }
        UpdateHeaderInfos();
    }

    private void OnPreviousYearBtnPressed()
    {
        GameManager.SelectedYear--;
        UpdateHeaderInfos();
    }

    private void OnNextYearBtnPressed()
    {
        GameManager.SelectedYear++;
        UpdateHeaderInfos();
    }


    private void UpdateHeaderInfos()
    {
        //Trigger event to update data
        Events.OnSelectedMonthChanged?.Invoke();

        //Update UI Feedbacks
        _monthText.text = GameManager.SelectedMonth.ToString();
        _yearText.text = GameManager.SelectedYear.ToString();
    }
}
