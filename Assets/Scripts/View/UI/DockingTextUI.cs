using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DockingTextUI : MonoBehaviour
{
    private Text _text;
    private Color _textColor;
    private CanvasGroup _canvasGroup;

    public ElevatorController ElevatorController;

    private float _lastProgress = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // ElevatorController.elevatorDockingProgressAction += onDockingStart;
        // ElevatorController.dockAction += onDockingFinished;
        ElevatorController.elevatorDockingProgressAction += onDockingProgressUpdate;

        _text = GetComponent<Text>();
        _textColor = GetComponent<Text>().color;
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void onDockingStart(float progress)
    {
        iTween.ColorTo(gameObject, iTween.Hash("a", 1, "looptime", "pingpong", "time", 0.5f));
    }

    private void onDockingFinished(Vector3 pos)
    {
        iTween.Stop(gameObject);
    }

    private void onDockingProgressUpdate(float progress)
    {
        if (progress == 0f)
        {
            //ドッキング完了あるいは，ドッキングエリアから出た
            iTween.Stop(gameObject);
            _canvasGroup.alpha = 0f;
            _lastProgress = progress;
        }

        if (_lastProgress == 0f && progress != 0)
        {
            //progressが0->0以外になったとき
            iTween.ValueTo(gameObject, iTween.Hash(
                "from", 0f,
                "to", 1f,
                "loopType", "pingPong",
                "time", 0.7f,
                "onUpdate", "alphaUpdate"));
            _lastProgress = progress;
        }
    }

    private void alphaUpdate(float value)
    {
        _canvasGroup.alpha = value;
    }
}
