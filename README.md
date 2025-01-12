# DeteccionObstaculosUnity
Proyecto desarrollado para la materia de herramientas de desarrollo para inteligencia artificial. Este proyecto fue realizado por la alumnas de séptimo semestre de la carrera de inteligencia artificial: Isabella Jiménez Bravo y Melissa A. York Sánchez.

https://media.tenor.com/SRyXgiZJAzsAAAAC/hello-kitty.gif


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
   ¡Bienvenido a **DeteccionObstaculosUnity**! Este es un proyecto desarrollado en Unity. Sigue estos pasos para clonar, configurar y ejecutar el proyecto.

## 🚀 Requisitos Previos

Antes de comenzar, asegúrate de tener instalados los siguientes programas:
- [Unity Hub](https://unity.com/download) y la versión de Unity especificada en este proyecto. (Consulta la sección **Versiones** más abajo).
- [Git](https://git-scm.com/) para clonar este repositorio.

## 📥 Clonar el Repositorio

1. Abre tu terminal (o Git Bash en Windows).
2. Navega a la carpeta donde deseas clonar el proyecto:
   ```bash
   cd /ruta/a/tu/carpeta
3. Clona este repositorio ejecutando:
   ```bash
   git clone https://github.com/IsabellaJB/DeteccionObstaculosUnity.git
4. Accede a la carpeta del proyecto:
   ```bash
   cd DeteccionObstaculosUnity
5. OPCIONAL: Descargar el proyecto como zip y descomprimir la carpeta en su computadora o clonar el github de manera sencilla con github Desktop

## 🛠 Abrir el Proyecto en Unity
1. Inicia Unity Hub.
2. Haz clic en el botón Add y selecciona la opción add from disk, y selecciona la carpeta del github clonado en tu computadora.
3. Unity cargará automáticamente el proyecto. Si es necesario, espera a que descargue los paquetes o reconstruya la biblioteca.

## 📦 Dependencias
Este proyecto utiliza algunos paquetes o herramientas específicas. Unity se encargará de instalar automáticamente las dependencias mencionadas en el archivo Packages/manifest.json.
Si el proyecto requiere herramientas externas (como Odin Inspector, DOTween, etc.), verifica su documentación para instalarlas correctamente.

## 🔧 Configuración Adicional
1. Abre la escena principal:
Navega a la carpeta Assets y abre Proyecto Final.unity.

## ▶️ Ejecutar el Proyecto
1. En Unity, haz clic en el botón Play para probar el proyecto.
2. Si encuentras errores, revisa la consola de Unity y corrige según sea necesario.

## 🛠 Versiones
Este proyecto fue desarrollado con:
1. Unity (6000.0.32f1 LTS)
