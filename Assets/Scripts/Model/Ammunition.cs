﻿using UnityEngine;


namespace Test
{
    public abstract class Ammunition : IModel
    {
        #region PrivateData

        private readonly float _contactOffSet;
        protected PoolObjectAmmunition _poolObject;

        #endregion


        #region Fields

        public ITimeRemaining TimeRemaining;

        #endregion


        #region Property
        public float Force { get; private set; }
        public GameObject GameObject { get; }
        public Transform Transform { get; }
        public AmmunitionBehaviour AmmunitionProviders { get; }
        public float CurDamage { get; }
        public float TimeToDestruct { get; }
        public float TimeToFire { get; }
        public float MaxDistance { get; } = 5;
        public bool IsActive { get; private set; }
        //public Animator PlayerFireAnimation { get; private set; }
        #endregion


        #region Class LifeCycle

        protected Ammunition(GameObject bulletObject, PoolObjectAmmunition poolObject)
        {
            _poolObject = poolObject;
            GameObject = bulletObject;
            Transform = GameObject.transform;
            AmmunitionProviders = GameObject.GetComponent<AmmunitionBehaviour>();

            TimeToDestruct = AmmunitionProviders.TimeToDestruct;
            TimeRemaining = new TimeRemaining(DestroyAmmunition, TimeToDestruct);

            CurDamage = AmmunitionProviders.CurDamage;
            _contactOffSet = GameObject.GetComponent<Renderer>().bounds.extents.z;
            MaxDistance += _contactOffSet;            
        }

        #endregion


        #region Methods

        public virtual void SetActive(bool value)
        {
            IsActive = value;
            if (value)
            {
                Transform.SetParent(null);
                GameObject.SetActive(true);                
            }
            else
            {
                GameObject.SetActive(false);
                Transform.position = Vector3.zero;                
                Force = 0;
            }
        }

        public void DestroyAmmunition()
        {
            _poolObject.ReturnToPool(GetHashCode());
        }
                
        public void AddForce(float force)
        {
            Force = force;
        }

        #endregion
    }
}
