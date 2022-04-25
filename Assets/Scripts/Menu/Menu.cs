using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("Menu setup")]
    public GameObject quitWindow;
    public GameObject quitWindowBackground;
    public float quitWindowDelay = .5f;
    public float smoothnessSlideColor = 20f;
    public Color backgroundColor = new Color32(6, 15, 22, 215);

    [Header("Ship animation setup")]
    public int shipSpacing = 25;
    public float duration = 5f;
    public List<GameObject> ships;

    [Header("Selector setup")]
    public float initialPositionX = 5;
    public float switchDuration = 1f;
    public Ease switchEase = Ease.OutSine;
    public TMP_Text shipLabel;
    public GameObject prevButton;
    public GameObject nextButton;

    private int _currentShipIndex = 0;
    private List<Tween> _tweens = new List<Tween>();
    private Color _transparent = new Color(0, 0, 0, 0);

    void Start()
    {
        SetShipLabel(0);
        prevButton.GetComponent<Button>().interactable = false;

        for (int i = 0; i < ships.Count; i++)
        {
            var s = ships[i];

            s.transform.localPosition = new Vector3(i * -shipSpacing, 0, 0);
            Tween t = s.transform
                .DORotate(new Vector3(0, 360, 0), duration, RotateMode.LocalAxisAdd)
                .SetLoops(-1)
                .SetEase(Ease.Linear);

            _tweens.Add(t);
        }
    }

    private void SetShipLabel(int index)
    {
        shipLabel.text = ships[index].GetComponent<Ship>()?.shipName;
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void SwitchQuitWindow(bool switchWindow)
    {
        if (switchWindow)
        {
            quitWindowBackground.SetActive(true);
            StartCoroutine(SlideColor(_transparent, backgroundColor));

            quitWindow.transform.localScale = Vector3.zero;
            quitWindow.SetActive(true);
            quitWindow.transform.DOScale(1, quitWindowDelay);
        }
        else
        {
            StartCoroutine(SlideColor(backgroundColor, _transparent, true));
            quitWindow.transform
                .DOScale(0, quitWindowDelay)
                .OnComplete(() =>
                {
                    quitWindow.SetActive(false);
                });
        }

    }

    IEnumerator SlideColor(Color from, Color to, bool disableOnFinish = false)
    {
        for (float i = 0; i < 1; i += 1 / smoothnessSlideColor)
        {
            yield return new WaitForSeconds(quitWindowDelay / smoothnessSlideColor);
            quitWindowBackground.GetComponent<Image>().color = Color.Lerp(from, to, i);
        }

        if (disableOnFinish) quitWindowBackground.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SwitchShip(int s)
    {
        int nextShipIndex = _currentShipIndex + s;

        prevButton.GetComponent<Button>().interactable = nextShipIndex > 0;
        nextButton.GetComponent<Button>().interactable = nextShipIndex < ships.Count - 1;

        if (nextShipIndex >= 0 && nextShipIndex < ships.Count)
        {
            float nextPositionX = initialPositionX + ships[nextShipIndex].transform.localPosition.x;

            gameObject.transform
                .DOMoveX(nextPositionX, switchDuration)
                .SetEase(switchEase);

            _currentShipIndex = nextShipIndex;

            SetShipLabel(_currentShipIndex);
        }
    }

    private void OnDestroy()
    {
        foreach (var t in _tweens)
        {
            t.Kill();
        }
    }
}
