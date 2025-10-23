using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace DefaultNamespace
{
    public class DisplayManager : MonoBehaviour
    {
        public static DisplayManager instance { get; private set; }

        [Header("Spawn Points")] public RectTransform[] spawnPoints;

        [Header("Prefabs par point (utilisé pour tests manuels)")]
        public GameObject[] imagePrefabsPoint1;

        public GameObject[] imagePrefabsPoint2;
        public GameObject[] imagePrefabsPoint3;

        [Header("Trigger & Movement")] public RectTransform triggerZone;
        public float fallSpeed = 2f;
        public int maxPerSpawnPoint = 3;

        private List<RectTransform>[] activeImages;
        private Dictionary<RectTransform, float> customSpeeds = new Dictionary<RectTransform, float>();

        private bool allCorrect = true;

        public bool AllCorect => allCorrect;

        private void Awake()
        {
            instance = this;

            activeImages = new List<RectTransform>[spawnPoints.Length];
            for (int i = 0; i < spawnPoints.Length; i++)
                activeImages[i] = new List<RectTransform>();
        }

        private void Update()
        {
            MoveImagesDown();
            CheckTriggers();
            IsCorrect();
        }

        #region === Affichage d’un pattern de Trap ===

        public void DisplayCustomPattern(TrapDisplayPattern pattern)
        {
            if (pattern == null || pattern.imagePrefabs == null || pattern.imagePrefabs.Length == 0)
            {
                Debug.LogWarning("Aucun pattern visuel défini pour ce trap.");
                return;
            }

            StartCoroutine(SpawnPatternWithDelay(pattern));
        }

        private IEnumerator SpawnPatternWithDelay(TrapDisplayPattern pattern)
        {
            int spawned = 0;
            int safety = 100;
            float interval = Mathf.Max(0f, pattern.spawnInterval);
            float customSpeed = pattern.customFallSpeed > 0 ? pattern.customFallSpeed : fallSpeed;

            while (spawned < pattern.numberToSpawn && safety-- > 0)
            {
                int spawnIndex = Random.Range(0, spawnPoints.Length);

                if (activeImages[spawnIndex].Count >= maxPerSpawnPoint)
                {
                    yield return null;
                    continue;
                }

                GameObject prefab = null;
                switch (spawnIndex)
                {
                    case 0:
                        prefab = imagePrefabsPoint1[Random.Range(0, imagePrefabsPoint1.Length)];
                        break;
                    case 1:
                        prefab = imagePrefabsPoint2[Random.Range(0, imagePrefabsPoint2.Length)];
                        break;
                    case 2:
                        prefab = imagePrefabsPoint3[Random.Range(0, imagePrefabsPoint3.Length)];
                        break;
                }

                GameObject newImg = Instantiate(prefab, spawnPoints[spawnIndex]);
                RectTransform rect = newImg.GetComponent<RectTransform>();
                rect.anchoredPosition = Vector2.zero;


                FallingImage fi = newImg.GetComponent<FallingImage>();
                fi.spawnIndex = spawnIndex;
                activeImages[spawnIndex].Add(rect);
                customSpeeds[rect] = customSpeed;

                spawned++;

                yield return new WaitForSeconds(interval);
            }

            if (safety <= 0)
                Debug.LogWarning("Boucle interrompue (trop d’objets actifs).");
        }

        #endregion


        #region === Checking Triggers ===

        private void MoveImagesDown()
        {
            for (int i = 0; i < activeImages.Length; i++)
            {
                for (int j = activeImages[i].Count - 1; j >= 0; j--)
                {
                    RectTransform img = activeImages[i][j];
                    if (img == null)
                    {
                        activeImages[i].RemoveAt(j);
                        continue;
                    }

                    float speed = customSpeeds.ContainsKey(img) ? customSpeeds[img] : fallSpeed;
                    Vector2 pos = img.anchoredPosition;
                    pos.y -= speed * Time.deltaTime;
                    img.anchoredPosition = pos;
                }
            }
        }

        private void CheckTriggers()
        {
            RectTransform canvasRect = triggerZone.GetComponentInParent<Canvas>().GetComponent<RectTransform>();

            for (int i = 0; i < activeImages.Length; i++)
            {
                for (int j = activeImages[i].Count - 1; j >= 0; j--)
                {
                    RectTransform img = activeImages[i][j];
                    if (img == null)
                    {
                        activeImages[i].RemoveAt(j);
                        continue;
                    }


                    Vector2 localPos;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect,
                        RectTransformUtility.WorldToScreenPoint(Camera.main, img.position),
                        Camera.main,
                        out localPos);

                    Vector2 triggerLocalPos;
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect,
                        RectTransformUtility.WorldToScreenPoint(Camera.main, triggerZone.position),
                        Camera.main,
                        out triggerLocalPos);

                    float margin = 0.09f;
                    Vector2 triggerSize = triggerZone.rect.size;
                    Rect triggerRect = new Rect(
                        triggerLocalPos.x - triggerSize.x / 2f,
                        triggerLocalPos.y - triggerSize.y / 2f + margin,
                        triggerSize.x,
                        triggerSize.y
                    );
                    FallingImage fi = img.GetComponent<FallingImage>();
                    bool isInTrigger = triggerRect.Contains(localPos);

                    if (isInTrigger)
                    {
                        if (!imagesInTrigger.Contains(fi))
                        {
                            OnObjectTriggered(img.gameObject);
                        }
                    }
                    else
                    {
                        if (imagesInTrigger.Contains(fi))
                        {
                            imagesInTrigger.Remove(fi);
                            Destroy(img.gameObject, 1f);
                            activeImages[i].RemoveAt(j);
                            customSpeeds.Remove(img);
                        }
                    }
                }
            }
        }

        private List<FallingImage> imagesInTrigger = new List<FallingImage>();

        private void OnObjectTriggered(GameObject obj)
        {
            FallingImage fi = obj.GetComponent<FallingImage>();
            if (fi != null && !imagesInTrigger.Contains(fi))
            {
                imagesInTrigger.Add(fi);
                for (int i = 0; i < imagesInTrigger.Count; i++)
                {
                    Debug.Log(imagesInTrigger[i]);
                }
            }
        }

        public void Button1Press()
        {
            CheckButtonPress(0);
        }

        public void Button2Press()
        {
            CheckButtonPress(1);
        }

        public void Button3Press()
        {
            CheckButtonPress(2);
        }

        private void CheckButtonPress(int buttonType)
        {
            for (int i = imagesInTrigger.Count - 1; i >= 0; i--)
            {
                FallingImage fi = imagesInTrigger[i];
                if (fi == null)
                {
                    imagesInTrigger.RemoveAt(i);
                    continue;
                }

                if (fi.spawnIndex != buttonType)
                    continue;

                if (fi.imageType == buttonType)
                {
                    SoundManager.PlaySound(SoundType.CORRECTRYTHM);
                    SoundManager.IncreasePitch();

                    Debug.Log($"Bon bouton pour {fi.name}");
                    RectTransform rect = fi.GetComponent<RectTransform>();

                    Destroy(fi.gameObject, 1f);
                    if (customSpeeds.ContainsKey(rect))
                        customSpeeds[rect] = 0f;
                    imagesInTrigger.RemoveAt(i);
                    
                    allCorrect = true;
                    return;
                }

                fi.SetColor(Color.darkOrange);
                SoundManager.ResetPitch();
                
                allCorrect = false;
                imagesInTrigger.RemoveAt(i);
                Debug.Log(imagesInTrigger[i]);
            }

            
        }
        
        public void IsCorrect()
        {
            if (allCorrect)
            {
                SoundManager.PlaySound(SoundType.ALLCORRECT);
            }
            else
            {
                //jouer sons false
            }
        }

        #endregion
    }
}