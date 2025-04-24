
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private EventBus _eventBus;

    public static GameManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); 
        }
    }

    private void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }



}
