using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GameObject loadingPanel;

    public void SetLoading(bool value)
    {
        loadingPanel.SetActive(value);
    }
}
