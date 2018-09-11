using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* Author:       Running
** Time:         18.9.11
** Describtion:  
*/

public class LoadingUI : MonoBehaviour
{
    [SerializeField]
    private Image frontImage;

    private float _totalTime = 0f;

    private void Awake()
    {
        gameObject.SetActive(true);
        frontImage.fillAmount = 0;
        _totalTime = 0f;
    }

    private void Update()
    {
        _totalTime += Time.deltaTime;
        if (_totalTime > 2f)
        {
            gameObject.SetActive(false);
        }
        else
        {
            frontImage.fillAmount = _totalTime;
        }
    }
}
