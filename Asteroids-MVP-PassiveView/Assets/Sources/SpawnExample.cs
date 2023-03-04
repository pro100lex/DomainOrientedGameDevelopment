using UnityEngine;
using Asteroids.Model;

public class SpawnExample : MonoBehaviour
{
    [SerializeField] private PresentersFactory _factory;
    [SerializeField] private Root _init;

    private int _index;
    private float _secondsPerIndex = 1f;
    
    private Guardian _guardianModel;
    private Nlo _nloModel;

    private void Update()
    {
        int newIndex = (int)(Time.time / _secondsPerIndex);

        if(newIndex > _index)
        {
            _index = newIndex;
            OnTick();
        }
    }

    private void OnTick()
    {
        float chance = Random.Range(0, 100);

        if (chance < 30)
        {
            _guardianModel = new Guardian(_nloModel, GetRandomPositionOutsideScreen(), Config.GuardianSpeed);
            _nloModel = new Nlo(_guardianModel, GetRandomPositionOutsideScreen(), Config.NloSpeed);
            _factory.CreateNlo(_nloModel);
            _factory.CreateGuardian(_guardianModel);
            
        }
        else
        {
            Vector2 position = GetRandomPositionOutsideScreen();
            Vector2 direction = GetDirectionThroughtScreen(position);

            _factory.CreateAsteroid(new Asteroid(position, direction, Config.AsteroidSpeed));
        }
    }

    private Vector2 GetRandomPositionOutsideScreen()
    {
        return Random.insideUnitCircle.normalized + new Vector2(0.5F, 0.5F);
    }

    private static Vector2 GetDirectionThroughtScreen(Vector2 postion)
    {
        return (new Vector2(Random.value, Random.value) - postion).normalized;
    }
    
    private static Vector2 GetRandomPositionInsideScreen(float x, float y)
    {
        return (new Vector2(x, y));
    }
}
