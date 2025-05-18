-- docker exec -it master-citus sh
-- psql -h localhost -p 5432 -U postgres -d postgres

CREATE DATABASE customers_orders_db;

\c customers_orders_db;

CREATE EXTENSION citus;

-- Criação de tabelas
CREATE TABLE customers (
    customer_id UUID PRIMARY KEY,
    name VARCHAR(100),
    email VARCHAR(150),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE orders (
    order_id UUID,
    customer_id UUID REFERENCES customers(customer_id),
    product_name VARCHAR(100),
    quantity INT,
    total_value DECIMAL(10,2),
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,

    PRIMARY KEY (order_id, customer_id)
);

-- Distribuir as tabelas (requer Citus ativo no cluster)
SELECT create_distributed_table('customers', 'customer_id');
SELECT create_distributed_table('orders', 'customer_id');

\dt;