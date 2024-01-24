using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AddTransactionButtonUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate
        {
            GameManager.Instance.LoadSceneAsync(SceneName.AddTransactionScene);
        });
    }
}
