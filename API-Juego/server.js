const express = require('express');
const bodyParser = require('body-parser');
const jugadorRoutes = require('./routes/jugadorRoutes');
const progresoRoutes = require('./routes/progresoRoutes');

const app = express();
const PORT = 3000;

app.use(bodyParser.json());

app.use('/api/jugadores', jugadorRoutes);
app.use('/api/jugadores', progresoRoutes);

app.listen(PORT, () => {
    console.log(`Servidor corriendo en http://localhost:${PORT}`);
});
