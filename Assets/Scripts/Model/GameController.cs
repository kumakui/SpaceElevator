using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject windowPrefab;
    public Vector3 initialPos;
    public int windowInterval;
    public GameObject UpperLimitObject;
    public Material ground;
    public Material space;
    public AudioClip bossBGM;
    public BossController bossController;

    public UnityAction<Region> onRegionChangeAction;

    private MeshRenderer _elevatorWaterRenderer;
    private float _wallUpperLimitHeight;
    private AudioSource _bgmAudioSource;

    public enum Region
    {
        Earth,
        Space
    }

    // Start is called before the first frame update
    void Start()
    {
        _bgmAudioSource = Camera.main.GetComponent<AudioSource>();

        // Debug.unityLogger.logEnabled = false;//Debug.Log()を無効化
        var elevatorWater = GameObject.Find("ElevatorWater2");
        // _elevatorWaterRenderer = elevatorWater.GetComponent<MeshRenderer>();
        _wallUpperLimitHeight = UpperLimitObject.transform.position.y;

        //エレベーター背景壁を生成
        var wallWidth = wallPrefab.transform.localScale.x;
        var wallHeight = wallPrefab.transform.localScale.y;

        var rot = Quaternion.Euler(180f, 0f, 0f);

        var i = 0;
        while (true)
        {
            var position = initialPos + new Vector3(0, initialPos.y * i, 0);
            if (position.y > _wallUpperLimitHeight)
            {
                break;
            }

            if ((i + 1) % windowInterval == 0)
            {
                Instantiate(windowPrefab, position, rot);
            }
            else
            {
                Instantiate(wallPrefab, position, rot);
            }

            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void onRegionChange(Region region)
    {
        // onRegionChangeAction.Invoke(region);

        if (region == Region.Space)
        {
            RenderSettings.skybox = space;
        }

        if (region == Region.Earth)
        {
            RenderSettings.skybox = ground;
        }
    }

    public void FinishGame()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            Application.runInBackground = false;
            Application.Quit();
        }
    }

    public void StopBGM()
    {
        _bgmAudioSource.Stop();
    }

    public void OnBossAwake()
    {
        _bgmAudioSource.PlayOneShot(bossBGM);
    }
}