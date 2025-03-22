const express = require('express');
const router = express.Router();
const jugadorController = require('../controllers/jugadorController'); // Asegúrate de usar la ruta correcta hacia tu controlador

// Crear un nuevo jugador
router.post('/jugador', jugadorController.crearJugador);

// Obtener todos los jugadores
router.get('/jugadores', jugadorController.obtenerJugadores);

// Obtener un jugador por ID
router.get('/jugador/:id', jugadorController.obtenerJugadorPorId);

// Actualizar datos de un jugador
router.put('/jugador/:id', jugadorController.actualizarJugador);

// Eliminar un jugador
router.delete('/jugador/:id', jugadorController.eliminarJugador);

// Login de un jugador
router.post('/login', jugadorController.loginJugador);

module.exports = router;
