using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLevel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _returnMenuButton;
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;

    [Header("Menu setup")]
    [SerializeField] private TMP_Text _menuLabel;
    [SerializeField] private GameObject menuWindow;
    [SerializeField] private GameObject menuWindowBackground;
    [SerializeField] private float menuWindowDelay = .5f;
    [SerializeField] private float smoothnessSlideColor = 20f;
    [SerializeField] private Color backgroundColor = new Color32(6, 15, 22, 215);

    private Color _transparent = new Color(0, 0, 0, 0);

    private void Start()
    {
        _pauseButton.onClick.AddListener(() => SwitchMenu(true));
        _continueButton.onClick.AddListener(() => SwitchMenu(false));

        _returnMenuButton.onClick.AddListener(() => LoadScene(0));
        _restartButton.onClick.AddListener(() => LoadScene(SceneManager.GetActiveScene().buildIndex));
    }

    private void LoadScene(int i)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(i);
    }

    public void SwitchMenu(bool switchMenu, bool playerLose = false)
    {
        _continueButton.gameObject.SetActive(!playerLose);
        _restartButton.gameObject.SetActive(playerLose);
        _menuLabel.text = playerLose ? "You Lost!" : "Game Paused";

        if (switchMenu)
        {
            Time.timeScale = 0;
            menuWindowBackground.SetActive(true);
            StartCoroutine(SlideColor(_transparent, backgroundColor));

            menuWindow.transform.localScale = Vector3.zero;
            menuWindow.SetActive(true);
            menuWindow.transform.DOScale(1, menuWindowDelay).SetUpdate(true);
        }
        else
        {
            Time.timeScale = 1;
            StartCoroutine(SlideColor(backgroundColor, _transparent, true));
            menuWindow.transform
                .DOScale(0, menuWindowDelay)
                .OnComplete(() =>
                {
                    menuWindow.SetActive(false);
                })
                .SetUpdate(true);
        }

    }

    private IEnumerator SlideColor(Color from, Color to, bool disableOnFinish = false)
    {
        for (float i = 0; i < 1; i += 1 / smoothnessSlideColor)
        {
            yield return new WaitForSecondsRealtime(menuWindowDelay / smoothnessSlideColor);
            menuWindowBackground.GetComponent<Image>().color = Color.Lerp(from, to, i);
        }

        if (disableOnFinish) menuWindowBackground.SetActive(false);
    }
}
