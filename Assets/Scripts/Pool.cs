using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class Pool
    {
        public List<T> Create<T>(GameObject prefab, int count)
            where T : MonoBehaviour
        {
            List<T> newPool = new List<T>();

            for (int i = 0; i < count; i++)
            {
                GameObject goProjectil = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
                T newProjectil = goProjectil.GetComponent<T>();

                newPool.Add(newProjectil);
            }

            return null;
        }
    }

    public class ProjectilePool : Pool
    {
        public List<Projectile> mProjectil = new List<Projectile>();

        public ProjectilePool(GameObject prefab, int count)
        {
            mProjectil = Create<Projectile>(prefab, count);
        }

        public void SetProjectiles()
        {
            foreach (Projectile projectile in mProjectil)
            {
                projectile.SetEnable();
            }
        }
    }
}
