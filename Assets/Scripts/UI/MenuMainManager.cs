using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameRPG
{
    public class MenuMainManager : MonoBehaviour
    {
        [Header("Main Menu Objects")]
        [SerializeField] private GameObject _loadingBarObject;
        [SerializeField] private Image _loadingBar;
        [SerializeField] private GameObject[] _objectToHide;


        private List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
        private void Awake()
        {
            _loadingBarObject.SetActive(false);
        }

        public void NewGame()
        {
            HideMenu();

            SaveGameManager.Instance.continueButtonActive = false;
            _loadingBarObject.SetActive(true);

            SaveGameManager.Instance.NewGame();

            StartCoroutine(ProgressLoadingBar());
        }

        public void ContinuesGame()
        {
            HideMenu();

            SaveGameManager.Instance.continueButtonActive = true;
            _loadingBarObject.SetActive(true);

            StartCoroutine(ProgressLoadingBar());
            SaveGameManager.Instance.ContinueGame();
        }

        private void HideMenu()
        {
            for (int i = 0; i < _objectToHide.Length; i++)
            {
                _objectToHide[i].SetActive(false);
            }
        }

        private IEnumerator ProgressLoadingBar()
        {
            float loadProgress = 0;

            for (int i = 0; i < scenesToLoad.Count; i++)
            {
                while (!scenesToLoad[i].isDone)
                {
                    loadProgress += scenesToLoad[i].progress;
                    _loadingBar.fillAmount = loadProgress / scenesToLoad.Count;
                    yield return null;
                }
            }
            yield return null;

        }


    }
}
