using BrickRage.Core;
using UnityEngine;

namespace BrickRage.Gameplay
{
    public class AimAndShootController : MonoBehaviour
    {
        [SerializeField] private GameConfig config;
        [SerializeField] private Transform firePoint;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private LineRenderer trajectoryRenderer;
        [SerializeField] private Camera gameplayCamera;
        [SerializeField] private float aimLineSegmentLength = 1.5f;

        private bool isAiming;
        private Vector2 aimDirection = Vector2.right;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TryBeginAim(Input.mousePosition);
            }

            if (isAiming && Input.GetMouseButton(0))
            {
                UpdateAim(Input.mousePosition);
                DrawTrajectory();
            }

            if (isAiming && Input.GetMouseButtonUp(0))
            {
                FireVolley();
                EndAim();
            }
        }

        private void TryBeginAim(Vector3 screenPoint)
        {
            if (screenPoint.x < Screen.width * config.battlefieldStartXNormalized)
            {
                return;
            }

            isAiming = true;
            trajectoryRenderer.enabled = true;
        }

        private void UpdateAim(Vector3 screenPoint)
        {
            var world = gameplayCamera.ScreenToWorldPoint(new Vector3(screenPoint.x, screenPoint.y, Mathf.Abs(gameplayCamera.transform.position.z)));
            var dir = (world - firePoint.position);
            aimDirection = dir.sqrMagnitude < 0.0001f ? Vector2.right : ((Vector2)dir).normalized;

            if (aimDirection.x < 0.1f)
            {
                aimDirection = new Vector2(0.1f, Mathf.Sign(aimDirection.y) * Mathf.Abs(aimDirection.y));
                aimDirection.Normalize();
            }
        }

        private void FireVolley()
        {
            int count = Random.Range(config.minVolleyCount, config.maxVolleyCount + 1);
            float spread = config.volleySpreadAngle;

            for (int i = 0; i < count; i++)
            {
                float t = count == 1 ? 0.5f : i / (float)(count - 1);
                float angle = Mathf.Lerp(-spread * 0.5f, spread * 0.5f, t);
                Vector2 dir = Quaternion.Euler(0, 0, angle) * aimDirection;

                Projectile proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
                float speed = Random.Range(config.minProjectileSpeed, config.maxProjectileSpeed);
                proj.Launch(dir, speed, config.bounceSpeedMultiplier, config.baseProjectileDamage);
            }
        }

        private void DrawTrajectory()
        {
            trajectoryRenderer.positionCount = config.trajectoryBouncesToPreview + 2;
            trajectoryRenderer.SetPosition(0, firePoint.position);

            Vector2 start = firePoint.position;
            Vector2 dir = aimDirection;

            for (int i = 0; i <= config.trajectoryBouncesToPreview; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(start, dir, 100f, LayerMask.GetMask("Walls"));

                if (!hit.collider)
                {
                    trajectoryRenderer.SetPosition(i + 1, start + dir * aimLineSegmentLength);
                    continue;
                }

                trajectoryRenderer.SetPosition(i + 1, hit.point);
                start = hit.point;
                dir = Vector2.Reflect(dir, hit.normal).normalized;
            }
        }

        private void EndAim()
        {
            isAiming = false;
            trajectoryRenderer.enabled = false;
            trajectoryRenderer.positionCount = 0;
        }
    }
}
