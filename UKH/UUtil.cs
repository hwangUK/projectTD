using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization.Formatters.Binary;

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Linq;

namespace UKH
{
    public enum EInputAxis
    {
        Zero,
        LeftUp,
        Up,
        RightUp,
        Left,
        Right,
        LeftDown,
        Down,
        RightDown
    }

    public delegate void VoidEventCallback();

    [Serializable]
    public class SerializeDictionary<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver
    {
        [SerializeField]
        List<K> keys = new List<K>();

        [SerializeField]
        List<V> values = new List<V>();

        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();

            foreach (KeyValuePair<K, V> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();

            for (int i = 0, icount = keys.Count; i < icount; ++i)
            {
                this.Add(keys[i], values[i]);
            }
        }
    }

    public class UUtil
    {
        public static GameObject FindDeActiveObject(GameObject parent)
        {
            Transform[] pool = parent.transform.GetComponentsInChildren<Transform>(true);
            foreach(Transform target in pool)
            {
                if (target.gameObject.activeSelf == false)
                    return target.gameObject;
            }
            return null;
        }
        public static T FindUseableUnit<T>(List<List<T>> srcPool, EUnitPresetType uType)
        {
            for (int i = 0; i < srcPool[(int)uType].Count; i++)
            {
                if(srcPool[(int)uType][i] is UPool)
                {
                    UPool cash = srcPool[(int)uType][i] as UPool;
                    if (cash.IsAlive == false)
                        return srcPool[(int)uType][i];
                }
                else
                {
                    Debug.LogError("UPool 타입이 아니다");
                }
            }
            return default(T);
        }
        public static void ChangeMaterialSpriteRenderer(GameObject src, Material mat)
        {
            src.GetComponent<SpriteRenderer>().material = mat;
        }
        public static void ChangeGOBJRootSrcToDst(Transform src, Transform dst)
        {
            src.SetParent(dst);
        }
        public static T FindEnableCircleCollision2DComponent<T>(GameObject root)
        {
            Transform[] resultArray = root.GetComponentsInChildren<Transform>(true);
            for (int i = 1; i < resultArray.Length; i++)
            {
                if (resultArray[i].gameObject.GetComponent<CircleCollider2D>().isActiveAndEnabled == false)
                {
                    return resultArray[i].gameObject.GetComponent<T>();
                }
            }
            Debug.LogError("남은 비활성화 오브젝트가 없다.");
            return default(T);
        }

        public static T FindUseablePoolObject<T>(GameObject root)
        {
            Transform[] resultArray = root.GetComponentsInChildren<Transform>(true);
            for (int i = 1; i < resultArray.Length; i++)
            {
                if(resultArray[i].gameObject.activeSelf == false)
                {
                    return resultArray[i].gameObject.GetComponent<T>();
                }
            }
            Debug.LogError("남은 비활성화 오브젝트가 없다.");
            return default(T);
        }

        public static T FindUseableListenPoolObject<T>(GameObject ojs, int autoIncreasementType, bool force)
        {
            T[] resultArray = ojs.GetComponentsInChildren<T>(true);
            for (int i = 1; i < resultArray.Length; i++)
            {
                if(resultArray[i] is UListenPool)
                {
                    UListenPool target = resultArray[i] as UListenPool;

                    /*이거 분리나중에*/
                    if(force)
                    {
                        target.AwakePool(autoIncreasementType);
                        return resultArray[i];
                    }
                    else
                    {
                        if (target.IsPoolOn == false)
                        {
                            target.AwakePool(autoIncreasementType);
                            return resultArray[i];
                        }
                    }
                }
                else
                {
                    Debug.LogError("POOL 오브젝트가 아닌데?");
                    break;
                }
            }
            return default(T);
        }
      
        public static Sprite Get8AxisAnimSprite(List<Sprite> src, int axisPos, int framePos)
        {
            int idx = axisPos + (framePos * 8/*8방향*/);
            if (idx >= src.Count)
                return src[idx % src.Count];
            else
                return src[idx];
        }
        public static Sprite Get8AxisAnimSprite(Sprite[] src, int axisPos, int framePos)
        {
            int idx = axisPos + (framePos * 8/*8방향*/);
            if (idx >= src.Length)
                return src[idx % src.Length];
            else
                return src[idx];
        }
        public static Vector2 GetRectPosFromWorldPos(Canvas canvas, Vector3 worldPos)
        {
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(worldPos);
            Vector2 WorldObject_ScreenPosition = new Vector2(
            ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
            ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

            //now you can set the position of the ui element
            return WorldObject_ScreenPosition;
        }

        public static int GetDigitNumber(int src, int idx)
        {
            return int.Parse((src.ToString())[idx].ToString());
        }
        public static bool OccurByPercent(int percentage)
        {
            int value = UnityEngine.Random.Range(0, 100);
            if (value <= percentage)
                return true;
            else
                return false;
        }
        public static List<T> GetShuffleList<T>(List<T> source)
        {
            for (int i = 0; i < source.Count; i++)
            {
                int rnd = UnityEngine.Random.Range(0, 2);
                T cash = source[i];
                source[i] = source[rnd];
                source[rnd] = cash;
            }
            return source;
        }

        public static bool CheckInsideCircleBoundery(Vector3 src, Vector3 target, float rdus)
        {
            if (Math.Pow(rdus, 2) >= (Math.Pow(src.x - target.x, 2) + Math.Pow(src.y - target.y, 2))) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void SetVector3IntX(ref Vector3Int src, int x)
        {
            src = new Vector3Int(x, src.y, src.z);
        }

        public static void SetVector3IntY(ref Vector3Int src, int y)
        {
            src = new Vector3Int(src.x, y, src.z);
        }

        public static void SetVector3IntZ(ref Vector3Int src, int z)
        {
            src = new Vector3Int(src.x, src.y, z);
        }

        public static bool EqualCell(AStarNode p1, AStarNode p2)
        {
            if (p1 == null)
            {
                Debug.LogError("p1 is null");
            }
            if (p2 == null)
            {
                Debug.LogError("p1 is null");
            }
            return p1.X == p2.X && p1.Y == p2.Y;
        }

        public static float MagnitudeDistanceOnAxis(EInputAxis inputAxis, Vector3 target, Vector3 origin)
        {
            switch (inputAxis)
            {
                case EInputAxis.Zero:
                    return 0;
                case EInputAxis.LeftUp:
                    return (new Vector3(target.x, target.y, 0f) - new Vector3(origin.x, origin.y, 0f)).magnitude;
                case EInputAxis.Up:
                    return (new Vector3(0f, target.y, 0f) - new Vector3(0f, origin.y, 0f)).magnitude;
                case EInputAxis.RightUp:
                    return (new Vector3(target.x, target.y, 0f) - new Vector3(origin.x, origin.y, 0f)).magnitude;
                case EInputAxis.Left:
                    return (new Vector3(target.x, 0f, 0f) - new Vector3(origin.x, 0f, 0f)).magnitude;
                case EInputAxis.Right:
                    return (new Vector3(target.x, 0f, 0f) - new Vector3(origin.x, 0f, 0f)).magnitude;
                case EInputAxis.LeftDown:
                    return (new Vector3(target.x, target.y, 0f) - new Vector3(origin.x, origin.y, 0f)).magnitude;
                case EInputAxis.Down:
                    return (new Vector3(0f, target.y, 0f) - new Vector3(0f, origin.y, 0f)).magnitude;
                case EInputAxis.RightDown:
                    return (new Vector3(target.x, target.y, 0f) - new Vector3(origin.x, origin.y, 0f)).magnitude;
            }
            return 0;

        }
        public static Vector2Int DirAxisEnumToVectorInt(EInputAxis inputAxis)
        {
            switch (inputAxis)
            {
                case EInputAxis.Zero:
                    return new Vector2Int(0, 0);
                case EInputAxis.LeftUp:
                    return new Vector2Int(-1, 1);
                case EInputAxis.Up:
                    return new Vector2Int(0, 1);
                case EInputAxis.RightUp:
                    return new Vector2Int(1, 1);
                case EInputAxis.Left:
                    return new Vector2Int(-1, 0);
                case EInputAxis.Right:
                    return new Vector2Int(1, 0);
                case EInputAxis.LeftDown:
                    return new Vector2Int(-1, -1);
                case EInputAxis.Down:
                    return new Vector2Int(0, -1);
                case EInputAxis.RightDown:
                    return new Vector2Int(1, -1);
            }
            return Vector2Int.zero;
        }
        public static Vector2 DirAxisEnumToVector(EInputAxis inputAxis)
        {
            switch (inputAxis)
            {
                case EInputAxis.Zero:
                    return new Vector2(0f, 0f);
                case EInputAxis.LeftUp:
                    return new Vector2(-0.65f, 0.25f);
                case EInputAxis.Up:
                    return new Vector2(0f, 0.5f);
                case EInputAxis.RightUp:
                    return new Vector2(0.65f, 0.25f);
                case EInputAxis.Left:
                    return new Vector2(-1f, 0);
                case EInputAxis.Right:
                    return new Vector2(1f, 0);
                case EInputAxis.LeftDown:
                    return new Vector2(-0.65f, -0.25f);
                case EInputAxis.Down:
                    return new Vector2(0, -0.5f);
                case EInputAxis.RightDown:
                    return new Vector2(0.65f, -0.25f);
            }
            return Vector2Int.zero;
        }

        public static EInputAxis DirAxisVectorToEnum(Vector2 input)
        {
            EInputAxis result = EInputAxis.Zero;

            if (input.x > 0 && input.y == 0)                result = EInputAxis.Right;
            else if (input.x > 0 && input.y > 0)            result = EInputAxis.RightUp;
            else if (input.x > 0 && input.y < 0)            result = EInputAxis.RightDown;

            else if (input.x < 0 && input.y == 0)           result = EInputAxis.Left;
            else if (input.x < 0 && input.y > 0)            result = EInputAxis.LeftUp;
            else if (input.x < 0 && input.y < 0)            result = EInputAxis.LeftDown;

            else if (input.x == 0 && input.y == 0)          result = EInputAxis.Zero;
            else if (input.x == 0 && input.y > 0)           result = EInputAxis.Up;
            else if (input.x == 0 && input.y < 0)           result = EInputAxis.Down;
           
            return result;
        }
        public static EInputAxis DirAxisCorrectVectorToEnum(Vector2 input)
        {
            EInputAxis result = EInputAxis.Zero;

            if (input.x > 0 && Mathf.Abs(input.y) < 0.5f) result = EInputAxis.Right;
            else if (input.x < 0 && Mathf.Abs(input.y) < 0.5f) result = EInputAxis.Left;
            else if (Mathf.Abs(input.x) < 0.3f && input.y > 0) result = EInputAxis.Up;
            else if (Mathf.Abs(input.x) < 0.3f && input.y < 0) result = EInputAxis.Down;

            else if (input.x > 0 && input.y > 0.5f) result = EInputAxis.RightUp;
            else if (input.x > 0 && input.y < 0.5f) result = EInputAxis.RightDown;

            else if (input.x < 0 && input.y > 0.5f) result = EInputAxis.LeftUp;
            else if (input.x < 0 && input.y < 0.5f) result = EInputAxis.LeftDown;

            else if (input.x == 0 && input.y == 0) result = EInputAxis.Zero;

            return result;
        }
        public static EInputAxis GetLookAxis(Vector3 dirV, EInputAxis prev = EInputAxis.Down)
        {
            dirV = Vector3.Normalize(dirV);
            EInputAxis res = prev;

            int vertical    = dirV.y > 0.2f ? 1 : dirV.y < -0.2f ? -1 : 0;
            int horizontal  = dirV.x > 0.2f ? 1 : dirV.x < -0.2f ? -1 : 0;

            if (vertical == 1)
            {
                if (horizontal == 1)
                {
                    res = EInputAxis.RightUp;
                }
                else if (horizontal == -1)
                {
                    res = EInputAxis.LeftUp;
                }
                else
                {
                    res = EInputAxis.Up;
                }
            }
            else if (vertical == -1)
            {
                if (horizontal == 1)
                {
                    res = EInputAxis.RightDown;
                }
                else if (horizontal == -1)
                {
                    res = EInputAxis.LeftDown;
                }
                else
                {
                    res = EInputAxis.Down;
                }
            }
            else
            {
                if (horizontal == 1)
                {
                    res = EInputAxis.Right;
                }
                else if (horizontal == -1)
                {
                    res = EInputAxis.Left;
                }
            }

            return res;
        }

    }
    
    public class UUtilData
    {
        public static void JsonDataSaveToFile<T>(T data, string _fileName)
        {
            try
            {
                string json = JsonUtility.ToJson(data, true);

                if (json.Equals("{}"))
                {
                    Debug.Log("json null");
                    return;
                }
                Debug.LogWarning(json);

                string path = Application.persistentDataPath + "/" + _fileName;
                File.WriteAllText(path, json);

            }
            catch (FileNotFoundException e)
            {
                Debug.Log("The file was not found:" + e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Debug.Log("The directory was not found: " + e.Message);
            }
            catch (IOException e)
            {
                Debug.Log("The file could not be opened:" + e.Message);
            }
        }

        public static T JsonDataLoadFromFile<T>(string _fileName)
        {
            try
            {
                string path = Application.persistentDataPath + "/" + _fileName;
                if (File.Exists(path))
                {
                    string json = File.ReadAllText(path);
                    Debug.Log(json);
                    T t = JsonUtility.FromJson<T>(json);
                    return t;
                }
            }
            catch (FileNotFoundException e)
            {
                Debug.Log("The file was not found:" + e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Debug.Log("The directory was not found: " + e.Message);
            }
            catch (IOException e)
            {
                Debug.Log("The file could not be opened:" + e.Message);
            }
            return default;
        }

        public static T BinaryRead<T>(string path)
        {
            if (File.Exists(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(path, FileMode.Open);

                T returnval = (T)bf.Deserialize(file);
                file.Close();
                return returnval;
            }
            else
            {
                return default(T);
            }
        }

        public static void BinaryWrite<T>(T serializeData, string path, string filename)
        {           
            BinaryFormatter bf = new BinaryFormatter();
            if(Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }
            FileStream file = File.Create(System.IO.Path.Combine(path, filename));
            bf.Serialize(file, serializeData);
            file.Close();
        }
    }

    public class UUtilUI
    {
        public static void UISizeAnimation(GameObject src, float startSize, float endSize, float timeDuration)
        {
            src.SetActive(true);
            src.GetComponent<RectTransform>().localScale = new Vector3(startSize, startSize, startSize);
            src.GetComponent<RectTransform>().DOScale(endSize, timeDuration).SetEase(Ease.InOutQuad);
        }
      
        #region FadeZeroToOne
        public delegate void FadeEventCallback();
        static public void FadeZeroToOne(GameObject src, float show_t, float hide_t = -1f, FadeEventCallback callback = null, bool isOpacity = false)
        {
            TextMeshProUGUI[] tm = src.GetComponentsInChildren<TextMeshProUGUI>();
            Image[] im = src.GetComponentsInChildren<Image>();
            Text[] text = src.GetComponentsInChildren<Text>();

            for (int i = 0; i < tm.Length; i++)
                FadeZeroToOne(tm[i], show_t, hide_t, isOpacity);
            for (int j = 0; j < im.Length; j++)
                FadeZeroToOne(im[j], show_t, hide_t, isOpacity);
            for (int k = 0; k < text.Length; k++)
                FadeZeroToOne(text[k], show_t, hide_t, isOpacity);

            if (callback != null)
            {
                float ht = hide_t == -1f ? 0f : hide_t;
                src.transform.DOMoveX(src.transform.position.x, show_t + ht + /*여유값*/0.3f)
                    .OnComplete(() =>
                    {
                        callback();
                    }
                    );
            }
        }

        static public void FadeZeroToOne(Image src, float show_t, float hide_t, FadeEventCallback callback = null, bool isOpacity = false)
        {
            float one = isOpacity ? src.color.a : 1f;
            src.DOFade(0f, 0f).OnComplete(() =>
            {
                src.DOFade(one, show_t).OnComplete(() =>
                {
                    if (hide_t == -1)
                    {
                        callback();
                    }
                    else
                    {
                        src.DOFade(0f, hide_t).OnComplete(() =>
                        {
                            callback();
                        });
                    }
                });
            });
        }

        static public void FadeZeroToOne(Image src, float show_t, float hide_t = -1f, bool isOpacity = false)
        {
            float one = isOpacity ? src.color.a : 1f;
            src.DOFade(0f, 0f).OnComplete(() =>
            {
                src.DOFade(one, show_t).OnComplete(() =>
                {
                    if (hide_t != -1f)
                        src.DOFade(0f, hide_t);
                });
            });
        }
        static public void FadeZeroToOne(TextMeshProUGUI src, float show_t, float hide_t = -1f, bool isOpacity = false)
        {
            float one = isOpacity ? src.color.a : 1f;
            src.DOFade(0f, 0f).OnComplete(() =>
            {
                src.DOFade(one, show_t).OnComplete(() =>
                {
                    if (hide_t != -1f)
                        src.DOFade(0f, hide_t);
                });
            });
        }

        static public void FadeZeroToOne(Text src, float show_t, float hide_t = -1f, bool isOpacity = false)
        {
            float one = isOpacity ? src.color.a : 1f;
            src.DOFade(0f, 0f).OnComplete(() =>
            {
                src.DOFade(one, show_t).OnComplete(() =>
                {
                    if (hide_t != -1f)
                        src.DOFade(0f, hide_t);
                });
            });
        }

        #endregion

        #region FadeOneToZero
        static public void FadeOneToZero(Image src, float hide_t, bool isOpacity = false)
        {
            float one = isOpacity ? src.color.a : 1f;
            src.DOFade(one, 0f).OnComplete(() =>
            {
                src.DOFade(0f, hide_t);
            });
        }
        static public void FadeOneToZero(TextMeshProUGUI src, float hide_t, bool isOpacity = false)
        {
            float one = isOpacity ? src.color.a : 1f;
            src.DOFade(one, 0f).OnComplete(() =>
            {
                src.DOFade(0f, hide_t);
            });
        }
        static public void FadeOneToZero(Text src, float hide_t, bool isOpacity = false)
        {
            float one = isOpacity ? src.color.a : 1f;
            src.DOFade(one, 0f).OnComplete(() =>
            {
                src.DOFade(0f, hide_t);
            });
        }

        static public void FadeOneToZero(Image src, float hide_t, FadeEventCallback callback = null, bool isOpacity = false)
        {
            float one = isOpacity ? src.color.a : 1f;
            src.DOFade(one, 0f).OnComplete(() =>
            {
                src.DOFade(0f, hide_t).OnComplete(() =>
                {
                    callback();
                });
            });
        }
        static public void FadeOneToZero(GameObject src, float hide_t, FadeEventCallback callback = null, bool isOpacity = false)
        {
            TextMeshProUGUI[] tm = src.GetComponentsInChildren<TextMeshProUGUI>();
            Image[] im = src.GetComponentsInChildren<Image>();
            Text[] text = src.GetComponentsInChildren<Text>();

            for (int i = 0; i < tm.Length; i++)
                FadeOneToZero(tm[i], hide_t, isOpacity);
            for (int k = 0; k < text.Length; k++)
                FadeOneToZero(text[k], hide_t, isOpacity);
            for (int j = 0; j < im.Length; j++)
                FadeOneToZero(im[j], hide_t, callback, isOpacity);
        }
        #endregion


        public enum UIMoveDirectionArrow
        {
            BottomToMiddle,
            LeftToMiddle,
            RightToMiddle,
            TopToMiddle,

            BottomToDownSide,
            BottomToUpSide,

            TopToUpSide,
            TopToDownSide,

            LeftToLeftSide,
            LeftToRightSide,

            RightToLeftSide,
            RightToRightSide,
        }


        public static void UIMoveAnimation(GameObject gameObject, bool isOn, UIMoveDirectionArrow direction, VoidEventCallback func, float duration = 1f)
        {
            float xScreenSize = Screen.width;
            float yScreenSize = Screen.height;

            int bottomHidePosY = (int)-(yScreenSize + yScreenSize / 2);
            int topHidePosY = (int)(yScreenSize + yScreenSize / 2);
            int leftHidePosX = (int)-(xScreenSize + xScreenSize / 5);
            int rightHidePosX = (int)(xScreenSize);// + xScreenSize / 5);

            int middlePosX = 0;//(int)(xScreenSize / 2);
            int middlePosY = 0;//(int)(yScreenSize / 2);

            int quaterX = (int)(xScreenSize / 4);
            int quaterY = (int)(yScreenSize / 4);

            switch (direction)
            {
                case UIMoveDirectionArrow.BottomToMiddle:
                    {
                        if (isOn)
                        {
                            gameObject.SetActive(true);
                            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(middlePosX, bottomHidePosY);
                            gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, duration).SetEase(Ease.InOutQuad).OnComplete(
                                () => {
                                    if (func != null)
                                        func();
                                }
                                );
                        }
                        else
                        {
                            //gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2((int)xScreenSize / 2, (int)(yScreenSize / 2));
                            gameObject.GetComponent<RectTransform>().DOAnchorPosY(bottomHidePosY, duration).SetEase(Ease.InOutQuad).OnComplete(
                                () => {
                                    gameObject.SetActive(false);
                                    if (func != null)
                                        func();
                                }
                                );
                        }
                    }
                    break;
                case UIMoveDirectionArrow.LeftToMiddle:
                    {
                        if (isOn)
                        {
                            gameObject.SetActive(true);
                            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(leftHidePosX, middlePosY);
                            gameObject.GetComponent<RectTransform>().DOAnchorPosX(middlePosX, duration).SetEase(Ease.InOutQuad).OnComplete(
                                () => {
                                    if (func != null)
                                        func();
                                }
                                );
                        }
                        else
                        {
                            //gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2((int)xScreenSize / 2, (int)(yScreenSize / 2));
                            gameObject.GetComponent<RectTransform>().DOAnchorPosX(leftHidePosX, duration).SetEase(Ease.InOutQuad).OnComplete(
                                () => {
                                    gameObject.SetActive(false);
                                    if (func != null)
                                        func();
                                }
                                );
                        }
                    }
                    break;
                case UIMoveDirectionArrow.RightToMiddle:
                    {
                        if (isOn)
                        {
                            gameObject.SetActive(true);
                            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(rightHidePosX, middlePosY);
                            gameObject.GetComponent<RectTransform>().DOAnchorPosX(middlePosX, duration).SetEase(Ease.OutQuad).OnComplete(
                                () => {
                                    if (func != null)
                                        func();
                                }
                                );
                        }
                        else
                        {
                            //gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2((int)xScreenSize / 2, (int)(yScreenSize / 2));
                            gameObject.GetComponent<RectTransform>().DOAnchorPosX(rightHidePosX, duration).SetEase(Ease.InOutQuad).OnComplete(
                                () => {
                                    gameObject.SetActive(false);
                                    if (func != null)
                                        func();
                                }
                                );
                        }
                    }
                    break;
                case UIMoveDirectionArrow.TopToMiddle:
                    {
                        if (isOn)
                        {
                            gameObject.SetActive(true);
                            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(middlePosX, topHidePosY);
                            gameObject.GetComponent<RectTransform>().DOAnchorPosY(0, duration).SetEase(Ease.InOutQuad).OnComplete(
                                () => {
                                    if (func != null)
                                        func();
                                }
                                );
                        }
                        else
                        {
                            //gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2((int)xScreenSize / 2, (int)(yScreenSize / 2));
                            gameObject.GetComponent<RectTransform>().DOAnchorPosY(topHidePosY, duration).SetEase(Ease.InOutQuad).OnComplete(
                                () => {
                                    gameObject.SetActive(false);
                                    if (func != null)
                                        func();
                                }
                                );
                        }
                    }
                    break;
                case UIMoveDirectionArrow.BottomToDownSide:
                    {
                        if (isOn)
                        {

                        }
                        else
                        {

                        }
                    }
                    break;
                case UIMoveDirectionArrow.BottomToUpSide:
                    {
                        if (isOn)
                        {

                        }
                        else
                        {

                        }
                    }
                    break;
                case UIMoveDirectionArrow.TopToUpSide:
                    {
                        if (isOn)
                        {

                        }
                        else
                        {

                        }
                    }
                    break;
                case UIMoveDirectionArrow.TopToDownSide:
                    {
                        if (isOn)
                        {

                        }
                        else
                        {

                        }
                    }
                    break;
                case UIMoveDirectionArrow.LeftToLeftSide:
                    {
                        if (isOn)
                        {
                            gameObject.SetActive(true);
                            gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(leftHidePosX, middlePosY);
                            gameObject.GetComponent<RectTransform>().DOAnchorPosX(middlePosX - quaterX, duration).SetEase(Ease.InOutQuad).OnComplete(
                                () => {
                                    if (func != null)
                                        func();
                                }
                                );
                        }
                        else
                        {
                            //gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2((int)xScreenSize / 2, (int)(yScreenSize / 2));
                            gameObject.GetComponent<RectTransform>().DOAnchorPosX(leftHidePosX, duration).SetEase(Ease.InOutQuad).OnComplete(
                                () => {
                                    gameObject.SetActive(false);
                                    if (func != null)
                                        func();
                                }
                                );
                        }
                    }
                    break;
                case UIMoveDirectionArrow.LeftToRightSide:
                    {
                        if (isOn)
                        {

                        }
                        else
                        {

                        }
                    }
                    break;
                case UIMoveDirectionArrow.RightToLeftSide:
                    {
                        if (isOn)
                        {

                        }
                        else
                        {

                        }
                    }
                    break;
                case UIMoveDirectionArrow.RightToRightSide:
                    {
                        if (isOn)
                        {

                        }
                        else
                        {

                        }
                    }
                    break;
            }
        }


    }

    public static class UUtilString
    {
        public static string bold = "<b>{0}</b>";
        public static string italic = "<i>{0}</i>";
        public static string size = "<size={0}>{1}</size>";
        public static string color = "<color={0}>{1}</color>";

        public static string ToString(this string str, string msg)
        {
            return string.Format(str, msg);
        }

        public static string ToString(this string str, int size, string msg)
        {
            return string.Format(str, size, msg);
        }
    }

}
