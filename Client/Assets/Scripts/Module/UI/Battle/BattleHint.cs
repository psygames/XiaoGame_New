using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RedStone.UI;

namespace RedStone
{

    public class BattleHint : MonoBehaviour
    {
        public Text text;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Show(string text)
        {
            gameObject.SetActive(true);
            this.text.text = text;
            transform.localScale = Vector3.one;
            uTools.uTweenScale.Begin(gameObject, Vector3.one, Vector3.up, 0.3f, 1).method = uTools.EaseType.easeInCirc;
        }
    }
}