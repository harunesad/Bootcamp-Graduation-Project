using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropObject : MonoBehaviour
{
    public LayerMask Mask;
    public int FirstPosX, FirstPosZ, LastPosX, LastPosZ;
    public float LastPosY;

    private Vector3 _mousePosition;
    private bool _isDraging = false;
    private bool _isCellEmpty;
    private bool _isEnemyArea;

    void Start()
    {
        LastPosX = ((int)transform.position.x);
        LastPosZ = ((int)transform.position.z);
        FirstPosX = ((int)transform.position.x);
        FirstPosZ = ((int)transform.position.z);
        LastPosY = transform.position.y;
    }

    private void DragObject()
    {
        if (_isDraging)
        {
            _mousePosition = Input.mousePosition;
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

                            transform.position = new Vector3(PosX, LastPosY, PosZ);
                            GridManager.Instance._nodes[LastPosX, LastPosZ].IsPlaceable = false;
                            GridManager.Instance._nodes[FirstPosX, FirstPosZ].IsPlaceable = true;
                        }
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
    }

    private void OnMouseDrag()
    {
        DragObject();
    }

    private bool CheckNodeEmpty(int PosX, int PosZ)
    {
        if (GridManager.Instance._nodes[PosX, PosZ].IsPlaceable)
            _isCellEmpty = true;
        else
            _isCellEmpty = false;

        return _isCellEmpty;
    }

    private bool CheckEnemyGridArea(int PosZ)
    {
        if (PosZ < 3)
            _isEnemyArea = true;
        else
            _isEnemyArea = false;
        return _isEnemyArea;
    }
}
