using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


public class PlayerController : MonoBehaviour
{
    enum CursorType
    {
        None,
        Movement,
        Interact
    }

    [System.Serializable]
    struct CursorMapping
    {
        public CursorType type;
        public Texture2D texture;
        public Vector2 hotspot;
    }


    [SerializeField] CursorMapping[] cursorMappings = null;
    [SerializeField] float maxNavMeshProjectionDistance = 1f;
    [SerializeField] float raycastRadius = 0.1f;
    [SerializeField] DialogueManager dialogueManager;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InteractWithUI()) return;
        if (dialogueManager.IsInDialogue()) return;
        if (InteractWithComponent()) return;
        if (InteractWithMovement()) return;

        SetCursor(CursorType.None);
    }

    private bool InteractWithUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private bool InteractWithComponent()
    {
        
        RaycastHit[] hits = RaycastAllSorted();
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(CursorType.Interact);
                        return true;
                    }
                }
            }
        return false;
    }

    private bool InteractWithMovement()
        {
            
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);
            if (hasHit)
            {
                if (!GetComponent<Mover>().CanMoveTo(target)) return false;
                
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(target, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

    RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(), raycastRadius);
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

    private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();

            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHit) return false;

            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(
                hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);
            if (!hasCastToNavMesh) return false;

            target = navMeshHit.position;

            return true;
        }

    private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

    private void SetCursor(CursorType cursorType)
    {
        CursorMapping mapping = GetCursorMapping(cursorType);
        Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
    }

    private CursorMapping GetCursorMapping(CursorType type)
    {
        foreach(CursorMapping mapping in cursorMappings)
        {
            if (mapping.type == type)
            {
                return mapping;
            }
        }
        return cursorMappings[0];
    }
}
