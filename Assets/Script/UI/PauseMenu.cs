using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Diaco.Cannonman.UI
{
    public class PauseMenu : MonoBehaviour
    {
        public Button Play_btn;
        public Button HomeApp_btn;

        public TMP_Text CurrentScore_txt;

        public StartMenu StartMenu_UI;
        public GameHUD GameHUD_UI;

        private GameManager gameManager;
        private void Update()
        {
           /* if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                FindObjectOfType<Controller>().GameStarted = false;
                GameHUD_UI.Show(true);
                Show(true);
            }*/
        }
        private void OnEnable()
        {
            GameHUD_UI.Show(false);
            Time.timeScale = 0;

            if (gameManager == null)
                gameManager = FindObjectOfType<GameManager>();
            if (gameManager.humen_spwaned)
                CurrentScore_txt.text = Mathf.RoundToInt(gameManager.BodyCurrentHeight).ToString();

            Play_btn.onClick.AddListener(() =>
            {

                Time.timeScale = 1;
                
                GameHUD_UI.Show(true);
                FindObjectOfType<Controller>().GameStarted = true;
                Show(false);

            });
            HomeApp_btn.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                StartMenu_UI.Show(true);
                Show(false);
                gameManager.OnUI = true;
            });
      
        }
        public void Show(bool show)
        {
            this.gameObject.SetActive(show);



 
        }
    }
}