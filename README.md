# RollABall

## Descripción

**RollABall** es un pequeño videojuego desarrollado en **Unity utilizando C#**, creado como parte de una experiencia de aprendizaje donde se le enseña a un niño llamado **Nicolás** los fundamentos del desarrollo de videojuegos.

El juego está inspirado en la clásica mecánica de *roll-a-ball*, donde el jugador controla una esfera que debe desplazarse por el escenario, superar obstáculos y recolectar llaves para poder avanzar al siguiente nivel.

Además de la jugabilidad, el proyecto busca introducir conceptos importantes de **programación y arquitectura de software** utilizados en el desarrollo profesional de videojuegos.

---

## Jugabilidad

En **RollABall**, el jugador controla una esfera que debe:

* Moverse a través de diferentes escenarios
* Superar obstáculos en el camino
* Recolectar llaves distribuidas por el nivel
* Desbloquear el acceso al siguiente nivel

El diseño del juego es sencillo e intuitivo, pensado como una introducción al desarrollo y la lógica de los videojuegos.

---

## Aspectos Técnicos Destacados

Aunque el juego es simple, el proyecto pone especial énfasis en el uso de **buenas prácticas de programación** y organización del código.

### Event Bus

El proyecto implementa un sistema de **Event Bus**, el cual permite que diferentes partes del juego se comuniquen mediante eventos en lugar de depender directamente unas de otras.

Esto ayuda a mejorar:

* La **desacoplación del código**
* La **modularidad**
* La **escalabilidad del proyecto**
* La **facilidad de mantenimiento**

Algunos ejemplos de eventos utilizados pueden incluir:

* Recolección de llaves
* Apertura de puertas
* Cambio de nivel
* Actualización de la interfaz de usuario

---

### Patrón MVC (Model–View–Controller)

El proyecto también utiliza una estructura basada en el patrón **MVC**, lo que permite organizar mejor la lógica del juego.

**Model**
Gestiona los datos y el estado del juego.

**View**
Representa los elementos visuales del juego como el jugador, las llaves y el entorno.

**Controller**
Se encarga de la lógica de interacción entre el jugador y el mundo del juego.

Este enfoque facilita la organización del proyecto y permite que el código sea más claro y fácil de ampliar.

---

## Tecnologías Utilizadas

* **Unity Engine**
* **C#**
* Arquitectura basada en **Event Bus**
* Patrón de diseño **MVC**

---

## Objetivos de Aprendizaje

Este proyecto fue desarrollado con los siguientes objetivos:

* Introducir a un principiante en el proceso de crear un videojuego
* Enseñar conceptos básicos de **programación de mecánicas de juego**
* Aplicar principios de **arquitectura limpia en Unity**
* Implementar comunicación entre sistemas mediante **Event Bus**
* Practicar la organización del código utilizando **MVC**

---

## Posibles Mejoras Futuras

Algunas mejoras que podrían añadirse al proyecto incluyen:

* Nuevos niveles
* Diferentes tipos de obstáculos
* Sistema de puntuación
* Efectos de sonido y música
* Mejora de la interfaz de usuario
* Sistema de guardado de progreso

---

## Autor

**Felipe Agudelo**
Ingeniero Informático

Apasionado por el desarrollo de videojuegos, la programación y la creación de experiencias interactivas educativas.

---

## Contexto Educativo

Este proyecto fue desarrollado como parte de una actividad educativa en la que **Nicolás** aprende de manera práctica cómo se construye un videojuego, fomentando la creatividad, el pensamiento lógico y el interés por la programación.
