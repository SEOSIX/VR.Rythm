using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

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

        [Header("DetectIfCorrect")] 
        [SerializeField] private Image detect;

        [Header("Trigger & Movement")] public RectTransform triggerZone;
        public float fallSpeed = 2f;
        public int maxPerSpawnPoint = 3;

        private List<RectTransform>[] activeImages;
        private Dictionary<RectTransform, float> customSpeeds = new Dictionary<RectTransform, float>();

        private bool allCorrect = true;
        private bool hasMissed = false;
        public bool AllCorect => allCorrect;
        
        public event Action OnAllPatternsCleared;

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

        // elle est un peu mystique cette fonction, ca pourrait etre cool de la splitter un peu pour
        // réduire la charge intellectuelle à la lecture
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
                // c'est quoi spawnindex 0 1 2 ? 
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

                // vraiment aucun moyen de stocker ce component pour éviter le getcomponent ici?
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
                // bon réflexe le -- avec le remove
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

        // pareil, hésite pas à splitter un peu, et éviter le getcomponent si possible, là c'est une belle bête
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

                    float margin = 0.01f;
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
                            hasMissed = false;
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

                            hasMissed = true;
                            IsCorrect();
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
            }
        }

        // C'est bien DRY, c'est cool
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
        
        // parcontre wallah zeubi la méthode elle fait le pain, le café et la toiture
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

                RectTransform rect = fi.GetComponent<RectTransform>();
                
                if (fi.imageType == buttonType)
                {
                    SoundManager.PlaySound(SoundType.CORRECTRYTHM);
                    SoundManager.IncreasePitch();
                    imagesInTrigger.RemoveAt(i);
                    RemoveRectFromActiveImages(rect);
                    if (customSpeeds.ContainsKey(rect))
                        customSpeeds.Remove(rect);
                    Destroy(fi.gameObject);

                    allCorrect = true;
                    if (AreAllPatternsCleared())
                        IsCorrect();

                    return;
                }
                SoundManager.ResetPitch();
                imagesInTrigger.RemoveAt(i);
                RemoveRectFromActiveImages(rect);
                if (customSpeeds.ContainsKey(rect))
                    customSpeeds.Remove(rect);
                Destroy(fi.gameObject, 0.3f);

                allCorrect = false;
                Debug.Log($"BIG ERROR sur {fi.name}, imagesInTrigger count {imagesInTrigger.Count}");
            }
        }

        private void RemoveRectFromActiveImages(RectTransform rect)
        {
            if (rect == null) return;

            for (int k = 0; k < activeImages.Length; k++)
            {
                if (activeImages[k].Remove(rect))
                    return;
            }
        }

        public bool AreAllPatternsCleared()
        {
            if (hasMissed)
            {
                return false;
            }
            for (int i = 0; i < activeImages.Length; i++)
            {
                if (activeImages[i].Count > 0)
                    return false;
            }

            return imagesInTrigger.Count == 0;
        }
        
        // de maniere générale, dans ta nomenclature, si tu mets Is{truc}, on s'attend à avoir une méthode qui
        // retourne un booléen, là elle fait des choses, ca devrait pas être le cas
        void IsCorrect()
        {
            if (AreAllPatternsCleared())
            {
                SoundManager.ResetPitch();
                OnAllPatternsCleared?.Invoke();
                StartCoroutine(FadeImageColor(detect, Color.black, Color.green, 1f));
                DisplayError.instance.DecreaseUsageRythms(DisplayError.instance.numberUsageRythmActivator);
                TrapManager.tryActivateTrap = false;

            }
            else
            {
                TrapManager.tryActivateTrap = false;
                SoundManager.ResetPitch();
                StartCoroutine(FadeImageColor(detect, Color.black, Color.red, 1f));
                DisplayError.instance.DecreaseUsageRythms(DisplayError.instance.numberUsageRythmActivator);
            }
        }
        
        IEnumerator FadeImageColor(Image img, Color fromColor, Color toColor, float time)
        {
            float elapsed = 0f;
            while (elapsed < time)
            {
                elapsed += Time.deltaTime;
                img.color = Color.Lerp(fromColor, toColor, elapsed / time);
                yield return null;
            }
            elapsed = 0f;
            while (elapsed < time)
            {
                elapsed += Time.deltaTime;
                img.color = Color.Lerp(toColor, fromColor, elapsed / time);
                yield return null;
            }

            img.color = fromColor;
        }

        #endregion
    }
}