const mysql = require('mysql2');

const pool = mysql.createPool({
    host: 'localhost',
    user: 'root',      // tu usuario de MySQL
    password: 'Pancho123',      // tu contraseña de MySQL
    database: 'juegopelota',
    port: '3308'
});

module.exports = pool.promise();
