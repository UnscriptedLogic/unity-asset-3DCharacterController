using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquip : MonoBehaviour
{
    PlayerController controller;

    public GameObject bulletPrefab;
    public float shootInterval;
    public float bulletSpeed;
    public float bulletLifetime;
    public Transform bulletSpawnPoint;

    float _interval;

    private void Start()
    {
        controller = GetComponentInParent<ItemToPlayerBridge>().playerController;

        controller.playerInput.RegisterMouseBind(Fire, "Fire Pistol", 0, TriggerType.GetKey);
    }

    private void Update()
    {
        if (_interval >= 0f)
        {
            _interval -= Time.deltaTime;
        }
    }

    private void Fire()
    {
        if (_interval <= 0)
        {
            if (controller.playerInput.RaycastCamera(out RaycastHit hit))
            {
                bulletSpawnPoint.LookAt(hit.point);
            } else
            {
                bulletSpawnPoint.SetPositionAndRotation(bulletSpawnPoint.transform.position, transform.parent.rotation);
            }

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * (bulletSpeed * 100));

            Destroy(bullet, bulletLifetime);

            _interval = shootInterval;
        }
    }

    private void OnDestroy()
    {
        if (controller)
        {
            controller.playerInput.UnRegisterMousebind(Fire, "Fire Pistol");
        }
    }
}
