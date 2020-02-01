using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentFader : MonoBehaviour
{
    private enum ComponentType
    {
        SPRITE_RENDERER,
        IMAGE
    };

    [SerializeField]
    private ComponentType _componentType;

    private float _targetAlpha = 1f;

    //Components
    private SpriteRenderer _rend;
    private Image _image;

    private void Start()
    {
        GameManager.instance.ReportComponentFader(this);
        GetSpecificComponent();
    }
    
    private void GetSpecificComponent()
    {
        switch(_componentType)
        {
            case ComponentType.SPRITE_RENDERER:
                {
                    _rend = GetComponent<SpriteRenderer>();
                    break;
                }
            case ComponentType.IMAGE:
                {
                    _image = GetComponent<Image>();
                    break;
                }
        }
    }

    private void Update()
    {
        switch (_componentType)
        {
            case ComponentType.SPRITE_RENDERER:
                {
                    Color c = _rend.color;
                    c.a = GameManager.instance.GetFadeValue();
                    _rend.color = c;
                    break;
                }
            case ComponentType.IMAGE:
                {
                    Color c = _image.color;
                    c.a = GameManager.instance.GetFadeValue();
                    _image.color = c;
                    break;
                }
        }
    }
}
