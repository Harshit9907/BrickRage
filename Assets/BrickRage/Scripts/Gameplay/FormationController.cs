using System.Collections.Generic;
using BrickRage.Core;
using UnityEngine;

namespace BrickRage.Gameplay
{
    public class FormationController : MonoBehaviour
    {
        [SerializeField] private GameConfig config;
        [SerializeField] private float advanceUnitsPerSecond = 0.4f;
        [SerializeField] private HeroHealth heroHealth;

        private readonly List<Brick> bricks = new();
        private float breachWorldX;

        private void Start()
        {
            breachWorldX = Camera.main.ViewportToWorldPoint(new Vector3(config.breachLineXNormalized, 0f, Mathf.Abs(Camera.main.transform.position.z))).x;
            RegisterChildren();
        }

        private void Update()
        {
            transform.position += Vector3.left * (advanceUnitsPerSecond * Time.deltaTime);

            if (transform.position.x <= breachWorldX)
            {
                heroHealth.TakeDamage(10f * Time.deltaTime);
            }
        }

        private void RegisterChildren()
        {
            bricks.Clear();
            foreach (Brick brick in GetComponentsInChildren<Brick>())
            {
                bricks.Add(brick);
            }
        }
    }
}
