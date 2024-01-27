using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TouchToStart.Utility;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace TouchToStart
{
    public static class CameraSplitPresets
    {
        public static Rect[] Default =
        {
            new Rect(0,0,0.5f,1),
            new Rect(0.5f, 0.5f, 0.5f, 0.5f),
            new Rect(0.5f, 0, 0.25f, 0.5f),
            new Rect()
        };
    }

    [Serializable]
    public class ReferenceRect
    {
        [SerializeField]
        private Rect _rect;

        public float x
        {
            get => _rect.x;
            set => _rect.x = value;
        }

        public float y
        {
            get => _rect.y;
            set => _rect.y = value;
        }

        public float xMin
        {
            get => _rect.xMin;
            set => _rect.xMin = value;
        }
        public float xMax
        {
            get => _rect.xMax;
            set => _rect.xMax = value;
        }
        public float yMin
        {
            get => _rect.yMin;
            set => _rect.yMin = value;
        }
        public float yMax
        {
            get => _rect.yMax;
            set => _rect.yMax = value;
        }
        public float width
        {
            get => _rect.width;
            set => _rect.width = value;
        }
        public float height
        {
            get => _rect.height;
            set => _rect.height = value;
        }

        public ReferenceRect(Rect rect)
        {
            _rect = rect;
        }

        public ReferenceRect(Vector2 position, Vector2 size)
        {
            _rect = new Rect(position, size);
        }

        public ReferenceRect(float x, float y, float width, float height)
        {
            _rect = new Rect(x, y, width, height);
        }

        public static implicit operator Rect(ReferenceRect rrect) => rrect._rect;
    }

    [Serializable]
    public class SplitedScreenRect
    {
        [NonSerialized]
        public Rect Rect;

        [NonSerialized]
        public SplitedScreenRect Divided1;
        [NonSerialized]
        public SplitedScreenRect Divided2;

        public bool IsHorizontal;
        [SerializeField]
        private float _division = 1;

        public SplitedScreenRect()
        {
            Rect = new Rect(0, 0, 1, 1);
        }

        public float GetDivision() => _division;

        public void SetDivision(float value)
        {
            _division = value;

            UpdateDivisionRect();
        }

        public void SetAxis(bool toHorizontal)
        {
            IsHorizontal = toHorizontal;
            
            UpdateDivisionRect();
        }

        public void UpdateDivisionRect()
        {
            var dividedRects = DivideRect(Rect, _division, IsHorizontal);
            
            if (Divided1 != null)
            {
                Divided1.Rect = dividedRects.Item1;
                Divided1.UpdateDivisionRect();
            }

            if (Divided2 != null)
            {
                Divided2.Rect = dividedRects.Item2;
                Divided2.UpdateDivisionRect();                
            }
        }
        
        private (Rect, Rect) DivideRect(Rect rect, float division, bool isHorizontal)
        {
            var result = (new Rect(rect), new Rect(rect));
            
            if (!isHorizontal)
            {
                result.Item1.x = rect.xMin;
                result.Item1.width = Mathf.Lerp(0, rect.width, division);
                result.Item2.x = result.Item1.x + result.Item1.width;
                result.Item2.width = Mathf.Clamp01(rect.xMax - result.Item2.x);
            }
            else
            {
                result.Item1.y = rect.yMin;
                result.Item1.height = Mathf.Lerp(0, rect.height, division);
                result.Item2.y = result.Item1.y + result.Item1.height;
                result.Item2.height = Mathf.Clamp01(rect.yMax - result.Item2.y);
            }

            return result;
        }

        public void CopyFrom(SplitedScreenRect other)
        {
            IsHorizontal = other.IsHorizontal;
            _division = other._division;
        }
    }

    [Serializable]
    public class SplitableCams
    {
        public Camera Cam;
        public SplitedScreenRect ScreenRect;
        public bool UseDivided2AsCamRect;

        public Rect CamRect => UseDivided2AsCamRect? ScreenRect.Divided2.Rect: ScreenRect.Divided1.Rect;
        public Rect RemainingRect => UseDivided2AsCamRect? ScreenRect.Divided1.Rect: ScreenRect.Divided2.Rect;

        public SplitableCams(Camera cam)
        {
            Cam = cam;
        }

        public void UpdateRect()
        {
            Cam.rect = CamRect;
            Cam.orthographicSize = CameraSplit.GetCameraOrthogonalSizeWithRatio(Cam.rect);
        }

        // public static implicit operator SplitableCams(Camera cam) => new SplitableCams(cam);
        // public static implicit operator Camera(SplitableCams splitableCams) => splitableCams.Cam;
    }
    
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class CameraSplit : Singleton<CameraSplit>
    {
        public SplitableCams[] Cams;
        public Camera BackgroundCam;

        public float OpenStageTweenDuration;
        public float ZoomTweenDuration;
        
        public Rect RenderRect;
        [SerializeField]
        private Rect _validRect;

        public int CurrentFirstCamIndex { get; private set; } = 0;
        public int MaxDepth => Cams.Length;

#if UNITY_EDITOR
        private bool isInitialized = false;
        [SerializeField] private bool UpdateInEditMode = false;
#endif
        private void Awake()
        {
            if (Screen.width > Screen.height * DEFAULT_SCREEN_RATIO)
            {
                DefaultScreenHeight = Screen.height;
                DefaultScreenWidth = (int)(DefaultScreenHeight * DEFAULT_SCREEN_RATIO);
            }
            else
            {
                DefaultScreenWidth = Screen.width;
                DefaultScreenHeight = (int)(DefaultScreenWidth / DEFAULT_SCREEN_RATIO);
            }

            float x = (Screen.width - DefaultScreenWidth) / 2f / Screen.width;
            float y = (Screen.height - DefaultScreenHeight) / 2f / Screen.height;

            _validRect = new Rect(x, y, 1 - 2 * x, 1 - 2 * y);
        }

        private void Start()
        {
            Initialize();
            RenderRect = new Rect(0.5f, 0.5f, 0, 0);
            DOTween.To(() => RenderRect, (rect) => RenderRect = rect, _validRect, OpenStageTweenDuration);
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (!UpdateInEditMode && !EditorApplication.isPlaying)
            {
                isInitialized = false;
                return;
            }
            
            if (!isInitialized)
            {
                print("Camera Split MonoBehaviour update in edit mode Initialize");
                Start();
                isInitialized = true;
            }
#endif
            Cams[0].ScreenRect.Rect = RenderRect;
            BackgroundCam.rect = RenderRect;
            
            foreach (var cam in Cams)
            {
                cam.UpdateRect();
            }
            
            Cams[0].ScreenRect.UpdateDivisionRect();
        }

        private void Initialize()
        {
            for (int i = 0; i < Cams.Length; i++)
            {
                if (i > 0)
                {
                    if (Cams[i-1].UseDivided2AsCamRect)
                    {
                        Cams[i-1].ScreenRect.Divided1.CopyFrom(Cams[i].ScreenRect);
                        Cams[i].ScreenRect = Cams[i-1].ScreenRect.Divided1;
                    }
                    else
                    {
                        Cams[i-1].ScreenRect.Divided2.CopyFrom(Cams[i].ScreenRect);
                        Cams[i].ScreenRect = Cams[i-1].ScreenRect.Divided2;
                    }
                }
                
                Cams[i].ScreenRect.Divided1 = new SplitedScreenRect();
                Cams[i].ScreenRect.Divided2 = new SplitedScreenRect();
            }
        }

        private void OnValidate()
        {
            for (int i = 1; i < Cams.Length; i++)
            {
                if (Cams[i-1].UseDivided2AsCamRect)
                {
                    Cams[i - 1].ScreenRect.Divided1 = Cams[i].ScreenRect;
                    Cams[i - 1].ScreenRect.Divided2 = new SplitedScreenRect();
                }
                else
                {
                    Cams[i - 1].ScreenRect.Divided2 = Cams[i].ScreenRect;
                    Cams[i - 1].ScreenRect.Divided1 = new SplitedScreenRect();
                }
            }
        }

        public void ZoomIn(int camDepth)
        {
            for (int i = 0; i < camDepth; i++)
            {
                if (Cams[i].UseDivided2AsCamRect)
                {
                    DOTween.To(Cams[i].ScreenRect.GetDivision, Cams[i].ScreenRect.SetDivision, 1, ZoomTweenDuration);
                }
                else
                {
                    DOTween.To(Cams[i].ScreenRect.GetDivision, Cams[i].ScreenRect.SetDivision, 0, ZoomTweenDuration);
                }
            }

            StartCoroutine(Wait(ZoomTweenDuration, () => RearrangeCamDepth(camDepth)));
        }

        IEnumerator Wait(float time, Action action)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }

        private void RearrangeCamDepth(int newFirstDepth)
        {
            SplitableCams[] tempArr = new SplitableCams[Cams.Length];
            Array.Copy(Cams, newFirstDepth, tempArr, 0, Cams.Length - newFirstDepth);
            Array.Copy(Cams, 0, tempArr, Cams.Length - newFirstDepth, newFirstDepth);
            Array.Copy(tempArr, Cams, Cams.Length);

            CurrentFirstCamIndex += newFirstDepth;
            CurrentFirstCamIndex = CurrentFirstCamIndex % MaxDepth;
            
            Initialize();
        }

        public void SetDivisions(float[] divisionsOfCam)
        {
            Assert.AreEqual(Cams.Length, divisionsOfCam.Length, "카메라 세팅을 위해 넘겨진 rect의 개수가 카메라의 개수와 다릅니다.");

            for (int i = 0; i < Cams.Length; i++)
            {
                Cams[i].ScreenRect.SetDivision(divisionsOfCam[i]);
            }
        }

        public static int DefaultScreenWidth;
        public static int DefaultScreenHeight;
        
        public const float DEFAULT_SCREEN_RATIO = 2;
        public const float DEFAULT_CAMERA_ORTHOGONAL_SIZE = 5;
        
        
        public static float GetCameraOrthogonalSizeWithRatio(Rect camRect)
        {
            float realWidthPixel = DefaultScreenWidth * camRect.width;
            float realHeightPixel = DefaultScreenHeight * camRect.height;

            float screenRatio = realWidthPixel / realHeightPixel;
            float ratioOfScreenRatio = screenRatio / DEFAULT_SCREEN_RATIO;
            float heightRatio = realHeightPixel / Screen.height;

            return DEFAULT_CAMERA_ORTHOGONAL_SIZE / (ratioOfScreenRatio * heightRatio);
        }
    }
}