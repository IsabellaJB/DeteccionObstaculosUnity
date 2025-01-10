// using UnityEngine;
// using System.Collections;
// using System.Collectios.Generic;

// public class Path : MonoBehaviour
// {
//     public Color lineColor;

//     private List<Transform> nodes = new List<Transform>();

//     void OnDrawGizmos(){
//         Gizmos.color = lineColor;

//         Transform[] pathTransform = GetComponentsInChildren<Transform>();

//         nodes = new List<Transform>();

//         for(int i = 0; i < pathTransform.Length; i++){
//             if(pathTransform[i] != transform){
//                 nodes.Add(pathTransforms[i]);
//             }
//         }

//         for(int i = 0; i < nodes.Count; i++){
//             Vector3 currentNode = nodes[i].position;
//             Vector3 previousNode = Vector3.zero;
//             if(i>0){
//                 previousNode = nodes[i-1].position;
//             } else if (i == 0 && nodes.Count>1){
//                 previousNode = nodes[nodes.Count - 1].position;
//             }

//             Gizmos.DrawLine(previousNode, currentNode);
            
//         }

//     }
// }

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Path : MonoBehaviour
{
    public Color lineColor = Color.white; // Default color if not set in the inspector.

    private List<Transform> nodes = new List<Transform>();

    // Selected
    void OnDrawGizmosSelected()
    {
        Gizmos.color = lineColor;

        // Get all child transforms
        Transform[] pathTransform = GetComponentsInChildren<Transform>();

        nodes.Clear(); // Clear the list to avoid duplicates if this runs multiple times.

        // Populate the nodes list, skipping the parent (this object).
        for (int i = 0; i < pathTransform.Length; i++)
        {
            if (pathTransform[i] != transform)
            {
                nodes.Add(pathTransform[i]);
            }
        }

        // Draw lines between nodes
        for (int i = 0; i < nodes.Count; i++)
        {
            Vector3 currentNode = nodes[i].position;
            Vector3 nextNode = nodes[(i + 1) % nodes.Count].position; // Loop back to the first node.

            Gizmos.DrawLine(currentNode, nextNode);
            // Dentro del bucle for
            Gizmos.DrawWireSphere(currentNode, 0.3f);

        }
    }
}
