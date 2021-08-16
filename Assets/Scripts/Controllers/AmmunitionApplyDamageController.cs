﻿using UnityEngine;


namespace Test
{
    public sealed class AmmunitionApplyDamageController : BaseController, IExecute
    {
        
        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            foreach (var ammunitionObject in Services.Instance.LevelService.BulletAmmunitions)
            {
                if (!ammunitionObject.IsActive)
                {
                    continue;
                }
                if (Physics.Raycast(ammunitionObject.Transform.position, ammunitionObject.Transform.TransformDirection(Vector3.forward),
                    out var hit, ammunitionObject.MaxDistance))
                {
                    AmmunitionApplyDamage(ammunitionObject, hit.collider);
                }
            }
            foreach (var ammunitionObject in Services.Instance.LevelService.FractionBulletAmmunitions)
            {
                if (!ammunitionObject.IsActive)
                {
                    continue;
                }
                if (Physics.Raycast(ammunitionObject.Transform.position, ammunitionObject.Transform.TransformDirection(Vector3.forward),
                    out var hit, ammunitionObject.MaxDistance))
                {
                    AmmunitionApplyDamage(ammunitionObject, hit.collider);
                }
            }
        }
              

        private void AmmunitionApplyDamage(Ammunition ammunition, Collider collision)
        {            
            var tempObj = collision.gameObject.GetComponent<ISetDamage>();

            if (tempObj != null)
            {
                tempObj.SetDamage(new InfoCollision(ammunition.CurDamage));
            }

            ammunition.DestroyAmmunition();
        }
    }
}