using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private float saveInterval = 1.0f; // 60�t���[���i1�b�j���Ƃɕۑ�
    private float timer = 0.0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // �V�[�N�ʒu��ǂݍ���ōĐ����J�n
        float savedTime = PlayerPrefs.GetFloat("MusicTime", 0.0f);
        audioSource.time = savedTime;
        audioSource.Play();
    }

    void FixedUpdate()
    {
        // �^�C�}�[���X�V
        timer += Time.deltaTime;

        // �w�肵���C���^�[�o�����ƂɃV�[�N�ʒu��ۑ�
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