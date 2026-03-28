-- ============================================================
--  Mini Sistema de Gestión de Inventario - CCL
-- ============================================================

CREATE TABLE IF NOT EXISTS usuarios (
    id        SERIAL PRIMARY KEY,
    username  VARCHAR(100) NOT NULL UNIQUE,
    password  VARCHAR(255) NOT NULL,
    activo    BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS productos (
    id          SERIAL PRIMARY KEY,
    nombre      VARCHAR(200)   NOT NULL,
    precio      NUMERIC(10,2)  NOT NULL DEFAULT 0,
    cantidad    INTEGER        NOT NULL DEFAULT 0 CHECK (cantidad >= 0)
);

CREATE TABLE IF NOT EXISTS movimientos (
    id           SERIAL PRIMARY KEY,
    producto_id  INTEGER      NOT NULL REFERENCES productos(id),
    tipo         VARCHAR(10)  NOT NULL CHECK (tipo IN ('entrada', 'salida')),
    cantidad     INTEGER      NOT NULL CHECK (cantidad > 0),
    fecha        TIMESTAMP    NOT NULL DEFAULT NOW(),
    observacion  VARCHAR(500)
);

INSERT INTO usuarios (username, password, activo)
VALUES ('admin', 'Admin123!', TRUE)
ON CONFLICT (username) DO NOTHING;

INSERT INTO productos (nombre, precio, cantidad) VALUES
    ('PL Dama Nata',          45000, 25),
    ('PL Dama Canguro',       45000, 25),
    ('PL Dama Aleja',         45000, 25),
    ('PL Dama Paula',         45000, 25),
    ('PL Dama Mary',          45000, 25),
    ('Pesquero Dama Nata',    35000, 25),
    ('Pesquero Dama Canguro', 35000, 25),
    ('Pesquero Dama Aleja',   35000, 25),
    ('Pesquero Dama Paula',   35000, 25),
    ('Pesquero Dama Mary',    35000, 25),
    ('Bata Rosita',           28000, 25),
    ('Bata Señorial Única',   28000, 25),
    ('Bata Señorial XL',      30000, 25)
ON CONFLICT DO NOTHING;

CREATE INDEX IF NOT EXISTS idx_movimientos_producto ON movimientos(producto_id);
CREATE INDEX IF NOT EXISTS idx_movimientos_fecha    ON movimientos(fecha DESC);
