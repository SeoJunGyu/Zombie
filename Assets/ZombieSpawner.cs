using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public UIManager uiManager;

    public Zombie prefab;

    public ZombieData[] zombieDatas; //����Ʈ�� �����ϴ�.
    public Transform[] spawnPoints;

    private List<Zombie> zombies = new List<Zombie>();
    private int wave;

    private void Update()
    {
        if(zombies.Count == 0)
        {
            SpawnWave();
        }
    }

    private void SpawnWave()
    {
        wave++;
        int count = Mathf.RoundToInt(wave * 1.5f);
        for (int i = 0; i < count; i++)
        {
            CreateZombie();
        }

        uiManager.SetWaveInfo(wave, zombies.Count);
    }

    public void CreateZombie()
    {
        var point = spawnPoints[Random.Range(0, spawnPoints.Length)];

        //���ӿ�����Ʈ�� �ƴ� Ŭ������ Instantiate �ϸ� �ش� Ŭ������ ������Ʈ�� ���� ���ӿ�����Ʈ�� ��°�� �����ȴ�. -> �̰� ��ȯ���� ���ش�.
        var zombie = Instantiate(prefab, point.position, point.rotation);
        zombie.Setup(zombieDatas[Random.Range(0, zombieDatas.Length)]);

        zombies.Add(zombie);

        //��������Ʈ ���� / zombie Ŭ������ �ִ� OnDeath Action ��������Ʈ�� �����ߴ�.
        zombie.OnDeath += () => zombies.Remove(zombie);
        zombie.OnDeath += () => uiManager.SetWaveInfo(wave, zombies.Count);
        zombie.OnDeath += () => Destroy(zombie.gameObject, 5f);
    }
}
