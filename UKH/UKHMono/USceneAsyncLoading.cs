using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UKH
{
    public class USceneAsyncLoading : MonoBehaviour
    {
        //AsyncTestClass asyncTest;
        public static int nextScene;

        static string loadSceneName = "Loading";
        static LoadSceneMode loadSceneMode;

        // ===================================================
        // 로딩씬 호출
        public static void LoadSceneOnLoading(int _sceneIndex, LoadSceneMode _loadSceneMode)
        {
            nextScene       = _sceneIndex;
            loadSceneMode   = _loadSceneMode;
            UnityEngine.SceneManagement.SceneManager.LoadScene(loadSceneName);
        }

        public static void LoadSceneOnLoading(string _sceneName, LoadSceneMode _loadSceneMode)
        {
            nextScene = SceneIndexFromName(_sceneName);
            loadSceneMode = _loadSceneMode;
            UnityEngine.SceneManagement.SceneManager.LoadScene(loadSceneName);
        }
      
        // ===================================================
        // 로딩씬 로드후 비동기 로딩

        private void Start()
        {
            StartCoroutine(CoSceneLoad());
        }
    
        IEnumerator CoSceneLoad()
        {
            yield return null;
            AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(nextScene, loadSceneMode);
            operation.allowSceneActivation = false;
            while (operation.isDone == false)
            {
                yield return null;
                if (operation.progress < 0.9f)
                {
                    //TODO  : 프로그레스
                }
                else
                {
                    operation.allowSceneActivation = true;
                }
            }
        }


        private static int SceneIndexFromName(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                string testedScreen = NameFromIndex(i);
                //print("sceneIndexFromName: i: " + i + " sceneName = " + testedScreen);
                if (testedScreen == sceneName)
                    return i;
            }
            return -1;
        }
        private static string NameFromIndex(int BuildIndex)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
            int slash = path.LastIndexOf('/');
            string name = path.Substring(slash + 1);
            int dot = name.LastIndexOf('.');
            return name.Substring(0, dot);
        }
    }
}
