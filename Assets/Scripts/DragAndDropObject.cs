using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class DragAndDropObject : MonoBehaviour
{
    public LayerMask Mask;
    public int FirstPosX, FirstPosZ, LastPosX, LastPosZ;
    public float LastPosY;

    private GameObject _mergeableObject;
    private Vector3 _mousePosition;
    private bool _isDraging = false;
    private bool _isCellEmpty;
    private bool _isEnemyArea;
    private Animator _animator;
    private static readonly int IsMergeable = Animator.StringToHash("isMergeable");

    void Start()
    {
        _animator = GetComponent<Animator>();

        var position = transform.position;
        LastPosX = ((int)position.x);
        LastPosZ = ((int)position.z);
        FirstPosX = ((int)position.x);
        FirstPosZ = ((int)position.z);
        LastPosY = position.y;
    }

    private void Update()
    {
        if (GameManager.Instance.isStarted)
        {
            _animator.SetLayerWeight(2,0);
        }
    }

    private void DragObject()
    {
        if (!_isDraging || GameManager.Instance.isStarted) return;
        _mousePosition = Input.mousePosition;
        var worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(_mousePosition.x, _mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z));
        transform.position = new Vector3(worldPosition.x, 0.5f, worldPosition.z);
        Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, Mask))
        {
            int PosX = (int)Mathf.Round(hit.point.x);
            int PosZ = (int)Mathf.Round(hit.point.z);
            if (PosX != LastPosX || PosZ != LastPosZ)
            {
                if (!CheckEnemyGridArea(PosZ))
                    if (CheckNodeEmpty(PosX, PosZ))
                    {
                        FirstPosX = LastPosX;
                        FirstPosZ = LastPosZ;
                        LastPosX = PosX;
                        LastPosZ = PosZ;
                        
                        GridManager.Instance.Nodes[LastPosX, LastPosZ].IsPlaceable = false;
                        GridManager.Instance.Nodes[FirstPosX, FirstPosZ].IsPlaceable = true;
                        GridManager.Instance.Nodes[LastPosX, LastPosZ].Tag = gameObject.tag;
                        GridManager.Instance.Nodes[FirstPosX, FirstPosZ].Tag = "";
                    }
            }
        }
    }

    private void OnMouseDown()
    {
        _isDraging = true;
    }

    private void OnMouseUp()
    {
        _isDraging = false;
        if(_mergeableObject != null)
            MergeObject(_mergeableObject);
        if(!GameManager.Instance.isStarted)
            transform.position = new Vector3(LastPosX, LastPosY, LastPosZ);
    }

    private void OnMouseDrag()
    {
        DragObject();
    }

    private bool CheckNodeEmpty(int PosX, int PosZ)
    {
        _isCellEmpty = GridManager.Instance.Nodes[PosX, PosZ].IsPlaceable;
        return _isCellEmpty;
    }

    private bool CheckEnemyGridArea(int PosZ)
    {
        _isEnemyArea = PosZ < GridManager.Instance.Height / 2;
        return _isEnemyArea;
    }

    private void MergeObject(GameObject mergeableObject)
    {
        var mergeObjectTransform = mergeableObject.transform.position;
        if(GridManager.Instance.Nodes[LastPosX, LastPosZ].Tag == GridManager.Instance.Nodes[(int)mergeObjectTransform.x, (int)mergeObjectTransform.z].Tag)
        {
            var levelUpObjectPrefab = GetSoldierLevelWithTag();
            if(levelUpObjectPrefab == null)
                return;
            
            Destroy(gameObject);
            Destroy(mergeableObject);
           
            var levelUpObject = Instantiate(PrefabManager.Instance.GetPrefab(levelUpObjectPrefab),new Vector3(mergeObjectTransform.x, 0f, mergeObjectTransform.z), PrefabManager.Instance.GetPrefab(levelUpObjectPrefab).transform.rotation);
            var particle = Instantiate(PrefabManager.Instance.SpawnParticle, levelUpObject.transform.position,
                Quaternion.Euler(-90, 0, 0));
            particle.Play();
            Destroy(particle, 1f);
            
            GridManager.Instance.Nodes[LastPosX, LastPosZ].IsPlaceable = true;
            GridManager.Instance.Nodes[LastPosX, LastPosZ].Tag = "";
            
            GridManager.Instance.Nodes[(int)mergeObjectTransform.x, (int)mergeObjectTransform.z].Tag = levelUpObject.tag;
            GridManager.Instance.Nodes[(int)mergeObjectTransform.x, (int)mergeObjectTransform.z].IsPlaceable = false;
        }
    }

    private string GetSoldierLevelWithTag()
    {
        var splitTag = gameObject.tag.Split(" ");
        var soldierType = splitTag.GetValue(0);
        var soldierLevel = Int32.Parse((string)splitTag.GetValue(1));
        var sb = new StringBuilder();
        sb.Append(soldierType);
        sb.Append((soldierLevel+1).ToString());

        return soldierLevel + 1 == 4 ? null : sb.ToString(); // en son 3. seviyeye kadar mergelenebilir. 3 seviye iki obje mergelenemez.
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(gameObject.tag)) return;
        _mergeableObject = other.gameObject;
        if(GetSoldierLevelWithTag() != null)
            other.gameObject.GetComponent<Animator>().SetBool(IsMergeable, true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag(gameObject.tag)) return;
        if(GetSoldierLevelWithTag() != null)
            other.gameObject.GetComponent<Animator>().SetBool(IsMergeable, false);
        _mergeableObject = null;
    }
}
