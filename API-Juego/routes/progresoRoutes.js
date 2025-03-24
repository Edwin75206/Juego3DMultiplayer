const express = require('express');
const router = express.Router();
const progresoController = require('../controllers/progresoController'); // Asegúrate de usar la ruta correcta hacia tu controlador

// Crear un progreso jugador
router.post('/progreso', progresoController.crearProgreso);

// Obtener todos los jugadores
router.get('/progresos', progresoController.obtenerProgresos);

// Obtener progreso de un jugador por ID
router.get('/progreso/:id', progresoController.obtenerProgresoPorId);

// Actualizar datos de un jugador
router.put('/progreso/:id', progresoController.actualizarProgreso);

// Eliminar un jugador
router.delete('/progreso/:id', progresoController.eliminarProgreso);

module.exports = router;
