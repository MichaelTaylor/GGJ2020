using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterfaceManager : MonoBehaviour
{
    [SerializeField]
    private Image _healthBar;

    private List<ComponentFader> _componentFaderList = new List<ComponentFader>();

    public void SetHealthBarValue(float value)
    {
        _healthBar.fillAmount = value;
    }

    public void ReportComponentFader(ComponentFader fader)
    {
        if (!_componentFaderList.Contains(fader))
        {
            _componentFaderList.Add(fader);
        }
    }
}
