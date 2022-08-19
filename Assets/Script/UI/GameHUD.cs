using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace Diaco.Cannonman.UI
{
    public class GameHUD : MonoBehaviour
    {
        public Button Pause_btn;
       

        public TMP_Text Ammo_txt;
        public TMP_Text Coin_txt;
        public TMP_Text Score_txt;

        
        public PauseMenu PauseMenu_UI;

        private GameManager gameManager;
        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            SetAmmoText();
            SetCoinvalue(0);
            
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                FindObjectOfType<Controller>().GameStarted = false;
                PauseMenu_UI.Show(true);
            }
        }
        void LateUpdate()
        {
            if (gameManager)
                Score_txt.text =  Mathf.RoundToInt( gameManager.BodyCurrentHeight).ToString();
        }

        void OnEnable()
        {  gameManager = FindObjectOfType<GameManager>();
            Pause_btn.onClick.AddListener(() =>
            {
                Time.timeScale = 0;
                PauseMenu_UI.Show(true);
            });
          
            SetAmmoText();
            SetCoinvalue(0);

        }
        void OnDisable()
        {
            Pause_btn.onClick.RemoveAllListeners();



        }
        public void SetCoinvalue(int c)
        {
            gameManager.CoinCount += c;
            Coin_txt.text = gameManager.CoinCount.ToString();
            //  Debug.Log("1");
        }
        public void SetAmmoText()
        {
           // Ammo_txt.text = gameManager.gun.AmmoCount.ToString();
        }
        public void Show(bool show)
        {
            this.gameObject.SetActive(show);
        }
    }
}