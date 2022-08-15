﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MRD
{
    public class OwnRelicImageController : MonoBehaviour
    {
        [SerializeField]
        private Transform imageParent;
        
        public void SetOwnRelicImage()
        {
            var ownRelics = RoundManager.Inst.RelicManager.OwnRelics;

            var transforms = SetRelicsLayers(ownRelics.Count);

            for (int i = 0; i < transforms.Length; i++)
            {
                //간격 조정
                ((RectTransform)transforms[i]).anchoredPosition = new Vector2(-4 + i * 1.5f, 0);

                //이미지 출력
                /*HaiType type = doraList[i].HaiType;
                int number = type is HaiType.Kaze or HaiType.Sangen
                    ? doraList[i].Number + 1
                    : doraList[i].Number;
                var images = SetImageLayers(transforms[i], 2);
                images[0].sprite = Tower.SingleMentsuSpriteDict["BackgroundHai1"];
                images[1].sprite = Tower.SingleMentsuSpriteDict[type + number.ToString()];*/
            }
        }
        private Transform[] SetRelicsLayers(int n)
        {
            var transforms = new Transform[n];

            int childNum = imageParent.childCount;

            var relicsImage = imageParent.GetChild(0);

            for (int i = childNum; i < n; i++) Instantiate(relicsImage, imageParent);

            int newChildNum = imageParent.childCount;

            Transform tmp;
            for (int i = 0; i < n; i++)
            {
                tmp = imageParent.GetChild(i);
                tmp.gameObject.SetActive(true);
                transforms[i] = tmp;
            }

            for (int i = n; i < newChildNum; i++) imageParent.GetChild(i).gameObject.SetActive(false);

            return transforms;
        }

        private Image[] SetImageLayers(Transform t, int n)
        {
            var images = new Image[n];

            int childNum = t.childCount;

            var backGround = t.GetChild(0);

            for (int i = childNum; i < n; i++) Instantiate(backGround, t);

            int newChildNum = t.childCount;

            Transform tmp;
            for (int i = 0; i < n; i++)
            {
                tmp = t.GetChild(i);
                tmp.gameObject.SetActive(true);
                images[i] = tmp.GetComponent<Image>();
                images[i].rectTransform.anchoredPosition = Vector2.zero;
            }

            for (int i = n; i < newChildNum; i++) t.GetChild(i).gameObject.SetActive(false);

            return images;
        }

    }
}
