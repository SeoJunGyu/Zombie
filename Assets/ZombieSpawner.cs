using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public UIManager uiManager;

    public Zombie prefab;

    public ZombieData[] zombieDatas; //리스트도 가능하다.
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

        //게임오브젝트가 아닌 클래스를 Instantiate 하면 해당 클래스를 컴포넌트로 붙인 게임오브젝트가 통째로 생성된다. -> 이걸 반환까지 해준다.
        var zombie = Instantiate(prefab, point.position, point.rotation);
        zombie.Setup(zombieDatas[Random.Range(0, zombieDatas.Length)]);

        zombies.Add(zombie);

        //델리게이트 연결 / zombie 클래스에 있는 OnDeath Action 델리게이트에 연결했다.
        zombie.OnDeath += () => zombies.Remove(zombie);
        zombie.OnDeath += () => uiManager.SetWaveInfo(wave, zombies.Count);
        zombie.OnDeath += () => Destroy(zombie.gameObject, 5f);
    }
}
