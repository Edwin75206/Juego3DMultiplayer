const db = require('../config/db');

// Crear progreso
exports.crearProgreso = async (req, res) => {
    const { player_id, score, lives, time, level } = req.body;
    try {
        const [result] = await db.execute(
            'INSERT INTO progreso_jugador (player_id, score, lives, time, level) VALUES (?, ?, ?, ?, ?)',
            [player_id, score, lives, time, level]
        );      
        res.status(201).json({ id: result.insertId, message: 'Progreso registrado' });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};

// Obtener progreso de los jugadores
exports.obtenerProgresos = async (req, res) => {
    try {
        const [rows] = await db.execute('SELECT * FROM progreso_jugador');
        res.status(200).json(rows);
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};

// Obtener progreso por jugador ID
exports.obtenerProgresoPorId = async (req, res) => {
    const { id } = req.params;

    try {
        const [rows] = await db.execute('SELECT * FROM progreso_jugador WHERE player_id = ?', [id]);
        
        if (rows.length === 0) {
            return res.status(404).json({ message: 'Progreso no encontrado' });
        }

        res.status(200).json(rows[0]);
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};

// Actualizar datos de un progreso
exports.actualizarProgreso = async (req, res) => {
    const { id } = req.params;
    const { player_id, score, lives, time, level } = req.body;

    try {
        const [result] = await db.execute(
            'UPDATE progreso_jugador SET player_id = ?, score = ?, lives = ?, time = ?, level = ? WHERE id = ?',
            [player_id, score, lives, time, level, id]
        );

        if (result.affectedRows === 0) {
            return res.status(404).json({ message: 'Progreso del Jugador no encontrado' });
        }

        res.status(200).json({ message: 'Progreso del Jugador actualizado con éxito' });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};

// Eliminar progreso
exports.eliminarProgreso = async (req, res) => {
    const { id } = req.params;

    try {
        const [result] = await db.execute('DELETE FROM progreso_jugador WHERE id = ?', [id]);

        if (result.affectedRows === 0) {
            return res.status(404).json({ message: 'Progreso del jugador no encontrado' });
        }

        res.status(200).json({ message: 'Progreso del jugador eliminado con éxito' });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};

