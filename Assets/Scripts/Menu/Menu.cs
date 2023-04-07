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
    [SerializeField] private GameObject quitWindow;
    [SerializeField] private GameObject quitWindowBackground;
    [SerializeField] private float quitWindowDelay = .5f;
    [SerializeField] private float smoothnessSlideColor = 20f;
    [SerializeField] private Color backgroundColor = new Color32(6, 15, 22, 215);

    [Header("Ship animation setup")]
    [SerializeField] private int shipSpacing = 25;
    [SerializeField] private float duration = 5f;
    [SerializeField] private List<Ship> ships;

    [Header("Selector setup")]
    [SerializeField] private float initialPositionX = 5;
    [SerializeField] private float switchDuration = 1f;
    [SerializeField] private Ease switchEase = Ease.OutSine;
    [SerializeField] private TMP_Text shipLabel;
    [SerializeField] private Image atkBar;
    [SerializeField] private Image hpBar;
    [SerializeField] private Image spdBar;
    [SerializeField] private GameObject prevButton;
    [SerializeField] private GameObject nextButton;

    private int _currentShipIndex = 0;
    private List<Tween> _tweens = new List<Tween>();
    private Color _transparent = new Color(0, 0, 0, 0);

    private int _atkMaximo = 6;
    private int _hpMaximo = 15;
    private int _spdMaximo = 25;

    void Start()
    {
        SelectShip(_currentShipIndex);
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

    private void SelectShip(int index)
    {
        Ship ship = ships[index];
        shipLabel.text = ship.Data.ShipName;

        atkBar.rectTransform.sizeDelta = new Vector2((float)ship.Data.Damage / (float)_atkMaximo * 100 * 185 / 100, 26);
        hpBar.rectTransform.sizeDelta = new Vector2((float)ship.Data.MaxLife / (float)_hpMaximo * 100 * 185 / 100, 26);
        spdBar.rectTransform.sizeDelta = new Vector2((float)ship.Data.Speed / (float)_spdMaximo * 100 * 185 / 100, 26);

        ShipSelector.Instance.SelectShip(ship.Data);
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

            SelectShip(_currentShipIndex);
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
