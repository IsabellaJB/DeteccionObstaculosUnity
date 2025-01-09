# DeteccionObstaculosUnity
Proyecto desarrollado para la materia de herramientas de desarrollo para inteligencia artificial. Este proyecto fue realizado por la alumnas de séptimo semestre de la carrera de inteligencia artificial: Isabella Jiménez Bravo y Melissa A. York Sánchez.


Este proyecto implementa un auto autónomo en Unity utilizando sensores y lógica manual para navegación y esquive de obstáculos. El auto sigue un camino predeterminado y detecta objetos en su trayectoria mediante rayos (raycasts), ajustando su dirección y velocidad en tiempo real.

## Características principales  
- **Navegación basada en nodos:** El auto se mueve a través de puntos de control definidos como nodos.  
- **Sensores frontales y angulares:** Detectan obstáculos directamente frente al vehículo y en ángulos laterales.  
- **Esquive dinámico:** Ajusta la dirección y la velocidad para evitar colisiones, incrementando el torque del motor en maniobras críticas.  
- **Sistema de estabilidad:** El centro de masa del vehículo se configura para mejorar su estabilidad.  

## Requisitos  
- Unity 2021.3 o superior.  
- Conocimientos básicos de C# y física en Unity.  

## Cómo usar  
1. **Descargar el proyecto**  
   Clona este repositorio en tu máquina local:  
   ```bash
   git clone https://github.com/tu-usuario/auto-autonomo-unity.git
