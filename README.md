# 🎮 Juego 3D Multiplayer

Este proyecto fue desarrollado por **Edwin Donovan** como un videojuego 3D multijugador utilizando Unity. Incluye una API creada en .NET para almacenar el progreso de los jugadores.

## 🧠 Descripción

Juego 3D multijugador con conexión a una API externa que permite registrar el avance del jugador. Los usuarios pueden interactuar en tiempo real dentro de un entorno 3D dinámico y competitivo.

## 🛠️ Tecnologías Utilizadas

* 🎮 Unity (motor de juego)
* 💬 C# (scripts y lógica del juego)
* 🌐 ASP.NET Core (para la API del progreso)
* 🗃️ JSON y HTTP (comunicación entre cliente y servidor)
* 🎨 ShaderLab (para efectos visuales)

## 📁 Estructura del Proyecto

```
Juego3DMultiplayer/
├── API-Juego/           --> Backend ASP.NET para guardar el progreso
├── Assets/              --> Recursos del juego (scripts, modelos, shaders)
├── Juego1/              --> Primer entorno/juego
├── JuegoPelota/         --> Segundo entorno con pelota y física
├── ProjectSettings/     --> Configuración de Unity
└── README.md
```

## 🧑‍💻 Cómo Instalar y Correr el Proyecto

### Requisitos

* Unity 2020.3 o superior
* .NET SDK 6.0 o superior
* Visual Studio (opcional, para editar la API)
* Git

### 1. Clonar el Repositorio

```bash
git clone https://github.com/Edwin75206/Juego3DMultiplayer.git
cd Juego3DMultiplayer
```

### 2. Abrir el Proyecto en Unity

1. Abre Unity Hub.
2. Haz clic en **Add** y selecciona la carpeta del proyecto `Juego3DMultiplayer`.
3. Espera a que cargue y abre la escena que deseas ejecutar (`Juego1` o `JuegoPelota`).

### 3. Ejecutar la API (Opcional pero Recomendado)

1. Abre la carpeta `API-Juego` en Visual Studio o VS Code.
2. Abre una terminal y ejecuta:

```bash
dotnet restore
dotnet run
```

3. La API se ejecutará en `https://localhost:5001` o `http://localhost:5000`.

### 4. Jugar

En Unity, presiona el botón **Play** para iniciar el juego. La API debe estar corriendo si quieres que el progreso se registre correctamente.

## 🧪 Pruebas y Validación

* Las pruebas se pueden hacer localmente conectando dos instancias del juego.
* Para verificar la conexión con la API, asegúrate que Unity tenga habilitado el uso de WebRequests.


## ✍️ Autor y Contacto

Desarrollado por **Edwin Donovan**

* 📧 Email: [edwin.donovan75206@gmail.com](mailto:edwin.donovan75206@gmail.com)
* 📱 WhatsApp: +52 554 801 1040
