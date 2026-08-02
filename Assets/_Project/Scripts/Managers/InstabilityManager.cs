// using UnityEngine;
// using System.Collections.Generic;
// // sin usar 
// public class InstabilityManager : MonoBehaviour
// {
//     public static InstabilityManager Instance;

//     [Header("Configuración")]
//     public float updateInterval = 2f;
//     public float instabilityThreshold = 50f;

//     // Diccionario que guarda la inestabilidad de cada jugador
//     private Dictionary<Transform, float> playerInstability = new Dictionary<Transform, float>();
//     private Transform currentTarget;

//     void Awake()
//     {
//         if (Instance == null) Instance = this;
//         else Destroy(gameObject);
//     }

//     void Start()
//     {
//         InvokeRepeating(nameof(RecalculateInstability), 1f, updateInterval);
//     }

//     public void RegisterPlayer(Transform player)
//     {
//         if (!playerInstability.ContainsKey(player))
//         {
//             playerInstability[player] = 0f;
//             Debug.Log($"Jugador registrado: {player.name}");
//         }
//         else
//         {
//             Debug.LogWarning($"El jugador {player.name} ya está registrado.");
//         }
//     }

//     public void UnregisterPlayer(Transform player)
//     {
//         if (playerInstability.ContainsKey(player))
//         {
//             playerInstability.Remove(player);
//             Debug.Log($"Jugador desregistrado: {player.name}");
//         }
//     }

//     public void AddInstability(Transform player, float amount)
//     {
//         if (playerInstability.ContainsKey(player))
//         {
//             playerInstability[player] = Mathf.Clamp(playerInstability[player] + amount, 0f, 100f);
//             Debug.Log($"Inestabilidad de {player.name}: {playerInstability[player]}");
//         }
//         else
//         {
//             Debug.LogWarning($"El jugador {player.name} no está registrado. Registrándolo...");
//             RegisterPlayer(player);
//             playerInstability[player] = Mathf.Clamp(amount, 0f, 100f);
//         }
//     }

//     public float GetInstability(Transform player)
//     {
//         if (playerInstability.ContainsKey(player))
//             return playerInstability[player];
//         return 0f;
//     }

//     void RecalculateInstability()
//     {
//         if (playerInstability.Count == 0)
//         {
//             CreatureController.Instance?.SetTarget(null);
//             return;
//         }

//         Transform mostUnstable = null;
//         float highestValue = -1f;

//         foreach (var kvp in playerInstability)
//         {
//             if (kvp.Key == null)
//             {
//                 // Limpiar entradas nulas
//                 continue;
//             }

//             if (kvp.Value > highestValue)
//             {
//                 highestValue = kvp.Value;
//                 mostUnstable = kvp.Key;
//             }
//         }

//         ///Si el jugador más inestable supera el umbral, activar la criatura Aun no sirve cambiar metodo//
//         if (mostUnstable != null && highestValue >= instabilityThreshold)
//         {
//             // Solo actualizar si cambia el target
//             if (currentTarget != mostUnstable)
//             {
//                 currentTarget = mostUnstable;
//                 CreatureController.Instance?.SetTarget(currentTarget);
//                 Debug.Log($"Criatura activada para: {currentTarget.name} (Inestabilidad: {highestValue})");
//             }
//         }
//         else
//         {
//             // Si no hay target válido o la inestabilidad baja, desactivar
//             if (currentTarget != null)
//             {
//                 currentTarget = null;
//                 CreatureController.Instance?.SetTarget(null);
//                 Debug.Log("Criatura desactivada (inestabilidad baja o sin jugadores)");
//             }
//         }
//     }

//     // Método para forzar la activación desde el Inspector (para pruebas)
//     public void ForceActivation()
//     {
//         if (playerInstability.Count == 0) return;
//         // Tomar el primer jugador registrado
//         foreach (var kvp in playerInstability)
//         {
//             if (kvp.Key != null)
//             {
//                 AddInstability(kvp.Key, 80f);
//                 break;
//             }
//         }
//     }
// }