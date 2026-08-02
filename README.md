# 🧪 SUBJECT_04

![Unity](https://img.shields.io/badge/Unity-6.0-000000?style=flat&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-11.0-239120?style=flat&logo=csharp&logoColor=white)
![Estado](https://img.shields.io/badge/Estado-En_Desarrollo-yellow)
![Licencia](https://img.shields.io/badge/Licencia-MIT-blue)

### 🧠 Juego de Terror Psicológico Multijugador

> *"Un experimento psicológico donde una IA estudia cómo tomas decisiones cuando la información deja de ser confiable."*

---

## 📖 Descripción

SUBJECT_04 es un juego de terror psicológico donde el enemigo no es una criatura, sino la pérdida de confianza en la propia percepción. Los jugadores son "sujetos de experimento" en una instalación controlada por una IA que manipula su percepción de la realidad, creando paranoia, desconfianza y momentos de tensión psicológica.

### Los Cinco Pilares
1. **La realidad puede mentir** — La IA manipula la percepción individual.
2. **Toda la información está incompleta** — Nadie tiene toda la verdad.
3. **Todos parecen culpables** — Incluso los inocentes generan sospechas.
4. **La IA aprende** — Se adapta a las decisiones de los jugadores.
5. **Mentir tiene consecuencias** — Pero decir la verdad también.

---

## 🎯 Características Principales

### ✅ Sistemas Implementados

| Sistema | Descripción |
|---------|-------------|
| **Movimiento en 3ra Persona** | Cámara orbital, agachado, giros en idle, velocidades diferenciadas. |
| **Cámara Estilo RE4** | Órbita libre, cambio de hombro (Q), colisiones con obstáculos. |
| **Interacción con Objetos** | Raycasting, resaltado visual, prompt en UI, puertas y objetos recogibles. |
| **Puertas Espaciales** | Animación deslizante/rotatoria, auto-cierre, múltiples zonas de interacción. |
| **Sistema de Inestabilidad** | Evalúa el estado psicológico de cada jugador. |
| **Head Look IK** | Mirada realista con límites de ángulo. |
| **Sistema de Agachado** | Toggle con Ctrl, transiciones suaves, altura de collider dinámica. |
| **Sistema de Inventario** | Almacenamiento de objetos, límite de slots. |

### 🚧 En Desarrollo

- Sistema de Radio (comunicación manipulada por IA)
- Sistema de Protocolos (objetivos generados por combinación)
- Sistema de Perfiles Psicológicos (seguimiento de decisiones)
- Sistema de Presupuesto de IA (gasto de puntos en manipulaciones)
- Multijugador (Netcode for GameObjects)

---

## 🛠️ Tecnologías Utilizadas

- **Motor**: Unity 6
- **Lenguaje**: C# 11
- **Animación**: Animator Controller, Blend Trees, Triggers
- **Física**: CharacterController, SphereCast, Raycast
- **IK**: OnAnimatorIK, Humanoid Avatar
- **UI**: TextMeshPro, Canvas, Event System
- **Control de Versiones**: Git, GitHub

---

## 🗂️ Estructura del Proyecto
 Assets/_Project/
├── Animations/ # Clips de animación y controladores
├── Art/ # Modelos, texturas y materiales
│ ├── Characters/ # Personajes (Player, Enemies)
│ ├── Environments/ # Escenarios y props
│ └── UI/ # Elementos de interfaz
├── Audio/ # Sonidos y música
├── Prefabs/ # Prefabs reutilizables
├── Scenes/ # Escenas del juego
├── Scripts/ # Todos los scripts
│ ├── Camera/ # Cámara y efectos visuales
│ ├── Player/ # Movimiento, interacción, inventario
│ ├── Enemies/ # Criatura y comportamiento
│ ├── Managers/ # GameManager, DirectorIA, InstabilityManager
│ └── Utilities/ # Scripts de ayuda y extensiones
├── Settings/ # Configuraciones de render pipeline
└── ThirdParty/ # Assets de terceros
