


// using UnityEngine;
// using System.Collections;
// using System.Collections.Generic;

// public class CarEngine : MonoBehaviour
// {
//     public Transform path;
//     public float maxSteerAngle = 45f;
//     public WheelCollider wheelFL;
//     public WheelCollider wheelFR;
//     public float maxMotorTorque = 80f;
//     public float currentSpeed;
//     public float maxSpeed = 100f;
//     private List<Transform> nodes;
//     private int currentNode = 0;
//     public Vector3 centerOfMass;


//     [Header("Sensors")]
//     public float sensorLength = 5f;
//     public float frontSensorPosition = 0.5f;

//     private void Start(){
//         GetComponent<Rigidbody>().centerOfMass = centerOfMass;

//         Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
//         nodes = new List<Transform>();

//         for (int i = 0; i < pathTransforms.Length; i++){
//             if (pathTransforms[i] != path.transform){
//                 nodes.Add(pathTransforms[i]);
//             }
//         }
//     }

//     private void FixedUpdate(){
//         if (nodes == null || nodes.Count == 0)
//             return; // Evita errores si no hay nodos.

//         Sensors();
//         ApplySteer();
//         Drive();
//         CheckWaypointDistance();
//     }

//     private void Sensors(){
//         RaycastHit hit;
//         Vector3 sensorStartPos = transform.position;
//         sensorStartPos.z += frontSensorPosition;

//         if(Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength)){

//         }
//         Debug.DrawLine(sensorStartPos, hit.point);

//     }



//     private void ApplySteer()
//     {
//         // Calcula el vector relativo al nodo actual
//         Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
//         float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;

//         // Ajusta el ángulo de dirección de las ruedas
//         wheelFL.steerAngle = newSteer;
//         wheelFR.steerAngle = newSteer;
//     }

//     private void Drive(){
//         currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFR.rpm * 60 / 1000;

//         if (currentSpeed < maxSpeed){
//             wheelFL.motorTorque = maxMotorTorque;
//             wheelFR.motorTorque = maxMotorTorque;
//         } else {
//             wheelFL.motorTorque = 0;
//             wheelFR.motorTorque = 0;
//         }


//     }

//     private void CheckWaypointDistance()
//     {
//         // Comprueba si el coche está cerca del nodo actual - 0.05f
//         if (Vector3.Distance(transform.position, nodes[currentNode].position) < 1.0f)
//         {
//             currentNode++;
//             if (currentNode >= nodes.Count)
//             {
//                 currentNode = 0; // Reinicia el recorrido al principio
//             }
//         }
//     }
// // }


























using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarEngine : MonoBehaviour
{
    public Transform path;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public float maxMotorTorque = 80f;
    public float currentSpeed;
    public float maxSpeed = 100f;
    private List<Transform> nodes;
    private int currentNode = 0;
    public Vector3 centerOfMass;

    [Header("Sensors")]
    public float sensorLength = 7f;
    public float frontSensorPosition = 0.5f;
    public float sideSensorPosition = 0.2f; // Distancia lateral desde el centro
    public float angleSensorLength = 7f; // Longitud de los sensores angulares
    public float angleSensorOffset = 30f; // Ángulo de los sensores angulados
    private float avoidMultiplier = 0; // Multiplicador para esquivar obstáculos

    private void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = centerOfMass;
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    private void FixedUpdate()
    {
        if (nodes == null || nodes.Count == 0)
            return;

        Sensors();
        ApplySteer();
        Drive();
        CheckWaypointDistance();
    }

    private void Sensors()
    {
        RaycastHit hit;
        Vector3 sensorStartPos = transform.TransformPoint(new Vector3(0, 0.5f, frontSensorPosition)); // Eleva los sensores 0.5 unidades en el eje Y
        avoidMultiplier = 0;

        // Sensor central
        if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
        {
            Debug.Log($"Central Sensor hit: {hit.collider.name}");
            avoidMultiplier -= 1; // Reduce hacia la izquierda
        }
        Debug.DrawLine(sensorStartPos, sensorStartPos + transform.forward * sensorLength, Color.red);

        // Sensor lateral derecho
        Vector3 rightSensorStartPos = transform.TransformPoint(new Vector3(sideSensorPosition, 0.5f, frontSensorPosition)); // Eleva también este sensor
        if (Physics.Raycast(rightSensorStartPos, transform.forward, out hit, sensorLength))
        {
            Debug.Log($"Right Sensor hit: {hit.collider.name}");
            avoidMultiplier -= 1.5f; // Mayor inclinación hacia la izquierda
        }
        Debug.DrawLine(rightSensorStartPos, rightSensorStartPos + transform.forward * sensorLength, Color.blue);

        // Sensor lateral izquierdo
        Vector3 leftSensorStartPos = transform.TransformPoint(new Vector3(-sideSensorPosition, 0.5f, frontSensorPosition)); // Eleva este sensor
        if (Physics.Raycast(leftSensorStartPos, transform.forward, out hit, sensorLength))
        {
            Debug.Log($"Left Sensor hit: {hit.collider.name}");
            avoidMultiplier += 1.5f; // Mayor inclinación hacia la derecha
        }
        Debug.DrawLine(leftSensorStartPos, leftSensorStartPos + transform.forward * sensorLength, Color.green);

        // Sensor angular derecho
        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(angleSensorOffset, transform.up) * transform.forward, out hit, angleSensorLength))
        {
            Debug.Log($"Right Angle Sensor hit: {hit.collider.name}");
            avoidMultiplier -= 0.75f; // Esquiva hacia la izquierda
        }
        Debug.DrawLine(sensorStartPos, sensorStartPos + Quaternion.AngleAxis(angleSensorOffset, transform.up) * transform.forward * angleSensorLength, Color.yellow);

        // Sensor angular izquierdo
        if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-angleSensorOffset, transform.up) * transform.forward, out hit, angleSensorLength))
        {
            Debug.Log($"Left Angle Sensor hit: {hit.collider.name}");
            avoidMultiplier += 0.75f; // Esquiva hacia la derecha
        }
        Debug.DrawLine(sensorStartPos, sensorStartPos + Quaternion.AngleAxis(-angleSensorOffset, transform.up) * transform.forward * angleSensorLength, Color.cyan);
    }

    // private void Sensors()
    // {
    //     RaycastHit hit;
    //     Vector3 sensorStartPos = transform.TransformPoint(new Vector3(0, 0, frontSensorPosition));
    //     avoidMultiplier = 0;

    //     // Sensor central
    //     if (Physics.Raycast(sensorStartPos, transform.forward, out hit, sensorLength))
    //     {
    //         Debug.Log($"Central Sensor hit: {hit.collider.name}");
    //         avoidMultiplier -= 1; // Reduce hacia la izquierda
    //     }
    //     Debug.DrawLine(sensorStartPos, sensorStartPos + transform.forward * sensorLength, Color.red);

    //     // Sensor lateral derecho
    //     Vector3 rightSensorStartPos = transform.TransformPoint(new Vector3(sideSensorPosition, 0, frontSensorPosition));
    //     if (Physics.Raycast(rightSensorStartPos, transform.forward, out hit, sensorLength))
    //     {
    //         Debug.Log($"Right Sensor hit: {hit.collider.name}");
    //         avoidMultiplier -= 1.5f; // Mayor inclinación hacia la izquierda
    //     }
    //     Debug.DrawLine(rightSensorStartPos, rightSensorStartPos + transform.forward * sensorLength, Color.blue);

    //     // Sensor lateral izquierdo
    //     Vector3 leftSensorStartPos = transform.TransformPoint(new Vector3(-sideSensorPosition, 0, frontSensorPosition));
    //     if (Physics.Raycast(leftSensorStartPos, transform.forward, out hit, sensorLength))
    //     {
    //         Debug.Log($"Left Sensor hit: {hit.collider.name}");
    //         avoidMultiplier += 1.5f; // Mayor inclinación hacia la derecha
    //     }
    //     Debug.DrawLine(leftSensorStartPos, leftSensorStartPos + transform.forward * sensorLength, Color.green);

    //     // Sensor angular derecho
    //     if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(angleSensorOffset, transform.up) * transform.forward, out hit, angleSensorLength))
    //     {
    //         Debug.Log($"Right Angle Sensor hit: {hit.collider.name}");
    //         avoidMultiplier -= 0.75f; // Esquiva hacia la izquierda
    //     }
    //     Debug.DrawLine(sensorStartPos, sensorStartPos + Quaternion.AngleAxis(angleSensorOffset, transform.up) * transform.forward * angleSensorLength, Color.yellow);

    //     // Sensor angular izquierdo
    //     if (Physics.Raycast(sensorStartPos, Quaternion.AngleAxis(-angleSensorOffset, transform.up) * transform.forward, out hit, angleSensorLength))
    //     {
    //         Debug.Log($"Left Angle Sensor hit: {hit.collider.name}");
    //         avoidMultiplier += 0.75f; // Esquiva hacia la derecha
    //     }
    //     Debug.DrawLine(sensorStartPos, sensorStartPos + Quaternion.AngleAxis(-angleSensorOffset, transform.up) * transform.forward * angleSensorLength, Color.cyan);
    // }

    private void ApplySteer()
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);

        // Ajuste de dirección considerando el esquive
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        newSteer += avoidMultiplier * maxSteerAngle; // Aplicar el multiplicador de esquiva

        wheelFL.steerAngle = newSteer;
        wheelFR.steerAngle = newSteer;
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFR.rpm * 60 / 1000;

        float adjustedTorque = maxMotorTorque;

        // Incrementar el torque si hay un esquive activo
        if (Mathf.Abs(avoidMultiplier) > 0)
        {
            adjustedTorque *= 1.5f; // Aumentar el torque en un 50% durante el esquive
        }

        if (currentSpeed < maxSpeed)
        {
            wheelFL.motorTorque = adjustedTorque;
            wheelFR.motorTorque = adjustedTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFR.motorTorque = 0;
        }
    }


    private void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 1.0f)
        {
            currentNode++;
            if (currentNode >= nodes.Count)
            {
                currentNode = 0;
            }
        }
    }
}

