const bcrypt = require('bcryptjs');
const jwt = require('jsonwebtoken');
const db = require('../config/db');

// Crear un nuevo jugador
exports.crearJugador = async (req, res) => {
    const {
        nombre_jugador,
        apellidos_jugador,
        email,
        phone,
        password,
        username
    } = req.body;

    try {
        // Encriptar la contraseña
        const hashedPassword = await bcrypt.hash(password, 10);

        // Insertar en la tabla 'jugador'
        const [result] = await db.execute(
            `INSERT INTO jugador (
                nombre_jugador,
                apellidos_jugador,
                email,
                phone,
                password,
                username
            ) VALUES (?, ?, ?, ?, ?, ?)`,
            [
                nombre_jugador,
                apellidos_jugador,
                email,
                phone,
                hashedPassword,
                username
            ]
        );

        res.status(201).json({
            id_jugador: result.insertId,
            message: 'Jugador creado con éxito'
        });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};

// Obtener todos los jugadores
exports.obtenerJugadores = async (req, res) => {
    try {
        const [rows] = await db.execute('SELECT * FROM jugador');
        res.status(200).json(rows);
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};

// Obtener un jugador por ID
exports.obtenerJugadorPorId = async (req, res) => {
    const { id } = req.params; // El parámetro de ruta se llama "id"

    try {
        // Usamos 'id_jugador' como clave primaria en la tabla
        const [rows] = await db.execute(
            'SELECT * FROM jugador WHERE idJugador = ?',
            [id]
        );

        if (rows.length === 0) {
            return res.status(404).json({ message: 'Jugador no encontrado' });
        }

        res.status(200).json(rows[0]);
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};

// Actualizar datos de un jugador
exports.actualizarJugador = async (req, res) => {
    const { id } = req.params; // El parámetro de ruta se llama "id"
    const {
        nombre_jugador,
        apellidos_jugador,
        email,
        phone,
        username,
        password
    } = req.body;

    try {
        // Si el usuario envía un nuevo password, lo encriptamos
        const hashedPassword = password
            ? await bcrypt.hash(password, 10)
            : undefined;

        // Actualizamos las columnas reales de tu tabla
        const [result] = await db.execute(
            `UPDATE jugador
             SET
                nombre_jugador = ?,
                apellidos_jugador = ?,
                email = ?,
                phone = ?,
                username = ?,
                password = ?
             WHERE idJugador = ?`,
            [
                nombre_jugador,
                apellidos_jugador,
                email,
                phone,
                username,
                hashedPassword || password, // Si no se cambió, se mantiene
                id
            ]
        );

        if (result.affectedRows === 0) {
            return res.status(404).json({ message: 'Jugador no encontrado' });
        }

        res.status(200).json({ message: 'Jugador actualizado con éxito' });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};

// Eliminar un jugador
exports.eliminarJugador = async (req, res) => {
    const { id } = req.params; // El parámetro de ruta se llama "id"

    try {
        const [result] = await db.execute(
            'DELETE FROM jugador WHERE idJugador = ?',
            [id]
        );

        if (result.affectedRows === 0) {
            return res.status(404).json({ message: 'Jugador no encontrado' });
        }

        res.status(200).json({ message: 'Jugador eliminado con éxito' });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};

// Login de un jugador
exports.loginJugador = async (req, res) => {
    const { username, password } = req.body;

    try {
        // Verificamos que el usuario exista
        const [rows] = await db.execute(
            'SELECT * FROM jugador WHERE username = ?',
            [username]
        );

        if (rows.length === 0) {
            return res.status(400).json({ message: 'Credenciales incorrectas' });
        }

        const jugador = rows[0];

        // Comparamos la contraseña ingresada con la almacenada
        const isMatch = await bcrypt.compare(password, jugador.password);

        if (!isMatch) {
            return res.status(400).json({ message: 'Credenciales incorrectas' });
        }

        // Generar token JWT
        const token = jwt.sign(
            {
                id_jugador: jugador.id_jugador,
                username: jugador.username
            },
            'tu_clave_secreta',
            { expiresIn: '1h' }
        );

        res.status(200).json({ token });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
};
