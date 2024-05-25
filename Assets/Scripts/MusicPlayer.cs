using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private float saveInterval = 1.0f; // 60フレーム（1秒）ごとに保存
    private float timer = 0.0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // シーク位置を読み込んで再生を開始
        float savedTime = PlayerPrefs.GetFloat("MusicTime", 0.0f);
        audioSource.time = savedTime;
        audioSource.Play();
    }

    void FixedUpdate()
    {
        // タイマーを更新
        timer += Time.deltaTime;

        // 指定したインターバルごとにシーク位置を保存
        if (timer >= saveInterval)
        {
            SaveMusicTime();
            timer = 0.0f;
        }
    }

    void SaveMusicTime()
    {
        float currentTime = audioSource.time;
        PlayerPrefs.SetFloat("MusicTime", currentTime);
        PlayerPrefs.Save();
    }
}