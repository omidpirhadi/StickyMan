using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
namespace Diaco.Cannonman.UI
{
    public class DialogNewAchive : MonoBehaviour
    {
        public TMP_Text Dialog_txt;
        public float DialogDurationShow = 5;
        public float AnimationTextDuration = 3;
        private Tween DelayCall_tw;
        public void SetContext(string context)
        {
            Dialog_txt.DOText(context, AnimationTextDuration, true, ScrambleMode.Uppercase);
            DelayCall_tw.Kill();
        }
        public void Show(bool show)
        {
            
            this.gameObject.SetActive(show);
            DelayCall_tw = DOVirtual.DelayedCall(DialogDurationShow, () => { Show(false); });
        }
    }
}