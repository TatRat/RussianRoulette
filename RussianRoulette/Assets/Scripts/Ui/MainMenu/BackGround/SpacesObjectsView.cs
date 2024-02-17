using System;
using System.Collections.Generic;
using TatRat.API;
using TatRat.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ui.MainMenu.BackGround
{
    public class SpacesObjectsView : MonoBehaviour
    {
        [Header("Core")] 
        [SerializeField, CheckObject] private Transform _rootObject;
        [Space]
        [SerializeField, CheckObject] private List<GameObject> _objectPrefabs;
        [SerializeField] private int _countObjects;
        [SerializeField] private Vector3 _sizeSpace;
        [SerializeField] private float _deltaTeleportBorder;
        [Space]
        [Header("Movement")]
        [SerializeField] private float _minRandomMovementSpeed;
        [SerializeField] private float _maxRandomMovementSpeed;
        [SerializeField] private Vector3 _speedMovementDirection;
        [Space]
        [Header("Rotation")]
        [SerializeField] private bool _isEnableRotaionX;
        [SerializeField] private bool _isEnableRotaionY;
        [SerializeField] private bool _isEnableRotaionZ;
        [SerializeField] private float _minRandomRotationSpeed;
        [SerializeField] private float _maxRandomRotationSpeed;
        [Space]
        [Header("Instantiate")]
        [SerializeField] private bool _isRandomStartRotaionX;
        [SerializeField] private bool _isRandomStartRotaionY;
        [SerializeField] private bool _isRandomStartRotaionZ;
        [Space]
        [SerializeField] private float _deltaSpawningPositionAboutBorder;
        
        private List<SpaceObjectModel> _spawnedObjects = new List<SpaceObjectModel>();
        private void Start()
        {
            for (int i = 0; i < _countObjects; i++) 
                InstantiateObject();
        }
        
        private void Update()
        {
            foreach (var objectModel in _spawnedObjects)
            {
                MoveObject(objectModel);
                RotateObject(objectModel);
                CheckBordersObject(objectModel);
            }
        }
        
        private void InstantiateObject()
        {
            var prefab = _objectPrefabs.GetRandom();
            var go = Instantiate(prefab, _rootObject);
            
            var spawningZone = _sizeSpace - (Vector3.one * _deltaSpawningPositionAboutBorder);
            var spawningPos = transform.position.GetRandom(spawningZone);
            go.transform.position = spawningPos;
            
            go.transform.rotation = Quaternion.Euler(GetRandomStartRotation());
            
            var movementSpeed = Random.Range(_minRandomMovementSpeed, _maxRandomMovementSpeed);
            var rotationSpeed = GetRandomRotationSpeed();
            var model = new SpaceObjectModel
            {
                GameObject = go,
                MovementSpeed = movementSpeed,
                RotationSpeed = rotationSpeed
            };
            _spawnedObjects.Add(model);
        }
        
        private Vector3 GetRandomStartRotation()
        {
            var res = Vector3.zero;
            res.x = _isRandomStartRotaionX? GetRandomStartRotationAxis() : 0;
            res.y = _isRandomStartRotaionY? GetRandomStartRotationAxis() : 0;
            res.z = _isRandomStartRotaionZ? GetRandomStartRotationAxis() : 0;
            return res;
        }
        
        private float GetRandomStartRotationAxis() => 
            Random.Range(-180f, 180f);
        
        private Vector3 GetRandomRotationSpeed()
        {
            var res = Vector3.zero;
            res.x = _isEnableRotaionX? GetRandomRotationSpeedAxis() : 0;
            res.y = _isEnableRotaionY? GetRandomRotationSpeedAxis() : 0;
            res.z = _isEnableRotaionZ? GetRandomRotationSpeedAxis() : 0;
            return res;
        }
        
        private float GetRandomRotationSpeedAxis() => 
            Random.Range(_minRandomRotationSpeed, _maxRandomRotationSpeed);
        
        private void MoveObject(SpaceObjectModel objectModel) =>
            objectModel.GameObject.transform.position += 
                objectModel.MovementSpeed * Time.deltaTime * _speedMovementDirection;
        
        private void CheckBordersObject(SpaceObjectModel objectModel)
        {
            var curPos = objectModel.GameObject.transform.position;
            var centerPos = transform.position;
            var deltaPos = curPos - centerPos;
            var absDeltaPos = new Vector3(MathF.Abs(deltaPos.x), MathF.Abs(deltaPos.y), MathF.Abs(deltaPos.z));
            if (absDeltaPos.x > _sizeSpace.x)
                curPos.x = centerPos.x - (deltaPos.x - (_deltaTeleportBorder * deltaPos.x < 0 ? -1 : 1));
            if (absDeltaPos.y > _sizeSpace.y)
                curPos.y = centerPos.y - (deltaPos.y - (_deltaTeleportBorder * deltaPos.y < 0 ? -1 : 1));
            if (absDeltaPos.z > _sizeSpace.z)
                curPos.z = centerPos.z - (deltaPos.z - (_deltaTeleportBorder * deltaPos.z < 0 ? -1 : 1));
            
            objectModel.GameObject.transform.position = curPos;
        }
        
        private void RotateObject(SpaceObjectModel objectModel)
        {
            var curRot = objectModel.GameObject.transform.rotation.eulerAngles;
            curRot += objectModel.RotationSpeed * Time.deltaTime;
            objectModel.GameObject.transform.rotation = Quaternion.Euler(curRot);
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, _sizeSpace * 2);
        }
#endif
    }
}