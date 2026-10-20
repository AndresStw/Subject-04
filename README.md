# 🧪 SUBJECT_04

![Unity](https://img.shields.io/badge/Unity-6.0-000000?style=flat&logo=unity&logoColor=white)
![C%23](https://img.shields.io/badge/C%23-11.0-239120?style=flat&logo=csharp&logoColor=white)
![Estado](https://img.shields.io/badge/Estado-EN_PAUSA-orange)
![Licencia](https://img.shields.io/badge/Licencia-MIT-blue)

### 🧠 Juego de Terror Psicológico Multijugador

> *"Un experimento psicológico donde una IA estudia cómo tomas decisiones cuando la información deja de ser confiable."*

---

## ⏸️ Estado del Proyecto: EN PAUSA

**Este proyecto está actualmente pausado.** El desarrollo se detuvo para priorizar otros proyectos y aprender nuevas herramientas. Este README se conserva como documentación del estado en el que quedó, para poder retomarlo sin perder contexto.

### 📌 Resumen de la pausa

| Aspecto | Detalle |
|---------|---------|
| **Estado** | Pausado (no abandonado) |
| **Motivo** | Reorientación de prioridades y aprendizaje de nuevas herramientas |
| **Último avance** | Sistemas base de movimiento, cámara, interacción e inventario funcionales |
| **Qué falta** | Sistemas de IA, multijugador y pulido visual |
| **Para retomar** | Ver sección "Cómo retomar este proyecto" al final |

> 💡 **Nota personal:** Este proyecto sigue siendo una idea que quiero terminar. La pausa es temporal y estratégica, no un descarte.

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

## 🎯 Características

### ✅ Sistemas Implementados (funcionales al momento de la pausa)

| Sistema | Descripción | Estado |
|---------|-------------|--------|
| **Movimiento en 3ra Persona** | Cámara orbital, agachado, giros en idle, velocidades diferenciadas. | ✅ Completo |
| **Cámara Estilo RE4** | Órbita libre, cambio de hombro (Q), colisiones con obstáculos. | ✅ Completo |
| **Interacción con Objetos** | Raycasting, resaltado visual, prompt en UI, puertas y objetos recogibles. | ✅ Completo |
| **Puertas Espaciales** | Animación deslizante/rotatoria, auto-cierre, múltiples zonas de interacción. | ✅ Completo |
| **Sistema de Inestabilidad** | Evalúa el estado psicológico de cada jugador. | ✅ Completo |
| **Head Look IK** | Mirada realista con límites de ángulo. | ✅ Completo |
| **Sistema de Agachado** | Toggle con Ctrl, transiciones suaves, altura de collider dinámica. | ✅ Completo |
| **Sistema de Inventario** | Almacenamiento de objetos, límite de slots. | ✅ Completo |

### 🚧 Pendiente para cuando se retome

- [ ] Sistema de Radio (comunicación manipulada por IA)
- [ ] Sistema de Protocolos (objetivos generados por combinación)
- [ ] Sistema de Perfiles Psicológicos (seguimiento de decisiones)
- [ ] Sistema de Presupuesto de IA (gasto de puntos en manipulaciones)
- [ ] Multijugador (Netcode for GameObjects)
- [ ] Pulido visual y dirección de arte
- [ ] Audio y atmósfera sonora

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

## 📸 Capturas del estado actual

> *(Capturas tomadas antes de la pausa)*

<img width="1310" height="706" alt="image" src="https://github.com/user-attachments/assets/3ecc9941-591e-4dba-ab00-2ce5f99163b0" />
<img width="1301" height="660" alt="image" src="https://github.com/user-attachments/assets/1c469868-9d6f-4ab9-a574-474668112b93" />

---

## 🗂️ Estructura del Proyecto
