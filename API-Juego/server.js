const express = require('express');
const bodyParser = require('body-parser');
const jugadorRoutes = require('./routes/jugadorRoutes');

const app = express();
const PORT = 3000;

app.use(bodyParser.json());

app.use('/api/jugadores', jugadorRoutes);

app.listen(PORT, () => {
    console.log(`Servidor corriendo en http://localhost:${PORT}`);
});
