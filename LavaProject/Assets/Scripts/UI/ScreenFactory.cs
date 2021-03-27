using System;
using UnityEngine;
using UnityEngine.UI;


public sealed class ScreenFactory
{
    private Canvas _canvas;
    private CanvasUI _canvasUI;

    public ScreenFactory()
    {
        if (_canvas == null)
        {
            var canvas = Resources.Load<Canvas>(AssetsPath.Path[GameObjectType.Canvas]);
            _canvas = GameObject.Instantiate(canvas);
        }
    }

    public CanvasUI Canvas
    {
        get
        {
            if (_canvasUI == null)
            {
                _canvasUI = _canvas.GetComponent<CanvasUI>();
            }
            return _canvasUI;
        }
    }
}