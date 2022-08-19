using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
namespace Diaco.Cannonman.UI
{
    public class StartMenu : MonoBehaviour
    {

       
        public Button StartGame_btn;
        public Button ExitApp_btn;
        public TMP_Text HightScore_txt;
        public Image Image_Rotator;
        public float Speed_Rotator;
        private GameManager gameManager;
        private void Start()
        {
            Image_Rotator.rectTransform.DORotate(new Vector3(0, 0, 180f), Speed_Rotator).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }
        private void OnEnable()
        {
            gameManager = FindObjectOfType<GameManager>();
            StartGame_btn.onClick.AddListener(() => {

                StartCoroutine(gameManager.LevelSpwan());
            });
            ExitApp_btn.onClick.AddListener(() => {
                Application.Quit();
            });
            HightScore_txt.text = gameManager.leaderboard.totalScore.ToString();
        }
        private void OnDisable()
        {
            StartGame_btn.onClick.RemoveAllListeners();

            ExitApp_btn.onClick.RemoveAllListeners();
        }
        public void Show(bool show)
        {
            this.gameObject.SetActive(show);
          //  gameManager.OnUI = show;
        }
    }
}