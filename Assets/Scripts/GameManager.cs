using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;




    [SerializeField] private TransactionManager _transactionManager;
    [SerializeField] private DetailViewUIManager _detailViewUIManager;

    public TransactionManager TransactionManager { get { return _transactionManager; } }
    public DetailViewUIManager DetailViewUIManager { get { return _detailViewUIManager; } }

    public static Month SelectedMonth;
    public static int SelectedYear;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        SelectedMonth = (Month)DateTime.Now.Month - 1;
        SelectedYear = DateTime.Now.Year;
        DontDestroyOnLoad(this);
    }

    public void LoadSceneAsync(SceneName sceneName, LoadSceneMode mode = LoadSceneMode.Single)
    {
        SceneManager.LoadSceneAsync((int)sceneName, mode);
    }
}
