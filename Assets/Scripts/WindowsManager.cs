using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static WindowsManager Layout; // создаем синголтон для удобного управления
    [SerializeField] private GameObject[] windows;
    private void Awake()
    {
        Layout = this;
        foreach(GameObject window in windows)
        {
            window.SetActive(false);
        }
    }
    void Start()
    {
        OpenLayout("PanelLoading");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenLayout(string namePanel)
    {
        foreach (GameObject window in windows)
        {
            if(window.name == namePanel)
            {
                window.SetActive(true);
            }
            else
            {
                window.SetActive(false);
            }
        }
    }
}
