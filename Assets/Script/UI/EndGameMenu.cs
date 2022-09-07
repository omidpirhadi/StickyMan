using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
namespace Diaco.Cannonman.UI
{
    public class EndGameMenu : MonoBehaviour
    {

        public Button PlayAgine_btn;
        public Button PlayAds_btn;
        public Button HomeApp_btn;

        public TMP_Text Score_txt;
        public TMP_Text YourRecord_txt;

        public float SpeedAnimationText = 1;
        public StartMenu StartMenu_UI;

        private GameManager gameManager;
        private void Start()
        {

        }

        private void OnEnable()
        {
            gameManager = FindObjectOfType<GameManager>();

            var socer_current_record = gameManager.CalculateScore();
            PlayAgine_btn.interactable = true;
            PlayAds_btn.interactable = true;
            HomeApp_btn.interactable = true;

            PlayAgine_btn.onClick.AddListener(() => {

                StartCoroutine(gameManager.LevelSpwan());
                PlayAgine_btn.interactable = false;
            });
            PlayAds_btn.onClick.AddListener(() => {
                StartMenu_UI.Show(true);
                PlayAds_btn.interactable = false;
            });
            HomeApp_btn.onClick.AddListener(() => {

                StartMenu_UI.Show(true);
                Show(false);
                HomeApp_btn.interactable = false;

            });

            Score_txt.DOText(socer_current_record.ToString(), SpeedAnimationText, true, ScrambleMode.Numerals);
            YourRecord_txt.DOText(Mathf.RoundToInt(gameManager.BodyCurrentHeight).ToString(), SpeedAnimationText, true, ScrambleMode.Numerals);
           
            gameManager.SaveLeaderboard("cannonman");
        }
        private void OnDisable()
        {
            PlayAgine_btn.onClick.RemoveAllListeners();
            PlayAds_btn.onClick.RemoveAllListeners();
            HomeApp_btn.onClick.RemoveAllListeners();
        }
        public void Show(bool show)
        {
            this.gameObject.SetActive(show);

            // gameManager.OnUI = show;


        }
    }
}